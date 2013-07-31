using System;
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
	public partial class PersonForm : Form {
		private DataHandler _db = new DataHandler(
			Configuration.ConnectionString
		);

		private Person _person = new Person();

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="name">The name.</param>
		public PersonForm(string name) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._LoadData(false);

			name = name.Trim();

			if(name.Contains(" ") && name.IndexOf(" ") > 0) {
				this.txtFirstname.Text = name.Substring(0, name.LastIndexOf(" ")).Trim();
				this.txtLastname.Text = name.Substring(name.LastIndexOf(" ")).Trim();
			}
			else {
				this.txtFirstname.Text = name;
			}

			this.chkIsActor.Checked = true;
		}

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="forEditing">if set to <c>true</c> [for editing].</param>
		public PersonForm(string id, bool forEditing) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._person = this._db.GetPerson(id);

			this._LoadData(forEditing);
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get a value that indicates that this person is a new person
		/// </summary>
		public bool IsNewPerson {
			get {
				if(this.txtFirstname.Tag == null
				|| this.txtFirstname.Tag.ToString().Trim() == "") {
					return true;
				}
				else {
					return false;
				}
			}
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
		/// Handles the Shown event of the PersonForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void PersonForm_Shown(object sender, EventArgs e) {
			this.txtFirstname.Focus();
		}

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Load the data
		/// </summary>
		/// <param name="forEditing"></param>
		private void _LoadData(bool forEditing) {
			if(forEditing) {
				this.txtFirstname.Tag = this._person.ID;
			}

			this.txtFirstname.Text = this._person.Firstname;
			this.txtLastname.Text = this._person.Lastname;
			this.chkIsActor.Checked = this._person.IsActor;
			this.chkIsDirector.Checked = this._person.IsDirector;
			this.chkIsProducer.Checked = this._person.IsProducer;
			this.chkIsMusician.Checked = this._person.IsMusician;
			this.chkIsCameraman.Checked = this._person.IsCameraman;
			this.chkIsCutter.Checked = this._person.IsCutter;
			this.chkIsWriter.Checked = this._person.IsWriter;
		}

		/// <summary>
		/// Save the data
		/// </summary>
		private void _SaveData() {
			this._person.Firstname = this.txtFirstname.Text.Trim().Replace("'", "´");
			this._person.Lastname = this.txtLastname.Text.Trim().Replace("'", "´");
			this._person.IsActor = this.chkIsActor.Checked;
			this._person.IsDirector = this.chkIsDirector.Checked;
			this._person.IsProducer = this.chkIsProducer.Checked;
			this._person.IsMusician = this.chkIsMusician.Checked;
			this._person.IsCameraman = this.chkIsCameraman.Checked;
			this._person.IsCutter = this.chkIsCutter.Checked;
			this._person.IsWriter = this.chkIsWriter.Checked;

			if(this.IsNewPerson) {
				this._db.SavePerson(this._person, SaveMethod.CreateNew);
			}
			else {
				this._db.SavePerson(this._person, SaveMethod.SaveChanges);
			}
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------
	}
}