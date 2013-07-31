using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Toenda.MovieMatic {
	static class Program {
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() {
			//if(Environment.OSVersion.Version.Major < 6) {
			//    MessageBox.Show("This program requires Windows Vista or later to run.", "Vista Not Found");
			//    return;
			//}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run(new MainForm());
		}
	}
}