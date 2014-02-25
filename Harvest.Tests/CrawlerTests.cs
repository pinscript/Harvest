using System;
using NUnit.Framework;

namespace Harvest.Tests
{
    public class CrawlerTests
    {
        [Test]
        public void Test()
        {
            var seed = new Uri("http://nyqui.st");

            var cache = new Cache(seed);
            var crawler = new Crawler(cache);
            bool finished = false;

            crawler.OnCompleted += () =>
            {
                Console.WriteLine("[Main] Crawl completed!");
                finished = true;
            };

            crawler.OnPageDownloaded += (page) => { Console.WriteLine("[Main] Got page {0}", page.Url); };

            crawler.Start(seed);
            Console.WriteLine("[Main] Crawler started.");

            while (true)
            {
                if (finished)
                {
                    Assert.True(true);
                    break;
                }
            }
        }
    }
}