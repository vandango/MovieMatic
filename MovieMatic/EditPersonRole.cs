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
	public partial class EditPersonRole : Form {
		private DataHandler _db = new DataHandler(
			Configuration.ConnectionString
		);
		private StaticHandler _sh = new StaticHandler(
			Configuration.ConnectionString
		);
		private string _movieId;

		public string RoleName { get; set; }
		public string RoleType { get; set; }
		public string RoleTypeName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EditPersonRole"/> class.
		/// </summary>
		/// <param name="personId">The person id.</param>
		/// <param name="personName">Name of the person.</param>
		/// <param name="roleName">Name of the role.</param>
		/// <param name="roleType">Type of the role.</param>
		public EditPersonRole(string movieId, string personId, string personName, string roleName, string roleType) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			this._movieId = movieId;

			// load role type
			List<Static> roleTypes = this._sh.GetStaticItemList("RT01");
			this.cbRoleType.DisplayMember = "Content";
			this.cbRoleType.ValueMember = "Value";
			this.cbRoleType.DataSource = roleTypes;
			this.cbRoleType.SelectedIndex = 0;

			// load data
			this.lblActorName.Text = personName;
			this.lblActorName.Tag = personId;

			this.txtRoleName.Text = roleName;

			if(!roleType.IsNullOrTrimmedEmpty()) {
				this.cbRoleType.SelectedValue = (object)roleType;
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
			this.RoleName = this.txtRoleName.Text;
			
			if(this.cbRoleType.SelectedIndex > 0) {
				this.RoleType = ( (Static)this.cbRoleType.SelectedItem ).Value;
				this.RoleTypeName = ( (Static)this.cbRoleType.SelectedItem ).Content;
			}

			// save data
			this._db.UpdatePersonRole(
				this._movieId,
				this.lblActorName.Tag.ToString(),
				this.RoleName,
				this.RoleType
			);

			// return to parent
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		/// <summary>
		/// Handles the Shown event of the EditPersonRole control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void EditPersonRole_Shown(object sender, EventArgs e) {
			this.txtRoleName.Focus();
		}
	}
}
