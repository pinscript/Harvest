using System;
using System.Text.RegularExpressions;

namespace Harvest.ExcludeFilters
{
    public class ExcludeImagesFilter : IExcludeFilter
    {
        private readonly Regex _regex = new Regex(@"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico)",
                      RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public bool IsMatch(Uri url)
        {
            return _regex.IsMatch(url.AbsoluteUri);
        }
    }
}