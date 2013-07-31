using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Import;

namespace Toenda.MovieMatic {
	public partial class MigrateMSSQLToSQLiteForm : Form {
		private SQLServerToSQLiteImporter _importer;

		public MigrateMSSQLToSQLiteForm() {
			InitializeComponent();

			this.btnOK.Enabled = true;
			this.btnCancel.Enabled = true;

			this.pbMigrate.Minimum = 0;
			this.pbMigrate.Maximum = 100;
		}

		private void btnOK_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnImport_Click(object sender, EventArgs e) {
			this.btnOK.Enabled = false;
			this.btnCancel.Enabled = false;

			if(StaticWindows.Requester(
				"Wollen Sie wirklich Ihre Microsoft SQL Server Datenbank in die SQLite Datenbank migrieren?"
			) == System.Windows.Forms.DialogResult.Yes) {
				this.lblState.Text = "Migration gestartet, lade Daten...";

				this.btnCancel.Enabled = true;

				this._importer = new SQLServerToSQLiteImporter();
				this._importer.PercentState += new ImportPercentEventHandler(_importer_PercentState);

				if(this._importer.Import()) {
					StaticWindows.InfoBox("Erfolgreich migriert!");
				}
				else {
					StaticWindows.ErrorBox("Während der Migration ist ein Fehler aufgetreten!");
				}
			}
			else {
				this.lblState.Text = "Migration abgebrochen!";

				this.btnOK.Enabled = true;
				this.btnCancel.Enabled = true;
			}
		}

		/// <summary>
		/// _importer_PercentState
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void _importer_PercentState(object sender, ImportStateEventArgs e) {
			int val = 0;

			int.TryParse(
				Math.Round(e.PercentValue, 0, MidpointRounding.ToEven).ToString(),
				out val
			);

			if(val >= this.pbMigrate.Minimum
			&& val <= this.pbMigrate.Maximum) {
				this.pbMigrate.Value = val;
				this.lblState.Text = e.Message;

				this.pbMigrate.Refresh();
				this.pbMigrate.Update();
			}

			if(e.PercentValue >= this.pbMigrate.Maximum) {
				this.lblState.Text = e.Message;
				this._importer.Dispose();
			}
		}
	}
}
