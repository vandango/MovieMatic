using System;
using System.Collections.Generic;
using System.Text;

using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Import {
	/// <summary>
	/// Class ImportTableColumnSettings
	/// </summary>
	public class ImportTableColumnSettings {
		private int _index;
		private Type _type;
		private string _name;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public ImportTableColumnSettings() {
		}

		/// <summary>
		/// Get a value that indicates if this object is a real object
		/// </summary>
		public bool IsRealObject {
			get {
				if(this._name != null
				&& this._name.Trim() != "") {
					return true;
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// Get or set the index
		/// </summary>
		public int Index {
			get { return this._index; }
			set { this._index = value; }
		}

		/// <summary>
		/// Get or set the name
		/// </summary>
		public string Name {
			get { return this._name; }
			set { this._name = value; }
		}

		/// <summary>
		/// Get or set the name
		/// </summary>
		public Type CellDataType {
			get { return this._type; }
			set { this._type = value; }
		}
	}

	/// <summary>
	/// Class ImportHelper
	/// </summary>
	public class ImportHelper {
		public static ImportHelper Self = new ImportHelper();

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public ImportHelper() {
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROPERTIES
		// ---------------------------------------------------

		// ---------------------------------------------------
		// EVENTS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PRIVATE MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROTECTED MEMBERS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PUBLIC MEMBERS
		// ---------------------------------------------------

		/// <summary>
		/// Check if a column is allocated
		/// </summary>
		/// <param name="allocation"></param>
		/// <param name="originalColumn"></param>
		/// <returns></returns>
		public bool CheckForAllocatedColumn(List<ColumnAllocation> allocation, string originalColumn) {
			foreach(ColumnAllocation ca in allocation) {
				if(ca.OriginalColumn == originalColumn) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get the allocated column name
		/// </summary>
		/// <param name="allocation"></param>
		/// <param name="originalColumn"></param>
		/// <returns></returns>
		public string GetAllocatedColumnName(List<ColumnAllocation> allocation, string originalColumn) {
			foreach(ColumnAllocation ca in allocation) {
				if(ca.OriginalColumn == originalColumn) {
					return ca.AllocatedColumn;
				}
			}

			return "";
		}
	}
}
