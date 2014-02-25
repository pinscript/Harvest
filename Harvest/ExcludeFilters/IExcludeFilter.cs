using System;

namespace Harvest.ExcludeFilters
{
    public interface IExcludeFilter
    {
        bool IsMatch(Uri url);
    }
}