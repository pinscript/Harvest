using System;
using System.Collections;
using System.Collections.Generic;

namespace Harvest
{
    public class Cache : IEnumerable<Uri>
    {
        /// <summary>
        /// Holds a list of all visited pages.
        /// </summary>
        private readonly HashSet<Uri> _cache;

        private readonly object _mutex = new object();

        public Cache()
        {
            _cache = new HashSet<Uri>();
        }

        public Cache(Uri seedUrl) : this()
        {
            Add(seedUrl);
        }

        public IEnumerator<Uri> GetEnumerator()
        {
            return _cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a new url to the cache.
        /// </summary>
        /// <param name="url">The url.</param>
        public void Add(Uri url)
        {
            lock (_mutex)
            {
                _cache.Add(url);
            }
        }

        /// <summary>
        /// Check if a given url exists in cache.
        /// </summary>
        /// <param name="url">The url to check.</param>
        public bool Exists(Uri url)
        {
            var exists = _cache.Contains(url);
            return exists;
        }
    }
}