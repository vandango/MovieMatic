using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Toenda.Foundation.Data;
using Toenda.Foundation.IO;
using Toenda.Foundation;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class DatabaseBackupForm
	/// </summary>
	public partial class DatabaseBackupForm : Form {
		private BackupRestoreHandler _brh;

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public DatabaseBackupForm() {
			InitializeComponent();

			this._brh = new BackupRestoreHandler(Configuration.ConnectionString);

			this.DialogResult = DialogResult.Cancel;

			this._brh.BackupPercentEvent += new BackupRestoreHandler.BackupPercentEventHandler(_brh_BackupPercentEvent);
			this._brh.BackupCompleteEvent += new BackupRestoreHandler.BackupCompleteEventHandler(_brh_BackupCompleteEvent);

			this.pbProgress.Minimum = 0;
			this.pbProgress.Maximum = 100;

			this.fbDialog.RootFolder = Environment.SpecialFolder.MyComputer;

			try {
				this.gbBackup.Text = "Letztes Backup am: " + this._brh.GetLastBackupDate().ToString();
				this.txtPath.Text = Configuration.CommonApplicationTempPath;
			}
			catch(Exception ex) {
				StringBuilder str = new StringBuilder();
				str.Append("Die Backupschnittstelle konnte nicht initialisiert werden.");
				str.Append(" Eventuell sind nicht alle notwendigen Komponenten installiert.");
				str.Append("\n\n");
				str.Append("Fehlermeldung: ");
				str.Append(ex.Message);

				StaticWindows.ErrorBox(str.ToString());
			}
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROPERTIES
		// ---------------------------------------------------

		// ---------------------------------------------------
		// EVENTS
		// ---------------------------------------------------

		/// <summary>
		/// _brh_BackupPercentEvent
		/// </summary>
		/// <param name="percentValue"></param>
		private void _brh_BackupPercentEvent(int percentValue) {
			if(this.pbProgress.Value < this.pbProgress.Maximum) {
				this.pbProgress.Value = percentValue;
			}
		}

		/// <summary>
		/// _brh_BackupCompleteEvent
		/// </summary>
		/// <param name="error"></param>
		private void _brh_BackupCompleteEvent(System.Data.SqlClient.SqlError error) {
			if(error != null 
			&& error.Class != 0) {
				MessageBox.Show("ERROR: " + error.Message, "Datenbank Sicherung");
			}
			else {
				this.pbProgress.Value = 0;

				MessageBox.Show("Datenbank Backup erfolgreich erstellt!", "Datenbank Sicherung");
			}
		}

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
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		/// <summary>
		/// btnFolder_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFolder_Click(object sender, EventArgs e) {
			if(this.fbDialog.ShowDialog(this) == DialogResult.OK) {
				this.txtPath.Text = this.fbDialog.SelectedPath;
			}
		}

		/// <summary>
		/// btnStart_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStart_Click(object sender, EventArgs e) {
			if(this.txtPath.Text.Trim() != "") {
				this.txtPath.Text = FileSystem.Self.GetDirectoryNameFromPath(this.txtPath.Text.Trim() + "\\");
				this._brh.SavePath = this.txtPath.Text;
				this.txtPath.Text += @"\" + this._brh.BackupDatabase();

				this.gbBackup.Text = "Letztes Backup am: " + this._brh.GetLastBackupDate().ToString();
			}
		}

		// ---------------------------------------------------
		// PRIVATE MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROTECTED MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PUBLIC MEMBERS
		// ---------------------------------------------------
	}
}