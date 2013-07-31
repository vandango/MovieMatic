namespace Toenda.MovieMatic {
	partial class EditPersonRole {
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
			this.gbActor = new System.Windows.Forms.GroupBox();
			this.txtRoleName = new System.Windows.Forms.TextBox();
			this.cbRoleType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblActorName = new System.Windows.Forms.Label();
			this.gbActor.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbActor
			// 
			this.gbActor.Controls.Add(this.txtRoleName);
			this.gbActor.Controls.Add(this.cbRoleType);
			this.gbActor.Controls.Add(this.label2);
			this.gbActor.Controls.Add(this.label1);
			this.gbActor.Location = new System.Drawing.Point(12, 37);
			this.gbActor.Name = "gbActor";
			this.gbActor.Size = new System.Drawing.Size(370, 77);
			this.gbActor.TabIndex = 12;
			this.gbActor.TabStop = false;
			this.gbActor.Text = "Daten zur Rolle im Film";
			// 
			// txtRoleName
			// 
			this.txtRoleName.Location = new System.Drawing.Point(92, 19);
			this.txtRoleName.Name = "txtRoleName";
			this.txtRoleName.Size = new System.Drawing.Size(272, 20);
			this.txtRoleName.TabIndex = 1;
			// 
			// cbRoleType
			// 
			this.cbRoleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRoleType.FormattingEnabled = true;
			this.cbRoleType.Location = new System.Drawing.Point(92, 45);
			this.cbRoleType.Name = "cbRoleType";
			this.cbRoleType.Size = new System.Drawing.Size(272, 21);
			this.cbRoleType.TabIndex = 2;
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
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(226, 123);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(307, 123);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblActorName
			// 
			this.lblActorName.AutoSize = true;
			this.lblActorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
			this.lblActorName.Location = new System.Drawing.Point(12, 9);
			this.lblActorName.Name = "lblActorName";
			this.lblActorName.Size = new System.Drawing.Size(173, 20);
			this.lblActorName.TabIndex = 13;
			this.lblActorName.Text = "Firstname Lastname";
			// 
			// EditPersonRole
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(394, 158);
			this.Controls.Add(this.lblActorName);
			this.Controls.Add(this.gbActor);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditPersonRole";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rolle des Schauspielers bearbeiten";
			this.Shown += new System.EventHandler(this.EditPersonRole_Shown);
			this.gbActor.ResumeLayout(false);
			this.gbActor.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gbActor;
		private System.Windows.Forms.TextBox txtRoleName;
		private System.Windows.Forms.ComboBox cbRoleType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblActorName;
	}
}