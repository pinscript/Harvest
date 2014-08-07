namespace Harvest.Winforms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.btnCrawl = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lstCrawledPages = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Location = new System.Drawing.Point(5, 13);
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(462, 20);
            this.txtSiteUrl.TabIndex = 0;
            this.txtSiteUrl.Text = "http://nyqui.st";
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(473, 10);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(75, 23);
            this.btnCrawl.TabIndex = 1;
            this.btnCrawl.Text = "Crawl";
            this.btnCrawl.UseVisualStyleBackColor = true;
            this.btnCrawl.Click += new System.EventHandler(this.btnCrawl_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(554, 15);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(106, 13);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "0 pages downloaded";
            // 
            // lstCrawledPages
            // 
            this.lstCrawledPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCrawledPages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstCrawledPages.FullRowSelect = true;
            this.lstCrawledPages.Location = new System.Drawing.Point(5, 39);
            this.lstCrawledPages.Name = "lstCrawledPages";
            this.lstCrawledPages.Size = new System.Drawing.Size(1311, 723);
            this.lstCrawledPages.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstCrawledPages.TabIndex = 3;
            this.lstCrawledPages.UseCompatibleStateImageBehavior = false;
            this.lstCrawledPages.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Url";
            this.columnHeader1.Width = 207;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status Code";
            this.columnHeader2.Width = 182;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Content-Type";
            this.columnHeader3.Width = 187;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 764);
            this.Controls.Add(this.lstCrawledPages);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnCrawl);
            this.Controls.Add(this.txtSiteUrl);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSiteUrl;
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ListView lstCrawledPages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

