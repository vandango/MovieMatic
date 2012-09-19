using System;
using System.Collections.Generic;
using System.Text;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Class ColumnAllocation
	/// </summary>
	public class ColumnAllocation {
		private string _original_column;
		private string _allocated_column;

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public ColumnAllocation() {
		}

		/// <summary>
		/// Specific Ctor
		/// </summary>
		/// <param name="originalColumn"></param>
		/// <param name="allocatedColumn"></param>
		public ColumnAllocation(string originalColumn, string allocatedColumn) {
			this._original_column = originalColumn;
			this._allocated_column = allocatedColumn;
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		// ---------------------------------------------------
		// PROPERTIES
		// ---------------------------------------------------

		/// <summary>
		/// Get or set the original column
		/// </summary>
		public string OriginalColumn {
			get { return this._original_column; }
			set { this._original_column = value; }
		}

		/// <summary>
		/// Get or set the allocated column
		/// </summary>
		public string AllocatedColumn {
			get { return this._allocated_column; }
			set { this._allocated_column = value; }
		}

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
	}
}
