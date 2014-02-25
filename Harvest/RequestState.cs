using System;
using System.IO;
using System.Net;
using System.Text;

namespace Harvest
{
    public class RequestState
    {
        public const int BufferSize = 1024;

        public byte[] BufferRead;
        public StringBuilder RequestData;
        public Stream ResponseStream;

        //TODO: Create Decoder for appropriate enconding type.
        public Decoder StreamDecode = Encoding.UTF8.GetDecoder();

        public RequestState(WebRequest request)
        {
            BufferRead = new byte[BufferSize];
            RequestData = new StringBuilder(String.Empty);
            Request = request;
            ResponseStream = null;
        }

        public WebRequest Request { get; private set; }
    }
}