namespace Toenda.MovieMatic {
	partial class ExportForm {
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
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtSheettitle = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbExport = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTargetPath = new System.Windows.Forms.TextBox();
			this.btnSaveFileName = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.colsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.pbState = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblState = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtSheettitle);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtFilename);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cbExport);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtTargetPath);
			this.groupBox1.Controls.Add(this.btnSaveFileName);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(466, 130);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tabellen Informationen";
			// 
			// txtSheettitle
			// 
			this.txtSheettitle.Location = new System.Drawing.Point(130, 98);
			this.txtSheettitle.Name = "txtSheettitle";
			this.txtSheettitle.Size = new System.Drawing.Size(296, 20);
			this.txtSheettitle.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Tabellentitel";
			// 
			// txtFilename
			// 
			this.txtFilename.Location = new System.Drawing.Point(130, 19);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(296, 20);
			this.txtFilename.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Dateiname";
			// 
			// cbExport
			// 
			this.cbExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbExport.FormattingEnabled = true;
			this.cbExport.Location = new System.Drawing.Point(130, 71);
			this.cbExport.Name = "cbExport";
			this.cbExport.Size = new System.Drawing.Size(296, 21);
			this.cbExport.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Export Ziel";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Datei speichern unter";
			// 
			// txtTargetPath
			// 
			this.txtTargetPath.Location = new System.Drawing.Point(130, 45);
			this.txtTargetPath.Name = "txtTargetPath";
			this.txtTargetPath.Size = new System.Drawing.Size(296, 20);
			this.txtTargetPath.TabIndex = 6;
			// 
			// btnSaveFileName
			// 
			this.btnSaveFileName.Location = new System.Drawing.Point(432, 44);
			this.btnSaveFileName.Name = "btnSaveFileName";
			this.btnSaveFileName.Size = new System.Drawing.Size(24, 21);
			this.btnSaveFileName.TabIndex = 0;
			this.btnSaveFileName.Text = "...";
			this.btnSaveFileName.UseVisualStyleBackColor = true;
			this.btnSaveFileName.Click += new System.EventHandler(this.btnSaveFileName_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.colsPanel);
			this.groupBox2.Location = new System.Drawing.Point(12, 148);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(466, 166);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Zu exportierende Spalten wählen";
			// 
			// colsPanel
			// 
			this.colsPanel.Location = new System.Drawing.Point(9, 19);
			this.colsPanel.Name = "colsPanel";
			this.colsPanel.Size = new System.Drawing.Size(447, 141);
			this.colsPanel.TabIndex = 0;
			// 
			// pbState
			// 
			this.pbState.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.pbState.Location = new System.Drawing.Point(12, 320);
			this.pbState.Name = "pbState";
			this.pbState.Size = new System.Drawing.Size(466, 23);
			this.pbState.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbState.TabIndex = 8;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.Location = new System.Drawing.Point(403, 362);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(322, 362);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "Start";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblState
			// 
			this.lblState.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblState.AutoSize = true;
			this.lblState.Location = new System.Drawing.Point(12, 346);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(95, 13);
			this.lblState.TabIndex = 11;
			this.lblState.Text = "Export läuft nicht...";
			// 
			// ExportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(490, 397);
			this.Controls.Add(this.lblState);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pbState);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Daten exportieren";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtTargetPath;
		private System.Windows.Forms.Button btnSaveFileName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cbExport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFilename;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.FlowLayoutPanel colsPanel;
		private System.Windows.Forms.TextBox txtSheettitle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ProgressBar pbState;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblState;
	}
}