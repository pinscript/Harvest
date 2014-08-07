using System;
using System.Diagnostics;
using System.Windows.Forms;
using Harvest.ExcludeFilters;
using Harvest.Winforms.Extensions;

namespace Harvest.Winforms
{
    public partial class Main : Form
    {
        private readonly Cache _cache;
        private readonly Crawler _crawler;
        private Stopwatch _watch;

        public Main()
        {
            InitializeComponent();
            lstCrawledPages.DoubleBuffering(true);

            _cache = new Cache();
            _crawler = new Crawler(_cache)
            {
                ExcludeFilters = new IExcludeFilter[]
                {
                    new ExcludeImagesFilter(),
                    new ExcludeTrackbacks(),
                    new ExcludeMailTo(),
                    new ExcludeHostsExcept(new Uri(txtSiteUrl.Text).Host)
                }
            };

            _crawler.OnPageDownloaded += page =>
            {
                lstCrawledPages.InvokeIfRequired(() =>
                {
                    lstCrawledPages.Items.Add(new ListViewItem(new[]
                        {
                            page.Url.AbsoluteUri,
                            (int)page.StatusCode + " " + page.StatusCode,
                            page.ContentType
                        }));

                    lblProgress.InvokeIfRequired(() =>
                    {
                        lblProgress.Text = lstCrawledPages.Items.Count + " pages downloaded";
                    });
                });
            };

            _crawler.OnCompleted += () =>
            {
                lblProgress.InvokeIfRequired(() => {
                        _watch.Stop();
                    lblProgress.Text += string.Format(" (Completed, took {0} ms ({1} seconds)", _watch.ElapsedMilliseconds, _watch.Elapsed.TotalSeconds);
                });
            };
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            btnCrawl.Enabled = false;
            
            _watch = new Stopwatch();
            _watch.Start();

            _crawler.Start(new Uri(txtSiteUrl.Text));
        }
    }
}
