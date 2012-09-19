namespace Toenda.MovieMatic {
	partial class AddItemForm {
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lvItems = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.btnAddItem = new System.Windows.Forms.Button();
			this.gbActor = new System.Windows.Forms.GroupBox();
			this.txtRoleName = new System.Windows.Forms.TextBox();
			this.cbRoleType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gbActor.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(214, 341);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(133, 341);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lvItems
			// 
			this.lvItems.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvItems.FullRowSelect = true;
			this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvItems.HideSelection = false;
			this.lvItems.Location = new System.Drawing.Point(12, 12);
			this.lvItems.Name = "lvItems";
			this.lvItems.Size = new System.Drawing.Size(277, 240);
			this.lvItems.TabIndex = 3;
			this.lvItems.UseCompatibleStateImageBehavior = false;
			this.lvItems.View = System.Windows.Forms.View.Details;
			this.lvItems.DoubleClick += new System.EventHandler(this.lvItems_DoubleClick);
			this.lvItems.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvItems_ItemSelectionChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 256;
			// 
			// btnAddItem
			// 
			this.btnAddItem.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnAddItem.Location = new System.Drawing.Point(12, 341);
			this.btnAddItem.Name = "btnAddItem";
			this.btnAddItem.Size = new System.Drawing.Size(115, 23);
			this.btnAddItem.TabIndex = 9;
			this.btnAddItem.Text = "Item hinzufügen...";
			this.btnAddItem.UseVisualStyleBackColor = true;
			this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
			// 
			// gbActor
			// 
			this.gbActor.Controls.Add(this.txtRoleName);
			this.gbActor.Controls.Add(this.cbRoleType);
			this.gbActor.Controls.Add(this.label2);
			this.gbActor.Controls.Add(this.label1);
			this.gbActor.Location = new System.Drawing.Point(12, 258);
			this.gbActor.Name = "gbActor";
			this.gbActor.Size = new System.Drawing.Size(277, 77);
			this.gbActor.TabIndex = 9;
			this.gbActor.TabStop = false;
			this.gbActor.Text = "Daten zur Rolle im Film";
			// 
			// txtRoleName
			// 
			this.txtRoleName.Location = new System.Drawing.Point(92, 19);
			this.txtRoleName.Name = "txtRoleName";
			this.txtRoleName.Size = new System.Drawing.Size(179, 20);
			this.txtRoleName.TabIndex = 5;
			// 
			// cbRoleType
			// 
			this.cbRoleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRoleType.FormattingEnabled = true;
			this.cbRoleType.Location = new System.Drawing.Point(92, 45);
			this.cbRoleType.Name = "cbRoleType";
			this.cbRoleType.Size = new System.Drawing.Size(179, 21);
			this.cbRoleType.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Art der Rolle";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Name der Rolle";
			// 
			// AddItemForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(301, 376);
			this.Controls.Add(this.gbActor);
			this.Controls.Add(this.btnAddItem);
			this.Controls.Add(this.lvItems);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddItemForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Item auswählen...";
			this.Shown += new System.EventHandler(this.AddItemForm_Shown);
			this.gbActor.ResumeLayout(false);
			this.gbActor.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ListView lvItems;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button btnAddItem;
		private System.Windows.Forms.GroupBox gbActor;
		private System.Windows.Forms.TextBox txtRoleName;
		private System.Windows.Forms.ComboBox cbRoleType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}