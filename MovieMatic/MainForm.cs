using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Toenda.Foundation;
using Toenda.Foundation.Windows.Forms;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class MainForm
	/// </summary>
	public partial class MainForm : Form {//, IMessageFilter {
		/* Struktur, die bestimmt, um wie viele Pixel der Rahmen 
		zu jeder Seite verbreitert wird */
		[StructLayout(LayoutKind.Sequential)]
		public struct MARGINS {
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}

		/* Verbreitert den Vista-Rahmen nach innen */
		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd,
		   ref MARGINS margins);

		/* Überprüft, ob die Desktopgestaltung aktiviert ist */
		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern bool DwmIsCompositionEnabled();

		//private VirtualConsole _console;

		private DataHandler _db = new DataHandler(Configuration.ConnectionString);
		private StaticHandler _sb = new StaticHandler(Configuration.ConnectionString);

		private List<Movie> _movies;
		private int _current_amount;

		private bool _close_app = false;
		private bool _search = false;
		private bool _initial = false;
		private bool _deleting = false;

		private bool __useDataBind = true;

		delegate void LoadDataCallback(FilterType filter, string value);

		private ListSortDirection _sortDirection = ListSortDirection.Ascending;
		private string _sortExpression = "sort_value";
		private FilterType _searchFilter = FilterType.NoFilter;
		private string _searchValue = "";

		/// <summary>
		/// Default Ctor
		/// </summary>
		public MainForm() {
			InitializeComponent();
			//Application.AddMessageFilter(this);

			this.toolStrip.Renderer = new WindowsVistaRenderer();

			// check the for initial Xml configuration
			if(Configuration.ConnectionString == "Server=;Database=MovieMatic;Uid=;Pwd=;") {
				this._initial = true;

				StringBuilder str = new StringBuilder();
				str.Append("MovieMatic wurde neu installiert oder die Konfigurationsdatei wurde gelöscht!\n");
				str.Append("\nWollen sie die Datenbank-Zugangsdaten nun anpassen?\n");

				if(MessageBox.Show(
					str.ToString(),
					"MovieMatic",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question) == DialogResult.Yes) {
					try {
						this._OpenOptionDialogAndTryDBConnect();
					}
					catch(Exception ex) {
						StringBuilder str2 = new StringBuilder();
						str2.Append("Fehler beim Aufbau einer Verbindung zur Datenbank!\n");

						if(ex.Message != null) {
							str2.Append("Fehlerbeschreibung:\n" + ex.Message + "\n");
						}

						if(ex.InnerException != null) {
							if(ex.InnerException.Message != null) {
								str2.Append("Weitergereichte Fehlerbeschreibung:\n");
								str2.Append(ex.InnerException.Message + "\n");
							}
						}

						str2.Append("\nWollen sie die Datenbank Einstellungen verändern?\n");

						if(MessageBox.Show(
							str2.ToString(),
							"MovieMatic ERROR",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Error) == DialogResult.Yes) {
							this._OpenOptionDialogAndTryDBConnect();
						}
						else {
							MessageBox.Show(
								"MovieMativ wird aufgrund eines Fehlers beendet!",
								"MovieMatic ERROR"
							);

							this._close_app = true;
						}
					}
				}
				else {
					MessageBox.Show(
						"MovieMativ konnte nicht auf die Datenbank zugreifen und wird beendet!",
						"MovieMatic ERROR"
					);

					this._close_app = true;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is glass frame enabled.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is glass frame enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsGlassFrameEnabled {
			get {
				try {
					return DwmIsCompositionEnabled();
				}
				catch(DllNotFoundException) {
					// Die DLL dwmapi.dll ist nicht verfügbar. 
					// Wahrscheinlich läuft die Anwendung unter
					// einer älteren Windows-Version
					return false;
				}
			}
		}

		/// <summary>
		/// Handles the RunWorkerCompleted event of the loadDataAsync control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
		private void loadDataAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			this._LoadData(this._searchFilter, this._searchValue);
		}

		/// <summary>
		/// MainForm_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e) {
			if(this._close_app) {
				this.Close();
			}
		}

		/// <summary>
		/// Handles the Shown event of the MainForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainForm_Shown(object sender, EventArgs e) {
			//this._console = new VirtualConsole(this);
			//this._console.SetVisibility(false);

			Stopwatch watch = Stopwatch.StartNew();

			watch.Start();

			// bei erstem start nach installation
			// hier fenster öffnen und datenbank
			// einfügen sowie die zugangsdaten
			// eingeben lassen

			if(!Directory.Exists(Configuration.CommonApplicationDataPath)) {
				Directory.CreateDirectory(Configuration.CommonApplicationDataPath);
			}

			if(!Directory.Exists(Configuration.CommonApplicationTempPath)) {
				Directory.CreateDirectory(Configuration.CommonApplicationTempPath);
			}

			watch.Stop();
			TimeSpan span = watch.Elapsed;

			Console.WriteLine(
				"MainForm - Required time: {0} - {1}ms ({2} Ticks)",
				span.ToString(),
				watch.ElapsedMilliseconds,
				watch.ElapsedTicks
			);

			watch = Stopwatch.StartNew();

			this._search = true;

			if(this._close_app) {
				this.Close();
			}

			// load data
			this._InitControls();
			this._LoadData(FilterType.NoFilter);

			watch.Stop();
			span = watch.Elapsed;

			Console.WriteLine(
				"MainForm checks - Required time: {0} - {1}ms ({2} Ticks)",
				span.ToString(),
				watch.ElapsedMilliseconds,
				watch.ElapsedTicks
			);
		}

		/// <summary>
		/// Handles the KeyUp event of the MainForm control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_KeyUp(object sender, KeyEventArgs e) {
			
		}

		/// <summary>
		/// Handles the KeyDown event of the MainForm control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_KeyDown(object sender, KeyEventArgs e) {
			if(e.Control
			&& e.KeyCode == Keys.F) {
				this.txtSearchBar.SelectAll();
				this.txtSearchBar.Focus();
			}
		}

		private void checkNewerVersionToolStripMenuItem_Click(object sender, EventArgs e) {
			Cursor c = this.Cursor;
			this.Cursor = Cursors.WaitCursor;

			VersionChecker vc = new VersionChecker(AssemblyInfo.Version);

			if(vc.CheckForNewVersion()) {
				//vc.LoadNewVersion();
				//if(StaticWindows.Requester(
				//    string.Format(
				//        "Es gibt eine neue Version von MovieMatic (Version {0}).",
				//        vc.NewVersion
				//    )
				//) == DialogResult.No) {
				//}
				//else {
				//}

				StaticWindows.InfoBox(
					string.Format(
						"Es gibt eine neue Version von MovieMatic.\n"
						+ "({0})\n\n"
						+ "Sie können sie sich unter http://www.toenda.com/moviematic herunterladen.",
						vc.NewVersionString
					)
				);
			}
			else {
				StaticWindows.InfoBox(
					string.Format(
						"Die installierte Version ist aktuell.",
						vc.NewVersionString
					)
				);
			}

			this.Cursor = c;
		}

		private void checkDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
			try {
			    // check database
			    try {
			        //Stopwatch watchDBCheck = new Stopwatch();

			        //watchDBCheck.Start();

			        this._db.CheckDatabaseVersion();

			        //watchDBCheck.Stop();

			        //TimeSpan spanDBCheck = watch.Elapsed;

			        //Console.WriteLine(
			        //    "CheckDatabaseVersion - Required time: {0} - {1}ms ({2} Ticks)",
			        //    spanDBCheck.ToString(),
			        //    watchDBCheck.ElapsedMilliseconds,
			        //    watchDBCheck.ElapsedTicks
			        //);
			    }
			    catch(Exception ex) {
			        StaticWindows.DisplayErrorMessagebox(ex);
			    }

			    // load data
			    this._InitControls();
			    this._LoadData(FilterType.NoFilter);
			}
			catch(Exception ex) {
			    StringBuilder str = new StringBuilder();
			    str.Append("Fehler beim Aufbau einer Verbindung zur Datenbank!\n");

			    if(ex.Message != null) {
			        str.Append("Fehlerbeschreibung:\n" + ex.Message + "\n");
			    }

			    if(ex.InnerException != null) {
			        if(ex.InnerException.Message != null) {
			            str.Append("Weitergereichte Fehlerbeschreibung:\n");
			            str.Append(ex.InnerException.Message + "\n");
			        }
			    }

			    str.Append("\nWollen sie die Datenbank Einstellungen verändern?\n");

			    if(MessageBox.Show(
			        str.ToString(),
			        "MovieMatic ERROR",
			        MessageBoxButtons.YesNo,
			        MessageBoxIcon.Error) == DialogResult.Yes) {
			        this._OpenOptionDialogAndTryDBConnect();
			    }
			    else {
			        MessageBox.Show(
			            "MovieMativ wird aufgrund eines Fehlers beendet!",
			            "MovieMatic ERROR"
			        );

			        this._close_app = true;
			    }
			}
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e) {
			MessageBox.Show(
				//"Tastaturkürzel:"
				//+ "\n"
				//+ "\n[ALT]: Wechselt zur Filmliste"
				//+ "\n"
				"Suchparameter:"
				+ "\n"
				+ "\nSuchanfragen beginnend mit folgenden Parametern suchen:"
				+ "\nA: nach Schauspielern (A = ACTOR)."
				+ "\nD: nach Regisseuren (D = DIRECTOR)."
				+ "\nP: nach Produzenten (P = PRODUCER)."
				+ "\nM: nach Musikern (M = MUSICIAN)."
				+ "\nCA: nach Kameramännern (CA = CAMERAMAN)."
				+ "\nCU: nach Cuttern (Filmschnitt) (CU = CUTTER)."
				+ "\nW: nach (Drehbuch-)Autoren (W = WRITER/AUTHOR)."
				+ "\nC: nach Ländern (C = COUNTRY).",
				"MovieMatic"
			);
		}

		private void infoToolStripMenuItem_Click(object sender, EventArgs e) {
			AboutForm af = new AboutForm();

			if(af.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the ButtonClick event of the tsbtnOrganize control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void tsbtnOrganize_ButtonClick(object sender, EventArgs e) {
			Point pos = this.e.DropDown.PointToScreen(this.e.DropDown.Location);

			int x = pos.X - 115;
			int y = pos.Y + this.e.Size.Height - 2;

			this.e.DropDown.Show(x, y);
		}

		/// <summary>
		/// dgvMovies_CellDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			if(e.RowIndex >= 0 
			&& e.RowIndex < this._current_amount) {
				this._OpenMovieEditor(this.dgvMovies.Rows[e.RowIndex].Cells[0].Value.ToString());
			}
			else if(e.RowIndex >= 0) {
				this._OpenMovieEditor();
			}
		}

		/// <summary>
		/// editCmsItem_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editCmsItem_Click(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvMovies.SelectedRows;

			if(dgvsrc != null && dgvsrc.Count > 0) {
				if(dgvsrc[0].Index >= 0
				&& dgvsrc[0].Index < this._current_amount) {
					this._OpenMovieEditor(this.dgvMovies.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
				}
				else if(dgvsrc[0].Index >= 0) {
					this._OpenMovieEditor();
				}
			}
		}

		/// <summary>
		/// dgvMovies_SelectionChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMovies_SelectionChanged(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvMovies.SelectedRows;

			if(dgvsrc != null && dgvsrc.Count > 0) {
				if(dgvsrc[0].Index >= 0
				&& dgvsrc[0].Index >= this._current_amount) {
					this.editCmsItem.Text = "Neu...";
				}
				else {
					this.editCmsItem.Text = "Bearbeiten...";
				}
			}
		}

		/// <summary>
		/// cbFilterBar_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbFilterBar_SelectedIndexChanged(object sender, EventArgs e) {
			this._ApplySpecialFilter();
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the cbGenreBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void cbGenreBar_SelectedIndexChanged(object sender, EventArgs e) {
			this._ApplyGenreFilter();
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the cbCategoryBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void cbCategoryBar_SelectedIndexChanged(object sender, EventArgs e) {
			this._ApplyCategoryFiler();
		}

		/// <summary>
		/// dgvMovies_UserDeletingRow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMovies_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
			if(MessageBox.Show(
				"Wollen sie den Eintrag wirklich löschen?",
				"MovieMatic",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			) == DialogResult.No) {
				e.Cancel = true;
			}
			else {
				this._db.DeleteMovie(
					e.Row.Cells[0].Value.ToString()
				);

				//this.dgvMovies.Rows.Remove(e.Row);

				//this._LoadData(FilterType.NoFilter);

				this._UpdateStateBar(true, 1);
			}
		}

		/// <summary>
		/// Handles the KeyDown event of the dgvMovies control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void dgvMovies_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Return) {
				if(!this._deleting) {
					DataGridViewSelectedRowCollection dgvsrc = this.dgvMovies.SelectedRows;

					if(dgvsrc != null && dgvsrc.Count > 0) {
						this._OpenMovieEditor(this.dgvMovies.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
					}
					else {
						this._OpenMovieEditor();
					}
				}
			}
			else if(e.Control
			&& e.KeyCode == Keys.F) {
				this.txtSearchBar.SelectAll();
				this.txtSearchBar.Focus();
			}
			else if(e.KeyCode == Keys.Delete) {
				DataGridViewSelectedRowCollection dgvsrc = this.dgvMovies.SelectedRows;

				if(dgvsrc != null && dgvsrc.Count > 0) {
					if(MessageBox.Show(
						"Wollen sie den Eintrag wirklich löschen?",
						"MovieMatic",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					) == DialogResult.Yes) {
						this._db.DeleteMovie(this.dgvMovies.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
						//this._LoadData(FilterType.NoFilter);
						this._StartSearch();
					}
				}
			}
		}

		/// <summary>
		/// deleteCmsItem_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteCmsItem_Click(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvMovies.SelectedRows;

			if(dgvsrc != null && dgvsrc.Count > 0) {
				if(MessageBox.Show(
					"Wollen sie den Eintrag wirklich löschen?",
					"MovieMatic",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				) == DialogResult.Yes) {
					this._db.DeleteMovie(this.dgvMovies.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
					this._LoadData(FilterType.NoFilter);
				}
			}
		}

		/// <summary>
		/// dgvMovies_CellEndEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMovies_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			Movie mov;

			if(e.RowIndex <= this._current_amount) {
				mov = this._movies[e.RowIndex];

				mov.Name = this.dgvMovies.Rows[e.RowIndex].Cells[2].Value.ToString();
				mov.Number = this.dgvMovies.Rows[e.RowIndex].Cells[1].Value.ToString().ToInt32();
				mov.DiscAmount = this.dgvMovies.Rows[e.RowIndex].Cells[3].Value.ToString().ToInt32();
				mov.Codec = CodecHelper.GetCodecByString(this.dgvMovies.Rows[e.RowIndex].Cells[5].Value.ToString());
				mov.IsConferred = this.dgvMovies.Rows[e.RowIndex].Cells[8].Value.ToString().ToBoolean();
				mov.ConferredTo = this.dgvMovies.Rows[e.RowIndex].Cells[9].Value.ToString();

				this._db.SaveMovie(mov, SaveMethod.SaveChanges);
			}
			else {
				mov = new Movie();

				mov.Name = this.dgvMovies.Rows[e.RowIndex].Cells[2].Value.ToString();
				mov.Number = this.dgvMovies.Rows[e.RowIndex].Cells[1].Value.ToString().ToInt32();
				mov.DiscAmount = this.dgvMovies.Rows[e.RowIndex].Cells[3].Value.ToString().ToInt32();
				mov.Codec = CodecHelper.GetCodecByString(this.dgvMovies.Rows[e.RowIndex].Cells[5].Value.ToString());
				mov.IsConferred = this.dgvMovies.Rows[e.RowIndex].Cells[8].Value.ToString().ToBoolean();
				mov.ConferredTo = this.dgvMovies.Rows[e.RowIndex].Cells[9].Value.ToString();
				mov.Year = 0;

				this._db.SaveMovie(mov, SaveMethod.CreateNew);
			}

			//this._LoadData(FilterType.NoFilter);
		}

		/// <summary>
		/// Handles the Click event of the subNewMovieToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subNewMovieToolStripMenuItem_Click(object sender, EventArgs e) {
			this._OpenMovieEditor();
		}

		/// <summary>
		/// Handles the Click event of the subNewPersonToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subNewPersonToolStripMenuItem_Click(object sender, EventArgs e) {
			AdministerPersonsForm apf = new AdministerPersonsForm(true);
			DialogResult dr = apf.ShowDialog(this);

			if(dr == DialogResult.OK) {
			}
			else if(dr == DialogResult.Abort) {
				this.txtSearchBar.Text = ( apf.SelectedPersonType == FilterType.Actor 
					? "A" 
					: ( apf.SelectedPersonType == FilterType.Director 
						? "D" 
						: "P" 
					)
				);
				this.txtSearchBar.Text += ":";
				this.txtSearchBar.Text += apf.SelectedPerson;
				this._LoadData(apf.SelectedPersonType, apf.SelectedPerson);
			}
		}

		/// <summary>
		/// Handles the Click event of the subPersonsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subPersonsToolStripMenuItem_Click(object sender, EventArgs e) {
			AdministerPersonsForm apf = new AdministerPersonsForm(false);
			DialogResult dr = apf.ShowDialog(this);

			if(dr == DialogResult.OK) {
			}
			else if(dr == DialogResult.Abort) {
				this.txtSearchBar.Text = ( apf.SelectedPersonType == FilterType.Actor
					? "A"
					: ( apf.SelectedPersonType == FilterType.Director
						? "D"
						: "P"
					)
				);
				this.txtSearchBar.Text += ":";
				this.txtSearchBar.Text += apf.SelectedPerson;
				this._LoadData(apf.SelectedPersonType, apf.SelectedPerson);
			}
		}

		/// <summary>
		/// Handles the Click event of the subGenresToolStripMenuItem1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subGenresToolStripMenuItem1_Click(object sender, EventArgs e) {
			AdministerGenresForm agf = new AdministerGenresForm();

			if(agf.ShowDialog(this) == DialogResult.OK) {
				this._InitControls();
			}
		}

		/// <summary>
		/// Handles the Click event of the subCategoriesToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subCategoriesToolStripMenuItem_Click(object sender, EventArgs e) {
			AdministerCategoriesForm acf = new AdministerCategoriesForm();

			if(acf.ShowDialog(this) == DialogResult.OK) {
				this._InitControls();
			}
		}

		/// <summary>
		/// Handles the Click event of the subOptionsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subOptionsToolStripMenuItem_Click(object sender, EventArgs e) {
			this._OpenOptionDialogAndTryDBConnect();
		}

		/// <summary>
		/// Handles the Click event of the subImportToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subImportToolStripMenuItem_Click(object sender, EventArgs e) {
			ImportForm im = new ImportForm();

			if(im.ShowDialog(this) == DialogResult.OK) {
				this._LoadData(FilterType.NoFilter);
			}
		}

		/// <summary>
		/// Handles the Click event of the subExportToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subExportToolStripMenuItem_Click(object sender, EventArgs e) {
			ExportForm ef = new ExportForm(
				this._movies, 
				this.dgvMovies.Columns
			);

			if(ef.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the Click event of the subExitToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void subExitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		/// <summary>
		/// Handles the Click event of the tsbtnHelp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void tsbtnHelp_Click(object sender, EventArgs e) {
			MessageBox.Show(
				//"Tastaturkürzel:"
				//+ "\n"
				//+ "\n[ALT]: Wechselt zur Filmliste"
				//+ "\n"
				"Suchparameter:"
				+ "\n"
				+ "\nSuchanfragen beginnend mit folgenden Parametern suchen:"
				+ "\nA: nach Schauspielern (A = ACTOR)."
				+ "\nD: nach Regisseuren (D = DIRECTOR)."
				+ "\nP: nach Produzenten (P = PRODUCER)."
				+ "\nM: nach Musikern (M = MUSICIAN)."
				+ "\nCA: nach Kameramännern (CA = CAMERAMAN)."
				+ "\nCU: nach Cuttern (Filmschnitt) (CU = CUTTER)."
				+ "\nW: nach (Drehbuch-)Autoren (W = WRITER/AUTHOR)."
				+ "\nC: nach Ländern (C = COUNTRY).",
				"MovieMatic"
			);
		}

		/// <summary>
		/// Handles the Click event of the tsbtnInfo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void tsbtnInfo_Click(object sender, EventArgs e) {
			AboutForm af = new AboutForm();

			if(af.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the TextChanged event of the txtSearchBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void txtSearchBar_TextChanged(object sender, EventArgs e) {
			//
		}

		/// <summary>
		/// Handles the KeyUp event of the txtSearchBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void txtSearchBar_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 13
			&& this.txtSearchBar.Text.Trim().Length > 1) {
				this._StartSearch();
			}
			else if(e.KeyValue == 13
			&& this.txtSearchBar.Text.Trim().Length == 0) {
				this._LoadData(FilterType.NoFilter);
			}
			//else if(e.Alt) {
			//    this.dgvMovies.Focus();
			//}
		}

		/// <summary>
		/// Handles the DataError event of the dgvMovies control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
		private void dgvMovies_DataError(object sender, DataGridViewDataErrorEventArgs e) {
			//
		}

		/// <summary>
		/// Handles the DataBindingComplete event of the dgvMovies control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
		private void dgvMovies_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
			try {
				this.dgvMovies.FirstDisplayedScrollingRowIndex = this._current_amount - 1;
				this.dgvMovies.Rows[this._current_amount - 1].Selected = true;
			}
			catch(Exception) {
			}
		}

		/// <summary>
		/// Handles the RowContextMenuStripNeeded event of the dgvMovies control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs"/> instance containing the event data.</param>
		private void dgvMovies_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e) {
			this.dgvMovies.Rows[e.RowIndex].Selected = true;
		}

		/// <summary>
		/// Handles the ColumnHeaderMouseClick event of the dgvMovies control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
		private void dgvMovies_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			switch(this.dgvMovies.Columns[e.ColumnIndex].DataPropertyName) {
				case "SortValue":
					//if(this._sortDirection == ListSortDirection.Descending) {
					//    this._sortDirection = ListSortDirection.Ascending;
					//}
					//else {
					//    this._sortDirection = ListSortDirection.Descending;
					//}

					//this._sortExpression = "sort_value";

					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams("sort_value");
					this._StartSearch();
					break;

				case "Number":
					//if(this._sortDirection == ListSortDirection.Descending) {
					//    this._sortDirection = ListSortDirection.Ascending;
					//}
					//else {
					//    this._sortDirection = ListSortDirection.Descending;
					//}

					//this._sortExpression = "number";

					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams("number");
					this._StartSearch();
					break;

				case "Name":
					//if(this._sortDirection == ListSortDirection.Descending) {
					//    this._sortDirection = ListSortDirection.Ascending;
					//}
					//else {
					//    this._sortDirection = ListSortDirection.Descending;
					//}

					//this._sortExpression = "name";

					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams("name");
					this._StartSearch();
					break;

				default:
					this._ResetSortSymbols();
					break;
			}
		}

		/// <summary>
		/// Handles the Enter event of the txtSearchBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void txtSearchBar_Enter(object sender, EventArgs e) {
			//if(this.txtSearchBar.Text.IsNullOrTrimmedEmpty()
			//|| this.txtSearchBar.Text == "Suche") {
			//    this.txtSearchBar.BackColor = System.Drawing.SystemColors.Control;
			//    this.txtSearchBar.Font = new Font(
			//        "Microsoft Sans Serif",
			//        9F,
			//        System.Drawing.FontStyle.Regular,
			//        System.Drawing.GraphicsUnit.Point,
			//        ( (byte)( 0 ) )
			//    );
			//    this.txtSearchBar.ForeColor = System.Drawing.SystemColors.WindowText;
			//    this.txtSearchBar.Text = "";
			//}
			//else {
			//    this.txtSearchBar.BackColor = System.Drawing.SystemColors.ControlLight;
			//    this.txtSearchBar.Font = new Font(
			//        "Microsoft Sans Serif",
			//        9F,
			//        System.Drawing.FontStyle.Italic,
			//        System.Drawing.GraphicsUnit.Point,
			//        ( (byte)( 0 ) )
			//    );
			//    this.txtSearchBar.ForeColor = System.Drawing.SystemColors.GrayText;
			//    this.txtSearchBar.Text = "Suche";
			//}
		}

		/// <summary>
		/// Handles the Leave event of the txtSearchBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void txtSearchBar_Leave(object sender, EventArgs e) {
			//if(this.txtSearchBar.Text.IsNullOrTrimmedEmpty()
			//|| this.txtSearchBar.Text == "Suche") {
			//    this.txtSearchBar.BackColor = System.Drawing.SystemColors.ControlLight;
			//    this.txtSearchBar.Font = new Font(
			//        "Microsoft Sans Serif",
			//        9F,
			//        System.Drawing.FontStyle.Italic,
			//        System.Drawing.GraphicsUnit.Point,
			//        ( (byte)( 0 ) )
			//    );
			//    this.txtSearchBar.ForeColor = System.Drawing.SystemColors.GrayText;
			//    this.txtSearchBar.Text = "Suche";
			//}
			//else {
			//    this.txtSearchBar.BackColor = System.Drawing.SystemColors.Control;
			//    this.txtSearchBar.Font = new Font(
			//        "Microsoft Sans Serif",
			//        9F,
			//        System.Drawing.FontStyle.Regular,
			//        System.Drawing.GraphicsUnit.Point,
			//        ( (byte)( 0 ) )
			//    );
			//    this.txtSearchBar.ForeColor = System.Drawing.SystemColors.WindowText;
			//    this.txtSearchBar.Text = "";
			//}
		}

		/// <summary>
		/// Handles the Click event of the toolStripButton1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void toolStripButton1_Click(object sender, EventArgs e) {
			this.cbFilterBar.SelectedIndex = 0;
			this.cbGenreBar.SelectedIndex = 0;
			this.cbCategoryBar.SelectedIndex = 0;
			this.txtSearchBar.Text = "";

			this._sortDirection = ListSortDirection.Ascending;
			this._sortExpression = "sort_value";

			this._LoadData(FilterType.NoFilter);
		}

		/// <summary>
		/// Handles the Click event of the dbBackupToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void dbBackupToolStripMenuItem_Click(object sender, EventArgs e) {
			DatabaseBackupForm form = new DatabaseBackupForm();

			if(form.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the Click event of the dbRestoreToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void dbRestoreToolStripMenuItem_Click(object sender, EventArgs e) {
			DatabaseRestoreForm form = new DatabaseRestoreForm();

			if(form.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the Click event of the cleanupDBToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void cleanupDBToolStripMenuItem_Click(object sender, EventArgs e) {
			try {
				if(this._db.CleanDatabase()) {
					StaticWindows.InfoBox("Erfolgreich bereinigt...");
				}
				else {
					StaticWindows.ErrorBox("Fehler beim bereinigen der Datenbank!");
				}
			}
			catch(Exception ex) {
				StaticWindows.DisplayErrorMessagebox(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the moviesWithoutAddedPersonsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithoutAddedPersonsToolStripMenuItem_Click(object sender, EventArgs e) {
			EmptyPersonOnMovieForm form = new EmptyPersonOnMovieForm();

			if(form.ShowDialog(this) == DialogResult.OK) {
			}
		}

		/// <summary>
		/// Handles the Click event of the nonUsedIdsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void nonUsedIdsToolStripMenuItem_Click(object sender, EventArgs e) {
			NonUsedIdsForm form = new NonUsedIdsForm();
			form.Show();
			//if(form.ShowDialog(this) == DialogResult.OK) {
			//}
		}

		/// <summary>
		/// Handles the Click event of the newFromWikipediaToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void newFromWikipediaToolStripMenuItem_Click(object sender, EventArgs e) {
			this._OpenDialog(new FromWikipediaForm());
		}

		/// <summary>
		/// Handles the Click event of the importMSSQLToSQLiteToolStripMenuItem control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importMSSQLToSQLiteToolStripMenuItem_Click(object sender, EventArgs e) {
			this._OpenDialog(new MigrateMSSQLToSQLiteForm());
		}

		/// <summary>
		/// Applies the special filter.
		/// </summary>
		private void _ApplySpecialFilter() {
			if(this._search) {
				string tmp = "";

				this._search = false;
				this.cbGenreBar.SelectedIndex = 0;
				this.cbCategoryBar.SelectedIndex = 0;
				this._search = true;

				switch(this.cbFilterBar.SelectedIndex) {
					case 0:
						this.cbFilterBar.Enabled = false;
						this.txtSearchBar.Text = "";
						this._LoadData(FilterType.NoFilter);
						this.cbFilterBar.Enabled = true;
						break;

					case 1:
						this.cbFilterBar.Enabled = false;
						this.txtSearchBar.Text = "";
						this._LoadData(FilterType.NoFilter);
						this.cbFilterBar.Enabled = true;
						break;

					case 2:
						this.cbFilterBar.Enabled = false;
						this.txtSearchBar.Text = "";
						this._LoadData(FilterType.AllConferred);
						this.cbFilterBar.Enabled = true;
						break;

					case 3:
						this.cbFilterBar.Enabled = false;
						this.txtSearchBar.Text = "";
						this._LoadData(FilterType.AllOriginals);
						this.cbFilterBar.Enabled = true;
						break;

					case 4: // line
						//this.cbFilterBar.SelectedIndex = 0;
						break;

					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 14:
					case 15: // codecs
						this.cbFilterBar.Enabled = false;
						this.txtSearchBar.Text = "";
						tmp = this.cbFilterBar.SelectedItem.ToString()
							.Replace("Codec", "").Replace("anzeigen", "").Trim();

						//this._LoadData(
						//    FilterType.Codec,
						//    tmp
						//);

						this._searchFilter = FilterType.Codec;
						this._searchValue = tmp;

						//this.loadDataAsync.RunWorkerAsync();
						this._LoadData(this._searchFilter, this._searchValue);

						this.cbFilterBar.Enabled = true;
						break;

					default:
						break;
				}
			}
		}

		/// <summary>
		/// Applies the genre filter.
		/// </summary>
		private void _ApplyGenreFilter() {
			if(this._search) {
				try {
					this._search = false;
					this.cbFilterBar.SelectedIndex = 0;
					this.cbCategoryBar.SelectedIndex = 0;
					this._search = true;

					switch(this.cbGenreBar.SelectedIndex) {
						case 0:
							this.cbGenreBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.NoFilter);
							this.cbGenreBar.Enabled = true;
							break;

						case 1:
							this.cbGenreBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.NoFilter);
							this.cbGenreBar.Enabled = true;
							break;

						case 2: // without genre
							this.cbGenreBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.WithoutGenre);
							this.cbGenreBar.Enabled = true;
							break;

						case 3: // line
							break;

						default: // genres
							this.cbGenreBar.Enabled = false;
							this.txtSearchBar.Text = "";

							//this._LoadData(
							//    FilterType.Genre,
							//    ( (Genre)this.cbGenreBar.SelectedItem ).ID
							//);

							this._searchFilter = FilterType.Genre;
							this._searchValue = ( (Genre)this.cbGenreBar.SelectedItem ).ID;

							//this.loadDataAsync.RunWorkerAsync();
							this._LoadData(this._searchFilter, this._searchValue);

							this.cbGenreBar.Enabled = true;
							break;
					}
				}
				catch(Exception ex) {
					this.cbGenreBar.Enabled = true;
					StaticWindows.DisplayErrorMessagebox(ex);
				}
			}
		}

		/// <summary>
		/// Applies the category filer.
		/// </summary>
		private void _ApplyCategoryFiler() {
			if(this._search) {
				try {
					this._search = false;
					this.cbFilterBar.SelectedIndex = 0;
					this.cbGenreBar.SelectedIndex = 0;
					this._search = true;

					switch(this.cbCategoryBar.SelectedIndex) {
						case 0:
							this.cbCategoryBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.NoFilter);
							this.cbCategoryBar.Enabled = true;
							break;

						case 1:
							this.cbCategoryBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.NoFilter);
							this.cbCategoryBar.Enabled = true;
							break;

						case 2: // without genre
							this.cbCategoryBar.Enabled = false;
							this.txtSearchBar.Text = "";
							this._LoadData(FilterType.WithoutCategory);
							this.cbCategoryBar.Enabled = true;
							break;

						case 3: // line
							break;

						default: // categories
							this.cbCategoryBar.Enabled = false;
							this.txtSearchBar.Text = "";

							//this._LoadData(
							//    FilterType.Category,
							//    ( (Category)this.cbCategoryBar.SelectedItem ).ID
							//);

							this._searchFilter = FilterType.Category;
							this._searchValue = ( (Category)this.cbCategoryBar.SelectedItem ).ID;

							//this.loadDataAsync.RunWorkerAsync();
							this._LoadData(this._searchFilter, this._searchValue);

							this.cbCategoryBar.Enabled = true;
							break;
					}
				}
				catch(Exception ex) {
					this.cbCategoryBar.Enabled = true;
					StaticWindows.DisplayErrorMessagebox(ex);
				}
			}
		}

		/// <summary>
		/// Sets the sort symbol for the header of a column.
		/// </summary>
		/// <param name="columnIndex">Index of the column.</param>
		private void _SetSortSymbol(int columnIndex) {
			this._ResetSortSymbols();

			DataGridViewColumn col = this.dgvMovies.Columns[columnIndex];
			string symbolAsc = ( columnIndex == 3 ? "(a-z)" : "" );
			string symbolDesc = ( columnIndex == 3 ? "(z-a)" : "" );

			if(this._sortDirection == ListSortDirection.Descending) {
				col.HeaderText = col.ToolTipText + " " + symbolAsc;
			}
			else {
				col.HeaderText = col.ToolTipText + " " + symbolDesc;
			}
		}

		/// <summary>
		/// Resets the sort symbols.
		/// </summary>
		private void _ResetSortSymbols() {
			foreach(DataGridViewColumn col in this.dgvMovies.Columns) {
				col.HeaderText = col.ToolTipText;
			}
		}

		/// <summary>
		/// Sets the sort parameters.
		/// </summary>
		/// <param name="expression">The expression.</param>
		private void _SetSortParams(string expression) {
			if(this._sortDirection == ListSortDirection.Descending) {
				this._sortDirection = ListSortDirection.Ascending;
			}
			else {
				this._sortDirection = ListSortDirection.Descending;
			}

			this._sortExpression = expression;
		}

		/// <summary>
		/// Starts the search.
		/// </summary>
		private void _StartSearch() {
			if(this.txtSearchBar.Text.Trim().Length > 0) {
				this._search = false;
				//this.cbFilterBar.SelectedIndex = 0;
				this.cbGenreBar.SelectedIndex = 0;
				this.cbCategoryBar.SelectedIndex = 0;

				string val = this.txtSearchBar.Text.Trim();
				FilterType filter;

				if(val.ToLower().StartsWith("a:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.ActorAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.ActorAndAllOriginals;
					}
					//else if(this.cbFilterBar.SelectedIndex >= 5
					//&& this.cbFilterBar.SelectedIndex <= 15) {
					//    filter = FilterType.ActorAndCodec;
					//}
					//else if(this.cbFilterBar.SelectedIndex >= 17) {
					//    filter = FilterType.ActorAndGenre;
					//}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Actor;
					}
				}
				else if(val.ToLower().StartsWith("d:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.DirectorAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.DirectorAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Director;
					}
				}
				else if(val.ToLower().StartsWith("p:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.ProducerAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.ProducerAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Producer;
					}
				}
				else if(val.ToLower().StartsWith("c:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.CountryAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.CountryAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Country;
					}
				}
				else if(val.ToLower().StartsWith("m:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.MusicianAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.MusicianAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Musician;
					}
				}
				else if(val.ToLower().StartsWith("ca:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.CameramanAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.CameramanAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Cameraman;
					}
				}
				else if(val.ToLower().StartsWith("cu:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.CutterAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.CutterAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Cutter;
					}
				}
				else if(val.ToLower().StartsWith("w:")) {
					val = val.Substring(2).Trim();

					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.WriterAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.WriterAndAllOriginals;
					}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Writer;
					}
				}
				else {
					if(this.cbFilterBar.SelectedIndex == 2) {
						filter = FilterType.NameAndAllConferred;
					}
					else if(this.cbFilterBar.SelectedIndex == 3) {
						filter = FilterType.NameAndAllOriginals;
					}
					//else if(this.cbFilterBar.SelectedIndex >= 5
					//&& this.cbFilterBar.SelectedIndex <= 15) {
					//    filter = FilterType.NameAndCodec;
					//}
					//else if(this.cbFilterBar.SelectedIndex >= 17) {
					//    filter = FilterType.NameAndGenre;
					//}
					else {
						this.cbFilterBar.SelectedIndex = 0;
						filter = FilterType.Name;
					}
				}

				//this._LoadData(
				//    filter,
				//    val
				//);

				this._searchFilter = filter;
				this._searchValue = val;

				//this.loadDataAsync.RunWorkerAsync();
				this._LoadData(this._searchFilter, this._searchValue);

				this._search = true;
			}
			else if(this.cbGenreBar.SelectedIndex > 0) {
				this._ApplyGenreFilter();
			}
			else if(this.cbCategoryBar.SelectedIndex > 0) {
				this._ApplyCategoryFiler();
			}
			else {
				this.cbFilterBar.SelectedIndex = 0;
				this.cbGenreBar.SelectedIndex = 0;
				this.cbCategoryBar.SelectedIndex = 0;
				this.txtSearchBar.Text = "";

				this._LoadData(FilterType.NoFilter);
			}
		}

		/// <summary>
		/// Init the controls
		/// </summary>
		private void _InitControls() {
			this._InitControls(true);
		}

		/// <summary>
		/// Init the controls
		/// </summary>
		/// <param name="resetSelection">If set to <c>true</c> the selection are reseted.</param>
		private void _InitControls(bool resetSelection) {
			Stopwatch watch = new Stopwatch();

			watch.Start();

			bool isReloaded = false;

			if(this._search) {
				this._search = false;
				isReloaded = true;
			}

			// fill the filter combobox
			int indexFilter = 0;

			if(!resetSelection) {
				indexFilter = this.cbFilterBar.SelectedIndex;
			}

			this.cbFilterBar.Items.Clear();
			this.cbFilterBar.Items.Add((object)"<Filter>");
			this.cbFilterBar.Items.Add((object)"<Filter zurücksetzen...>");
			this.cbFilterBar.Items.Add((object)"Alle Verliehenen anzeigen");
			this.cbFilterBar.Items.Add((object)"Alle Originale anzeigen");
			//this.cbFilterBar.Items.Add((object)"------>\"Frat-Pack\"-Film");
			//this.cbFilterBar.Items.Add((object)"------>\"Philip K. Dick\"-Film");
			this.cbFilterBar.Items.Add((object)"---");

			foreach(string codec in Configuration.Codecs) {
				this.cbFilterBar.Items.Add((object)(codec));
			}

			this.cbFilterBar.SelectedIndex = indexFilter;

			// fill the genre combobox
			int indexGenre = 0;

			if(!resetSelection) {
				indexGenre = this.cbGenreBar.SelectedIndex;
			}

			this.cbGenreBar.Items.Clear();
			this.cbGenreBar.Items.Add((object)"<Genre Filter>");
			this.cbGenreBar.Items.Add((object)"<Genre Filter zurücksetzen...>");

			List<Genre> genres = this._db.GetGenreList();

			if(genres.Count > 0) {
				this.cbGenreBar.Items.Add((object)"Alle ohne Genre");
				this.cbGenreBar.Items.Add((object)"---");
			}

			foreach(Genre genre in genres) {
				this.cbGenreBar.Items.Add(genre);
			}

			this.cbGenreBar.SelectedIndex = indexGenre;

			// fill the category combobox
			int indexCategory = 0;

			if(!resetSelection) {
				indexCategory = this.cbCategoryBar.SelectedIndex;
			}

			this.cbCategoryBar.Items.Clear();
			this.cbCategoryBar.Items.Add((object)"<Kategorie Filter>");
			this.cbCategoryBar.Items.Add((object)"<Kategorie Filter zurücksetzen...>");

			List<Category> categories = this._db.GetCategoryList();

			if(categories.Count > 0) {
				this.cbCategoryBar.Items.Add((object)"Alle ohne Kategorie");
				this.cbCategoryBar.Items.Add((object)"---");
			}

			foreach(Category category in categories) {
				this.cbCategoryBar.Items.Add(category);
			}

			this.cbCategoryBar.SelectedIndex = indexCategory;

			//DataGridViewComboBoxColumn dgvcbc = (DataGridViewComboBoxColumn)this.dgvMovies.Columns[5];

			//foreach(string s in Configuration.Codecs) {
			//    dgvcbc.Items.Add(s);
			//}

			if(isReloaded) {
				this._search = true;
			}

			watch.Stop();
			TimeSpan span = watch.Elapsed;

			Console.WriteLine(
				"_InitControls - Required time: {0} - {1}ms ({2} Ticks)",
				span.ToString(),
				watch.ElapsedMilliseconds,
				watch.ElapsedTicks
			);
		}

		/// <summary>
		/// Open the OptionDialog and try to connect to the database
		/// </summary>
		private void _OpenOptionDialogAndTryDBConnect() {
			OptionForm of = new OptionForm();

			if(of.ShowDialog(this) == DialogResult.OK) {
				this._db = null;

				try {
					this._db = new DataHandler(
						Configuration.ConnectionString
					);
					this._LoadData(FilterType.NoFilter);
				}
				catch(Exception ex) {
					StringBuilder str = new StringBuilder();
					str.Append("Fehler beim Aufbau einer Verbindung zur Datenbank!\n");

					if(ex.Message != null) {
						str.Append("Fehlerbeschreibung:\n" + ex.Message + "\n");
					}

					if(ex.InnerException != null) {
						if(ex.InnerException.Message != null) {
							str.Append("Weitergereichte Fehlerbeschreibung:\n");
							str.Append(ex.InnerException.Message + "\n");
						}
					}

					str.Append("\nWollen sie die Datenbank Einstellungen verändern?\n");

					if(MessageBox.Show(
						str.ToString(),
						"MovieMatic ERROR",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Error) == DialogResult.Yes) {
						this._OpenOptionDialogAndTryDBConnect();
					}
				}
			}
			else {
				if(this._initial) {
					MessageBox.Show(
						"MovieMativ konnte nicht auf die Datenbank zugreifen und wird beendet!",
						"MovieMatic ERROR"
					);

					this._close_app = true;
				}
			}
		}

		/// <summary>
		/// Opens a dialog
		/// </summary>
		/// <param name="form">The form.</param>
		private void _OpenDialog(Form form) {
			if(form is FromWikipediaForm) {
				FromWikipediaForm wpForm = (FromWikipediaForm)form;
				DialogResult dr = wpForm.ShowDialog(this);

				if(dr == DialogResult.OK) {
					this._InitControls(false);
					this._StartSearch();
				}
				else if(dr == DialogResult.Retry) {
					this._OpenDialog(new FromWikipediaForm(wpForm.WikiParseType, wpForm.SearchWord));
				}
			}
			else if(form is MigrateMSSQLToSQLiteForm) {
				MigrateMSSQLToSQLiteForm mForm = (MigrateMSSQLToSQLiteForm)form;
				DialogResult dr = mForm.ShowDialog(this);

				if(dr == DialogResult.OK) {
					this._InitControls(false);
					this._StartSearch();
				}
			}
			else {
				DialogResult dr = form.ShowDialog(this);

				if(dr == DialogResult.OK) {
					this._InitControls(false);
					this._StartSearch();
				}
			}
		}

		/// <summary>
		/// Open the movie editor
		/// </summary>
		private void _OpenMovieEditor() {
			this._OpenMovieEditor("");
		}

		/// <summary>
		/// Open the movie editor
		/// </summary>
		/// <param name="id"></param>
		private void _OpenMovieEditor(string id) {
			MovieForm mf;

			if(id != null
			&& id.Trim() != "") {
				mf = new MovieForm(id);
			}
			else {
				mf = new MovieForm();
			}

			if(mf.ShowDialog(this) == DialogResult.OK) {
				this._InitControls(false);
				this._StartSearch();
			}
		}

		/// <summary>
		/// Load data
		/// </summary>
		/// <param name="filter"></param>
		private void _LoadData(FilterType filter) {
			//this._LoadData(filter, "");

			this._searchFilter = filter;
			this._searchValue = "";

			//this.loadDataAsync.RunWorkerAsync();
			this._LoadData(this._searchFilter, this._searchValue);
		}

		/// <summary>
		/// Load data
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="value">The value.</param>
		private void _LoadData(FilterType filter, string value) {
			if(this.dgvMovies.InvokeRequired) {
				LoadDataCallback ldCall = new LoadDataCallback(_LoadData);
				this.Invoke(ldCall, new object[] { filter, value });
			}
			else {
				this.Cursor = Cursors.WaitCursor;

				Stopwatch watch = new Stopwatch();

				watch.Start();

				if(__useDataBind) {
					this.dgvMovies.DataSource = null;
				}
				else {
					this.dgvMovies.Rows.Clear();
				}

				this._movies = new List<Movie>();

				/*
				 * GETMOVIELIST
				 */
				Stopwatch watch2 = new Stopwatch();

				watch2.Start();

				switch(filter) {
					case FilterType.NoFilter:
					case FilterType.ResetFilter:
						this._movies = this._db.GetMovieList(
							this._sortExpression,
							( this._sortDirection == ListSortDirection.Ascending ? DataSortDirection.Ascending : DataSortDirection.Descending )
						);
						break;

					case FilterType.AllConferred:
					case FilterType.AllOriginals:
					case FilterType.WithoutGenre:
						this._movies = this._db.GetMovieList(
							filter,
							this._sortExpression,
							( this._sortDirection == ListSortDirection.Ascending ? DataSortDirection.Ascending : DataSortDirection.Descending )
						);
						break;

					case FilterType.Codec:
						this._movies = this._db.GetMovieList(
							filter,
							"",
							CodecHelper.GetCodecNumber(value),
							false,
							this._sortExpression,
							( this._sortDirection == ListSortDirection.Ascending ? DataSortDirection.Ascending : DataSortDirection.Descending )
						);
						break;

					case FilterType.Genre:
					case FilterType.Category:
					case FilterType.Name:
					case FilterType.Actor:
					case FilterType.Director:
					case FilterType.Producer:
					default:
						this._movies = this._db.GetMovieList(
							filter,
							value,
							this._sortExpression,
							( this._sortDirection == ListSortDirection.Ascending ? DataSortDirection.Ascending : DataSortDirection.Descending )
						);
						break;
				}

				watch2.Stop();
				TimeSpan span2 = watch2.Elapsed;

				Console.WriteLine(
					"_LoadData [only GetMovieList] - Required time: {0} - {1}ms ({2} Ticks)",
					span2.ToString(),
					watch2.ElapsedMilliseconds,
					watch2.ElapsedTicks
				);

				/*
				 * DATABIND
				 */
				Stopwatch watch3 = new Stopwatch();

				watch3.Start();

				this._current_amount = this._movies.Count;
				this.tslblAmount.Text = "Anzahl Filme: " + this._current_amount.ToString();

				//this._movies.Sort();

				if(__useDataBind) {
					this.dgvMovies.AutoGenerateColumns = false;
					this.dgvMovies.DataSource = this._movies;
				}
				else {
					int count = 0;

					foreach(Movie mov in this._movies) {
						this.dgvMovies.Rows.Add(1);

						//tmp = mov.Quality.ToString();
						//qualiText = tmp.Substring(0, mov.Quality.ToString().IndexOf("_"));

						//int.TryParse(tmp.Substring(tmp.LastIndexOf("_") + 1), out quali);

						//Static stc = this._sb.GetStaticItem("C002", mov.Country);

						this.dgvMovies.Rows[count].Cells[0].Value = mov.Id;
						this.dgvMovies.Rows[count].Cells[1].Value = mov.SortValue;
						this.dgvMovies.Rows[count].Cells[2].Value = mov.Number;
						this.dgvMovies.Rows[count].Cells[3].Value = mov.Name;
						this.dgvMovies.Rows[count].Cells[4].Value = mov.DiscAmount.ToString();
						this.dgvMovies.Rows[count].Cells[5].Value = mov.IsOriginal;
						this.dgvMovies.Rows[count].Cells[6].Value = mov.Codec.ToString();
						this.dgvMovies.Rows[count].Cells[7].Value = mov.GenerateGenresString;
						this.dgvMovies.Rows[count].Cells[8].Value = mov.GenerateCategoriesString;
						this.dgvMovies.Rows[count].Cells[9].Value = mov.GenerateQualityString;
						this.dgvMovies.Rows[count].Cells[10].Value = mov.CountryString;
						this.dgvMovies.Rows[count].Cells[11].Value = mov.IsConferred;
						this.dgvMovies.Rows[count].Cells[12].Value = mov.ConferredTo;

						count++;
					}

					this.dgvMovies.FirstDisplayedScrollingRowIndex = this._current_amount - 1;
					this.dgvMovies.Rows[this._current_amount - 1].Selected = true;
				}

				watch.Stop();

				TimeSpan span = watch.Elapsed;

				watch3.Stop();
				TimeSpan span3 = watch3.Elapsed;

				Console.WriteLine(
					"_LoadData [only DataBind] - Required time: {0} - {1}ms ({2} Ticks)",
					span3.ToString(),
					watch3.ElapsedMilliseconds,
					watch3.ElapsedTicks
				);

				this.tslblAmount.Text += string.Format(
					" - Required time: {0} - {1}ms ({2} Ticks)",
					span.ToString(),
					watch.ElapsedMilliseconds,
					watch.ElapsedTicks
				);

				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Updates the state bar.
		/// </summary>
		/// <param name="removeValue">if set to <c>true</c> [remove value].</param>
		/// <param name="value">The value.</param>
		private void _UpdateStateBar(bool removeValue, int value) {
			if(removeValue) {
				this._current_amount -= value;
			}
			else {
				this._current_amount += value;
			}

			this.tslblAmount.Text = "Anzahl Filme: " + this._current_amount.ToString();
		}

		/// <summary>
		/// Extends the frame.
		/// </summary>
		private void ExtendFrame() {
			if(this.IsGlassFrameEnabled) {
				if(this.DesignMode == false) {
					//// Wenn nicht im Design-Modus: den Rahmen verbreitern
					//MARGINS margins = new MARGINS() {
					//    Left = this.Padding.Left,
					//    Top = this.Padding.Top,
					//    Right = this.Padding.Right,
					//    Bottom = this.Padding.Bottom
					//};
					//DwmExtendFrameIntoClientArea(this.Handle, ref margins);
				}
			}
		}

		///// <summary>
		///// Raises the <see cref="E:Load"/> event.
		///// </summary>
		///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		//protected override void OnLoad(EventArgs e) {
		//    base.OnLoad(e);

		//    this.ExtendFrame();
		//}

		///// <summary>
		///// Raises the <see cref="E:PaintBackground"/> event.
		///// </summary>
		///// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		//protected override void OnPaintBackground(PaintEventArgs e) {
		//    base.OnPaintBackground(e);

		//    if(this.IsGlassFrameEnabled) {
		//        // Die Hintergrundfarbe auf Schwarz setzen um den
		//        // Glass-Effekt zu ermöglichen
		//        //e.Graphics.Clear(Color.Black);
		//        e.Graphics.Clear(Color.Black);
		//    }
		//}

		///// <summary>
		///// WNDs the proc.
		///// </summary>
		///// <param name="m">The m.</param>
		//protected override void WndProc(ref Message m) {
		//    const int DWMCOMPOSITIONCHANGED = 0x031E;
		//    if(m.Msg == DWMCOMPOSITIONCHANGED) {
		//        // Den Rahmen neu definieren
		//        this.ExtendFrame();
		//        this.Invalidate();
		//    }
		//    base.WndProc(ref m);
		//}

		/// <summary>
		/// Alerts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void alert(string value) {
			StaticWindows.alert(value);
		}

		/// <summary>
		/// Alerts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void alert(int value) {
			StaticWindows.alert(value);
		}

		//public bool PreFilterMessage(ref Message msg) {
		//    int WM_KEYDOWN = 0x100;
		//    if(msg.Msg == WM_KEYDOWN) {
		//        if(msg.WParam.ToString() == "220") {
		//            // Taste ^
		//            this._console.SetVisibility(!this._console.GetVisibility());
		//            return true;
		//        }
		//        else {
		//            // Erlaube das scrollen in der Konsole nur wenn die Konsole sichtbar ist
		//            if(this._console.GetVisibility() == true) {
		//                if(msg.WParam.ToString() == "33") {
		//                    // Bildhoch
		//                    this._console.Scroll_Up();
		//                    return true;
		//                }
		//                else if(msg.WParam.ToString() == "34") {
		//                    // Bildrunter
		//                    this._console.Scroll_Down();
		//                    return true;
		//                }
		//            }
		//        }
		//    }

		//    return false;
		//}
	}
}
