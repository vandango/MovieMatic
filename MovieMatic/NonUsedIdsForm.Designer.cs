namespace Toenda.MovieMatic {
	partial class NonUsedIdsForm {
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
			this.lvNumbers = new System.Windows.Forms.ListView();
			this.columnHeader1 = ( (System.Windows.Forms.ColumnHeader)( new System.Windows.Forms.ColumnHeader() ) );
			this.SuspendLayout();
			// 
			// lvNumbers
			// 
			this.lvNumbers.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvNumbers.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.lvNumbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvNumbers.FullRowSelect = true;
			this.lvNumbers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvNumbers.HideSelection = false;
			this.lvNumbers.Location = new System.Drawing.Point(12, 12);
			this.lvNumbers.MultiSelect = false;
			this.lvNumbers.Name = "lvNumbers";
			this.lvNumbers.Size = new System.Drawing.Size(303, 237);
			this.lvNumbers.TabIndex = 3;
			this.lvNumbers.UseCompatibleStateImageBehavior = false;
			this.lvNumbers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 220;
			// 
			// NonUsedIdsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 261);
			this.Controls.Add(this.lvNumbers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "NonUsedIdsForm";
			this.Text = "Nicht verwendete Nummern anzeigen";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvNumbers;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}