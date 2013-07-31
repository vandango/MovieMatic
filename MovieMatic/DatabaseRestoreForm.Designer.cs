namespace Toenda.MovieMatic {
	partial class DatabaseRestoreForm {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkReplace = new System.Windows.Forms.CheckBox();
			this.chkDeleteBeforeRestore = new System.Windows.Forms.CheckBox();
			this.btnRestore = new System.Windows.Forms.Button();
			this.pbProcess = new System.Windows.Forms.ProgressBar();
			this.lvRestores = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colDate = new System.Windows.Forms.ColumnHeader();
			this.colFilename = new System.Windows.Forms.ColumnHeader();
			this.pgPath = new System.Windows.Forms.GroupBox();
			this.btnFolder = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.pgPath.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(573, 346);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.Location = new System.Drawing.Point(654, 346);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.groupBox1.Controls.Add(this.chkReplace);
			this.groupBox1.Controls.Add(this.chkDeleteBeforeRestore);
			this.groupBox1.Controls.Add(this.btnRestore);
			this.groupBox1.Controls.Add(this.pbProcess);
			this.groupBox1.Controls.Add(this.lvRestores);
			this.groupBox1.Location = new System.Drawing.Point(12, 65);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(717, 275);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Vorhandene Sicherungen";
			// 
			// chkReplace
			// 
			this.chkReplace.AutoSize = true;
			this.chkReplace.Location = new System.Drawing.Point(6, 199);
			this.chkReplace.Name = "chkReplace";
			this.chkReplace.Size = new System.Drawing.Size(216, 17);
			this.chkReplace.TabIndex = 16;
			this.chkReplace.Text = "Vorhandene Datenbank überschreiben?";
			this.chkReplace.UseVisualStyleBackColor = true;
			// 
			// chkDeleteBeforeRestore
			// 
			this.chkDeleteBeforeRestore.AutoSize = true;
			this.chkDeleteBeforeRestore.Location = new System.Drawing.Point(6, 222);
			this.chkDeleteBeforeRestore.Name = "chkDeleteBeforeRestore";
			this.chkDeleteBeforeRestore.Size = new System.Drawing.Size(219, 17);
			this.chkDeleteBeforeRestore.TabIndex = 15;
			this.chkDeleteBeforeRestore.Text = "Vorhandene Datenbank vorher löschen?";
			this.chkDeleteBeforeRestore.UseVisualStyleBackColor = true;
			// 
			// btnRestore
			// 
			this.btnRestore.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnRestore.Location = new System.Drawing.Point(599, 244);
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = new System.Drawing.Size(112, 23);
			this.btnRestore.TabIndex = 14;
			this.btnRestore.Text = "Wiederherstellen";
			this.btnRestore.UseVisualStyleBackColor = true;
			this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
			// 
			// pbProcess
			// 
			this.pbProcess.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.pbProcess.Location = new System.Drawing.Point(6, 244);
			this.pbProcess.Name = "pbProcess";
			this.pbProcess.Size = new System.Drawing.Size(587, 23);
			this.pbProcess.TabIndex = 13;
			// 
			// lvRestores
			// 
			this.lvRestores.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvRestores.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.lvRestores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colDate,
            this.colFilename});
			this.lvRestores.FullRowSelect = true;
			this.lvRestores.GridLines = true;
			this.lvRestores.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvRestores.Location = new System.Drawing.Point(6, 19);
			this.lvRestores.MultiSelect = false;
			this.lvRestores.Name = "lvRestores";
			this.lvRestores.Size = new System.Drawing.Size(705, 174);
			this.lvRestores.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.lvRestores.TabIndex = 0;
			this.lvRestores.UseCompatibleStateImageBehavior = false;
			this.lvRestores.View = System.Windows.Forms.View.Details;
			this.lvRestores.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvRestores_ItemSelectionChanged);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 150;
			// 
			// colDate
			// 
			this.colDate.Text = "Datum";
			this.colDate.Width = 150;
			// 
			// colFilename
			// 
			this.colFilename.Text = "Dateiname";
			this.colFilename.Width = 380;
			// 
			// pgPath
			// 
			this.pgPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.pgPath.Controls.Add(this.btnFolder);
			this.pgPath.Controls.Add(this.txtPath);
			this.pgPath.Location = new System.Drawing.Point(12, 12);
			this.pgPath.Name = "pgPath";
			this.pgPath.Size = new System.Drawing.Size(717, 47);
			this.pgPath.TabIndex = 14;
			this.pgPath.TabStop = false;
			this.pgPath.Text = "Aktueller Pfad";
			// 
			// btnFolder
			// 
			this.btnFolder.Location = new System.Drawing.Point(686, 19);
			this.btnFolder.Name = "btnFolder";
			this.btnFolder.Size = new System.Drawing.Size(25, 20);
			this.btnFolder.TabIndex = 12;
			this.btnFolder.Text = "...";
			this.btnFolder.UseVisualStyleBackColor = true;
			this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(6, 19);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(674, 20);
			this.txtPath.TabIndex = 11;
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			this.txtPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
			// 
			// DatabaseRestoreForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(741, 381);
			this.Controls.Add(this.pgPath);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseRestoreForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Datebank Sicherungen";
			this.Load += new System.EventHandler(this.DatabaseRestoreForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.pgPath.ResumeLayout(false);
			this.pgPath.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lvRestores;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDate;
		private System.Windows.Forms.Button btnRestore;
		private System.Windows.Forms.ProgressBar pbProcess;
		private System.Windows.Forms.ColumnHeader colFilename;
		private System.Windows.Forms.GroupBox pgPath;
		private System.Windows.Forms.Button btnFolder;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.FolderBrowserDialog fbDialog;
		private System.Windows.Forms.CheckBox chkDeleteBeforeRestore;
		private System.Windows.Forms.CheckBox chkReplace;
	}
}