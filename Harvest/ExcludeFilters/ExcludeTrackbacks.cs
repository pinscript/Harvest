using System;

namespace Harvest.ExcludeFilters
{
    public class ExcludeTrackbacks : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.AbsoluteUri.EndsWith("/trackback");
        }
    }
}