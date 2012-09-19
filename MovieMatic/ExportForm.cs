using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.IO;
using Toenda.Foundation.Utility;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;

namespace Toenda.MovieMatic {
	public partial class ExportForm : Form {
		List<Movie> _movies = new List<Movie>();
		DataGridViewColumnCollection _cols = new DataGridViewColumnCollection(null);

		private DataHandler _db = new DataHandler(
			Configuration.ConnectionString
		);
		private StaticHandler _sb = new StaticHandler(
			Configuration.ConnectionString
		);
		private ExcelExport _export = new ExcelExport();
		private string _filename = "";

		/// <summary>
		/// Initializes a new instance of the <see cref="ExportForm"/> class.
		/// </summary>
		/// <param name="movieList">The movie list.</param>
		/// <param name="cols">The cols.</param>
		public ExportForm(List<Movie> movieList, DataGridViewColumnCollection cols) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._movies = movieList;
			this._cols = cols;

			this._export.ExcelExportFinish += new ExcelExport.ExcelExportFinishEventhandler(_export_ExcelExportFinish);
			this._export.ExcelExportResponse += new ExcelExport.ExcelExportEventHandler(_export_ExcelExportResponse);

			this._InitControls();
		}

		/// <summary>
		/// Inits the controls.
		/// </summary>
		private void _InitControls() {
			// combobox
			this.cbExport.Items.Add("Microsoft Excel 2003/2007 (Excel Xml / *.xls)");
			//this.cbExport.Items.Add("OpenOffice.org 3.0 (OpenDocument)");
			this.cbExport.SelectedIndex = 0;

			// save file dialog
			StringBuilder str = new StringBuilder();
			str.Append("Microsoft Excel 2003 (.xls)|*.xls");
			str.Append("|Office Open XML (Microsoft Excel 2007) (.xlsx)|*.xlsx");
			//str.Append("|OpenDocument (.ods)|*.ods");

			this.sfd.OverwritePrompt = true;
			this.sfd.AddExtension = false;
			this.sfd.Filter = str.ToString();
			this.sfd.FileName = "Movies_" + DateTime.Now.ToFileNameString() + ".xls";
			this.sfd.InitialDirectory = Configuration.CommonApplicationTempPath;

			// textbox
			this.txtFilename.Text = "Movies_" + DateTime.Now.ToFileNameString() + ".xls";
			this.txtTargetPath.Text = Configuration.CommonApplicationTempPath;
			this.txtSheettitle.Text = "Filmliste vom " + DateTime.Now.ToShortDateString();

			// columns
			foreach(DataGridViewColumn col in this._cols) {
				if(col.Visible
				&& !col.HeaderText.Contains("liehen")) {
					CheckBox chk = new CheckBox();

					if(col.HeaderText.Contains("(")) {
						chk.Text = col.HeaderText.Substring(0, col.HeaderText.IndexOf("(")).Trim();
					}
					else {
						chk.Text = col.HeaderText;
					}

					if(col.HeaderText.Contains("No.")) {
						chk.Checked = true;
					}
					else if(col.HeaderText.Contains("Name")) {
						chk.Checked = true;
					}
					else if(col.HeaderText.Contains("Genre")) {
						chk.Checked = true;
					}
					else if(col.HeaderText.Contains("Codec")) {
						chk.Checked = true;
					}
					else if(col.HeaderText.Contains("Discs")) {
						chk.Checked = true;
					}

					this.colsPanel.Controls.Add(chk);
				}
			}

			this.colsPanel.Controls.Add(new CheckBox() { Text = "Darsteller", Checked = true });
			this.colsPanel.Controls.Add(new CheckBox() { Text = "Regisseur", Checked = true });
			this.colsPanel.Controls.Add(new CheckBox() { Text = "Produzent", Checked = true });

			this.pbState.Step = 1;
			this.pbState.Minimum = 0;
			this.pbState.Maximum = this._movies.Count;
			this.pbState.Value = 0;

			foreach(CheckBox chk in this.colsPanel.Controls) {
				this.pbState.Maximum += 1;
			}
		}

		/// <summary>
		/// Checks if a column is choosen.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		private bool _ColumnIsChoosen(string name) {
			foreach(CheckBox chk in this.colsPanel.Controls) {
				if(chk.Text.Contains(name)) {
					return chk.Checked;
				}
			}

