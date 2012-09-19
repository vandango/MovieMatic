using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation;
using Toenda.Foundation.Utility;

namespace Toenda.MovieMatic {
	public partial class MovieForm : Form {
		private DataHandler _db = new DataHandler(Configuration.ConnectionString);
		private StaticHandler _st = new StaticHandler(Configuration.ConnectionString);

		private Movie _mov = new Movie();
		private bool __isNewMovie = false;
		private bool __hasChanges = false;

		private int _actorAmount = 0;
		private int _directorAmount = 0;
		private int _producerAmount = 0;
		private int _musicianAmount = 0;
		private int _cameramanAmount = 0;
		private int _cutterAmount = 0;
		private int _writerAmount = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="MovieForm"/> class.
		/// </summary>
		public MovieForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._InitControls();
			this._LoadData();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MovieForm"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		public MovieForm(string id) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._mov = this._db.GetMovie(id);

			this._InitControls();
			this._LoadData();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MovieForm"/> class.
		/// </summary>
		/// <param name="mov">The mov.</param>
		public MovieForm(Movie mov) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._mov = mov;

			this._InitControls();
			this._LoadData();
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get or set the movie
		/// </summary>
		public Movie Movie {
			get { return this._mov; }
			set { this._mov = value; }
		}

		/// <summary>
		/// Get a value that indicates that this movie is a new movie
		/// </summary>
		public bool IsNewMovie {
			get {
				if(this._mov.Id == null
				|| this._mov.Id.Trim() == ""
				|| !this.txtID.Text.Trim().IsAlphaNumeric()) {
					return true;
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [form has changes].
		/// </summary>
		/// <value><c>true</c> if [form has changes]; otherwise, <c>false</c>.</value>
		public bool FormHasChanges {
			get { return this.__hasChanges; }
			set { this.__hasChanges = value; }
		}

		// -------------------------------------------------------
		// EVENTS
		// -------------------------------------------------------

		/// <summary>
		/// btnOK_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;

			if(this._SaveData()) {
				//MessageBox.Show(
				//    "Erfolgreich gespeichert!",
				//    "MovieMatic",
				//    MessageBoxButtons.OK,
				//    MessageBoxIcon.Information
				//);

				this.Close();
			}
			else {
				if(MessageBox.Show(
					"Beim Speichern tratt ein Fehler auf!"
					+ "\nWollen sie es nocheinmal versuchen?"
					+ "\n\n(Bei <Nein> werden alle Eingaben verworfen.)",
					"MovieMatic Fehler",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
					if(this._SaveData()) {
						MessageBox.Show(
							"Erfolgreich gespeichert!",
							"MovieMatic",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information
						);

						this.Close();
					}
					else {
						MessageBox.Show(
							"Beim Speichern tratt ein Fehler auf!"
							+ "\nDer Vorgang wird nun abgebrochen.",
							"MovieMatic",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						);

						this.Close();
					}
				}
				else {
					this.Close();
				}
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
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Genre);
		}

		/// <summary>
		/// Handles the Click event of the btnAddCat control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnAddCat_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Category);
		}

		/// <summary>
		/// btnAddActor_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddActor_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Actor);
		}

		/// <summary>
		/// btnAddDirector_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddDirector_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Director);
		}

		/// <summary>
		/// btnAddProducer_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddProducer_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Producer);
		}

