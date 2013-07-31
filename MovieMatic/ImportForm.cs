using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Import;

namespace Toenda.MovieMatic {
	public partial class ImportForm : Form {
		private List<ColumnAllocation> _allocation = new List<ColumnAllocation>();
		private IImporter _importer;
		private bool _fileCanImported;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public ImportForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this.btnCheckFile.Enabled = false;
			this.btnImport.Enabled = false;

			this.cbImport.Items.Add("OpenDocument Tabelle");
			this.cbImport.Items.Add("Microsoft Office 2003 Tabelle");
			this.cbImport.SelectedIndex = 0;

			this.txtSheet.Text = "Tabelle1";

			this._fileCanImported = false;

			this.pbImport.Minimum = 0;
			this.pbImport.Maximum = 100;
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
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		/// <summary>
		/// _importer_PercentState
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _importer_PercentState(object sender, ImportStateEventArgs e) {
			int val = 0;
			
			int.TryParse(
				Math.Round(e.PercentValue, 0, MidpointRounding.ToEven).ToString(), 
				out val
			);

			if(val >= this.pbImport.Minimum
			&& val <= this.pbImport.Maximum) {
				this.pbImport.Value = val;
			}

			if(e.PercentValue >= this.pbImport.Maximum) {
				MessageBox.Show("Erfolgreich importiert!", "MovieMatic");
				this._importer.Dispose();
			}
		}

