using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Harvest
{
    public class DownloaderQueue
    {
        /// <summary>
        /// A list of the currentl executing downloaders.
        /// </summary>
        private readonly IList<Downloader> _executing = new List<Downloader>();
        /// <summary>
        /// A queue of downloaders that are waiting for execution.
        /// </summary>
        private readonly IList<Downloader> _queue = new List<Downloader>();

        private readonly object _mutex = new object();
        private readonly Action<Page> _onPageDownloaded;

        /// <summary>
        /// Max number of threads to run concurrently.
        /// </summary>
        public const int MaxConcurrency = 5;

        /// <summary>
        /// Raised when crawler is completed.
        /// </summary>
        public event Action OnCrawlingCompleted;

        public DownloaderQueue(Action<Page> onPageDownloaded)
        {
            _onPageDownloaded = onPageDownloaded;
        }

        /// <summary>
        /// Enqueue a new downloader.
        /// </summary>
        /// <param name="downloader">Downloader to enqueue.</param>
        public void Enqueue(Downloader downloader)
        {
            lock (_mutex)
            {
                if (_queue.All(x => x.Url.AbsoluteUri != downloader.Url.AbsoluteUri))
                {
                    _queue.Add(downloader);
                }
            }
        }

        /// <summary>
        /// Process the downloader queue.
        /// </summary>
        public void Process()
        {
            ThreadStart enqueuerThreadMethod = () =>
            {
                var rand = new Random();
                while (true)
                {
                    lock (_mutex)
                    {
                        if (_queue.Any() && _executing.Count() < MaxConcurrency)
                        {
                            var fetcher = _queue[rand.Next(_queue.Count)];
                            if (_executing.Any(x => x.Url == fetcher.Url))
                            {
                                _queue.Remove(fetcher);
                            }
                            else
                            {
                                _queue.Remove(fetcher);
                                _executing.Add(fetcher);
                                fetcher.StartFetch();
                            }
                        }
                    }
                }
            };

            ThreadStart downloaderCompletionCheckMethod = () =>
            {
                while (true)
                {
                    lock (_mutex)
                    {
                        if (_executing.Any(x => x.Completed))
                        {
                            var downloader = _executing.First(x => x.Completed);
                            RemoveFetcher(downloader);

                            if (downloader.DownloadedPage != null)
                            {
                                // We did download a page
                                _onPageDownloaded(downloader.DownloadedPage);

                                if (CheckForCompleteness() && OnCrawlingCompleted != null)
                                {
                                    OnCrawlingCompleted();
                                    break;
                                }
                            }
                        }
                    }
                }
            };

            new Thread(enqueuerThreadMethod).Start();
            new Thread(downloaderCompletionCheckMethod).Start();
        }

        private bool CheckForCompleteness()
        {
            return !_queue.Any() && !_executing.Any();
        }

        /// <summary>
        ///     Remove a downloader from both the executing list and queue.
        /// </summary>
        /// <param name="downloader"></param>
        private void RemoveFetcher(Downloader downloader)
        {
            lock (_mutex)
            {
                _executing.Remove(downloader);
                _queue.Remove(downloader);
            }
        }
    }
}