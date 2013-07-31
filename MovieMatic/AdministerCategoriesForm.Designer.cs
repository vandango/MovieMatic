namespace Toenda.MovieMatic {
	partial class AdministerCategoriesForm {
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lvCategories = new System.Windows.Forms.ListView();
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
			// lvCategories
			// 
			this.lvCategories.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvCategories.FullRowSelect = true;
			this.lvCategories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvCategories.HideSelection = false;
			this.lvCategories.Location = new System.Drawing.Point(12, 32);
			this.lvCategories.MultiSelect = false;
			this.lvCategories.Name = "lvCategories";
			this.lvCategories.Size = new System.Drawing.Size(226, 250);
			this.lvCategories.TabIndex = 2;
			this.lvCategories.UseCompatibleStateImageBehavior = false;
			this.lvCategories.View = System.Windows.Forms.View.Details;
			this.lvCategories.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvGenres_MouseUp);
			this.lvCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvGenres_MouseDown);
			this.lvCategories.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGenres_ItemSelectionChanged);
			this.lvCategories.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvGenres_KeyUp);
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
			// AdministerCategoriesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(249, 329);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblFound);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lvCategories);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AdministerCategoriesForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Kategorien verwalten...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListView lvCategories;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label lblFound;
		private System.Windows.Forms.Label label1;
	}
}