using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Import {
	public class SQLServerToSQLiteImporter : IImporter {
		private string _filename;
		private string _connectionString;

		public SQLServerToSQLiteImporter() {
			this._filename = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData
			) + @"\Toenda\MovieMatic\moviematic.db";

			this._connectionString = string.Format(
				"Driver=SQLite;Data Source={0};Version=3;", 
				this._filename
			);
		}

		public bool Import() {
			bool result = false;
			
			// start real import

			// --> load data from MSSQL from 1. table
			// ----> save data to 1. SQLite table

			for(int i = 1; i < 1000; i++) {
				System.Threading.Thread.Sleep(1);
				this.PercentState(this, new ImportStateEventArgs("weiter ... " + i.ToString(), Convert.ToDouble(i) / 10D));
			}

			result = true;
			this.PercentState(this, new ImportStateEventArgs("fertich ... ", 100));

			return result;
		}

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
			return true;
		}

		/// <summary>
		/// Get a preview
		/// </summary>
		/// <returns></returns>
		public DataSet GetPreview() {
			return null;
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
	}
}
