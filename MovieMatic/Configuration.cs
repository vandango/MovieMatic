using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.IO;
using Toenda.Foundation.Utility;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class Configuration
	/// </summary>
	public static class Configuration {
		private static FileSystem _fs = new FileSystem();

		private static string _cn;
		private static bool _checkForNewVersion;
		private static string _common_app_path;
		private static string _common_app_data_path;
		private static string _common_app_data_configfile;
		private static string _common_app_temp_path;
		private static List<string> _codecs;
		private static List<string> _quality;

		/// <summary>
		/// Default Ctor
		/// </summary>
		static Configuration() {
			// create codecs list
			_codecs = new List<string>();
			_codecs.Add(Codec.Unknown.ToString());
			_codecs.Add(Codec.DVD.ToString());
			_codecs.Add(Codec.BlueRayDVD.ToString());
			_codecs.Add(Codec.Divx.ToString());
			_codecs.Add(Codec.Xvid.ToString());
			_codecs.Add(Codec.SVCD.ToString());
			_codecs.Add(Codec.VCD.ToString());
			_codecs.Add(Codec.MVCD.ToString());
			_codecs.Add(Codec.MPEG.ToString());
			_codecs.Add(Codec.WMV.ToString());
			_codecs.Add(Codec.VHS.ToString());
			_codecs.Add(Codec.Other.ToString());
			
			// create qualities list
			_quality = new List<string>();
			_quality.Add(Quality.PleaseAnnihilateIt_0.ToString());
			_quality.Add(Quality.VeryBad_1.ToString());
			_quality.Add(Quality.Bad_2.ToString());
			_quality.Add(Quality.TVQuality_3.ToString());
			_quality.Add(Quality.OK_4.ToString());
			_quality.Add(Quality.Good_5.ToString());
			_quality.Add(Quality.VeryGood_6.ToString());
			_quality.Add(Quality.Perfect_7.ToString());
			_quality.Add(Quality.DVDQuality_8.ToString());
			_quality.Add(Quality.CinemaQuality_9.ToString());
			_quality.Add(Quality.HighDefinition_10.ToString());

			// app path
			string path = Assembly.GetExecutingAssembly().Location;
			path = path.Replace(
				Assembly.GetExecutingAssembly().ManifestModule.Name,
				""
			);
			_common_app_path = path;

			// get app data path
			_common_app_data_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData
			) + @"\Toenda\MovieMatic\";
			//_common_app_data_path = path;

			_common_app_temp_path = _common_app_data_path + @"Temp\";

			// create if not exist
			DirectoryInfo dir = _fs.GetDirectory(_common_app_data_path, true);

			// get config file path
			FileInfo fi = new FileInfo(
				_common_app_data_path + "MovieMatic.xml"
			);

			_common_app_data_configfile = fi.FullName;

			// load config
			if(fi.Exists) {
				XmlProvider xml = new XmlProvider(fi.FullName, OpenMode.ForReading);

				if(xml.IsValidXml) {
					try {
						_cn = xml.ReadTag("data", "connectionString");

						string tmp = xml.ReadTag("data", "checkForNewVersion");
						if(!tmp.IsNullOrTrimmedEmpty()) {
							_checkForNewVersion = tmp.IsExpressionTrue();
						}
						else {
							_checkForNewVersion = true;
						}
					}
					catch(Exception ex) {
						string s = ex.Message;
						_cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
						_checkForNewVersion = true;
						_CreateXmlConfigFile(fi.FullName);
					}
				}
				else {
					_cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
					_checkForNewVersion = true;
					_CreateXmlConfigFile(fi.FullName);
				}
			}
			else {
				//_cn = ConfigurationManager.ConnectionStrings["mmConnection"].ConnectionString;
				//_cn = "[INITIAL]";
				_cn = "Server=;Database=MovieMatic;Uid=;Pwd=;";
				_checkForNewVersion = true;

				_CreateXmlConfigFile(fi.FullName);
			}
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get or set the ConnectionStrings
		/// </summary>
		public static string ConnectionString {
			get {
#if (DEBUG)
				return _cn;
				//return "Server=VANDANGO-PC\\VDSQL; User ID=sa; Password=banane; Database=MovieMatic2;";
#else
				return _cn;
#endif
			}
			set { _cn = value; }
		}

		/// <summary>
		/// Get or set a value that indicates if the application should check if there is new version
		/// </summary>
		public static bool CheckForNewVersion {
			get { return _checkForNewVersion; }
			set { _checkForNewVersion = value; }
		}

		/// <summary>
		/// Get or set the CommonApplicationPath
		/// </summary>
		public static string CommonApplicationPath {
			get { return _common_app_path; }
			set { _common_app_path = value; }
		}

		/// <summary>
		/// Get or set the CommonApplicationDataPath
		/// </summary>
		public static string CommonApplicationDataPath {
			get { return _common_app_data_path; }
			set { _common_app_data_path = value; }
		}

		/// <summary>
		/// Gets or sets the common application temp path.
		/// </summary>
		/// <value>The common application temp path.</value>
		public static string CommonApplicationTempPath {
			get { return _common_app_temp_path; }
			set { _common_app_temp_path = value; }
		}

		/// <summary>
		/// Get or set the ConfigurationFilepath
		/// </summary>
		public static string ConfigurationFilepath {
			get { return _common_app_data_configfile; }
			set { _common_app_data_configfile = value; }
		}

		/// <summary>
		/// Get or set the codecs
		/// </summary>
		public static List<string> Codecs {
			get { return _codecs; }
			set { _codecs = value; }
		}

		/// <summary>
		/// Get or set the qualities
		/// </summary>
		public static List<string> Qualities {
			get { return _quality; }
			set { _quality = value; }
		}

		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Save the connection string to the configuration file
		/// </summary>
		/// <param name="connectionString"></param>
		public static void SaveConnectionString(string connectionString) {
			_cn = connectionString;

			XmlProvider xml = new XmlProvider(
				_common_app_data_configfile, 
				OpenMode.ForWriting
			);

			xml.ChangeValueInSecondLevel("connectionString", _cn);
			xml.ChangeValueInSecondLevel("checkForNewVersion", _checkForNewVersion.ToString());

			//xml.CloseFile();
		}

		/// <summary>
		/// Save the xml
		/// </summary>
		public static void SaveXml() {
			XmlProvider xml = new XmlProvider(
				_common_app_data_configfile,
				OpenMode.ForWriting
			);

			xml.ChangeValueInSecondLevel("connectionString", _cn);
			xml.ChangeValueInSecondLevel("checkForNewVersion", _checkForNewVersion.ToString());

			//xml.CloseFile();
		}

		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public static string GetQualityName(int quality) {
			return QualityHelper.GetQualityName((Quality)quality);
		}

		/// <summary>
		/// Gets the name of the quality.
		/// </summary>
		/// <param name="quality">The quality.</param>
		/// <returns></returns>
		public static string GetQualityName(string quality) {
			int counter = 0;

			foreach(string val in _quality) {
				if(val == quality) {
					break;
				}

				counter++;
			}

			return QualityHelper.GetQualityName((Quality)counter);
		}

		/// <summary>
		/// Create the xml config file
		/// </summary>
		/// <param name="filename"></param>
		private static void _CreateXmlConfigFile(string filename) {
			XmlDocumentTemplate doc = new XmlDocumentTemplate();
			doc.AddDocumentInfo();
			doc.AddRootNode("configuration");
			doc.AddNode("data");
			doc.AddElement("connectionString", _cn);

			XmlProvider.CreateDocumentFromList(filename, doc);
		}
	}
}
