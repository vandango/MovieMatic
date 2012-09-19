using System;
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
	/// Class DatabaseRestoreForm
	/// </summary>
	public partial class DatabaseRestoreForm : Form {
		private BackupRestoreHandler _brh;
		
		private bool _load;
		private ListViewItem _selected_item;

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public DatabaseRestoreForm() {
			InitializeComponent();

			this._brh = new BackupRestoreHandler(Configuration.ConnectionString);

			this._selected_item = null;
			this._load = true;
			this.DialogResult = DialogResult.Cancel;

			this.pbProcess.Minimum = 0;
			this.pbProcess.Maximum = 100;

			this._brh.RestorePercentEvent += new BackupRestoreHandler.RestorePercentEventHandler(_brh_RestorePercentEvent);
			this._brh.RestoreCompleteEvent += new BackupRestoreHandler.RestoreCompleteEventHandler(_brh_RestoreCompleteEvent);

			this._brh.SavePath = Configuration.CommonApplicationTempPath;

			this.chkDeleteBeforeRestore.Checked = false;
			this.chkReplace.Checked = true;
			this.txtPath.Text = Configuration.CommonApplicationTempPath;
			this.fbDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			this.btnRestore.Enabled = false;

			this._LoadRestoreItems();
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
		/// DatabaseRestoreForm_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DatabaseRestoreForm_Load(object sender, EventArgs e) {
			if(!this._load) {
				this.Close();
			}
		}

		/// <summary>
		/// _brh_RestorePercentEvent
		/// </summary>
		/// <param name="percentValue"></param>
		private void _brh_RestorePercentEvent(int percentValue) {
			if(this.pbProcess.Value < this.pbProcess.Maximum) {
				this.pbProcess.Value = percentValue;
			}
		}

		/// <summary>
		/// _brh_RestoreCompleteEvent
		/// </summary>
		/// <param name="error"></param>
		private void _brh_RestoreCompleteEvent(System.Data.SqlClient.SqlError error) {
			if(error != null 
			&& error.Class != 0) {
				MessageBox.Show("ERROR: " + error.Message, "Datenbank Sicherung");
			}
			else {
				this.btnRestore.Enabled = false;
				this.pbProcess.Value = 0;

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

				if(FileSystem.Self.IsPathIsDirectoryName(this.txtPath.Text.Trim() + "\\")) {
					//this._brh.SavePath = this.txtPath.Text.Trim() + "\\";
					this._LoadRestoreItems();
				}
				else {
					MessageBox.Show(
						"Der angegebene Pfad ist nicht korrekt!",
						"MovieMatic"
					);
				}
			}
		}

		/// <summary>
		/// lvRestores_ItemSelectionChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvRestores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if(e.IsSelected) {
				this._selected_item = e.Item;
				this.btnRestore.Enabled = true;
			}
			else {
				this._selected_item = null;
				this.btnRestore.Enabled = false;
			}
		}

		/// <summary>
		/// btnRestore_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRestore_Click(object sender, EventArgs e) {
			if(this._selected_item != null) {
				this._brh.SavePath = FileSystem.Self.GetDirectoryNameFromPath(this.txtPath.Text.Trim() + "\\");

				this._brh.RestoreDatabase(
					this._selected_item.Name,
					this.chkDeleteBeforeRestore.Checked,
					this.chkReplace.Checked,
					false
				);
			}
		}

		/// <summary>
		/// txtPath_TextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtPath_TextChanged(object sender, EventArgs e) {
			if(this.txtPath.Text.Trim() != ""
			&& this.txtPath.Text.Trim().Length > 3) {
				if(FileSystem.Self.IsPathIsDirectoryName(this.txtPath.Text.Trim() + "\\")) {
					//this._brh.SavePath = this.txtPath.Text.Trim() + "\\";
					this._LoadRestoreItems();
				}
			}
		}

		/// <summary>
		/// txtPath_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtPath_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 13) {
				if(this.txtPath.Text.Trim() != ""
				&& this.txtPath.Text.Trim().Length > 3) {
					if(FileSystem.Self.IsPathIsDirectoryName(this.txtPath.Text.Trim() + "\\")) {
						//this._brh.SavePath = this.txtPath.Text.Trim() + "\\";
						this._LoadRestoreItems();
					}
				}
			}
		}

		// ---------------------------------------------------
		// PRIVATE MEMBERS
		// ---------------------------------------------------

		/// <summary>
		/// Load the restore items
		/// </summary>
		private void _LoadRestoreItems() {
			List<RestoreItem> list = new List<RestoreItem>();

			try {
				list = this._brh.GetRestoreItemList();
			}
			catch(Exception ex) {
				string s = ex.Message;
				//MessageBox.Show(ex.Message, "Compareos ERROR");
				//this._load = false;
			}
			finally {
				this.lvRestores.Items.Clear();
			}

			foreach(RestoreItem ri in list) {
				ListViewItem lvi = new ListViewItem();
				lvi.Name = ri.Filename;
				lvi.Text = ri.Name;
				lvi.Tag = ri.Filename;

				ListViewItem.ListViewSubItem lviSub1 = new ListViewItem.ListViewSubItem();
				lviSub1.Name = ri.Filename;
				lviSub1.Text = ri.Date.ToString();

				lvi.SubItems.Add(lviSub1);

				ListViewItem.ListViewSubItem lviSub2 = new ListViewItem.ListViewSubItem();
				lviSub2.Name = ri.Filename;
				lviSub2.Text = ri.Filename;

				lvi.SubItems.Add(lviSub2);

				this.lvRestores.Items.Add(lvi);
			}

			this.lvRestores.Sorting = SortOrder.Descending;
			this.lvRestores.Sort();
		}

		// ---------------------------------------------------
		// PROTECTED MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PUBLIC MEMBERS
		// ---------------------------------------------------
	}
}