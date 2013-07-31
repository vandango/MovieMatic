using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using tfl = Toenda.Foundation;
using mmi = Toenda.MovieMaticInterface.Base;
using Toenda.Foundation.SystemLayer;

namespace Toenda.MovieMatic {
	public partial class AboutForm : Form {
		/// <summary>
		/// Default Ctor
		/// </summary>
		public AboutForm() {
			InitializeComponent();

			this.lblCopyright.Text = this.AssemblyCopyright;
			this.lblVersion.Text = this.AssemblyTitle + " Version " + this.AssemblyVersion;
			this.lblInterfaceVersion.Text = mmi.AssemblyInfo.Title + " Version " + mmi.AssemblyInfo.Version;
			this.lblTFLVersion.Text = Toenda.Foundation.AssemblyInfo.Title + " Version " + Toenda.Foundation.AssemblyInfo.Version;
			
			this.lblPVersion.Text = "Prozessor: " + Toenda.Foundation.SystemLayer.Processor.Architecture.ToString();

			this.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// panel1_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panel1_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// AboutForm_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AboutForm_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// lblCopyright_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblCopyright_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// lblVersion_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblVersion_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// lblTFLVersion_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblTFLVersion_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// lblInterfaceVersion_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblInterfaceVersion_Click(object sender, EventArgs e) {
			this.Close();
		}

		// -----------------------------------------
		// PROPERTIES
		// -----------------------------------------

		/// <summary>
		/// Get the Assembly version
		/// </summary>
		public string AssemblyVersion {
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		/// <summary>
		/// Get the Assembly title
		/// </summary>
		public string AssemblyTitle {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

				if(attributes.Length > 0) {
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

					if(titleAttribute.Title != "")
						return titleAttribute.Title;
				}

				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		/// <summary>
		/// Get the Assembly description
		/// </summary>
		public string AssemblyDescription {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

				if(attributes.Length == 0)
					return "";

				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>
		/// Get the assembly copyright
		/// </summary>
		public string AssemblyCopyright {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				if(attributes.Length == 0)
					return "";

				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>
		/// Get the assembly product
		/// </summary>
		public string AssemblyProduct {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);

				if(attributes.Length == 0)
					return "";

				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		/// <summary>
		/// Get the assembly company
		/// </summary>
		public string AssemblyCompany {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

				if(attributes.Length == 0)
					return "";

				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
	}
}