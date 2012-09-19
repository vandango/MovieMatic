using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Toenda.MovieMatic {
	static class Program {
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() {
			bool __startOldApp = false;
			int __windowsVersion = Environment.OSVersion.Version.Major;

			if(__windowsVersion < 6) {
				StaticWindows.InfoBox(
					"MovieMatic2: Windows Vista Not Found", 
					"This program requires Windows Vista or later to run,"
					+ "\nbut for compatibility reason we start the old version."
				);

				__startOldApp = true;
			}

			if(__startOldApp) {
				string mmPath = Application.ResourceAssembly.Location;
				mmPath = mmPath.Replace(Application.ResourceAssembly.ManifestModule.Name, "MovieMatic.exe");
				
				if(File.Exists(mmPath)) {
					AppLoader loader = new AppLoader();
					loader.LoadApp(mmPath);
				}
				else {
					StaticWindows.ErrorBox(
						"MovieMatic2: MovieMatic version 1.x not found",
						"MovieMatic version 1.x cannot start then the program was not found."
					);
				}
			}
			else {
				App.Main();
			}
		}
	}
}
