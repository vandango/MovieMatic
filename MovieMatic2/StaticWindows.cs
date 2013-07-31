using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class ErrorHandler
	/// </summary>
	public static class StaticWindows {
		/// <summary>
		/// Display a question box
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		public static DialogResult Requester(string question) {
			return MessageBox.Show(
				question,
				AssemblyInfo.Title,
				MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question, 
				MessageBoxDefaultButton.Button2
			);
		}

		/// <summary>
		/// Display a error box
		/// </summary>
		/// <param name="text"></param>
		public static void ErrorBox(string text) {
			MessageBox.Show(
				text,
				AssemblyInfo.Title,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		/// <summary>
		/// Errors the box.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		public static void ErrorBox(string title, string text) {
			MessageBox.Show(
				text,
				title,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}

		/// <summary>
		/// Display a info box
		/// </summary>
		/// <param name="text"></param>
		public static void InfoBox(string text) {
			MessageBox.Show(
				text,
				AssemblyInfo.Title,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}

		/// <summary>
		/// Infoes the box.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		public static void InfoBox(string title, string text) {
			MessageBox.Show(
				text,
				title,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}

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
				AssemblyInfo.Title,
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
				AssemblyInfo.Title,
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

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(string value) {
			MessageBox.Show(value);
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(int value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(long value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(short value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(bool value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(decimal value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(double value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(float value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(DateTime value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(object value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(char value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(byte value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(uint value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(ulong value) {
			MessageBox.Show(value.ToString());
		}

		/// <summary>
		/// Displays a windows forms MessageBox (codestyle from JavaScript)
		/// </summary>
		/// <param name="value">The value.</param>
		public static void alert(ushort value) {
			MessageBox.Show(value.ToString());
		}
	}
}
