using System;
using System.IO;
using System.Net;
using System.Text;

namespace Harvest
{
    public class Downloader
    {
        public Uri Url { get; private set; }
        public bool Completed { get; private set; }
        public Page DownloadedPage { get; private set; }

        public Downloader(Uri url)
        {
            Url = url;
        }

        public void StartFetch()
        {
            var webRequest = WebRequest.Create(Url);
            webRequest.BeginGetResponse(EndGetResponse, new HttpRequestState(webRequest));
        }

        private void EndGetResponse(IAsyncResult ar)
        {
            var httpRequestState = ar.AsyncState as HttpRequestState;
            
            var webRequest = httpRequestState.WebRequest;
            try
            {
                var webResponse = webRequest.EndGetResponse(ar);
                var responseStream = webResponse.GetResponseStream();
                var outStream = new MemoryStream();
                var buffer = new byte[4096];

                responseStream.BeginRead(buffer, 0, 4096, EndRead, new HttpResponseState(webResponse, buffer, outStream));
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                if (response != null)
                {
                    DownloadedPage = new Page(Url, string.Empty)
                    {
                        StatusCode = response.StatusCode,
                        ContentType = response.ContentType
                    };
                }

                Completed = true;
            }
        }

        private void EndRead(IAsyncResult ar)
        {
            var httpResponseState = ar.AsyncState as HttpResponseState;

            var bytesRecieved = httpResponseState.WebResponse.GetResponseStream().EndRead(ar);
            var outStream = httpResponseState.OutStream;
            var responseStream = httpResponseState.WebResponse.GetResponseStream();

            if (bytesRecieved > 0)
            {
                outStream.Write(httpResponseState.Buffer, 0, bytesRecieved);
                responseStream.BeginRead(httpResponseState.Buffer, 0, 4096, EndRead, httpResponseState);
            }
            else
            {
                // No more data to read, completed.
                responseStream.Close();
                httpResponseState.WebResponse.Close();

                if(outStream.Length > 0) {
                    var buffer = outStream.ToArray();
                    var html = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                    var httpWebResponse = (HttpWebResponse) httpResponseState.WebResponse;

                    DownloadedPage = new Page(Url, html)
                    {
                        Headers = httpWebResponse.Headers,
                        StatusCode = httpWebResponse.StatusCode,
                        ContentType = httpWebResponse.ContentType
                    };
                }

                Completed = true;   
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Downloader)obj);
        }

        public override int GetHashCode()
        {
            return (Url != null ? Url.GetHashCode() : 0);
        }

        protected bool Equals(Downloader other)
        {
            return Equals(Url, other.Url);
        }

        private class HttpRequestState
        {
            public WebRequest WebRequest { get; private set; }

            public HttpRequestState(WebRequest webRequest)
            {
                WebRequest = webRequest;
            }
        }

        private class HttpResponseState
        {
            public WebResponse WebResponse { get; private set; }
            public byte[] Buffer { get; private set; }
            public MemoryStream OutStream { get; private set; }

            public HttpResponseState(WebResponse webResponse, byte[] buffer, MemoryStream outStream)
            {
                WebResponse = webResponse;
                Buffer = buffer;
                OutStream = outStream;
            }
        }
    }
}