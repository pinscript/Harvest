using System;
using System.Collections.Generic;
using System.Linq;

namespace Harvest.ExcludeFilters
{
    public class ExcludeHostsExcept : IExcludeFilter
    {
        private readonly IEnumerable<string> _hosts;

        public ExcludeHostsExcept(params string[] hosts)
        {
            _hosts = hosts;
        }

        public bool IsMatch(Uri url)
        {
            // Do not exclude hosts in _host
            if (_hosts.Contains(url.Host))
                return false;

            // Exclude
            return true;
        }
    }
}