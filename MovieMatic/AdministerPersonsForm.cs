using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation;
using Toenda.Foundation.Types;
using System.Threading;

namespace Toenda.MovieMatic {
	public partial class AdministerPersonsForm : Form {
		private DataHandler _db = new DataHandler(Configuration.ConnectionString);

		private bool _openForNew = false;
		private int _currentAmount = 0;
		private bool _startSearch = false;
		private int _current_amount = 0;
		private string _selectedPerson = "";

		private ListSortDirection _sortDirection = ListSortDirection.Ascending;
		private string _sortExpression = "fullname";

		private bool __useDataBind = true;

		delegate void LoadDataCallback(MovieObjectType type, string name);

		private System.Timers.Timer _searchTimer = null;
		private MovieObjectType _searchType = MovieObjectType.All;
		private string _searchName = "";

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="openForNew"></param>
		public AdministerPersonsForm(bool openForNew) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._openForNew = openForNew;

			this._searchTimer = new System.Timers.Timer();
			this._searchTimer.Elapsed += new ElapsedEventHandler(_searchTimer_Elapsed);
			this._searchTimer.Interval = 1;
			this._searchTimer.Start();

			this.txtName.Focus();
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get the selected person
		/// </summary>
		public string SelectedPerson {
			get { return this._selectedPerson; }
		}

		/// <summary>
		/// Gets or sets the type of the selected person.
		/// </summary>
		/// <value>The type of the selected person.</value>
		public FilterType SelectedPersonType { get; set; }

		// -------------------------------------------------------
		// EVENTS
		// -------------------------------------------------------

		/// <summary>
		/// Handles the RunWorkerCompleted event of the loadDataAsync control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
		private void loadDataAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			this._LoadData(this._searchType, this._searchName);
		}

		/// <summary>
		/// Handles the Elapsed event of the _searchTimer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
		protected void _searchTimer_Elapsed(object sender, ElapsedEventArgs e) {
			this._searchTimer.Stop();
			
			if(this.loadDataAsync.IsBusy) {
				while(this.loadDataAsync.IsBusy) {
					Thread.Sleep(12);
				}
			}

			this.loadDataAsync.RunWorkerAsync();
		}

