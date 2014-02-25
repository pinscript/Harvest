using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Harvest.Extensions;
using HtmlAgilityPack;

namespace Harvest
{
    public class Page
    {
        private readonly Lazy<HtmlDocument> _htmlDocument;
        
        public Uri Url { get; private set; }
        public string Html { get; private set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public WebHeaderCollection Headers { get; set; }

        public Page(Uri url, string html)
        {
            Url = url;
            Html = html;

            _htmlDocument = new Lazy<HtmlDocument>(() =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            });
        }

        private IEnumerable<Link> _links;
        /// <summary>
        /// Returns a list of all links on this page.
        /// </summary>
        public IEnumerable<Link> Links
        {
            get
            {
                if (_links == null)
                {
                    var linkNodes = _htmlDocument.Value.DocumentNode.SelectNodes("//a");
                    if (linkNodes == null)
                        return Enumerable.Empty<Link>();

                    var links = new List<Link>();
                    foreach (var linkNode in linkNodes)
                    {
                        var href = linkNode.GetAttributeValue("href", "#");
                        if (!Uri.IsWellFormedUriString(href, UriKind.RelativeOrAbsolute))
                            continue;
                        
                        var url = href.ToAbsoluteUri(Url);
                        var follow = linkNode.GetAttributeValue("rel", "follow");

                        links.Add(new Link(Url, url, linkNode.InnerText, follow));
                    }

                    _links = links;
                }

                return _links;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Page) obj);
        }

        public override int GetHashCode()
        {
            return (Url != null ? Url.GetHashCode() : 0);
        }

        protected bool Equals(Page other)
        {
            return Equals(Url, other.Url);
        }
    }

    public class Link
    {
        public Uri SourceUrl { get; private set; }
        public Uri TargetUrl { get; private set; }
        public string Rel { get; private set; }
        public string AnchorText { get; private set; }

        public Link(Uri sourceUrl, Uri targetUrl, string anchorText, string rel)
        {
            SourceUrl = sourceUrl;
            TargetUrl = targetUrl;
            AnchorText = anchorText;
            Rel = rel;
        }
    }
}