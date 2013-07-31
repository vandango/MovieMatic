using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation;

namespace Toenda.MovieMatic {
	public partial class AddItemForm : Form {
		private DataHandler _db = new DataHandler(Configuration.ConnectionString);
		private StaticHandler _sh = new StaticHandler(Configuration.ConnectionString);

		private MovieObjectType _mot;

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="mot"></param>
		public AddItemForm(MovieObjectType mot) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._mot = mot;

			switch(this._mot) {
				case MovieObjectType.Actor:
					this.gbActor.Visible = true;
					this.lvItems.Height = 240;
					break;

				default:
					this.gbActor.Visible = false;
					this.lvItems.Height = 323;
					break;
			}

			this._LoadData();
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get the selected genre items
		/// </summary>
		public List<Genre> SelectedGenreItems {
			get {
				List<Genre> genres = new List<Genre>();

				switch(this._mot) {
					case MovieObjectType.Genre:
						foreach(ListViewItem lvi in this.lvItems.SelectedItems) {
							genres.Add(new Genre(lvi.Name, Tools.GetCleanGenreOrCategoryName(lvi.Text)));
						}
						break;

					case MovieObjectType.Actor:
						break;

					case MovieObjectType.Director:
						break;

					case MovieObjectType.Producer:
						break;
				}

				return genres;
			}
		}

		/// <summary>
		/// Get the selected person items
		/// </summary>
		public List<Person> SelectedPersonItems {
			get {
				List<Person> person = new List<Person>();

				foreach(ListViewItem lvi in this.lvItems.SelectedItems) {
					person.Add(this._db.GetPerson(lvi.Name));
				}

				if(this._mot == MovieObjectType.Actor
				&& person.Count == 1) {
					person[0].Rolename = this.txtRoleName.Text;

					if(this.cbRoleType.SelectedIndex > 0) {
						person[0].Roletype = ( (Static)this.cbRoleType.SelectedItem ).Value;
					}
				}

				return person;
			}
		}

		/// <summary>
		/// Gets the selected category items.
		/// </summary>
		/// <value>The selected category items.</value>
		public List<Category> SelectedCategoryItems {
			get {
				List<Category> categories = new List<Category>();

				foreach(ListViewItem lvi in this.lvItems.SelectedItems) {
					categories.Add(new Category(lvi.Name, Tools.GetCleanGenreOrCategoryName(lvi.Text)));
				}

				return categories;
			}
		}

		// -------------------------------------------------------
		// EVENTS
		// -------------------------------------------------------

		/// <summary>
		/// Handles the ItemSelectionChanged event of the lvItems control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.ListViewItemSelectionChangedEventArgs"/> instance containing the event data.</param>
		private void lvItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if(this._mot == MovieObjectType.Actor
			&& this.lvItems.SelectedItems.Count == 1) {
				this.gbActor.Visible = true;
				this.lvItems.Height = 240;
			}
			else {
				this.gbActor.Visible = false;
				this.lvItems.Height = 323;
			}
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
		/// btnCancel_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// lvGenres_DoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvItems_DoubleClick(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		/// <summary>
		/// btnAddItem_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddItem_Click(object sender, EventArgs e) {
			switch(this._mot) {
				case MovieObjectType.Genre:
					AdministerGenresForm agf = new AdministerGenresForm();

					if(agf.ShowDialog(this) == DialogResult.OK) {
						this._LoadData();
					}
					break;

				case MovieObjectType.Actor:
				case MovieObjectType.Director:
				case MovieObjectType.Producer:
				case MovieObjectType.Musician:
				case MovieObjectType.Cameraman:
				case MovieObjectType.Cutter:
				case MovieObjectType.Writer:
					AdministerPersonsForm apf = new AdministerPersonsForm(true);

					DialogResult dr = apf.ShowDialog(this);
					this._LoadData();
					break;

				case MovieObjectType.Category:
					AdministerCategoriesForm acf = new AdministerCategoriesForm();

					if(acf.ShowDialog(this) == DialogResult.OK) {
						this._LoadData();
					}
					break;
			}
		}

		/// <summary>
		/// Handles the Shown event of the AddItemForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void AddItemForm_Shown(object sender, EventArgs e) {
			this.lvItems.Focus();
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Load the data
		/// </summary>
		private void _LoadData() {
			this.lvItems.Items.Clear();

			List<Genre> genres = new List<Genre>();
			List<Person> persons = new List<Person>();
			List<Category> categories = new List<Category>();
			//PersonCollection persons = new PersonCollection();

			switch(this._mot) {
				case MovieObjectType.Genre:
					this.Text = "Genre auswählen...";

					genres = this._db.GetGenreList();

					//genres.Sort((IComparer<Genre>)new Genre.SortByName());

					foreach(Genre g in genres) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = g.Name;
						lvi.Text += " (";
						lvi.Text += g.MovieCount.ToString();
						lvi.Text += ( g.MovieCount == 1 ? " Film" : " Filme" );
						lvi.Text += ")";

						lvi.Name = g.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Actor:
					this.Text = "Schauspieler auswählen...";

					// load role type
					List<Static> roleTypes = this._sh.GetStaticItemList("RT01");
					this.cbRoleType.DisplayMember = "Content";
					this.cbRoleType.ValueMember = "Value";
					this.cbRoleType.DataSource = roleTypes;
					this.cbRoleType.SelectedIndex = 0;

					persons = this._db.GetPersonList(MovieObjectType.Actor);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Director:
					this.Text = "Regisseur auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Director);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Producer:
					this.Text = "Produzent auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Producer);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Musician:
					this.Text = "Musiker auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Musician);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Cameraman:
					this.Text = "Kameramann auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Cameraman);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Cutter:
					this.Text = "Cutter auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Cutter);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Writer:
					this.Text = "Drehbuch Autor auswählen...";

					persons = this._db.GetPersonList(MovieObjectType.Writer);

					//persons.Sort((IComparer<Person>)new Person.SortByFirstname());

					foreach(Person p in persons) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Firstname + " " + p.Lastname;
						lvi.Name = p.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;

				case MovieObjectType.Category:
					this.Text = "Kategory auswählen...";

					categories = this._db.GetCategoryList();

					//genres.Sort((IComparer<Genre>)new Genre.SortByName());

					foreach(Category c in categories) {
						ListViewItem lvi = new ListViewItem();
						lvi.Text = c.Name;
						lvi.Text += " (";
						lvi.Text += c.MovieCount.ToString();
						lvi.Text += ( c.MovieCount == 1 ? " Film" : " Filme" );
						lvi.Text += ")";

						lvi.Name = c.ID;

						this.lvItems.Items.Add(lvi);
					}
					break;
			}
		}
	}
}