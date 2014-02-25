using System;

namespace Harvest.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Resolve an absolute or relative href to a site-specific uri.
        /// </summary>
        /// <param name="href">The link.</param>
        /// <param name="siteUrl">The sites Uri.</param>
        /// <returns>An absolute uri where host is the same as the sites.</returns>
        public static Uri ToAbsoluteUri(this string href, Uri siteUrl)
        {
            var uri = new Uri(href, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
                return uri;

            return new Uri(siteUrl, href.Trim('/'));
            // TODO: return new Uri(string.Format("{0}://{1}/{2}", siteUrl.Scheme, siteUrl.Authority.TrimEnd('/'), href.Trim('/')));
        }
    }
}