using System;

namespace Harvest.ExcludeFilters
{
    public class ExcludeAnchors : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.AbsoluteUri.Contains("#");
        }
    }
}