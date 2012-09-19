using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.IO;

using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class Configuration
	/// </summary>
	public class Configuration {
		public static Configuration Current = new Configuration();

		private FileSystem _fs = new FileSystem();

		private string _cn;
		private string _common_app_path;
		private string _common_app_data_path;
		private string _common_app_data_configfile;
		private string _common_app_temp_path;
		private List<string> _codecs;
		private List<string> _quality;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public Configuration() {
			// create codecs list
			this._codecs = new List<string>();
			this._codecs.Add(Codec.Unknown.ToString());
			this._codecs.Add(Codec.DVD.ToString());
			this._codecs.Add(Codec.Divx.ToString());
			this._codecs.Add(Codec.Xvid.ToString());
			this._codecs.Add(Codec.SVCD.ToString());
			this._codecs.Add(Codec.VCD.ToString());
			this._codecs.Add(Codec.MVCD.ToString());
			this._codecs.Add(Codec.MPEG.ToString());
			this._codecs.Add(Codec.WMV.ToString());
			this._codecs.Add(Codec.VHS.ToString());
			this._codecs.Add(Codec.Other.ToString());
			
			// create qualities list
			this._quality = new List<string>();
			this._quality.Add(Quality.PleaseAnnihilateIt_0.ToString());
			this._quality.Add(Quality.VeryBad_1.ToString());
			this._quality.Add(Quality.Bad_2.ToString());
			this._quality.Add(Quality.TVQuality_3.ToString());
			this._quality.Add(Quality.OK_4.ToString());
			this._quality.Add(Quality.Good_5.ToString());
			this._quality.Add(Quality.VeryGood_6.ToString());
			this._quality.Add(Quality.Perfect_7.ToString());
			this._quality.Add(Quality.DVDQuality_8.ToString());
			this._quality.Add(Quality.CinemaQuality_9.ToString());
			this._quality.Add(Quality.HighDefinition_10.ToString());

			// app path
			string path = Assembly.GetExecutingAssembly().Location;
			path = path.Replace(
				Assembly.GetExecutingAssembly().ManifestModule.Name,
				""
			);
			this._common_app_path = path;

			// get app data path
			this._common_app_data_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData
			) + @"\Toenda\MovieMatic\";
			//this._common_app_data_path = path;

			this._common_app_temp_path = _common_app_data_path + @"Temp\";

			// create if not exist
			DirectoryInfo dir = this._fs.GetDirectory(this._common_app_data_path, true);

			// get config file path
			FileInfo fi = new FileInfo(
				this._common_app_data_path + "MovieMatic.xml"
			);

			this._common_app_data_configfile = fi.FullName;

			// load config
			if(fi.Exists) {
				XmlProvider xml = new XmlProvider(fi.FullName, OpenMode.ForReading);

				if(xml.IsValidXml) {
					try {
						this._cn = xml.ReadTag("data", "connectionString");
					}
					catch(Exception ex) {
						string s = ex.Message;
						this._cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
						this._CreateXmlConfigFile(fi.FullName);
					}
				}
				else {
					this._cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
					this._CreateXmlConfigFile(fi.FullName);
				}
			}
			else {
				//this._cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
				//this._cn = "[INITIAL]";
				this._cn = "Server=;Database=MovieMatic;Uid=;Pwd=;";

				this._CreateXmlConfigFile(fi.FullName);
			}
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get or set the ConnectionStrings
		/// </summary>
		public string ConnectionString {
			get {
#if (DEBUG)
				return "Server=VANDANGO-PC; User ID=sa; Password=banane; Database=MovieMaticTest;";
#else
				return this._cn;
#endif
			}
			set { this._cn = value; }
		}

		/// <summary>
		/// Get or set the CommonApplicationPath
		/// </summary>
		public string CommonApplicationPath {
			get { return this._common_app_path; }
			set { this._common_app_path = value; }
		}

		/// <summary>
		/// Get or set the CommonApplicationDataPath
		/// </summary>
		public string CommonApplicationDataPath {
			get { return this._common_app_data_path; }
			set { this._common_app_data_path = value; }
		}

		/// <summary>
		/// Gets or sets the common application temp path.
		/// </summary>
		/// <value>The common application temp path.</value>
		public string CommonApplicationTempPath {
			get { return this._common_app_temp_path; }
			set { this._common_app_temp_path = value; }
		}

		/// <summary>
		/// Get or set the ConfigurationFilepath
		/// </summary>
		public string ConfigurationFilepath {
			get { return this._common_app_data_configfile; }
			set { this._common_app_data_configfile = value; }
		}

		/// <summary>
		/// Get or set the codecs
		/// </summary>
		public List<string> Codecs {
			get { return this._codecs; }
			set { this._codecs = value; }
		}

		/// <summary>
		/// Get or set the qualities
		/// </summary>
		public List<string> Qualities {
			get { return this._quality; }
			set { this._quality = value; }
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Save the connection string to the configuration file
		/// </summary>
		/// <param name="connectionString"></param>
		public void SaveConnectionString(string connectionString) {
			this._cn = connectionString;

			XmlProvider xml = new XmlProvider(
				this._common_app_data_configfile, 
				OpenMode.ForWriting
			);

			xml.ChangeValueInSecondLevel("connectionString", this._cn);
		}

		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public string GetQualityName(int quality) {
			return this.GetQualityName((Quality)quality);
		}

		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public string GetQualityName(string quality) {
			int counter = 0;

			foreach(string val in this._quality) {
				if(val == quality) {
					break;
				}

				counter++;
			}

			return this.GetQualityName((Quality)counter);
		}

		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public string GetQualityName(Quality quality) {
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

		/// <summary>
		/// Gets the codec number.
		/// </summary>
		/// <param name="codec">The codec.</param>
		/// <returns></returns>
		public int GetCodecNumber(string codec) {
			int counter = 0;

			foreach(string val in this._codecs) {
				if(val == codec) {
					return counter;
				}

				counter++;
			}

			//switch(codec) {
			//    case "Divx":
			//        return 0;

			//    case "DVD":
			//        return 1;

			//    case "MPEG":
			//        return 2;

			//    case "MVCD":
			//        return 3;

			//    case "Other":
			//        return 4;

			//    case "SVCD":
			//        return 5;

			//    case "Unknown":
			//        return 6;

			//    case "VCD":
			//        return 7;

			//    case "WMV":
			//        return 8;

			//    case "Xvid":
			//        return 9;

			//    case "VHS":
			//        return 10;
			//}

			return 6;
		}

		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Create the xml config file
		/// </summary>
		/// <param name="filename"></param>
		private void _CreateXmlConfigFile(string filename) {
			//List<XmlNodeTemplate> tree = new List<XmlNodeTemplate>();
			//tree.Add(new XmlNodeTemplate(TemplateNodeType.DocumentInfo, "", ""));
			//tree.Add(new XmlNodeTemplate(TemplateNodeType.RootNode, "configuration", ""));
			//tree.Add(new XmlNodeTemplate(TemplateNodeType.Node, "data", ""));
			//tree.Add(new XmlNodeTemplate(TemplateNodeType.Element, "connectionString", this._cn));

			XmlDocumentTemplate doc = new XmlDocumentTemplate();
			doc.AddDocumentInfo();
			doc.AddRootNode("configuration");
			doc.AddNode("data");
			doc.AddElement("connectionString", this._cn);

			XmlProvider.CreateDocumentFromList(filename, doc);
			//XmlProvider xml = new XmlProvider();
			//xml.CreateDocumentFromList(filename, doc);
		}
	}
}
