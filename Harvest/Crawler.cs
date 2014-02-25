using System;
using System.Collections.Generic;
using System.Linq;
using Harvest.ExcludeFilters;

namespace Harvest
{
    public class Crawler
    {
        /// <summary>
        /// Holds a list of all downloaded pages.
        /// </summary>
        private readonly Cache _cache;

        /// <summary>
        /// Holds a list of all queued pages to download.
        /// </summary>
        private readonly DownloaderQueue _queue;

        /// <summary>
        /// A list of regular expressions determining if we should exclude an url.
        /// </summary>
        public IEnumerable<IExcludeFilter> ExcludeFilters;

        /// <summary>
        /// Callback invoked when the crawling is completed.
        /// </summary>
        public event Action OnCompleted;

        /// <summary>
        /// Callback invoked when a page is downloaded.
        /// </summary>
        public event Action<Page> OnPageDownloaded;

        public Crawler(Cache cache)
        {
            _cache = cache;
            _queue = new DownloaderQueue(PageRecieved);

            ExcludeFilters = new List<IExcludeFilter>();
        }

        public Crawler() : this(new Cache())
        {
        }

        public void Enqueue(Uri url)
        {
            if(!_cache.Contains(url)) {
                _cache.Add(url);
            
                var fetcher = new Downloader(url);
                _queue.Enqueue(fetcher);
            }
        }

        /// <summary>
        /// Starts the crawlign process at the specified url.
        /// </summary>
        /// <param name="url">Crawler entry point.</param>
        public void Start(Uri url = null)
        {
            _queue.OnCrawlingCompleted += () =>
            {
                if (OnCompleted != null)
                {
                    OnCompleted();
                }
            };

            if(url != null) {
                Enqueue(url);
            }

            _queue.Process();
        }

        private void PageRecieved(Page page)
        {
            _cache.Add(page.Url);

            if (OnPageDownloaded != null)
            {
                OnPageDownloaded(page);
            }

            if (page.Html != string.Empty) {
                var links = page.Links;
                foreach (var link in links)
                {
                    if (_cache.Contains(link.TargetUrl))
                        continue;

                    if (ExcludeFilters.Any(x => x.IsMatch(link.TargetUrl)))
                        continue;

                    _queue.Enqueue(new Downloader(link.TargetUrl));
                }
            }
        }
    }
}