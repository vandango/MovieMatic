using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class ErrorHandler
	/// </summary>
	public static class ErrorHandler {
		/// <summary>
		/// Display a error messagebox
		/// </summary>
		/// <param name="ex">The default exception</param>
		public static void DisplayErrorMessagebox(Exception ex) {
			StringBuilder str = new StringBuilder();

			str.Append("ERROR\n");
			str.Append("Quelle: " + ex.Source + "\n");
			str.Append("Fehlermeldung: " + ex.Message + "\n");
			str.Append("StackTrace: " + ex.StackTrace + "\n");
			str.Append("\n");

			if(ex.InnerException != null) {
				str.Append("INNER EXCEPTION\n");
				str.Append("Quelle: " + ex.InnerException.Source + "\n");
				str.Append("Fehlermeldung: " + ex.InnerException.Message + "\n");
				str.Append("StackTrace: " + ex.InnerException.StackTrace + "\n");

				if(ex.InnerException.InnerException != null) {
					str.Append("\n");
					str.Append("INNER EXCEPTION - INNER EXCEPTION\n");
					str.Append("Quelle: " + ex.InnerException.InnerException.Source + "\n");
					str.Append("Fehlermeldung: " + ex.InnerException.InnerException.Message + "\n");
					str.Append("StackTrace: " + ex.InnerException.InnerException.StackTrace + "\n");
				}
			}

			str.Append("");

			MessageBox.Show(
				str.ToString(),
				"MovieMatic",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		/// <summary>
		/// Display a error messagebox
		/// </summary>
		/// <param name="ex">The FileNotFoundException exception</param>
		public static void DisplayErrorMessagebox(FileNotFoundException ex) {
			StringBuilder str = new StringBuilder();

			str.Append("ERROR\n");
			str.Append("Quelle: " + ex.Source + "\n");
			str.Append("Fehlermeldung: " + ex.Message + "\n");
			str.Append("StackTrace: " + ex.StackTrace + "\n");
			str.Append("\n");

			if(ex.InnerException != null) {
				str.Append("INNER EXCEPTION\n");
				str.Append("Quelle: " + ex.InnerException.Source + "\n");
				str.Append("Fehlermeldung: " + ex.InnerException.Message + "\n");
				str.Append("StackTrace: " + ex.InnerException.StackTrace + "\n");

				if(ex.InnerException.InnerException != null) {
					str.Append("\n");
					str.Append("INNER EXCEPTION - INNER EXCEPTION\n");
					str.Append("Quelle: " + ex.InnerException.InnerException.Source + "\n");
					str.Append("Fehlermeldung: " + ex.InnerException.InnerException.Message + "\n");
					str.Append("StackTrace: " + ex.InnerException.InnerException.StackTrace + "\n");
				}
			}

			str.Append("");

			MessageBox.Show(
				str.ToString(),
				"MovieMatic",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		/// <summary>
		/// Display a error messagebox
		/// </summary>
		/// <param name="ex">The ArgumentNullException exception</param>
		public static void DisplayErrorMessagebox(ArgumentNullException ex) {
			StringBuilder str = new StringBuilder();

			str.Append("ERROR\n");
			str.Append("Quelle: " + ex.Source + "\n");
			str.Append("Fehlermeldung: " + ex.Message + "\n");
			str.Append("StackTrace: " + ex.StackTrace + "\n");
			str.Append("\n");

			if(ex.InnerException != null) {
				str.Append("INNER EXCEPTION\n");
				str.Append("Quelle: " + ex.InnerException.Source + "\n");
				str.Append("Fehlermeldung: " + ex.InnerException.Message + "\n");
				str.Append("StackTrace: " + ex.InnerException.StackTrace + "\n");

				if(ex.InnerException.InnerException != null) {
					str.Append("\n");
					str.Append("INNER EXCEPTION - INNER EXCEPTION\n");
					str.Append("Quelle: " + ex.InnerException.InnerException.Source + "\n");
					str.Append("Fehlermeldung: " + ex.InnerException.InnerException.Message + "\n");
					str.Append("StackTrace: " + ex.InnerException.InnerException.StackTrace + "\n");
				}
			}

			str.Append("");

			MessageBox.Show(
				str.ToString(),
				"MovieMatic",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}
	}
}
