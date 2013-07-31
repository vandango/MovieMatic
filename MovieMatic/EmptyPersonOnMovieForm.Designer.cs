namespace Toenda.MovieMatic {
	partial class EmptyPersonOnMovieForm {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvMovies = new System.Windows.Forms.DataGridView();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SortValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGenre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colOriginal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dgvMovies ) ).BeginInit();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.dgvMovies);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(794, 525);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(794, 572);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(794, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
			this.toolStripStatusLabel1.Text = "Gefundene Filme: 0";
			// 
			// dgvMovies
			// 
			this.dgvMovies.AllowUserToAddRows = false;
			this.dgvMovies.AllowUserToResizeColumns = false;
			this.dgvMovies.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 238 ) ) ) ), ( (int)( ( (byte)( 240 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ));
			this.dgvMovies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvMovies.BackgroundColor = System.Drawing.Color.AntiqueWhite;
			this.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvMovies.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvMovies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMovies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.SortValue,
            this.colNumber,
            this.colName,
            this.colGenre,
            this.colOriginal});
			this.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvMovies.Location = new System.Drawing.Point(0, 0);
			this.dgvMovies.MultiSelect = false;
			this.dgvMovies.Name = "dgvMovies";
			this.dgvMovies.ReadOnly = true;
			this.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvMovies.Size = new System.Drawing.Size(794, 525);
			this.dgvMovies.TabIndex = 2;
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			this.colID.Visible = false;
			// 
			// SortValue
			// 
			this.SortValue.DataPropertyName = "SortValue";
			this.SortValue.FillWeight = 60F;
			this.SortValue.HeaderText = "Sort.";
			this.SortValue.Name = "SortValue";
			this.SortValue.ReadOnly = true;
			this.SortValue.ToolTipText = "Sort.";
			this.SortValue.Width = 60;
			// 
			// colNumber
			// 
			this.colNumber.DataPropertyName = "Number";
			this.colNumber.FillWeight = 50F;
			this.colNumber.HeaderText = "Nr.";
			this.colNumber.MinimumWidth = 50;
			this.colNumber.Name = "colNumber";
			this.colNumber.ReadOnly = true;
			this.colNumber.ToolTipText = "Nr.";
			this.colNumber.Width = 50;
			// 
			// colName
			// 
			this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colName.DataPropertyName = "Name";
			this.colName.FillWeight = 266.2958F;
			this.colName.HeaderText = "Name";
			this.colName.MinimumWidth = 120;
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			this.colName.ToolTipText = "Name";
			// 
			// colGenre
			// 
			this.colGenre.DataPropertyName = "GenresString";
			this.colGenre.FillWeight = 120F;
			this.colGenre.HeaderText = "Genre";
			this.colGenre.Name = "colGenre";
			this.colGenre.ReadOnly = true;
			this.colGenre.ToolTipText = "Genre";
			this.colGenre.Width = 120;
			// 
			// colOriginal
			// 
			this.colOriginal.DataPropertyName = "IsOriginal";
			this.colOriginal.HeaderText = "Original";
			this.colOriginal.Name = "colOriginal";
			this.colOriginal.ReadOnly = true;
			this.colOriginal.Width = 50;
			// 
			// EmptyPersonOnMovieForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 572);
			this.Controls.Add(this.toolStripContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EmptyPersonOnMovieForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Filme ohne zugeordnete Personen";
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dgvMovies ) ).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.DataGridView dgvMovies;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewTextBoxColumn SortValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGenre;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colOriginal;

	}
}