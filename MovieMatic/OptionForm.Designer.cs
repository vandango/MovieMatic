namespace Toenda.MovieMatic {
	partial class OptionForm {
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
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.rbWindows = new System.Windows.Forms.RadioButton();
			this.rbSQL = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(129, 193);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(210, 193);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUsername);
			this.groupBox1.Controls.Add(this.txtDatabase);
			this.groupBox1.Controls.Add(this.txtServer);
			this.groupBox1.Controls.Add(this.rbWindows);
			this.groupBox1.Controls.Add(this.rbSQL);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(273, 175);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Datenbank Einstellungen";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(185, 146);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "Passwort";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(185, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 13);
			this.label3.TabIndex = 21;
			this.label3.Text = "Benutzername";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(185, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Datenbank";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(185, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "SQL Server";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(6, 143);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(173, 20);
			this.txtPassword.TabIndex = 18;
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(6, 117);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(173, 20);
			this.txtUsername.TabIndex = 17;
			// 
			// txtDatabase
			// 
			this.txtDatabase.Location = new System.Drawing.Point(6, 91);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.ReadOnly = true;
			this.txtDatabase.Size = new System.Drawing.Size(173, 20);
			this.txtDatabase.TabIndex = 16;
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(6, 65);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(173, 20);
			this.txtServer.TabIndex = 15;
			// 
			// rbWindows
			// 
			this.rbWindows.AutoSize = true;
			this.rbWindows.Location = new System.Drawing.Point(6, 42);
			this.rbWindows.Name = "rbWindows";
			this.rbWindows.Size = new System.Drawing.Size(150, 17);
			this.rbWindows.TabIndex = 14;
			this.rbWindows.TabStop = true;
			this.rbWindows.Text = "Windows Authentifizierung";
			this.rbWindows.UseVisualStyleBackColor = true;
			this.rbWindows.CheckedChanged += new System.EventHandler(this.rbWindows_CheckedChanged);
			// 
			// rbSQL
			// 
			this.rbSQL.AutoSize = true;
			this.rbSQL.Location = new System.Drawing.Point(6, 19);
			this.rbSQL.Name = "rbSQL";
			this.rbSQL.Size = new System.Drawing.Size(127, 17);
			this.rbSQL.TabIndex = 13;
			this.rbSQL.TabStop = true;
			this.rbSQL.Text = "SQL Authentifizierung";
			this.rbSQL.UseVisualStyleBackColor = true;
			this.rbSQL.CheckedChanged += new System.EventHandler(this.rbSQL_CheckedChanged);
			// 
			// OptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 228);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Optionen";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbWindows;
		private System.Windows.Forms.RadioButton rbSQL;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtDatabase;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}