using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using Toenda.Foundation;

namespace Toenda.MovieMaticInterface.Base {
	public class VersionChecker {
		private string _version;
		private string _newVersion;
		private string _versionString;

		public VersionChecker(string currentVersion) {
			this._version = currentVersion;
		}

		public string NewVersion {
			get { return this._newVersion; }
		}

		public string NewVersionString {
			get { return this._versionString; }
		}

		public bool CheckForNewVersion() {
			bool result = false;

			Uri uri = new Uri("http://toenda.com/moviematic");

			// start request
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);

			webRequest.Credentials = CredentialCache.DefaultCredentials;
			webRequest.Accept = "text/xml";

			HttpWebResponse webResponse = null;
			Stream responseStream = null;

			// parse now
			try {
				webResponse = (HttpWebResponse)webRequest.GetResponse();
				responseStream = webResponse.GetResponseStream();

				using(StreamReader streamReader = new StreamReader(responseStream)) {
					while(!streamReader.EndOfStream) {
						string line = streamReader.ReadLine();

						// <!-- @buildversion=1.1.9.28562 -->
						// <strong>Aktuelle Version:</strong> Version 1.1.9 vom 11.10.2009

						if(line.Contains("<!-- @buildversion=")) {
							string tmp = line.Replace("<!-- @buildversion=", "")
								.Replace("-->", "")
								.Trim();

							//tmp = "1.2.1.38977";

							this._newVersion = tmp;

							string major = tmp.Substring(0, tmp.LastIndexOf("."));
							string build = tmp.Substring(tmp.LastIndexOf(".") + 1);

							this._versionString = string.Format(
								"Version {0} Build {1}",
								major, 
								build
							);

							string currentMajor = this._version.Substring(0, this._version.LastIndexOf("."));

							int newVersion = major.Replace(".", "").ToInt32();
							int currentVersion = currentMajor.Replace(".", "").ToInt32();

							if(newVersion > currentVersion) {
								result = true;
							}

							break;
						}

						//if(line.Contains("<strong>Aktuelle Version:</strong>")) {
						//    string tmp = line.Substring(
						//        line.IndexOf(
						//            "<strong>Aktuelle Version:</strong>"
						//        ) + ( "<strong>Aktuelle Version:</strong>" ).Length
						//    );

						//    int start = tmp.IndexOf("Version") + ( "Version" ).Length;
						//    int end = tmp.IndexOf("vom") - start;

						//    tmp = tmp.Substring(start, end).Trim();

						//    this._newVersion = tmp;

						//    int newVersion = tmp.Replace(".", "").ToInt32();
						//    int currentVersion = this._version.Substring(
						//        0, this._version.LastIndexOf(".")
						//    ).Replace(".", "").ToInt32();

						//    //newVersion = 121;

						//    if(newVersion > currentVersion) {
						//        result = true;
						//    }

						//    break;
						//}
					}
				}
			}
			catch(WebException webex) {
			}
			catch(Exception ex) {
			}
			finally {
				if(responseStream != null) {
					responseStream.Close();
					responseStream.Dispose();
				}

				if(webResponse != null) {
					webResponse.Close();
				}
			}

			return result;
		}

		public void LoadNewVersion() {
		}
	}
}
