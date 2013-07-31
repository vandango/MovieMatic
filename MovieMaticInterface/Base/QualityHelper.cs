using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Base {
	public class QualityHelper {
		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public static string GetQualityName(Quality quality) {
			switch(quality) {
				case Quality.PleaseAnnihilateIt_0:
					return "0: Bitte löschen";

				case Quality.VeryBad_1:
					return "1: Sehr schlecht";

				case Quality.Bad_2:
					return "2: Schlecht";

				case Quality.TVQuality_3:
					return "3: TV Qualität";

				case Quality.OK_4:
					return "4: OK";

				case Quality.Good_5:
					return "5: Gut";

				case Quality.VeryGood_6:
					return "6: Sehr gut";

				case Quality.Perfect_7:
					return "7: Perfekt";

				case Quality.DVDQuality_8:
					return "8: DVD Qualität";

				case Quality.CinemaQuality_9:
					return "9: Kino Qualität";

				case Quality.HighDefinition_10:
					return "10: High Definition";
			}

			return "";
		}
	}
}
