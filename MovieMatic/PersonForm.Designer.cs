namespace Toenda.MovieMatic {
	partial class PersonForm {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkIsMusician = new System.Windows.Forms.CheckBox();
			this.chkIsCameraman = new System.Windows.Forms.CheckBox();
			this.chkIsCutter = new System.Windows.Forms.CheckBox();
			this.chkIsWriter = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.chkIsProducer = new System.Windows.Forms.CheckBox();
			this.chkIsDirector = new System.Windows.Forms.CheckBox();
			this.chkIsActor = new System.Windows.Forms.CheckBox();
			this.txtLastname = new System.Windows.Forms.TextBox();
			this.txtFirstname = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(227, 251);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Abbrechen";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnOK.Location = new System.Drawing.Point(146, 251);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkIsMusician);
			this.groupBox1.Controls.Add(this.chkIsCameraman);
			this.groupBox1.Controls.Add(this.chkIsCutter);
			this.groupBox1.Controls.Add(this.chkIsWriter);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chkIsProducer);
			this.groupBox1.Controls.Add(this.chkIsDirector);
			this.groupBox1.Controls.Add(this.chkIsActor);
			this.groupBox1.Controls.Add(this.txtLastname);
			this.groupBox1.Controls.Add(this.txtFirstname);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(290, 229);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Personendaten";
			// 
			// chkIsMusician
			// 
			this.chkIsMusician.AutoSize = true;
			this.chkIsMusician.Location = new System.Drawing.Point(6, 163);
			this.chkIsMusician.Name = "chkIsMusician";
			this.chkIsMusician.Size = new System.Drawing.Size(77, 17);
			this.chkIsMusician.TabIndex = 19;
			this.chkIsMusician.Text = "Ist Musiker";
			this.chkIsMusician.UseVisualStyleBackColor = true;
			// 
			// chkIsCameraman
			// 
			this.chkIsCameraman.AutoSize = true;
			this.chkIsCameraman.Location = new System.Drawing.Point(6, 186);
			this.chkIsCameraman.Name = "chkIsCameraman";
			this.chkIsCameraman.Size = new System.Drawing.Size(96, 17);
			this.chkIsCameraman.TabIndex = 20;
			this.chkIsCameraman.Text = "Ist Kamermann";
			this.chkIsCameraman.UseVisualStyleBackColor = true;
			// 
			// chkIsCutter
			// 
			this.chkIsCutter.AutoSize = true;
			this.chkIsCutter.Location = new System.Drawing.Point(6, 209);
			this.chkIsCutter.Name = "chkIsCutter";
			this.chkIsCutter.Size = new System.Drawing.Size(126, 17);
			this.chkIsCutter.TabIndex = 21;
			this.chkIsCutter.Text = "Ist Cutter (Filmschnitt)";
			this.chkIsCutter.UseVisualStyleBackColor = true;
			// 
			// chkIsWriter
			// 
			this.chkIsWriter.AutoSize = true;
			this.chkIsWriter.Location = new System.Drawing.Point(6, 117);
			this.chkIsWriter.Name = "chkIsWriter";
			this.chkIsWriter.Size = new System.Drawing.Size(121, 17);
			this.chkIsWriter.TabIndex = 17;
			this.chkIsWriter.Text = "Ist (Drehbuch-)Autor";
			this.chkIsWriter.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(218, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "Nachname";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(218, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "Vorname";
			// 
			// chkIsProducer
			// 
			this.chkIsProducer.AutoSize = true;
			this.chkIsProducer.Location = new System.Drawing.Point(6, 140);
			this.chkIsProducer.Name = "chkIsProducer";
			this.chkIsProducer.Size = new System.Drawing.Size(88, 17);
			this.chkIsProducer.TabIndex = 18;
			this.chkIsProducer.Text = "Ist Produzent";
			this.chkIsProducer.UseVisualStyleBackColor = true;
			// 
			// chkIsDirector
			// 
			this.chkIsDirector.AutoSize = true;
			this.chkIsDirector.Location = new System.Drawing.Point(6, 94);
			this.chkIsDirector.Name = "chkIsDirector";
			this.chkIsDirector.Size = new System.Drawing.Size(87, 17);
			this.chkIsDirector.TabIndex = 16;
			this.chkIsDirector.Text = "Ist Regisseur";
			this.chkIsDirector.UseVisualStyleBackColor = true;
			// 
			// chkIsActor
			// 
			this.chkIsActor.AutoSize = true;
			this.chkIsActor.Location = new System.Drawing.Point(6, 71);
			this.chkIsActor.Name = "chkIsActor";
			this.chkIsActor.Size = new System.Drawing.Size(101, 17);
			this.chkIsActor.TabIndex = 15;
			this.chkIsActor.Text = "Ist Schauspieler";
			this.chkIsActor.UseVisualStyleBackColor = true;
			// 
			// txtLastname
			// 
			this.txtLastname.Location = new System.Drawing.Point(6, 45);
			this.txtLastname.Name = "txtLastname";
			this.txtLastname.Size = new System.Drawing.Size(206, 20);
			this.txtLastname.TabIndex = 14;
			// 
			// txtFirstname
			// 
			this.txtFirstname.Location = new System.Drawing.Point(6, 19);
			this.txtFirstname.Name = "txtFirstname";
			this.txtFirstname.Size = new System.Drawing.Size(206, 20);
			this.txtFirstname.TabIndex = 13;
			// 
			// PersonForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(314, 286);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PersonForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Person Editor";
			this.Shown += new System.EventHandler(this.PersonForm_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkIsProducer;
		private System.Windows.Forms.CheckBox chkIsDirector;
		private System.Windows.Forms.CheckBox chkIsActor;
		private System.Windows.Forms.TextBox txtLastname;
		private System.Windows.Forms.TextBox txtFirstname;
		private System.Windows.Forms.CheckBox chkIsMusician;
		private System.Windows.Forms.CheckBox chkIsCameraman;
		private System.Windows.Forms.CheckBox chkIsCutter;
		private System.Windows.Forms.CheckBox chkIsWriter;
	}
}