		/// <summary>
		/// btnCancel_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// btnAddPerson_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddPerson_Click(object sender, EventArgs e) {
			this._OpenPersonEditor(this.txtName.Text);
		}

		/// <summary>
		/// AdministerPersonsForm_Shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AdministerPersonsForm_Shown(object sender, EventArgs e) {
			if(this._openForNew) {
				this._openForNew = true;
				this._OpenPersonEditor("");
			}
			else {
				this.txtName.Focus();
			}
		}

		/// <summary>
		/// dgvPersons_CellDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvPersons_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			if(e.RowIndex >= 0
			&& e.RowIndex < this._currentAmount) {
				this._openForNew = false;
				this._OpenPersonEditor(
					true, 
					this.dgvPersons.Rows[e.RowIndex].Cells[0].Value.ToString(), 
					""
				);
			}
			else if(e.RowIndex >= 0) {
				this._openForNew = true;
				this._OpenPersonEditor("");
			}
		}

		/// <summary>
		/// Handles the CellClick event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void dgvPersons_CellClick(object sender, DataGridViewCellEventArgs e) {
			//this._selectedPerson = this.dgvPersons.Rows[e.RowIndex].Cells[1].Value.ToString();
		}

		/// <summary>
		/// dgvPersons_UserDeletingRow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvPersons_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
			if(MessageBox.Show(
				"Wollen sie die Person wirklich löschen?",
				"MovieMatic",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			) == DialogResult.No) {
				e.Cancel = true;
			}
			else {
				this._db.DeletePerson(e.Row.Cells[0].Value.ToString());
			}
		}

		/// <summary>
		/// Handles the DataBindingComplete event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
		private void dgvPersons_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
			try {
				this.lblFound.Text = this._current_amount.ToString();
			}
			catch(Exception) {
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkActor control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkActor_CheckedChanged(object sender, EventArgs e) {
			if(this.chkActor.Checked) {
				this._startSearch = false;

				this.chkDirector.Checked = false;
				this.chkProducer.Checked = false;
				this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Actor, "");

					this._searchType = MovieObjectType.Actor;
					this._searchName = "";
					
					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkDirector control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkDirector_CheckedChanged(object sender, EventArgs e) {
			if(this.chkDirector.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkProducer.Checked = false;
				this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Director, "");

					this._searchType = MovieObjectType.Director;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkProducer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkProducer_CheckedChanged(object sender, EventArgs e) {
			if(this.chkProducer.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkDirector.Checked = false;
				this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Producer, "");

					this._searchType = MovieObjectType.Producer;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkMusician control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkMusician_CheckedChanged(object sender, EventArgs e) {
			if(this.chkMusician.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkDirector.Checked = false;
				this.chkProducer.Checked = false;
				//this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Producer, "");

					this._searchType = MovieObjectType.Musician;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkCameraman control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkCameraman_CheckedChanged(object sender, EventArgs e) {
			if(this.chkCameraman.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkDirector.Checked = false;
				this.chkProducer.Checked = false;
				this.chkMusician.Checked = false;
				//this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Producer, "");

					this._searchType = MovieObjectType.Cameraman;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkCutter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkCutter_CheckedChanged(object sender, EventArgs e) {
			if(this.chkCutter.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkDirector.Checked = false;
				this.chkProducer.Checked = false;
				this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				//this.chkCutter.Checked = false;
				this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Producer, "");

					this._searchType = MovieObjectType.Cutter;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the CheckedChanged event of the chkWriter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void chkWriter_CheckedChanged(object sender, EventArgs e) {
			if(this.chkWriter.Checked) {
				this._startSearch = false;

				this.chkActor.Checked = false;
				this.chkDirector.Checked = false;
				this.chkProducer.Checked = false;
				this.chkMusician.Checked = false;
				this.chkCameraman.Checked = false;
				this.chkCutter.Checked = false;
				//this.chkWriter.Checked = false;

				this._startSearch = true;

				if(this._startSearch) {
					//this._LoadData(MovieObjectType.Producer, "");

					this._searchType = MovieObjectType.Writer;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
			else {
				if(this._startSearch) {
					//this._LoadData(MovieObjectType.All, "");

					this._searchType = MovieObjectType.All;
					this._searchName = "";

					this._searchTimer.Interval = 1;
					this._searchTimer.Start();
				}
			}
		}

		/// <summary>
		/// Handles the TextChanged event of the txtName control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void txtName_TextChanged(object sender, EventArgs e) {
			this._LoadingInit();
		}

		/// <summary>
		/// Handles the ColumnHeaderMouseClick event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
		private void dgvPersons_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			switch(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName) {
				case "Fullname":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantity":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsActor":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsDirector":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsProducer":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsMusician":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsCameraman":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsCutter":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				case "MovieQuantityAsWriter":
					this._SetSortSymbol(e.ColumnIndex);
					this._SetSortParams(this.dgvPersons.Columns[e.ColumnIndex].DataPropertyName);
					this._LoadingInit();
					break;

				default:
					this._ResetSortSymbols();
					break;
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the txtName control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void txtName_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyData == (Keys)Shortcut.CtrlN
			|| e.KeyData == (Keys)Shortcut.CtrlH) {
				this._OpenPersonEditor(this.txtName.Text);
			}
		}

		/// <summary>
		/// Handles the KeyDown event of the AdministerPersonsForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void AdministerPersonsForm_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyData == (Keys)Shortcut.CtrlN
			|| e.KeyData == (Keys)Shortcut.CtrlH) {
				this._OpenPersonEditor(this.txtName.Text);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnHelp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnHelp_Click(object sender, EventArgs e) {
			MessageBox.Show(
				"Filter:"
				+ "\nSuchanfragen werden bei Eingabe des Namens sofort ausgeführt."
				+ "\nDie Checkboxen können de/aktiviert werden um die Auswahl auf die jeweilige Position einzuschränken."
				+ "\n"
				+ "\nAktion:"
				+ "\nSTRG+H oder STRG+N drücken, um Personeneditor mit aktuellem Namen zu öffnen.",
				"MovieMatic"
			);
		}

		/// <summary>
		/// Handles the SelectionChanged event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void dgvPersons_SelectionChanged(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvPersons.SelectedRows;

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
		/// Handles the RowContextMenuStripNeeded event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs"/> instance containing the event data.</param>
		private void dgvPersons_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e) {
			this.dgvPersons.Rows[e.RowIndex].Selected = true;
			this._selectedPerson = this.dgvPersons.Rows[e.RowIndex].Cells[1].Value.ToString();

			this.cmsGridMenu.Items[2].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[2].Value;
			this.cmsGridMenu.Items[3].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[3].Value;
			this.cmsGridMenu.Items[4].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[4].Value;
			this.cmsGridMenu.Items[5].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[5].Value;
			this.cmsGridMenu.Items[6].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[6].Value;
			this.cmsGridMenu.Items[7].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[7].Value;
			this.cmsGridMenu.Items[8].Enabled = (bool)this.dgvPersons.Rows[e.RowIndex].Cells[8].Value;
		}

		/// <summary>
		/// editCmsItem_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editCmsItem_Click(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvPersons.SelectedRows;

			if(dgvsrc != null && dgvsrc.Count > 0) {
				if(dgvsrc[0].Index >= 0
				&& dgvsrc[0].Index < this._current_amount) {
					this._openForNew = false;
					this._OpenPersonEditor(
						true,
						this.dgvPersons.Rows[dgvsrc[0].Index].Cells[0].Value.ToString(),
						""
					);
				}
				else if(dgvsrc[0].Index >= 0) {
					this._OpenPersonEditor(this.txtName.Text);
					this._LoadingInit();
				}
			}
		}

		/// <summary>
		/// deleteCmsItem_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteCmsItem_Click(object sender, EventArgs e) {
			DataGridViewSelectedRowCollection dgvsrc = this.dgvPersons.SelectedRows;

			if(dgvsrc != null && dgvsrc.Count > 0) {
				if(MessageBox.Show(
					"Wollen sie die Person wirklich löschen?",
					"MovieMatic",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				) == DialogResult.Yes) {
					this._db.DeletePerson(this.dgvPersons.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
					this._LoadingInit();
				}
			}
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisActorToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisActorToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Actor;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisDirectorToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisDirectorToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Director;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisProducerToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisProducerToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Producer;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisMusicianToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisMusicianToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Musician;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisCameramanToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisCameramanToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Cameraman;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisCutterToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisCutterToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Cutter;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the Click event of the moviesWithThisWriterToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void moviesWithThisWriterToolStripMenuItem_Click(object sender, EventArgs e) {
			this.loadDataAsync.WorkerSupportsCancellation = true;
			this.loadDataAsync.CancelAsync();
			this._searchTimer.Stop();

			this.SelectedPersonType = FilterType.Writer;

			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		/// <summary>
		/// Handles the KeyUp event of the dgvPersons control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void dgvPersons_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Delete) {
				DataGridViewSelectedRowCollection dgvsrc = this.dgvPersons.SelectedRows;

				if(dgvsrc != null && dgvsrc.Count > 0) {
					if(MessageBox.Show(
						string.Format("Wollen sie die Person{0} wirklich löschen?", ( dgvsrc.Count > 1 ? "en" : "")),
						"MovieMatic",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					) == DialogResult.Yes) {
						foreach(DataGridViewRow row in dgvsrc) {
							//this._db.DeletePerson(this.dgvPersons.Rows[dgvsrc[0].Index].Cells[0].Value.ToString());
							this._db.DeletePerson(row.Cells[0].Value.ToString());
						}

						this._LoadingInit();
					}
				}
			}
		}

		#region PRIVATE MEMBERS
		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Sets the sort symbol for the header of a column.
		/// </summary>
		/// <param name="columnIndex">Index of the column.</param>
		private void _SetSortSymbol(int columnIndex) {
			this._ResetSortSymbols();

			DataGridViewColumn col = this.dgvPersons.Columns[columnIndex];
			string symbolAsc = ( columnIndex == 1 ? "(a-z)" : "");
			string symbolDesc = ( columnIndex == 1 ? "(z-a)" : "" );

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
			foreach(DataGridViewColumn col in this.dgvPersons.Columns) {
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
		/// Loadings the init.
		/// </summary>
		private void _LoadingInit() {
			if(!this.chkActor.Checked
			&& !this.chkDirector.Checked
			&& !this.chkProducer.Checked) {
				//this._LoadData(
				//    MovieObjectType.All,
				//    this.txtName.Text.Trim()
				//);

				this._searchType = MovieObjectType.All;
			}
			else {
				if(this.chkActor.Checked) {
					//this._LoadData(
					//    MovieObjectType.Actor,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Actor;
				}
				else if(this.chkDirector.Checked) {
					//this._LoadData(
					//    MovieObjectType.Director,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Director;
				}
				else if(this.chkProducer.Checked) {
					//this._LoadData(
					//    MovieObjectType.Producer,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Producer;
				}
				else if(this.chkMusician.Checked) {
					//this._LoadData(
					//    MovieObjectType.Producer,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Musician;
				}
				else if(this.chkCameraman.Checked) {
					//this._LoadData(
					//    MovieObjectType.Producer,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Cameraman;
				}
				else if(this.chkCutter.Checked) {
					//this._LoadData(
					//    MovieObjectType.Producer,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Cutter;
				}
				else if(this.chkWriter.Checked) {
					//this._LoadData(
					//    MovieObjectType.Producer,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.Writer;
				}
				else {
					//this._LoadData(
					//    MovieObjectType.All,
					//    this.txtName.Text.Trim()
					//);

					this._searchType = MovieObjectType.All;
				}
			}

			this._searchName = this.txtName.Text.Trim();

			int intervall = 1;

			if(this._searchName.Length == 0) {
				intervall = 1;
			}
			else if(this._searchName.Length < 2) {
				intervall = 1000;
			}
			else if(this._searchName.Length < 4) {
				intervall = 750;
			}
			else if(this._searchName.Length < 6) {
				intervall = 500;
			}
			else if(this._searchName.Length < 8) {
				intervall = 250;
			}
			else if(this._searchName.Length < 10) {
				intervall = 100;
			}
			else {
				intervall = 1;
			}

			this._searchTimer.Interval = intervall;
			this._searchTimer.Start();
		}

		/// <summary>
		/// Open the person editor
		/// </summary>
		private void _OpenPersonEditor(string name) {
			this._OpenPersonEditor(false, "", name);
		}

		/// <summary>
		/// Open the person editor
		/// </summary>
		/// <param name="forEditing"></param>
		/// <param name="id"></param>
		private void _OpenPersonEditor(bool forEditing, string id, string name) {
			PersonForm pf;

			if(forEditing) {
				pf = new PersonForm(id, true);
			}
			else {
				pf = new PersonForm(name);
			}

			int x = (this.Location.X
				+ this.btnAddPerson.Location.X
				+ this.btnAddPerson.Width
				+ 16) - pf.Width;

			int y = this.Location.Y
				+ this.btnAddPerson.Location.Y
				+ this.btnAddPerson.Height
				+ 35;

			pf.StartPosition = FormStartPosition.Manual;
			pf.Location = new Point(x, y);

			if(pf.ShowDialog(this) == DialogResult.OK) {
				//this._LoadData(MovieObjectType.All, "");
				this._LoadingInit();
			}

			this.txtName.Focus();
		}

		/// <summary>
		/// Load the data
		/// </summary>
		/// <param name="mot">The mot.</param>
		/// <param name="name">The name.</param>
		private void _LoadData(MovieObjectType mot, string name) {
			if(this.dgvPersons.InvokeRequired) {
				LoadDataCallback ldCall = new LoadDataCallback(_LoadData);
				this.Invoke(ldCall, new object[] { mot, name });
			}
			else {
				this.Cursor = Cursors.WaitCursor;

				if(__useDataBind) {
					this.dgvPersons.DataSource = null;
				}
				else {
					this.dgvPersons.Rows.Clear();
				}

				name = name.Replace("'", "´");

				List<Person> list = this._db.GetPersonList(
					mot, 
					name,
					this._sortExpression,
					( this._sortDirection == ListSortDirection.Ascending ? DataSortDirection.Ascending : DataSortDirection.Descending )
				);
				//PersonCollection list = this._db.GetPersonList(mot, name);

				//list.Sort((IComparer<Person>)new Person.SortByFirstname());

				this._currentAmount = list.Count;
				int count = 0;

				if(__useDataBind) {
					this.dgvPersons.AutoGenerateColumns = false;
					this.dgvPersons.DataSource = list;
				}
				else {
					foreach(Person p in list) {
						this.dgvPersons.Rows.Add(1);

						this.dgvPersons.Rows[count].Cells[0].Value = p.ID;
						this.dgvPersons.Rows[count].Cells[1].Value = p.Firstname + " " + p.Lastname;
						this.dgvPersons.Rows[count].Cells[2].Value = p.IsActor;
						this.dgvPersons.Rows[count].Cells[3].Value = p.IsDirector;
						this.dgvPersons.Rows[count].Cells[4].Value = p.IsProducer;
						this.dgvPersons.Rows[count].Cells[4].Value = p.IsMusician;
						this.dgvPersons.Rows[count].Cells[4].Value = p.IsCameraman;
						this.dgvPersons.Rows[count].Cells[4].Value = p.IsCutter;
						this.dgvPersons.Rows[count].Cells[4].Value = p.IsWriter;
						this.dgvPersons.Rows[count].Cells[5].Value = p.MovieQuantity;
						this.dgvPersons.Rows[count].Cells[6].Value = p.MovieQuantityAsActor;
						this.dgvPersons.Rows[count].Cells[7].Value = p.MovieQuantityAsDirector;
						this.dgvPersons.Rows[count].Cells[8].Value = p.MovieQuantityAsProducer;
						this.dgvPersons.Rows[count].Cells[8].Value = p.MovieQuantityAsMusician;
						this.dgvPersons.Rows[count].Cells[8].Value = p.MovieQuantityAsCameraman;
						this.dgvPersons.Rows[count].Cells[8].Value = p.MovieQuantityAsCutter;
						this.dgvPersons.Rows[count].Cells[8].Value = p.MovieQuantityAsWriter;

						count++;
					}

					this.dgvPersons.Sort(this.dgvPersons.Columns[1], ListSortDirection.Ascending);
				}

				this._current_amount = list.Count;
				this.lblFound.Text = this._current_amount.ToString();

				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Save the data
		/// </summary>
		private void _SaveData() {
			//List<Genre> list = new List<Genre>();

			//foreach(ListViewItem lvi in this.lvGenres.Items) {
			//    list.Add(
			//        new Genre(
			//            lvi.Name,
			//            lvi.Text
			//        )
			//    );
			//}

			//this._db.SaveGenreList(list, false);
		}

#endregion

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------
	}
}