			return false;
		}

		/// <summary>
		/// Exports the data to a table.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="targetPath">The target path.</param>
		/// <param name="sheetTitle">The sheet title.</param>
		private void _Export(string filename, string targetPath, string sheetTitle) {
			if(this._movies != null
			&& this._movies.Count > 0) {
				this.lblState.Text = "Sammle Informationen...";
				this.lblState.Refresh();
				this.lblState.Update();

				this.lblState.Text = "Sammle Informationen: Lade zusätzliche Daten...";
				this.lblState.Refresh();
				this.lblState.Update();

				this._movies = this._db.AddMovieAdditionals(this._movies);

				ExcelExport.Document doc = new ExcelExport.Document();

				doc.SheetTitle = sheetTitle;

				//if(this._ColumnIsChoosen("No.")) {
				//    doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Nummer", Width = 50 });
				//}

				//if(this._ColumnIsChoosen("Name")) {
				//    doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Name", Width = 250 });
				//}

				//if(this._ColumnIsChoosen("Genre")) {
				//    doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Genre", Width = 140 });
				//}

				//doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Codec", Width = 70 });
				//doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Anzahl CD's", Width = 40 });

				int count = 0;

				this.lblState.Text = "Sammle Informationen: Spalten hinzufügen...";
				this.lblState.Refresh();
				this.lblState.Update();

				foreach(CheckBox chk in this.colsPanel.Controls) {
					if(chk.Checked) {
						ExcelExport.TableColumn col = new ExcelExport.TableColumn();

						col.Value = chk.Text;
						col.Width = 40;

						if(chk.Text.Contains("Nr.")) {
							col.Value = "Nummer";
							col.Width = 50;
						}
						else if(chk.Text.Contains("Name")) {
							col.Width = 250;
						}
						else if(chk.Text.Contains("Genre")) {
							col.Width = 140;
						}
						else if(chk.Text.Contains("Codec")) {
							col.Width = 70;
						}
						else if(chk.Text.Contains("Discs")) {
							col.Value = "Anzahl CD's";
							col.Width = 40;
						}
						else if(chk.Text.Contains("Land")) {
							col.Width = 250;
						}
						else if(chk.Text.Contains("Kategorie")) {
							col.Width = 140;
						}
						else if(chk.Text.Contains("Qualität")) {
							col.Width = 50;
						}
						else if(chk.Text.Contains("Original")) {
							col.Width = 40;
						}
						else if(chk.Text.Contains("Darsteller")) {
							col.Width = 250;
						}
						else if(chk.Text.Contains("Regisseur")) {
							col.Width = 250;
						}
						else if(chk.Text.Contains("Produzent")) {
							col.Width = 250;
						}

						doc.Columns.Add(col);

						count++;
					}

					//this.pbState.PerformStep();
					this.pbState.Value += 1;
					this.pbState.Refresh();
					this.pbState.Update();

					this.lblState.Text = "Sammle Informationen: Spalte " + count.ToString() + " hinzugefügt...";
					this.lblState.Refresh();
					this.lblState.Update();
				}

				//doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Darsteller", Width = 250 });
				//doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Regisseur", Width = 250 });
				//doc.Columns.Add(new ExcelExport.TableColumn() { Value = "Produzent", Width = 250 });

				count = 1;

				this.lblState.Text = "Sammle Informationen: Filme hinzufügen...";
				this.lblState.Refresh();
				this.lblState.Update();

				foreach(Movie mov in this._movies) {
					//ExcelExportDataContainer obj = new ExcelExportDataContainer();

					//// columns
					//obj.Columns.Add("Nummer");
					//obj.Columns.Add("Name");
					//obj.Columns.Add("Genre");
					//obj.Columns.Add("Codec");
					//obj.Columns.Add("Anzahl CD's");
					////obj.Columns.Add("Country");
					//obj.Columns.Add("Actor");
					//obj.Columns.Add("Director");
					//obj.Columns.Add("Producer");

					//Static stc = this._sb.GetStaticItem("C002", mov.Country);

					// cells
					ExcelExport.DataContainer obj = new ExcelExport.DataContainer();

					foreach(CheckBox chk in this.colsPanel.Controls) {
						if(chk.Checked) {
							ExcelExport.TableCell cell = new ExcelExport.TableCell();
							
							cell.Type = "String";
							cell.Value = "";

							if(chk.Text.Contains("Nr.")) {
								cell.Value = mov.Number.ToString();
								cell.Type = "Number";
							}
							else if(chk.Text.Contains("Name")) {
								cell.Value = mov.Name;
							}
							else if(chk.Text.Contains("Genre")) {
								cell.Value = mov.GenerateGenresString;
							}
							else if(chk.Text.Contains("Codec")) {
								cell.Value = ( mov.Codec == Codec.Unknown ? "" : mov.Codec.ToString() );
							}
							else if(chk.Text.Contains("Discs")) {
								cell.Value = mov.DiscAmount.ToString();
								cell.Type = "Number";
							}
							else if(chk.Text.Contains("Original")) {
								cell.Value = mov.IsOriginal.ToString();
							}
							else if(chk.Text.Contains("Kategorie")) {
								cell.Value = mov.GenerateCategoriesString;
							}
							else if(chk.Text.Contains("Land")) {
								Static stc = this._sb.GetStaticItem("C002", mov.Country);
								cell.Value = stc.Content;
							}
							else if(chk.Text.Contains("Darsteller")) {
								cell.Value = mov.GenerateActorsString;
							}
							else if(chk.Text.Contains("Regisseur")) {
								cell.Value = mov.GenerateDirectorsString;
							}
							else if(chk.Text.Contains("Produzent")) {
								cell.Value = mov.GenerateProducersString;
							}

							obj.Cells.Add(cell);
						}
					}

					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.Number.ToString(), Type = "Number" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.Name, Type = "String" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.GenerateGenresString, Type = "String" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = ( mov.Codec == Codec.Unknown ? "" : mov.Codec.ToString() ), Type = "String" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.DiscAmount.ToString(), Type = "String" });

					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.ActorsString, Type = "String" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.DirectorsString, Type = "String" });
					//obj.Cells.Add(new ExcelExport.TableCell() { Value = mov.ProducersString, Type = "String" });

					//obj.Cells.Add(mov.Number.ToString());
					//obj.Cells.Add(mov.Name);
					//obj.Cells.Add(mov.GenerateGenresString);
					//obj.Cells.Add((mov.Codec == Codec.Unknown ? "" : mov.Codec.ToString()));
					//obj.Cells.Add(mov.DiscAmount.ToString());
					//obj.Cells.Add(stc.Content);
					//obj.Cells.Add(mov.ActorsString);
					//obj.Cells.Add(mov.DirectorsString);
					//obj.Cells.Add(mov.ProducersString);

					doc.Data.Add(obj);

					//this.pbState.PerformStep();
					this.pbState.Value += 1;
					this.pbState.Refresh();
					this.pbState.Update();

					this.lblState.Text = "Sammle Informationen: Film " + count.ToString() + " hinzugefügt...";
					this.lblState.Refresh();
					this.lblState.Update();

					count++;
				}

				this.pbState.Value = 0;
				this.pbState.Maximum = doc.ColumnAndCellCount;

				// save now
				this._filename = Path.GetFullPath(targetPath + "\\" + filename);

				this.lblState.Text = "Informationen geladen, starte nun Export...";

				this.lblState.Refresh();
				this.lblState.Update();

				this._export.ExportDocument(doc, this._filename, true);

				//try {
				//    AppLoader loader = new AppLoader();
				//    loader.LoadApp(this._filename);
				//}
				//catch(Exception ex) {
				//    StaticWindows.ErrorBox(ex.Message);
				//}
			}
		}

		/// <summary>
		/// Handles the Click event of the btnSaveFileName control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnSaveFileName_Click(object sender, EventArgs e) {
			if(this.sfd.ShowDialog(this) == DialogResult.OK) {
				this.txtTargetPath.Text = Path.GetDirectoryName(this.sfd.FileName);
				this.txtFilename.Text = Path.GetFileName(this.sfd.FileName);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnCancel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the btnOK control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnOK_Click(object sender, EventArgs e) {
			this.pbState.Value = 0;
			this.lblState.Text = "";

			this.lblState.Text = "Starte Export...";
			this.lblState.Refresh();
			this.lblState.Update();

			string filename = "Movies_" + DateTime.Now.ToFileNameString() + ".xls";
			string targetPath = Configuration.CommonApplicationTempPath;
			string sheetTitle = "Filmliste vom " + DateTime.Now.ToShortDateString();
			
			if(!this.txtFilename.Text.IsNullOrTrimmedEmpty()) {
				if(!PathExtensions.GetFullExtension(this.txtFilename.Text).ToLower().Contains(".xls")
				&& !PathExtensions.GetFullExtension(this.txtFilename.Text).ToLower().Contains(".xlsx")
				&& !PathExtensions.GetFullExtension(this.txtFilename.Text).ToLower().Contains(".ods")) {
					this.txtFilename.Text = this.txtFilename.Text + ".xls";
				}

				filename = this.txtFilename.Text;
			}

			if(!this.txtTargetPath.Text.IsNullOrTrimmedEmpty()) {
				bool thisTarget = false;

				if(!Directory.Exists(this.txtTargetPath.Text)) {
					if(StaticWindows.Requester(
						"Das angegebene Verzeichnis existiert nicht, wollen sie es nun anlegen?"
					) == DialogResult.Yes) {
						try {
							Directory.CreateDirectory(this.txtTargetPath.Text);

							thisTarget = true;
						}
						catch(Exception ex) {
							Exception ex2 = new Exception("Verzeichnis konnte nicht erstellt werden!", ex);

							StaticWindows.DisplayErrorMessagebox(ex2);
						}
					}
				}

				if(thisTarget) {
					targetPath = this.txtTargetPath.Text;
				}
			}

			if(!this.txtSheettitle.Text.IsNullOrTrimmedEmpty()) {
				sheetTitle = this.txtSheettitle.Text;
			}

			this.btnOK.Enabled = false;
			this.btnCancel.Enabled = false;

			this.txtFilename.Enabled = false;
			this.txtTargetPath.Enabled = false;
			this.txtSheettitle.Enabled = false;

			this.cbExport.Enabled = false;

			this.groupBox1.Refresh();
			this.groupBox1.Update();

			foreach(CheckBox chk in this.colsPanel.Controls) {
				chk.Enabled = false;
			}

			this.groupBox2.Refresh();
			this.groupBox2.Update();

			this._Export(filename, targetPath, sheetTitle);
		}

		/// <summary>
		/// _export_s the excel export response.
		/// </summary>
		/// <param name="currentColumnOrCellIndex">Index of the current column or cell.</param>
		/// <param name="currentRow">The current row.</param>
		/// <param name="text">The text.</param>
		void _export_ExcelExportResponse(int currentColumnOrCellIndex, int currentRow, string text) {
			this.pbState.Value = currentColumnOrCellIndex;

			this.lblState.Text = text;
			this.lblState.Refresh();
			this.lblState.Update();
		}

		/// <summary>
		/// _export_s the excel export finish.
		/// </summary>
		/// <param name="successfull">if set to <c>true</c> [successfull].</param>
		/// <param name="text">The text.</param>
		void _export_ExcelExportFinish(bool successfull, string text) {
			this.btnOK.Enabled = true;
			this.btnCancel.Enabled = true;

			this.txtFilename.Enabled = true;
			this.txtTargetPath.Enabled = true;
			this.txtSheettitle.Enabled = true;

			this.cbExport.Enabled = true;

			foreach(CheckBox chk in this.colsPanel.Controls) {
				chk.Enabled = true;
			}

			if(this._filename.Trim() != ""
			&& successfull) {
				this.pbState.Value = this.pbState.Maximum;
				this.lblState.Text = text;

				this.lblState.Refresh();
				this.lblState.Update();

				try {
					AppLoader loader = new AppLoader();
					loader.LoadApp(this._filename);
				}
				catch(Exception ex) {
					StaticWindows.ErrorBox(ex.Message);
				}

				this.pbState.Value = 0;
				this.lblState.Text = "";

				this.lblState.Refresh();
				this.lblState.Update();
			}
			else {
				StaticWindows.ErrorBox("Es trat ein unbekannter Fehler während des Exports auf!");
			}

			this.pbState.Maximum = this._movies.Count;

			foreach(CheckBox chk in this.colsPanel.Controls) {
				this.pbState.Maximum += 1;
			}
		}
	}
}
