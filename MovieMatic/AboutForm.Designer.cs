namespace Toenda.MovieMatic {
	partial class AboutForm {
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
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblTFLVersion = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblInterfaceVersion = new System.Windows.Forms.Label();
			this.lblPVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblCopyright
			// 
			this.lblCopyright.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.ForeColor = System.Drawing.Color.White;
			this.lblCopyright.Location = new System.Drawing.Point(12, 122);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(61, 13);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "lblCopyright";
			this.lblCopyright.Click += new System.EventHandler(this.lblCopyright_Click);
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblVersion.AutoSize = true;
			this.lblVersion.ForeColor = System.Drawing.Color.White;
			this.lblVersion.Location = new System.Drawing.Point(12, 136);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(52, 13);
			this.lblVersion.TabIndex = 3;
			this.lblVersion.Text = "lblVersion";
			this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
			// 
			// lblTFLVersion
			// 
			this.lblTFLVersion.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblTFLVersion.AutoSize = true;
			this.lblTFLVersion.ForeColor = System.Drawing.Color.White;
			this.lblTFLVersion.Location = new System.Drawing.Point(12, 165);
			this.lblTFLVersion.Name = "lblTFLVersion";
			this.lblTFLVersion.Size = new System.Drawing.Size(71, 13);
			this.lblTFLVersion.TabIndex = 4;
			this.lblTFLVersion.Text = "lblTFLVersion";
			this.lblTFLVersion.Click += new System.EventHandler(this.lblTFLVersion_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BackgroundImage = global::Toenda.MovieMatic.Properties.Resources.logo;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(502, 102);
			this.panel1.TabIndex = 0;
			this.panel1.Click += new System.EventHandler(this.panel1_Click);
			// 
			// lblInterfaceVersion
			// 
			this.lblInterfaceVersion.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblInterfaceVersion.AutoSize = true;
			this.lblInterfaceVersion.ForeColor = System.Drawing.Color.White;
			this.lblInterfaceVersion.Location = new System.Drawing.Point(12, 150);
			this.lblInterfaceVersion.Name = "lblInterfaceVersion";
			this.lblInterfaceVersion.Size = new System.Drawing.Size(94, 13);
			this.lblInterfaceVersion.TabIndex = 5;
			this.lblInterfaceVersion.Text = "lblInterfaceVersion";
			this.lblInterfaceVersion.Click += new System.EventHandler(this.lblInterfaceVersion_Click);
			// 
			// lblPVersion
			// 
			this.lblPVersion.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.lblPVersion.AutoSize = true;
			this.lblPVersion.ForeColor = System.Drawing.Color.White;
			this.lblPVersion.Location = new System.Drawing.Point(12, 180);
			this.lblPVersion.Name = "lblPVersion";
			this.lblPVersion.Size = new System.Drawing.Size(59, 13);
			this.lblPVersion.TabIndex = 6;
			this.lblPVersion.Text = "lblPVersion";
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.SteelBlue;
			this.ClientSize = new System.Drawing.Size(526, 202);
			this.Controls.Add(this.lblPVersion);
			this.Controls.Add(this.lblInterfaceVersion);
			this.Controls.Add(this.lblTFLVersion);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AboutForm";
			this.Opacity = 0.95;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutForm";
			this.Click += new System.EventHandler(this.AboutForm_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblTFLVersion;
		private System.Windows.Forms.Label lblInterfaceVersion;
		private System.Windows.Forms.Label lblPVersion;
	}
}