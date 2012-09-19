using MSSystem = System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Toenda.MovieMaticInterface.Base {
	/// <summary>
	/// Class AssemblyInfo
	/// </summary>
	public static class AssemblyInfo {
		/// <summary>
		/// Get the Assembly version
		/// </summary>
		public static string Version {
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		/// <summary>
		/// Get the Assembly title
		/// </summary>
		public static string Title {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyTitleAttribute),
					false
				);

				if(attributes.Length > 0) {
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

					if(titleAttribute.Title != "") {
						return titleAttribute.Title;
					}
				}

				return MSSystem.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		/// <summary>
		/// Get the Assembly description
		/// </summary>
		public static string Description {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyDescriptionAttribute),
					false
				);

				if(attributes.Length == 0) {
					return "";
				}

				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>
		/// Get the assembly copyright
		/// </summary>
		public static string Copyright {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyCopyrightAttribute),
					false
				);

				if(attributes.Length == 0) {
					return "";
				}

				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>
		/// Get the assembly product
		/// </summary>
		public static string Product {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyProductAttribute),
					false
				);

				if(attributes.Length == 0) {
					return "";
				}

				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		/// <summary>
		/// Get the assembly company
		/// </summary>
		public static string Company {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyCompanyAttribute),
					false
				);

				if(attributes.Length == 0) {
					return "";
				}

				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
	}
}
