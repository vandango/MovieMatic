using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Toenda.Foundation.Data;
using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Import {
	/// <summary>
	/// Class Excel2003Importer ("is a" IImporter)
	/// </summary>
	public class Excel2003Importer : IImporter {
		private DAL _db;
		private DALSettings _cfg;
		private string _filename;
		private List<string> _sheets = new List<string>();

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="filename">The filename including the path of the spreadsheet file.</param>
		/// <param name="sheet">The worksheet.</param>
		/// <exception cref="ArgumentNullException">If the filename parameter is null or empty.</exception>
		public Excel2003Importer(string filename, string sheet) {
			if(filename == null
			|| filename.Trim() == "") {
				throw new ArgumentNullException(
					"filename",
					"The filename parameter canot be null."
				);
			}

			if(sheet == null
			|| sheet.Trim() == "") {
				throw new ArgumentNullException(
					"sheet",
					"The sheet parameter canot be null."
				);
			}

			this._filename = filename;
			this._sheets = new List<string>();
			this._sheets.Add(sheet);
		}

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="filename">The filename including the path of the spreadsheet file.</param>
		/// <param name="sheet">The worksheet.</param>
		/// <exception cref="ArgumentNullException">If the filename parameter is null or empty.</exception>
		public Excel2003Importer(string filename, List<string> sheets) {
			if(filename == null
			|| filename.Trim() == "") {
				throw new ArgumentNullException(
					"filename",
					"The filename parameter canot be null."
				);
			}

			if(sheets == null
			|| sheets.Count == 0) {
				throw new ArgumentNullException(
					"sheets",
					"The sheets parameter canot be null."
				);
			}

			this._filename = filename;
			this._sheets = sheets;
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		/// <summary>
		/// Event ImportPercentEventHandler
		/// </summary>
		public event ImportPercentEventHandler PercentState;

		/// <summary>
		/// Load the file
		/// </summary>
		/// <exception cref="FileNotFoundException">If the file was not found or is not a file with the Excel2003 format.</exception>
		/// <returns></returns>
		public bool LoadFile() {
			if(!File.Exists(this._filename)) {
				throw new FileNotFoundException(
					"The file [" + this._filename + "] does not exist or is not a file with the Microsoft Excel 2003 format.",
					this._filename
				);
			}

			this._cfg = new DALSettings(
				ProviderType.OleDb,
				"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this._filename + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";"
			);
			this._db = new DAL(this._cfg);

			return true;
		}

		/// <summary>
		/// Get a preview
		/// </summary>
		/// <returns></returns>
		public DataSet GetPreview() {
			return this._ConvertSpreadsheetToDataset(false);
		}

		/// <summary>
		/// Start the import
		/// </summary>
		/// <param name="allocation">The column allocation.</param>
		/// <returns></returns>
		public bool Import(List<ColumnAllocation> allocation) {
			return true;
		}

		/// <summary>
		/// Close the file and dispose all the resources
		/// </summary>
		public void Dispose() {
		}

		// ---------------------------------------------------
		// PROPERTIES
		// ---------------------------------------------------

		// ---------------------------------------------------
		// EVENTS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PRIVATE MEMBERS
		// ---------------------------------------------------

		/// <summary>
		/// Convert the spreadsheet to a dataset
		/// </summary>
		/// <param name="withEventThrowing"></param>
		/// <returns></returns>
		private DataSet _ConvertSpreadsheetToDataset(bool withEventThrowing) {
			int count = 0;
			DataSet data = new DataSet();

			try {
				/*
				 * load column info																																																																			
				 * */
				//List<ImportTableColumnSettings> list = new List<ImportTableColumnSettings>();

				this._db.OpenConnection();
				this._db.CloseConnection();

				foreach(string sheet in this._sheets) {
					data = this._db.ExecuteQuery("SELECT * FROM [" + sheet + "]");

					//data = null;
				}
			}
			catch(Exception ex) {
				throw new Exception(
					"Error on loading data from Microsoft Excel 2003 spreadsheet!",
					ex
				);
			}

			return data;
		}

		// ---------------------------------------------------
		// PROTECTED MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PUBLIC MEMBERS
		// ---------------------------------------------------
	}
}
