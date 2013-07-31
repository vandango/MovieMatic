using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMatic {
	public partial class DataSetPreviewForm : Form {
		private List<ColumnAllocation> _allocation = new List<ColumnAllocation>();

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public DataSetPreviewForm(DataSet data) {
			InitializeComponent();

			this.DialogResult = DialogResult.Cancel;

			// original columns
			List<string> orgColumns = new List<string>();
			orgColumns.Add("* KEINE ZUORDNUNG *");
			orgColumns.Add("number");
			orgColumns.Add("name");
			orgColumns.Add("note");
			orgColumns.Add("has_cover");
			orgColumns.Add("is_original");
			orgColumns.Add("is_conferred");
			orgColumns.Add("codec");
			orgColumns.Add("conferred_to");
			orgColumns.Add("disc_amount");
			orgColumns.Add("year");
			orgColumns.Add("country");
			orgColumns.Add("quality");
			orgColumns.Add("__GENRE");
			orgColumns.Add("__ACTOR");
			orgColumns.Add("__DIRECTOR");
			orgColumns.Add("__PRODUCER");

			int columns = data.Tables[0].Columns.Count;
			int cb_x_location = 3;
			int lbl_x_location = 3;
			int y_location = 3;
			int count = 0;

			foreach(DataColumn col in data.Tables[0].Columns) {
				DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
				c.HeaderText = col.Caption;
				c.Name = col.ColumnName;
				c.ValueType = col.DataType;

				this.dgView.Columns.Add(c);

				// add column-order controls
				TextBox txt = new TextBox();
				txt.ReadOnly = true;
				txt.Size = new Size(150, 21);
				txt.Location = new Point(3, y_location);
				txt.Text = col.Caption;
				txt.Tag = "txt_" + count.ToString();

				lbl_x_location = txt.Size.Width + 3;

				Label lbl = new Label();
				lbl.Text = "wird zugeordnet zu";
				lbl.Size = new Size(100, 21);
				lbl.Location = new Point(lbl_x_location, y_location + 2);

				cb_x_location = txt.Size.Width + 3 + lbl.Size.Width + 3;

				ComboBox cb = new ComboBox();
				cb.Tag = "cb_" + count.ToString();
				cb.DropDownStyle = ComboBoxStyle.DropDownList;
				cb.Size = new Size(150, 21);
				cb.Location = new Point(cb_x_location, y_location);

				foreach(string s in orgColumns) {
					cb.Items.Add(s);
				}

				cb.SelectedIndex = 0;

				y_location += 3 + cb.Size.Height;

				this.pColumns.Controls.Add(txt);
				this.pColumns.Controls.Add(lbl);
				this.pColumns.Controls.Add(cb);

				count++;
			}

			count = 0;

			foreach(DataRow row in data.Tables[0].Rows) {
				this.dgView.Rows.Add(1);

				for(int i = 0; i < columns; i++) {
					this.dgView.Rows[count].Cells[i].Value = row[i];
				}

				count++;
			}

			// add column-order controls
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROPERTIES
		// ---------------------------------------------------

		/// <summary>
		/// Get the columnallocation
		/// </summary>
		public List<ColumnAllocation> ColumnAllocation {
			get {
				int index = 0;

				foreach(Control ctrl in this.pColumns.Controls) {
					if(ctrl.GetType().Name == "TextBox") {
						if(!this._CheckSelectedItemFromControlRow(index)) {
							ColumnAllocation ca = new ColumnAllocation();

							ca.OriginalColumn = this._GetSelectedItemFromControlRow(index);
							ca.AllocatedColumn = ((TextBox)ctrl).Text;

							this._allocation.Add(ca);
						}

						index++;
					}
				}

				return this._allocation;
			}
		}

		// ---------------------------------------------------
		// EVENTS
		// ---------------------------------------------------

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

		// ---------------------------------------------------
		// PRIVATE MEMBERS
		// ---------------------------------------------------

		/// <summary>
		/// Get the selected item from a combobox from the selected control row
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private string _GetSelectedItemFromControlRow(int index) {
			foreach(Control ctrl in this.pColumns.Controls) {
				if(ctrl.GetType().Name == "ComboBox") {
					ComboBox cb = (ComboBox)ctrl;

					if(cb.Tag.ToString() == "cb_" + index.ToString()) {
						return cb.SelectedItem.ToString();
					}
				}
			}

			return "";
		}

		/// <summary>
		/// Check if the selected item from a combobox from the selected control row is index zero
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private bool _CheckSelectedItemFromControlRow(int index) {
			foreach(Control ctrl in this.pColumns.Controls) {
				if(ctrl.GetType().Name == "ComboBox") {
					ComboBox cb = (ComboBox)ctrl;

					if(cb.Tag.ToString() == "cb_" + index.ToString()) {
						if(cb.SelectedIndex == 0) {
							return true;
						}
					}
				}
			}

			return false;
		}

		// ---------------------------------------------------
		// PROTECTED MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PUBLIC MEMBERS
		// ---------------------------------------------------
	}
}