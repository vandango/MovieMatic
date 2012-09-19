using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Import {
	/// <summary>
	/// Event ImportPercentEventHandler
	/// </summary>
	/// <param name="percentValue"></param>
	public delegate void ImportPercentEventHandler(object sender, ImportStateEventArgs e);

	/// <summary>
	/// Interface IImporter
	/// </summary>
	public interface IImporter {
		/// <summary>
		/// ImportPercentEventHandler event
		/// </summary>
		event ImportPercentEventHandler PercentState;

		/// <summary>
		/// Load the file
		/// </summary>
		/// <returns></returns>
		bool LoadFile();

		/// <summary>
		/// Get a preview
		/// </summary>
		/// <returns></returns>
		DataSet GetPreview();

		/// <summary>
		/// Start the import
		/// </summary>
		/// <returns></returns>
		bool Import(List<ColumnAllocation> allocation);

		/// <summary>
		/// Close the file and dispose all the resources
		/// </summary>
		void Dispose();
	}
}
