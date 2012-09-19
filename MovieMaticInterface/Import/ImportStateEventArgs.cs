using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.MovieMaticInterface.Import {
	public class ImportStateEventArgs : EventArgs {
		public string Message { get; set; }
		public double PercentValue { get; set; }

		public ImportStateEventArgs() {
			this.Message = "";
			this.PercentValue = 0;
		}

		public ImportStateEventArgs(double percentValue) {
			this.Message = "";
			this.PercentValue = percentValue;
		}

		public ImportStateEventArgs(string message, double percentValue) {
			this.Message = message;
			this.PercentValue = percentValue;
		}
	}
}