		/// <summary>
		/// Handles the Click event of the btnAddWriter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnAddWriter_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Writer);
		}

		/// <summary>
		/// Handles the Click event of the btnAddMusician control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnAddMusician_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Musician);
		}

		/// <summary>
		/// Handles the Click event of the btnAddCameraman control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnAddCameraman_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Cameraman);
		}

		/// <summary>
		/// Handles the Click event of the btnAddCutter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnAddCutter_Click(object sender, EventArgs e) {
			this._OpenAddForm(MovieObjectType.Cutter);
		}

		/// <summary>
		/// lvGenre_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGenre_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvGenre.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvGenre.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Genres.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Genre,
							lvi.Name
						)
					);
					this.lvGenre.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the lvCategories control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void lvCategories_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvCategories.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvCategories.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Categories.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Category,
							lvi.Name
						)
					);
					this.lvCategories.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// lvActors_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvActors_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvActors.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvActors.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Actors.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Actor,
							lvi.Name
						)
					);
					this.lvActors.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// lvDirectors_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvDirectors_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvDirectors.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvDirectors.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Directors.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Director,
							lvi.Name
						)
					);
					this.lvDirectors.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// lvProducers_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvProducers_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvProducers.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvProducers.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Producers.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Producer, 
							lvi.Name
						)
					);
					this.lvProducers.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the lvWriters control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void lvWriters_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvWriters.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvWriters.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Writers.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Writer,
							lvi.Name
						)
					);
					this.lvWriters.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the lvMusician control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void lvMusician_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvMusician.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvMusician.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Musicians.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Musician,
							lvi.Name
						)
					);
					this.lvMusician.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the lvCameraman control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void lvCameraman_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvCameraman.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvCameraman.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Cameramans.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Cameraman,
							lvi.Name
						)
					);
					this.lvCameraman.Items.Remove(lvi);
				}
			}
		}

		private void lvCutter_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvCutter.SelectedItems.Count != 0) {
				foreach(ListViewItem lvi in this.lvCutter.SelectedItems) {
					this.__hasChanges = true;

					this._mov.Cutters.RemoveAt(
						this._GetObjectIndexFromMovieList(
							MovieObjectType.Cutter,
							lvi.Name
						)
					);
					this.lvCutter.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// Handles the Scroll event of the tbQuality control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void tbQuality_Scroll(object sender, EventArgs e) {
			this.__hasChanges = true;

			this.lblQuality.Text = Configuration.GetQualityName(this.tbQuality.Value);
		}

		/// <summary>
		/// Handles the Shown event of the MovieForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MovieForm_Shown(object sender, EventArgs e) {
			this.txtName.Focus();
			this.__hasChanges = false;
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkIsConferred control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkIsConferred_CheckedChanged(object sender, EventArgs e) {
			this.__hasChanges = true;

			if(!this.chkIsConferred.Checked) {
				this.txtConferredTo.Text = "";
			}
			else {
				this.txtConferredTo.Focus();
			}
		}

		/// <summary>
		/// Handles the Click event of the btnPrevious control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnPrevious_Click(object sender, EventArgs e) {
			bool openNextMovie = true;

			if(this.FormHasChanges) {
				DialogResult dr = StaticWindows.Requester(
					"Der aktuelle Film wurde geändert,\nwollen Sie diese Änderungen jetzt speichern?"
				);
				
				if(dr == DialogResult.Yes) {
					this._SaveData();
				}
				else if(dr == DialogResult.Cancel) {
					openNextMovie = false;
				}
				else if(dr == DialogResult.No) {
					openNextMovie = true;
				}
			}

			if(openNextMovie
			&& this._mov != null) {
				string newId = this._db.GetPreviousMovieId(this._mov.Id, this._mov.Number);

				if(!newId.IsNullOrTrimmedEmpty()) {
					this._mov = this._db.GetMovie(newId);

					this._InitControls();
					this._LoadData();

					this.__hasChanges = false;
				}
			}
		}

		/// <summary>
		/// Handles the Click event of the btnNext control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnNext_Click(object sender, EventArgs e) {
			bool openNextMovie = true;

			if(this.FormHasChanges) {
				DialogResult dr = StaticWindows.Requester(
					"Der aktuelle Film wurde geändert,\nwollen Sie diese Änderungen jetzt speichern?"
				);

				if(dr == DialogResult.Yes) {
					this._SaveData();
					openNextMovie = true;
				}
				else if(dr == DialogResult.Cancel) {
					openNextMovie = false;
				}
				else if(dr == DialogResult.No) {
					openNextMovie = true;
				}
			}

			if(openNextMovie
			&& this._mov != null) {
				string newId = this._db.GetNextMovieId(this._mov.Id, this._mov.Number);

				if(!newId.IsNullOrTrimmedEmpty()) {
					this._mov = this._db.GetMovie(newId);

					this._InitControls();
					this._LoadData();

					this.__hasChanges = false;
				}
			}
		}

		/// <summary>
		/// Handles the TextChanged event of the formChanged control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void formChanged_TextChanged(object sender, EventArgs e) {
			this.__hasChanges = true;

			if(this.txtConferredTo.Text.Length > 0) {
				this.chkIsConferred.Checked = true;
			}
			else {
				this.chkIsConferred.Checked = false;
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the formChanged control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void formChanged_CheckedChanged(object sender, EventArgs e) {
			this.__hasChanges = true;
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the formChanged control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void formChanged_SelectedIndexChanged(object sender, EventArgs e) {
			this.__hasChanges = true;
		}

		/// <summary>
		/// Handles the Activated event of the MovieForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MovieForm_Activated(object sender, EventArgs e) {
			this.__hasChanges = false;
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the lvActors control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void lvActors_SelectedIndexChanged(object sender, EventArgs e) {
			if(this.lvActors.SelectedItems.Count == 1) {
				this.btnEditRole.Enabled = true;
			}
			else {
				this.btnEditRole.Enabled = false;
			}
		}

		/// <summary>
		/// Handles the Leave event of the lvActors control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void lvActors_Leave(object sender, EventArgs e) {
			if(this.lvActors.SelectedItems.Count == 1) {
				this.btnEditRole.Enabled = true;
			}
			else {
				this.btnEditRole.Enabled = false;
			}
		}

		/// <summary>
		/// Handles the Click event of the btnEditRole control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnEditRole_Click(object sender, EventArgs e) {
			if(!this.IsNewMovie) {
				string personId = "";
				string personName = "";
				string roleName = "";
				string roleType = "";

				if(this.lvActors.SelectedIndices.Count == 1) {
					int index = this.lvActors.SelectedIndices[0];
					ListViewItem sel = this.lvActors.Items[index];

					personId = sel.Name;
					personName = sel.Text;

					if(sel.SubItems.Count > 1) {
						roleName = sel.SubItems[1].Text;
					}

					if(sel.SubItems.Count > 2) {
						roleType = sel.SubItems[2].Name;
					}

					EditPersonRole form = new EditPersonRole(
						this._mov.Id,
						personId,
						personName,
						roleName,
						roleType
					);

					if(form.ShowDialog(this) == DialogResult.OK) {
						if(sel != null) {
							if(sel.SubItems.Count > 1) {
								sel.SubItems[1].Text = form.RoleName;
							}

							if(sel.SubItems.Count > 2) {
								sel.SubItems[2].Text = form.RoleTypeName;
								sel.SubItems[2].Name = form.RoleType;
							}
						}

						foreach(Person p in this._mov.Actors) {
							if(p.ID == personId) {
								p.Rolename = form.RoleName;
								p.Roletype = form.RoleType;
							}
						}

						this.lvActors.Refresh();
						this.lvActors.Update();
					}
				}
			}
			else {
				StaticWindows.InfoBox("Rollen können erst nach Anlegen des Films bearbeitet werden!");
			}
		}

		/// <summary>
		/// Handles the KeyDown event of the txtNote control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void txtNote_KeyDown(object sender, KeyEventArgs e) {
			if(e.Control && e.KeyCode == Keys.A) {
				this.txtNote.SelectAll();
			}
			else if(e.Control && e.KeyCode == Keys.C) {
				DataObject da = new DataObject(
					DataFormats.UnicodeText,
					this.txtNote.SelectedText
				);

				Clipboard.SetDataObject(da);
			}
		}

		/// <summary>
		/// Handles the KeyDown event of the MovieForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void MovieForm_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyData == Keys.Escape) {
				this.Close();
			}
			else {
				switch(e.KeyData) {
					case (Keys)Shortcut.CtrlShift1: // open tab: Daten
						this.tabControl1.SelectedIndex = 0;
						break;

					case (Keys)Shortcut.CtrlShift2: // open tab: Genre / Kategorie
						this.tabControl1.SelectedIndex = 1;
						break;

					case (Keys)Shortcut.CtrlShift3: // open tab: Beschreibung
						this.tabControl1.SelectedIndex = 2;
						this.txtNote.Focus();
						break;

					case (Keys)Shortcut.CtrlShift4: // open tab: Stab
						this.tabControl1.SelectedIndex = 3;
						break;

					case (Keys)Shortcut.CtrlShift5: // open tab: Besetzung
						this.tabControl1.SelectedIndex = 4;
						break;

					default:
						if(this.tabControl1.SelectedIndex == 1) {
							switch(e.KeyData) {
								case (Keys)Shortcut.CtrlG: // Genre
									this._OpenAddForm(MovieObjectType.Genre);
									break;

								case (Keys)Shortcut.CtrlK: // Kategorie
									this._OpenAddForm(MovieObjectType.Category);
									break;

								default:
									break;
							}
						}
						else if(this.tabControl1.SelectedIndex == 3) {
							switch(e.KeyData) {
								case (Keys)Shortcut.CtrlR: // regisseur
								case (Keys)Shortcut.CtrlD: // director
									this._OpenAddForm(MovieObjectType.Director);
									break;

								case (Keys)Shortcut.CtrlW: // writer
									this._OpenAddForm(MovieObjectType.Writer);
									break;

								case (Keys)Shortcut.CtrlP: // produzent, producer
									this._OpenAddForm(MovieObjectType.Producer);
									break;

								case (Keys)Shortcut.CtrlM: // musiker, musician
									this._OpenAddForm(MovieObjectType.Musician);
									break;

								case (Keys)Shortcut.CtrlK: // kameramann
									this._OpenAddForm(MovieObjectType.Cameraman);
									break;

								case (Keys)Shortcut.CtrlC: // cutter
									this._OpenAddForm(MovieObjectType.Cutter);
									break;

								default:
									break;
							}
						}
						else if(this.tabControl1.SelectedIndex == 4) {
							switch(e.KeyData) {
								case (Keys)Shortcut.CtrlA: // actor
								case (Keys)Shortcut.CtrlS: // schauspieler
									this._OpenAddForm(MovieObjectType.Actor);
									break;

								default:
									break;
							}
						}
						break;
				}
			}
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Open a AddItemForm window
		/// </summary>
		/// <param name="mot"></param>
		private void _OpenAddForm(MovieObjectType mot) {
			AddItemForm agf = new AddItemForm(mot);

			DialogResult dr = agf.ShowDialog(this);

			if(dr == DialogResult.OK) {
				this.__hasChanges = true;

				if(mot == MovieObjectType.Genre) {
					foreach(Genre g in agf.SelectedGenreItems) {
						if(!this._ObjectIsInListView(mot, g.ID)) {
							ListViewItem lvi = new ListViewItem();
							lvi.Text = g.Name;
							lvi.Name = g.ID;

							this.lvGenre.Items.Add(lvi);
							this._mov.Genres.Add(g);
						}
					}
				}
				else if(mot == MovieObjectType.Category) {
					foreach(Category c in agf.SelectedCategoryItems) {
						if(!this._ObjectIsInListView(mot, c.ID)) {
							ListViewItem lvi = new ListViewItem();
							lvi.Text = c.Name;
							lvi.Name = c.ID;

							this.lvCategories.Items.Add(lvi);
							this._mov.Categories.Add(c);
						}
					}
				}
				else {
					foreach(Person p in agf.SelectedPersonItems) {
						if(!this._ObjectIsInListView(mot, p.ID)) {
							ListViewItem lvi = new ListViewItem();
							lvi.Text = p.Firstname + " " + p.Lastname;
							lvi.Name = p.ID;

							switch(mot) {
								case MovieObjectType.Actor:
									// roleName
									ListViewItem.ListViewSubItem lviRN = new ListViewItem.ListViewSubItem();
									lviRN.Text = p.Rolename;

									lvi.SubItems.Add(lviRN);

									// roleType
									ListViewItem.ListViewSubItem lviRT = new ListViewItem.ListViewSubItem();

									if(!p.Roletype.IsNullOrTrimmedEmpty()) {
										Static stcRoleType = this._st.GetStaticItem("RT01", p.Roletype);

										if(stcRoleType != null) {
											lviRT.Text = stcRoleType.Content;
											lviRT.Name = stcRoleType.Value;
										}
									}

									lvi.SubItems.Add(lviRT);

									this.lvActors.Items.Add(lvi);
									this._mov.Actors.Add(p);

									this._SetQuantityLabel(this.lblActorAmount, this._mov.Actors.Count);
									break;

								case MovieObjectType.Director:
									this.lvDirectors.Items.Add(lvi);
									this._mov.Directors.Add(p);

									this._SetQuantityLabel(this.lblDirectorAmount, this._mov.Directors.Count);
									break;

								case MovieObjectType.Producer:
									this.lvProducers.Items.Add(lvi);
									this._mov.Producers.Add(p);

									this._SetQuantityLabel(this.lblProducerAmount, this._mov.Producers.Count);
									break;

								case MovieObjectType.Musician:
									this.lvMusician.Items.Add(lvi);
									this._mov.Musicians.Add(p);

									this._SetQuantityLabel(this.lblMusicianAmount, this._mov.Musicians.Count);
									break;

								case MovieObjectType.Cameraman:
									this.lvCameraman.Items.Add(lvi);
									this._mov.Cameramans.Add(p);

									this._SetQuantityLabel(this.lblCameramanAmount, this._mov.Cameramans.Count);
									break;

								case MovieObjectType.Cutter:
									this.lvCutter.Items.Add(lvi);
									this._mov.Cutters.Add(p);

									this._SetQuantityLabel(this.lblCutterAmount, this._mov.Cutters.Count);
									break;

								case MovieObjectType.Writer:
									this.lvWriters.Items.Add(lvi);
									this._mov.Writers.Add(p);

									this._SetQuantityLabel(this.lblWriterAmount, this._mov.Writers.Count);
									break;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Sets the quantity label.
		/// </summary>
		/// <param name="label">The label.</param>
		/// <param name="quantity">The quantity.</param>
		private void _SetQuantityLabel(Label label, int quantity) {
			if(label.Name.Contains("Actor")) {
				this._actorAmount = quantity;
			}
			else if(label.Name.Contains("Director")) {
				this._directorAmount = quantity;
			}
			else if(label.Name.Contains("Producer")) {
				this._producerAmount = quantity;
			}
			else if(label.Name.Contains("Musician")) {
				this._musicianAmount = quantity;
			}
			else if(label.Name.Contains("Cameraman")) {
				this._cameramanAmount = quantity;
			}
			else if(label.Name.Contains("Cutter")) {
				this._cutterAmount = quantity;
			}
			else if(label.Name.Contains("Writer")) {
				this._writerAmount = quantity;
			}

			label.Text = "Anzahl: " + quantity.ToString();
		}

		/// <summary>
		/// Get the index of the country combo box
		/// </summary>
		/// <param name="selectedValue"></param>
		/// <returns></returns>
		private int _GetCountryComboboxIndex(string selectedValue) {
			int index = 0;

			foreach(Static stc in this.cbCountry.Items) {
				if(stc.Value == selectedValue) {
					break;
				}

				index++;
			}

			return index;
		}

		/// <summary>
		/// _s the index of the get language combobox.
		/// </summary>
		/// <param name="selectedValue">The selected value.</param>
		/// <returns></returns>
		private int _GetLanguageComboboxIndex(string selectedValue) {
			int index = 0;

			foreach(Static stc in this.cbLanguage.Items) {
				if(stc.Value == selectedValue) {
					break;
				}

				index++;
			}

			return index;
		}

		/// <summary>
		/// Init controls
		/// </summary>
		private void _InitControls() {
			this.btnEditRole.Enabled = false;

			// codesc
			this.cbCodec.Items.Clear();

			foreach(string s in Configuration.Codecs) {
				this.cbCodec.Items.Add(s);
			}

			// countries
			this.cbCountry.Items.Clear();

			List<Static> countries = this._st.GetStaticItemList("C002", OrderStaticsBy.ContentASC);

			foreach(Static stc in countries) {
				this.cbCountry.Items.Add(stc);
			}

			this.cbCountry.DisplayMember = "Content";
			this.cbCountry.ValueMember = "Value";

			// languages
			this.cbLanguage.Items.Clear();

			List<Static> languages = this._st.GetStaticItemList("L002", OrderStaticsBy.ValueASC);

			foreach(Static stc in languages) {
				this.cbLanguage.Items.Add(stc);
			}

			this.cbLanguage.DisplayMember = "Content";
			this.cbLanguage.ValueMember = "Value";

			// next/prev buttons
			if(this._mov != null) {
				int minId = this._db.GetMovieNumber(NumberOperator.Min);
				int maxId = this._db.GetMovieNumber(NumberOperator.Max);
				int actId = this._mov.Number;

				if(actId > minId) {
					this.btnPrevious.Enabled = true;
				}
				else {
					this.btnPrevious.Enabled = false;
				}

				if(actId < maxId) {
					this.btnNext.Enabled = true;
				}
				else {
					this.btnNext.Enabled = false;
				}
			}
			else {
				this.btnPrevious.Enabled = false;
				this.btnNext.Enabled = false;
			}
		}

		/// <summary>
		/// Load the movie data to the controls
		/// </summary>
		private void _LoadData() {
			int index = 0;
			int langIndex = 0;

			try {
				if(this._mov.Id != null
				&& this._mov.Id.Trim() != "") {
					this.__isNewMovie = false;
				}
				else {
					this.__isNewMovie = true;
				}

				if(this._mov != null) {
					this.txtID.Text = this._mov.Number.ToString();
					this.txtID.Tag = this._mov.Id;

					this.txtName.Text = this._mov.Name;
					this.txtSortValue.Text = this._mov.SortValue;
					this.cbCodec.SelectedItem = this._mov.Codec.ToString();
					this.chkHasCover.Checked = this._mov.HasCover;
					this.chkIsOriginal.Checked = this._mov.IsOriginal;
					this.txtDiscAmount.Text = this._mov.DiscAmount.ToString();
					this.txtYear.Text = this._mov.Year.ToString();
					this.tbQuality.Value = (int)this._mov.Quality;
					this.chkIsConferred.Checked = this._mov.IsConferred;
					this.txtConferredTo.Text = this._mov.ConferredTo;
					this.txtNote.Text = this._mov.Note;

					this.lblQuality.Text = Configuration.GetQualityName(this.tbQuality.Value);

					// country
					if(!this._mov.Country.IsNullOrTrimmedEmpty()) {
						index = this._GetCountryComboboxIndex(this._mov.Country);
					}
					else {
						index = this._GetCountryComboboxIndex("us");
					}

					// language
					if(!this._mov.Language.IsNullOrTrimmedEmpty()) {
						langIndex = this._GetLanguageComboboxIndex(this._mov.Language);
					}
					else {
						langIndex = this._GetLanguageComboboxIndex("de");
					}

					// year
					if(this._mov.Year > 0
					&& this._mov.Year > 1799) {
						this.txtYear.Text = this._mov.Year.ToString().PadLeft(4, '0');
					}
					else {
						this.txtYear.Text = "";
					}

					// genres
					if(this._mov.Genres.Count > 0) {
						foreach(Genre g in this._mov.Genres) {
							if(!this._ObjectIsInListView(MovieObjectType.Genre, g.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = g.Name;
								lvi.Name = g.ID;

								this.lvGenre.Items.Add(lvi);
							}
						}
					}

					// categories
					if(this._mov.Categories.Count > 0) {
						foreach(Category c in this._mov.Categories) {
							if(!this._ObjectIsInListView(MovieObjectType.Category, c.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = c.Name;
								lvi.Name = c.ID;

								this.lvCategories.Items.Add(lvi);
							}
						}
					}

					// actors
					if(this._mov.Actors.Count > 0) {
						this._SetQuantityLabel(this.lblActorAmount, this._mov.Actors.Count);

						foreach(Person p in this._mov.Actors) {
							if(!this._ObjectIsInListView(MovieObjectType.Actor, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								// roleName
								ListViewItem.ListViewSubItem lviRN = new ListViewItem.ListViewSubItem();
								lviRN.Text = p.Rolename;

								lvi.SubItems.Add(lviRN);

								// roleType
								ListViewItem.ListViewSubItem lviRT = new ListViewItem.ListViewSubItem();

								if(!p.Roletype.IsNullOrTrimmedEmpty()) {
									Static stcRoleType = this._st.GetStaticItem("RT01", p.Roletype);

									if(stcRoleType != null) {
										lviRT.Text = stcRoleType.Content;
										lviRT.Name = stcRoleType.Value;
									}
								}

								lvi.SubItems.Add(lviRT);

								this.lvActors.Items.Add(lvi);
							}
						}
					}

					// directors
					if(this._mov.Directors.Count > 0) {
						this._SetQuantityLabel(this.lblDirectorAmount, this._mov.Directors.Count);

						foreach(Person p in this._mov.Directors) {
							if(!this._ObjectIsInListView(MovieObjectType.Director, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvDirectors.Items.Add(lvi);
							}
						}
					}

					// producers
					if(this._mov.Producers.Count > 0) {
						this._SetQuantityLabel(this.lblProducerAmount, this._mov.Producers.Count);

						foreach(Person p in this._mov.Producers) {
							if(!this._ObjectIsInListView(MovieObjectType.Producer, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvProducers.Items.Add(lvi);
							}
						}
					}

					// musician
					if(this._mov.Musicians.Count > 0) {
						this._SetQuantityLabel(this.lblMusicianAmount, this._mov.Musicians.Count);

						foreach(Person p in this._mov.Musicians) {
							if(!this._ObjectIsInListView(MovieObjectType.Musician, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvMusician.Items.Add(lvi);
							}
						}
					}

					// cameraman
					if(this._mov.Cameramans.Count > 0) {
						this._SetQuantityLabel(this.lblCameramanAmount, this._mov.Cameramans.Count);

						foreach(Person p in this._mov.Cameramans) {
							if(!this._ObjectIsInListView(MovieObjectType.Cameraman, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvCameraman.Items.Add(lvi);
							}
						}
					}

					// cutter
					if(this._mov.Cutters.Count > 0) {
						this._SetQuantityLabel(this.lblCutterAmount, this._mov.Cutters.Count);

						foreach(Person p in this._mov.Cutters) {
							if(!this._ObjectIsInListView(MovieObjectType.Cutter, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvCutter.Items.Add(lvi);
							}
						}
					}

					// writer
					if(this._mov.Writers.Count > 0) {
						this._SetQuantityLabel(this.lblWriterAmount, this._mov.Writers.Count);

						foreach(Person p in this._mov.Writers) {
							if(!this._ObjectIsInListView(MovieObjectType.Writer, p.ID)) {
								ListViewItem lvi = new ListViewItem();
								lvi.Text = p.Firstname + " " + p.Lastname;
								lvi.Name = p.ID;

								this.lvWriters.Items.Add(lvi);
							}
						}
					}
				}
				else {
					index = this._GetCountryComboboxIndex("us");
					langIndex = this._GetLanguageComboboxIndex("de");
					this.txtID.Text = this._db.GetNextMovieNumber().ToString();
					this.tbQuality.Value = 5;
					this.cbCodec.SelectedItem = Codec.Unknown.ToString();
				}

				this.cbCountry.SelectedIndex = index;
				this.cbLanguage.SelectedIndex = langIndex;
			}
			catch(Exception ex) {
				StaticWindows.DisplayErrorMessagebox(ex);
			}
		}

		/// <summary>
		/// Save the movie
		/// </summary>
		/// <returns></returns>
		private bool _SaveData() {
			bool result = true;

			try {
				//if(!this.IsNewMovie) {
				this._mov.Number = Convert.ToInt32(this.txtID.Text.Trim().Replace("'", "´"));
				//}

				this._mov.Name = this.txtName.Text.Trim().Replace("'", "´");
				this._mov.SortValue = this.txtSortValue.Text.Replace("'", "´");
				this._mov.Note = this.txtNote.Text.Trim().Replace("'", "´");
				this._mov.HasCover = this.chkHasCover.Checked;
				this._mov.IsOriginal = this.chkIsOriginal.Checked;
				this._mov.IsConferred = this.chkIsConferred.Checked;
				this._mov.DiscAmount = this.txtDiscAmount.Text.Replace("'", "´".Trim()).ToInt32();
				this._mov.ConferredTo = this.txtConferredTo.Text.Trim().Replace("'", "´");
				this._mov.Quality = (Quality)this.tbQuality.Value;
				this._mov.Codec = CodecHelper.GetCodecByString((string)this.cbCodec.SelectedItem);
				this._mov.Country = ( (Static)this.cbCountry.SelectedItem ).Value;
				this._mov.Language = ( (Static)this.cbLanguage.SelectedItem ).Value;

				try {
					if(this.txtYear.Text.Trim().Replace("'", "´") == null
					|| this.txtYear.Text.Trim().Replace("'", "´") == ""
					|| this.txtYear.Text.Trim().Replace("'", "´").Length < 4) {
						this._mov.Year = 0;
					}
					else {
						this._mov.Year = this.txtYear.Text.Trim()
							.Replace("'", "´")
							.CleanFromSpacesAndSpecialChars()
							.ToInt32();
					}
				}
				catch(Exception) {
					this._mov.Year = 0;
				}

				if(this.IsNewMovie) {
					this._db.SaveMovie(this._mov, SaveMethod.CreateNew);
				}
				else {
					this._db.SaveMovie(this._mov, SaveMethod.SaveChanges);
				}
			}
			catch(Exception ex) {
				ErrorHandler.DisplayErrorMessagebox(ex);

				result = false;
			}

			return result;
		}

		/// <summary>
		/// DEBUG: Throw a Exception
		/// </summary>
		private void _DEBUG_ThrowException() {
			throw new Exception("DEBUG Exception");
		}

		/// <summary>
		/// Check if a object is also in list
		/// </summary>
		/// <param name="mot"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		private bool _ObjectIsInListView(MovieObjectType mot, string id) {
			switch(mot) {
				case MovieObjectType.Genre:
					foreach(ListViewItem lvi in this.lvGenre.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Category:
					foreach(ListViewItem lvi in this.lvCategories.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Actor:
					foreach(ListViewItem lvi in this.lvActors.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Director:
					foreach(ListViewItem lvi in this.lvDirectors.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Producer:
					foreach(ListViewItem lvi in this.lvProducers.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Musician:
					foreach(ListViewItem lvi in this.lvMusician.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Cameraman:
					foreach(ListViewItem lvi in this.lvCameraman.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Cutter:
					foreach(ListViewItem lvi in this.lvCutter.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Writer:
					foreach(ListViewItem lvi in this.lvWriters.Items) {
						if(lvi.Name == id) {
							return true;
						}
					}
					break;
			}

			return false;
		}

		/// <summary>
		/// Check if a object is also in the movie list
		/// </summary>
		/// <param name="mot"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool _ObjectIsInMovieList(MovieObjectType mot, string id) {
			switch(mot) {
				case MovieObjectType.Genre:
					foreach(Genre g in this._mov.Genres) {
						if(g.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Category:
					foreach(Category c in this._mov.Categories) {
						if(c.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Actor:
					foreach(Person p in this._mov.Actors) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Director:
					foreach(Person p in this._mov.Directors) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Producer:
					foreach(Person p in this._mov.Producers) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Musician:
					foreach(Person p in this._mov.Musicians) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Cameraman:
					foreach(Person p in this._mov.Cameramans) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Cutter:
					foreach(Person p in this._mov.Cutters) {
						if(p.ID == id) {
							return true;
						}
					}
					break;

				case MovieObjectType.Writer:
					foreach(Person p in this._mov.Writers) {
						if(p.ID == id) {
							return true;
						}
					}
					break;
			}

			return false;
		}

		/// <summary>
		/// Get the index of a object in the movie list
		/// </summary>
		/// <param name="mot"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public int _GetObjectIndexFromMovieList(MovieObjectType mot, string id) {
			int index = 0;

			switch(mot) {
				case MovieObjectType.Genre:
					foreach(Genre g in this._mov.Genres) {
						if(g.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Category:
					foreach(Category c in this._mov.Categories) {
						if(c.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Actor:
					foreach(Person p in this._mov.Actors) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Director:
					foreach(Person p in this._mov.Directors) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Producer:
					foreach(Person p in this._mov.Producers) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Musician:
					foreach(Person p in this._mov.Musicians) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Cameraman:
					foreach(Person p in this._mov.Cameramans) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Cutter:
					foreach(Person p in this._mov.Cutters) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;

				case MovieObjectType.Writer:
					foreach(Person p in this._mov.Writers) {
						if(p.ID == id) {
							return index;
						}

						index++;
					}
					break;
			}

			return index;
		}
	}
}