using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation;
using Toenda.Foundation.Data;

namespace Toenda.MovieMatic {
	public partial class OptionForm : Form {
		private DALSettings _cfg;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public OptionForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._LoadData();
		}

		// -------------------------------------------------------
		// EVENTS
		// -------------------------------------------------------

		/// <summary>
		/// btnCancel_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// btnOK_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, EventArgs e) {
			try {
				this._SaveData();

				this.DialogResult = DialogResult.OK;

				this.Close();
			}
			catch(Exception ex) {
				MessageBox.Show(
					ex.Message + (
						ex.InnerException != null
						? " (" + ex.InnerException.Message + ")"
						: ""
					),
					"ERROR"
				);
			}
		}

		/// <summary>
		/// rbWindows_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbWindows_CheckedChanged(object sender, EventArgs e) {
			if(this.rbWindows.Checked) {
				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = false;
				this.txtPassword.Enabled = false;

				this.rbSQL.Checked = false;
			}
			else {
				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = true;
				this.txtPassword.Enabled = true;

				this.rbSQL.Checked = true;
			}
		}

		/// <summary>
		/// rbSQL_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbSQL_CheckedChanged(object sender, EventArgs e) {
			if(this.rbSQL.Checked) {
				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = true;
				this.txtPassword.Enabled = true;

				this.rbWindows.Checked = false;
			}
			else {
				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = false;
				this.txtPassword.Enabled = false;

				this.rbWindows.Checked = true;
			}
		}

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Load the data
		/// </summary>
		private void _LoadData() {
			this._cfg = UdlParser.ParseConnectionString(
				Configuration.ConnectionString
			);

			if(this._cfg.IsWindowsAuthentication) {
				this.rbWindows.Checked = true;
				this.rbSQL.Checked = false;

				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = false;
				this.txtPassword.Enabled = false;

				this.txtServer.Text = this._cfg.Server;
				this.txtDatabase.Text = this._cfg.Database;
			}
			else {
				this.rbWindows.Checked = false;
				this.rbSQL.Checked = true;

				this.txtServer.Enabled = true;
				this.txtDatabase.Enabled = true;
				this.txtUsername.Enabled = true;
				this.txtPassword.Enabled = true;

				this.txtServer.Text = this._cfg.Server;
				this.txtDatabase.Text = this._cfg.Database;
				this.txtUsername.Text = this._cfg.User;
				this.txtPassword.Text = this._cfg.Password;
			}
		}

		/// <summary>
		/// Save the data
		/// </summary>
		private void _SaveData() {
			if(this.rbSQL.Checked) {
				this._cfg.Server = this.txtServer.Text.Trim();
				this._cfg.Database = this.txtDatabase.Text.Trim();
				this._cfg.User = this.txtUsername.Text.Trim();
				this._cfg.Password = this.txtPassword.Text.Trim();
				this._cfg.IntegratedSecurity = false;
			}
			else {
				this._cfg = null;
				this._cfg = new DALSettings(
					ProviderType.MSSql2005,
					this.txtServer.Text.Trim(),
					this.txtDatabase.Text.Trim()
				);
			}

			try {
				Configuration.SaveConnectionString(this._cfg.ConnectionString);
			}
			catch(Exception) {
			}
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------
	}
}