namespace Toenda.MovieMatic {
	partial class ImportForm {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOpenFD = new System.Windows.Forms.Button();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSheet = new System.Windows.Forms.TextBox();
			this.pbImport = new System.Windows.Forms.ProgressBar();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnCheckFile = new System.Windows.Forms.Button();
			this.cbImport = new System.Windows.Forms.ComboBox();
			this.ofdFile = new System.Windows.Forms.OpenFileDialog();
			this.btnHelp = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(390, 118);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.Location = new System.Drawing.Point(471, 118);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOpenFD
			// 
			this.btnOpenFD.Location = new System.Drawing.Point(501, 18);
			this.btnOpenFD.Name = "btnOpenFD";
			this.btnOpenFD.Size = new System.Drawing.Size(24, 21);
			this.btnOpenFD.TabIndex = 2;
			this.btnOpenFD.Text = "...";
			this.btnOpenFD.UseVisualStyleBackColor = true;
			this.btnOpenFD.Click += new System.EventHandler(this.btnOpenFD_Click);
			// 
			// txtFilename
			// 
			this.txtFilename.Location = new System.Drawing.Point(199, 19);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(296, 20);
			this.txtFilename.TabIndex = 4;
			this.txtFilename.TextChanged += new System.EventHandler(this.txtFilename_TextChanged);
			this.txtFilename.Leave += new System.EventHandler(this.txtFilename_Leave);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtSheet);
			this.groupBox1.Controls.Add(this.pbImport);
			this.groupBox1.Controls.Add(this.btnImport);
			this.groupBox1.Controls.Add(this.btnCheckFile);
			this.groupBox1.Controls.Add(this.cbImport);
			this.groupBox1.Controls.Add(this.txtFilename);
			this.groupBox1.Controls.Add(this.btnOpenFD);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(534, 100);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Datei wählen...";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(329, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Arbeitsmappen Name";
			// 
			// txtSheet
			// 
			this.txtSheet.Location = new System.Drawing.Point(6, 46);
			this.txtSheet.Name = "txtSheet";
			this.txtSheet.Size = new System.Drawing.Size(317, 20);
			this.txtSheet.TabIndex = 9;
			// 
			// pbImport
			// 
			this.pbImport.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.pbImport.Location = new System.Drawing.Point(6, 71);
			this.pbImport.Name = "pbImport";
			this.pbImport.Size = new System.Drawing.Size(438, 23);
			this.pbImport.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbImport.TabIndex = 7;
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnImport.Location = new System.Drawing.Point(450, 71);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 6;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnCheckFile
			// 
			this.btnCheckFile.Location = new System.Drawing.Point(450, 44);
			this.btnCheckFile.Name = "btnCheckFile";
			this.btnCheckFile.Size = new System.Drawing.Size(75, 23);
			this.btnCheckFile.TabIndex = 5;
			this.btnCheckFile.Text = "Zuordnen";
			this.btnCheckFile.UseVisualStyleBackColor = true;
			this.btnCheckFile.Click += new System.EventHandler(this.btnCheckFile_Click);
			// 
			// cbImport
			// 
			this.cbImport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImport.FormattingEnabled = true;
			this.cbImport.Location = new System.Drawing.Point(6, 19);
			this.cbImport.Name = "cbImport";
			this.cbImport.Size = new System.Drawing.Size(187, 21);
			this.cbImport.TabIndex = 0;
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.btnHelp.Location = new System.Drawing.Point(12, 118);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(23, 23);
			this.btnHelp.TabIndex = 6;
			this.btnHelp.Text = "?";
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// ImportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(558, 153);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Datei importieren";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOpenFD;
		private System.Windows.Forms.TextBox txtFilename;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCheckFile;
		private System.Windows.Forms.ComboBox cbImport;
		private System.Windows.Forms.OpenFileDialog ofdFile;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.ProgressBar pbImport;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSheet;
	}
}