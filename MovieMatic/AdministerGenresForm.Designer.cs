namespace Toenda.MovieMatic {
	partial class AdministerGenresForm {
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
			this.lvGenres = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnNew = new System.Windows.Forms.Button();
			this.lblFound = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(81, 294);
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
			this.btnCancel.Location = new System.Drawing.Point(162, 294);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lvGenres
			// 
			this.lvGenres.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvGenres.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvGenres.FullRowSelect = true;
			this.lvGenres.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvGenres.HideSelection = false;
			this.lvGenres.Location = new System.Drawing.Point(12, 32);
			this.lvGenres.MultiSelect = false;
			this.lvGenres.Name = "lvGenres";
			this.lvGenres.Size = new System.Drawing.Size(226, 250);
			this.lvGenres.TabIndex = 2;
			this.lvGenres.UseCompatibleStateImageBehavior = false;
			this.lvGenres.View = System.Windows.Forms.View.Details;
			this.lvGenres.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvGenres_MouseUp);
			this.lvGenres.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvGenres_MouseDown);
			this.lvGenres.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGenres_ItemSelectionChanged);
			this.lvGenres.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvGenres_KeyUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 220;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(53, 6);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(134, 20);
			this.txtName.TabIndex = 3;
			this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(12, 9);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Name";
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(193, 6);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(45, 20);
			this.btnNew.TabIndex = 5;
			this.btnNew.Text = "Neu";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// lblFound
			// 
			this.lblFound.AutoSize = true;
			this.lblFound.Location = new System.Drawing.Point(35, 299);
			this.lblFound.Name = "lblFound";
			this.lblFound.Size = new System.Drawing.Size(13, 13);
			this.lblFound.TabIndex = 12;
			this.lblFound.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 299);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "#:";
			// 
			// AdministerGenresForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(249, 329);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblFound);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lvGenres);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AdministerGenresForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Genres verwalten...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListView lvGenres;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label lblFound;
		private System.Windows.Forms.Label label1;
	}
}