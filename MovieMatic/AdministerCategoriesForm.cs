using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation;
using Toenda.Foundation.Utility;

namespace Toenda.MovieMatic {
	public partial class AdministerCategoriesForm : Form {
		private DataHandler _db = new DataHandler(Configuration.ConnectionString);

		/// <summary>
		/// Initializes a new instance of the <see cref="AdministerCategoriesForm"/> class.
		/// </summary>
		public AdministerCategoriesForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._LoadData();
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get the selected items
		/// </summary>
		public List<Category> SelectedItems {
			get {
				List<Category> categories = new List<Category>();

				foreach(ListViewItem lvi in this.lvCategories.SelectedItems) {
					categories.Add(new Category(lvi.Name, lvi.Text));
				}

				return categories;
			}
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
					"MovieMatic Fehler"
				);
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
		/// lvGenres_ItemSelectionChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGenres_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			this.txtName.Text = e.Item.Text;
			this.txtName.Tag = e.ItemIndex;
		}

		/// <summary>
		/// txtName_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtName_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 13) {
				if(this.txtName.Text.Trim() != "") {
					if(this.txtName.Tag != null
					&& this.txtName.Tag.ToString().Trim() != ""
					&& this.txtName.Tag.ToString().Trim().IsAlphaNumeric()) {
						this.lvCategories.Items[Convert.ToInt32(this.txtName.Tag)].Text = this.txtName.Text;
					}
					else {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = this.txtName.Text;

						this.lvCategories.Items.Add(lvi);
					}
				}

				this.txtName.Text = "";
				this.txtName.Tag = "";
			}
		}

		/// <summary>
		/// btnNew_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNew_Click(object sender, EventArgs e) {
			if(this.txtName.Text.Trim() != "") {
				ListViewItem lvi = new ListViewItem();
				lvi.Text = this.txtName.Text;

				this.lvCategories.Items.Add(lvi);

				this.txtName.Text = "";
				this.txtName.Tag = "";
			}
		}

		/// <summary>
		/// lvGenres_KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGenres_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 46
			&& this.lvCategories.SelectedItems.Count != 0) {
				ListView.SelectedListViewItemCollection sic = this.lvCategories.SelectedItems;

				foreach(ListViewItem lvi in sic) {
					this.lvCategories.Items.Remove(lvi);
				}
			}
		}

		/// <summary>
		/// lvGenres_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGenres_MouseDown(object sender, MouseEventArgs e) {
			this.txtName.Text = "";
			this.txtName.Tag = "";
		}

		/// <summary>
		/// lvGenres_MouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGenres_MouseUp(object sender, MouseEventArgs e) {
			if(this.lvCategories.SelectedItems.Count == 0) {
				this.txtName.Text = "";
				this.txtName.Tag = "";
			}
		}

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Load the data
		/// </summary>
		private void _LoadData() {
			List<Category> list = this._db.GetCategoryList();

			list.Sort((IComparer<Category>)new Category.SortByName());

			foreach(Category c in list) {
				ListViewItem lvi = new ListViewItem();
				lvi.Text = c.Name;
				lvi.Text += " (";
				lvi.Text += c.MovieCount.ToString();
				lvi.Text += ( c.MovieCount == 1 ? " Film" : " Filme" );
				lvi.Text += ")";

				lvi.Name = c.ID;

				this.lvCategories.Items.Add(lvi);
			}

			this.lblFound.Text = list.Count.ToString();
		}

		/// <summary>
		/// Save the data
		/// </summary>
		private void _SaveData() {
			List<Category> list = new List<Category>();

			foreach(ListViewItem lvi in this.lvCategories.Items) {
				list.Add(
					new Category(
						lvi.Name,
						lvi.Text
					)
				);
			}

			this._db.SaveCategoryList(list, false);
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------
	}
}