		/// <summary>
		/// btnCheckFile_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCheckFile_Click(object sender, EventArgs e) {
			if(this.txtFilename.Text != null
			&& this.txtFilename.Text.Trim() != ""
			&& this.txtSheet.Text != null
			&& this.txtSheet.Text.Trim() != "") {
				DataSet data;

				switch(this.cbImport.SelectedIndex) {
					case 0:
						if(File.Exists(this.txtFilename.Text.Trim())) {
							try {
								this._importer = (IImporter)new OpenDocumentImporter(
									Configuration.ConnectionString, 
									this.txtFilename.Text.Trim()
								);

								if(this._importer.LoadFile()) {
									this.btnImport.Enabled = true;
									this._fileCanImported = true;

									this._importer.PercentState += new ImportPercentEventHandler(_importer_PercentState);

									data = this._importer.GetPreview();

									DataSetPreviewForm dspf = new DataSetPreviewForm(data);

									if(dspf.ShowDialog(this) == DialogResult.OK) {
										this._allocation = dspf.ColumnAllocation;
									}
								}
								else {
									this.btnImport.Enabled = false;
									this._fileCanImported = false;
								}
							}
							catch(FileNotFoundException fileex) {
								ErrorHandler.DisplayErrorMessagebox(fileex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(ArgumentNullException nullex) {
								ErrorHandler.DisplayErrorMessagebox(nullex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(Exception ex) {
								ErrorHandler.DisplayErrorMessagebox(ex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
						}
						break;

					case 1:
						if(File.Exists(this.txtFilename.Text.Trim())) {
							try {
								this._importer = (IImporter)new Excel2003Importer(
									this.txtFilename.Text.Trim(), 
									this.txtSheet.Text.Trim()
								);

								if(this._importer.LoadFile()) {
									this.btnImport.Enabled = true;
									this._fileCanImported = true;

									this._importer.PercentState += new ImportPercentEventHandler(_importer_PercentState);

									data = this._importer.GetPreview();
								}
								else {
									this.btnImport.Enabled = false;
									this._fileCanImported = false;
								}
							}
							catch(FileNotFoundException fileex) {
								ErrorHandler.DisplayErrorMessagebox(fileex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(ArgumentNullException nullex) {
								ErrorHandler.DisplayErrorMessagebox(nullex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(Exception ex) {
								ErrorHandler.DisplayErrorMessagebox(ex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
						}
						break;

					default:
						if(File.Exists(this.txtFilename.Text.Trim())) {
							try {
								this._importer = (IImporter)new OpenDocumentImporter(
									Configuration.ConnectionString, 
									this.txtFilename.Text.Trim()
								);

								if(this._importer.LoadFile()) {
									this.btnImport.Enabled = true;
									this._fileCanImported = true;

									this._importer.PercentState += new ImportPercentEventHandler(_importer_PercentState);

									// Hier Fenster mit Datenvorschau
								}
								else {
									this.btnImport.Enabled = false;
									this._fileCanImported = false;
								}
							}
							catch(FileNotFoundException fileex) {
								ErrorHandler.DisplayErrorMessagebox(fileex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(ArgumentNullException nullex) {
								ErrorHandler.DisplayErrorMessagebox(nullex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
							catch(Exception ex) {
								ErrorHandler.DisplayErrorMessagebox(ex);

								this.btnImport.Enabled = false;
								this._fileCanImported = false;
							}
						}
						break;
				}
			}
		}

		/// <summary>
		/// btnImport_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnImport_Click(object sender, EventArgs e) {
			if(this._CheckImportSettings()) {
				try {
					if(!this._importer.Import(this._allocation)) {
						MessageBox.Show(
							"Während des Imports ist ein Fehler aufgetreten!", 
							"MovieMatic"
						);
					}
				}
				catch(Exception ex) {
					ErrorHandler.DisplayErrorMessagebox(ex);

					this.btnImport.Enabled = false;
					this._fileCanImported = false;
				}
			}
		}

		/// <summary>
		/// btnOpenFD_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenFD_Click(object sender, EventArgs e) {
			StringBuilder str = new StringBuilder();
			str.Append("All supported spreadsheets|*.ods;*.xls");
			str.Append("|OpenDocument (.ods)|*.ods");
			str.Append("|Microsoft Excel 2003 (.xls)|*.xls");

			this.ofdFile.FileName = "";
			this.ofdFile.Multiselect = false;
			this.ofdFile.Filter = str.ToString();

			if(this.ofdFile.ShowDialog() == DialogResult.OK) {
				string ext = System.IO.Path.GetExtension(this.ofdFile.FileName).ToLower();

				if(ext == ".ods"
				|| ext == ".xls") {
					this.txtFilename.Text = this.ofdFile.FileName;
					this.btnCheckFile.Enabled = true;
				}
				else {
					MessageBox.Show("Die gewählte Datei hat das falsche Format!", "MovieMatic");
				}
			}
			else {
				if(this.txtFilename.Text.Trim() == "") {
					this.btnCheckFile.Enabled = false;
					this.btnImport.Enabled = false;
				}
			}
		}

		/// <summary>
		/// txtFilename_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtFilename_Leave(object sender, EventArgs e) {
			if(this.txtFilename.Text.Trim() == "") {
				this.btnCheckFile.Enabled = false;
				this.btnImport.Enabled = false;
			}
		}

		/// <summary>
		/// txtFilename_TextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtFilename_TextChanged(object sender, EventArgs e) {
			if(this.txtFilename.Text.Trim() == "") {
				this.btnCheckFile.Enabled = false;
				this.btnImport.Enabled = false;
			}
		}

		/// <summary>
		/// btnHelp_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, EventArgs e) {
			MessageBox.Show(
				"Import Vorgaben:\n"
				+ "- Erste Zeile muss der Spaltenname sein.\n"
				+ "- Derzeit ist es nur möglich, eine Datei mit einer Tabelle zu importieren.", 
				"MovieMatic"
			);
		}

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Check all needed import settings
		/// </summary>
		/// <returns></returns>
		private bool _CheckImportSettings() {
			if(this.txtFilename.Text != null
			&& this.txtFilename.Text.Trim() != ""
			&& this.txtSheet.Text != null
			&& this.txtSheet.Text.Trim() != ""
			&& File.Exists(this.txtFilename.Text.Trim())
			&& this._importer != null
			&& this._fileCanImported) {
				return true;
			}
			else {
				return false;
			}
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------
	}
}