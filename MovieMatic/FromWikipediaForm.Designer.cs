namespace Toenda.MovieMatic {
	partial class FromWikipediaForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && ( components != null )) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FromWikipediaForm));
			this.txtArticle = new System.Windows.Forms.TextBox();
			this.txtResponse = new System.Windows.Forms.TextBox();
			this.btnArticle = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnOpenEditor = new System.Windows.Forms.Button();
			this.lblRequestTime = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pBrowser = new System.Windows.Forms.Panel();
			this.wbResponse = new System.Windows.Forms.WebBrowser();
			this.lbResult = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.pBrowser.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtArticle
			// 
			this.txtArticle.Location = new System.Drawing.Point(6, 19);
			this.txtArticle.Name = "txtArticle";
			this.txtArticle.Size = new System.Drawing.Size(596, 20);
			this.txtArticle.TabIndex = 1;
			this.txtArticle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtArticle_KeyUp);
			// 
			// txtResponse
			// 
			this.txtResponse.Location = new System.Drawing.Point(12, 110);
			this.txtResponse.Multiline = true;
			this.txtResponse.Name = "txtResponse";
			this.txtResponse.ReadOnly = true;
			this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResponse.Size = new System.Drawing.Size(770, 421);
			this.txtResponse.TabIndex = 2;
			this.txtResponse.WordWrap = false;
			// 
			// btnArticle
			// 
			this.btnArticle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnArticle.Location = new System.Drawing.Point(608, 17);
			this.btnArticle.Name = "btnArticle";
			this.btnArticle.Size = new System.Drawing.Size(75, 23);
			this.btnArticle.TabIndex = 3;
			this.btnArticle.Text = "Artikel";
			this.btnArticle.UseVisualStyleBackColor = true;
			this.btnArticle.Click += new System.EventHandler(this.btnArticle_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSearch);
			this.groupBox1.Controls.Add(this.btnArticle);
			this.groupBox1.Controls.Add(this.txtArticle);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(770, 53);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Artikel in Wikipedia";
			// 
			// btnSearch
			// 
			this.btnSearch.Enabled = false;
			this.btnSearch.Location = new System.Drawing.Point(689, 17);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 4;
			this.btnSearch.Text = "Suche";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnOpenEditor
			// 
			this.btnOpenEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenEditor.Enabled = false;
			this.btnOpenEditor.Location = new System.Drawing.Point(662, 537);
			this.btnOpenEditor.Name = "btnOpenEditor";
			this.btnOpenEditor.Size = new System.Drawing.Size(120, 23);
			this.btnOpenEditor.TabIndex = 5;
			this.btnOpenEditor.Text = "Im Filmeditor öffnen";
			this.btnOpenEditor.UseVisualStyleBackColor = true;
			this.btnOpenEditor.Click += new System.EventHandler(this.btnOpenEditor_Click);
			// 
			// lblRequestTime
			// 
			this.lblRequestTime.AutoSize = true;
			this.lblRequestTime.Location = new System.Drawing.Point(12, 542);
			this.lblRequestTime.Name = "lblRequestTime";
			this.lblRequestTime.Size = new System.Drawing.Size(0, 13);
			this.lblRequestTime.TabIndex = 7;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(12, 68);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(192, 31);
			this.lblTitle.TabIndex = 8;
			this.lblTitle.Text = "Suchergebnis";
			// 
			// pBrowser
			// 
			this.pBrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pBrowser.Controls.Add(this.wbResponse);
			this.pBrowser.Location = new System.Drawing.Point(12, 110);
			this.pBrowser.Name = "pBrowser";
			this.pBrowser.Size = new System.Drawing.Size(770, 421);
			this.pBrowser.TabIndex = 10;
			// 
			// wbResponse
			// 
			this.wbResponse.AllowNavigation = false;
			this.wbResponse.AllowWebBrowserDrop = false;
			this.wbResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbResponse.IsWebBrowserContextMenuEnabled = false;
			this.wbResponse.Location = new System.Drawing.Point(0, 0);
			this.wbResponse.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbResponse.Name = "wbResponse";
			this.wbResponse.Size = new System.Drawing.Size(768, 419);
			this.wbResponse.TabIndex = 10;
			// 
			// lbResult
			// 
			this.lbResult.FormattingEnabled = true;
			this.lbResult.Location = new System.Drawing.Point(12, 110);
			this.lbResult.Name = "lbResult";
			this.lbResult.Size = new System.Drawing.Size(770, 121);
			this.lbResult.TabIndex = 6;
			this.lbResult.DoubleClick += new System.EventHandler(this.lbResult_DoubleClick);
			// 
			// FromWikipediaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 572);
			this.Controls.Add(this.pBrowser);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblRequestTime);
			this.Controls.Add(this.txtResponse);
			this.Controls.Add(this.lbResult);
			this.Controls.Add(this.btnOpenEditor);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FromWikipediaForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Neu aus Wikipedia";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FromWikipediaForm_FormClosing);
			this.Shown += new System.EventHandler(this.FromWikipediaForm_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.pBrowser.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtArticle;
		private System.Windows.Forms.TextBox txtResponse;
		private System.Windows.Forms.Button btnArticle;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnOpenEditor;
		private System.Windows.Forms.Label lblRequestTime;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Panel pBrowser;
		private System.Windows.Forms.WebBrowser wbResponse;
		private System.Windows.Forms.ListBox lbResult;
	}
}