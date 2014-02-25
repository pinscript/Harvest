using System;

namespace Harvest.ExcludeFilters
{
    public class ExcludeJavaScript : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.Scheme.Equals("javascript");
        }
    }
}