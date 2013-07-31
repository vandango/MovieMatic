using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Diagnostics;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.Foundation.Utility;
using Toenda.Foundation.Net;

namespace Toenda.MovieMatic {
	public enum WikiOutputType {
		Html = 0,
		Xml = 1
	}

	public enum WikiParseType {
		Article = 0, 
		Search = 1
	}

	public partial class FromWikipediaForm : Form {
		private const string WP_LANG = "de";
		private const string WP_URL = "http://" + WP_LANG + ".wikipedia.org/wiki/";
		private const string WP_SEARCH_SCRIPT = "Spezial:Search?ns0=1&search={0}&fulltext=Suche&limit=50&offset=0";
		
		//private const string WP_EXPORT_SCRIPT = "Special:Export/{0}";
		private const string WP_EXPORT_SCRIPT = "http://de.wikipedia.org/w/index.php?title=Spezial:Exportieren&action=submit&pages={0}";

		private const string WP_EXPORT_SCRIPT_ADD = "Special:Export/";
		//private const string WP_EXPORT_SCRIPT_ADD = "http://de.wikipedia.org/w/index.php?title=Spezial:Exportieren&action=submit&pages=";

		private string _searchWord;
		private WikiParseType _wikiParseType;

		/// <summary>
		/// Initializes a new instance of the <see cref="FromWikipediaForm"/> class.
		/// </summary>
		public FromWikipediaForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.OK;

			this.btnOpenEditor.Enabled = false;
			this.wbResponse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbResponse_DocumentCompleted);

			this.txtArticle.Focus();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FromWikipediaForm"/> class.
		/// </summary>
		/// <param name="wpt"></param>
		/// <param name="searchWord"></param>
		public FromWikipediaForm(WikiParseType wpt, string searchWord) {
			InitializeComponent();

			this.DialogResult = DialogResult.OK;

			this._searchWord = searchWord;
			this._wikiParseType = wpt;
			this.txtArticle.Text = searchWord;

			this.txtArticle.Focus();

			this.Cursor = Cursors.WaitCursor;

			this.ParseWikipedia(
				searchWord,
				WikiOutputType.Html,
				wpt
			);

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Gets the search word.
		/// </summary>
		/// <value>The search word.</value>
		public string SearchWord {
			get { return this._searchWord; }
		}

		/// <summary>
		/// Gets the type of the wiki parse.
		/// </summary>
		/// <value>The type of the wiki parse.</value>
		public WikiParseType WikiParseType {
			get { return this._wikiParseType; }
		}

		/// <summary>
		/// Handles the Shown event of the FromWikipediaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void FromWikipediaForm_Shown(object sender, EventArgs e) {
			this.txtArticle.Focus();
		}

		/// <summary>
		/// Handles the FormClosing event of the FromWikipediaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
		private void FromWikipediaForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Handles the KeyUp event of the txtArticle control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void txtArticle_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyValue == 13
			&& this.txtArticle.Text.Trim().Length > 1) {
				this.ReadWikipedia(this.txtArticle.Text.Trim());
			}
		}

		/// <summary>
		/// Handles the Click event of the btnArticle control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnArticle_Click(object sender, EventArgs e) {
			if(this.txtArticle.Text.Trim().Length > 0) {
				this.ReadWikipedia(this.txtArticle.Text.Trim());
			}
		}

		/// <summary>
		/// Handles the Click event of the btnSearch control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnSearch_Click(object sender, EventArgs e) {
			if(this.txtArticle.Text.Trim().Length > 0) {
				this.SearchWikipedia(this.txtArticle.Text.Trim());
			}
		}

		/// <summary>
		/// Handles the DoubleClick event of the lbResult control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void lbResult_DoubleClick(object sender, EventArgs e) {
			string item = this.lbResult.SelectedItem.ToString();
			item = item.Substring(
				0, 
				item.IndexOf("(Wiki Titel:")
			);

			this._searchWord = item;
			this._wikiParseType = WikiParseType.Article;

			this.DialogResult = DialogResult.Retry;

			this.Close();

			this.DialogResult = DialogResult.Retry;

			//this.txtArticle.Text = item;

			//this.Cursor = Cursors.WaitCursor;
			
			//this.ParseWikipedia(
			//    item,
			//    WikiOutputType.Html,
			//    WikiParseType.Article
			//);

			//this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Handles the Click event of the btnOpenEditor control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnOpenEditor_Click(object sender, EventArgs e) {
			if(this.txtResponse.Text.Length > 0) {
				WikipediaParser parser = new WikipediaParser(
					Configuration.ConnectionString
				);

				Movie mov = parser.Parse(this.txtResponse.Text);

				// search for movie with title like mov.Title

				if(mov != null) {
					MovieForm form = new MovieForm(mov);

					if(form.ShowDialog(this) == DialogResult.OK) {
						// alles schliessen
						this.DialogResult = DialogResult.OK;
						this.Close();
						this.DialogResult = DialogResult.OK;
					}
				}
				else {
					StaticWindows.ErrorBox("Fehler: Es konnte kein Film geparst werden!");
				}
			}
			else {
				StaticWindows.ErrorBox("Fehler: Es ist kein Wikipedia Artikel ausgewählt!");
			}
		}

		protected void wbResponse_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.btnOpenEditor.Enabled = true;
			//this.txtResponse.Text = this.wbResponse.DocumentText;
		}

		/// <summary>
		/// Toes the camel case.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <returns></returns>
		private string ToCamelCase(string Name) {
			StringBuilder sb = new StringBuilder();
			
			foreach(string s in Name.Split(' ')) {
				if(s.Length > 0) {
					sb.Append(s.Substring(0, 1).ToUpper() + s.Substring(1) + " ");
				}
			}

			return sb.ToString().Trim();
		}

		/// <summary>
		/// Reads the wikipedia.
		/// </summary>
		/// <param name="searchWord">The search word.</param>
		public void ReadWikipedia(string searchWord) {
			this.Cursor = Cursors.WaitCursor;

			this._searchWord = searchWord;
			this._wikiParseType = WikiParseType.Article;

			this.ParseWikipedia(
				searchWord,
				WikiOutputType.Html,
				WikiParseType.Article
			);

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Searches the wikipedia.
		/// </summary>
		/// <param name="searchWord">The search word.</param>
		public void SearchWikipedia(string searchWord) {
			this.Cursor = Cursors.WaitCursor;

			this._searchWord = searchWord;
			this._wikiParseType = WikiParseType.Search;

			this.ParseWikipedia(
				searchWord, 
				WikiOutputType.Html, 
				WikiParseType.Search
			);

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Parses the wikipedia.
		/// </summary>
		/// <param name="searchWord">The search word.</param>
		/// <param name="outputType">Type of the output.</param>
		/// <param name="parseType">Type of the parse.</param>
		public void ParseWikipedia(string searchWord, WikiOutputType outputType, WikiParseType parseType) {
			string uri = "";
			StringBuilder str = new StringBuilder();

			switch(parseType) {
				case WikiParseType.Article:
					this.lblTitle.Text = "Wikipedia Artikel";

					Uri webUri;
					Uri downloadUri;

					if(WebValidator.IsWebAddress(searchWord)) {
						// http://de.wikipedia.org/wiki/Die_Entf%C3%BChrung_der_U-Bahn_Pelham_123
						//searchWord = searchWord.Substring(searchWord.IndexOf("wiki/") + 5);
						//uri = WP_URL + string.Format(WP_EXPORT_SCRIPT, searchWord);
						if(uri.Contains(WP_EXPORT_SCRIPT_ADD)) {
							//uri = searchWord;
							uri = searchWord.Replace("/wiki/" + WP_EXPORT_SCRIPT_ADD, "/wiki/");
						}
						else {
							//uri = searchWord.Replace("/wiki/", "/wiki/" + WP_EXPORT_SCRIPT_ADD);
							uri = searchWord;
						}

						if(!searchWord.Contains(WP_EXPORT_SCRIPT_ADD)) {
							searchWord = searchWord.Replace("/wiki/", "/wiki/" + WP_EXPORT_SCRIPT_ADD);
						}

						webUri = new Uri(uri);
						downloadUri = new Uri(searchWord);//.Replace("/wiki/", "/wiki/" + WP_EXPORT_SCRIPT_ADD));
					}
					else {
						searchWord = this.ToCamelCase(searchWord);
						searchWord = searchWord.Replace(' ', '_');

						if(uri.Contains(WP_EXPORT_SCRIPT_ADD)) {
							uri = WP_URL + searchWord;
						}
						else {
							uri = WP_URL + string.Format(WP_EXPORT_SCRIPT, searchWord);
						}

						webUri = new Uri(WP_URL + searchWord);
						downloadUri = new Uri(WP_URL + string.Format(WP_EXPORT_SCRIPT, searchWord));
					}

					this.wbResponse.Stop();
					//this.wbResponse = new WebBrowser();
					//this.wbResponse.Url = null;
					this.wbResponse.Url = webUri;
					this.wbResponse.Navigate(webUri);

					WebClient wc = new WebClient();
					wc.Headers.Add("user-agent", "MovieMatic (compatible; .NET 3.5;)");
					string response = wc.DownloadString(downloadUri.AbsoluteUri);

					this.txtResponse.Text = response;

					//this.wbResponse.Refresh(WebBrowserRefreshOption.Completely);
					break;

				case WikiParseType.Search:
					this.lblTitle.Text = "Suchergebnis";

					searchWord = searchWord.Replace(' ', '+');

					uri = WP_URL + string.Format(WP_SEARCH_SCRIPT, searchWord);
					break;

				default:
					break;
			}

			//Stopwatch watch = new Stopwatch();
			//watch.Start();

			//// start request
			//HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);

			//webRequest.Credentials = CredentialCache.DefaultCredentials;
			////webRequest.Accept = "text/xml";

			//HttpWebResponse webResponse = null;
			//Stream responseStream = null;

			//try {
			//    webResponse = (HttpWebResponse)webRequest.GetResponse();
			//    responseStream = webResponse.GetResponseStream();

			//    using(StreamReader streamReader = new StreamReader(responseStream)) {
			//        while(!streamReader.EndOfStream) {
			//            str.Append(streamReader.ReadLine());
			//            str.Append("\r\n");
			//        }

			//        //this.txtResponse.Text = str.ToString();
			//    }
			//}
			//catch(WebException webex) {
			//    str.Append("WebException:\r\n");
			//    str.Append(webex.ToString());
			//}
			//catch(Exception ex) {
			//    str.Append("Exception:\r\n");
			//    str.Append(ex.ToString());
			//}
			//finally {
			//    if(responseStream != null) {
			//        responseStream.Close();
			//        responseStream.Dispose();
			//    }

			//    if(webResponse != null) {
			//        webResponse.Close();
			//    }
			//}

			////watch.Stop();
			////TimeSpan span = watch.Elapsed;

			////this.lblRequestTime.Text = string.Format(
			////    "Request time: {0} - {1}ms ({2} Ticks)",
			////    span.ToString(),
			////    watch.ElapsedMilliseconds,
			////    watch.ElapsedTicks
			////);

			//// show result
			//this.txtResponse.Text = str.ToString();


			//if(str.Length > 0) {
			//    //this.txtResponse.Text = str.ToString();

			//    switch(parseType) {
			//        case WikiParseType.Article:
			//            this.lbResult.Visible = false;
			//            this.txtResponse.Visible = false;
			//            this.pBrowser.Visible = true;
			//            //this.wbResponse.Visible = true;
			//            this.wbResponse.Show();

			//            if(str.ToString() != Resources.WikipediaText.EmptyExportResult + "\r\n") {
			//                this.btnOpenEditor.Enabled = true;

			//                if(str.ToString().Contains("#REDIRECT")) {
			//                    //  <text xml:space="preserve">#REDIRECT [[Star Trek (2009)]]</text>
			//                    string tmp = str.ToString();
			//                    int start = tmp.IndexOf("<text xml:space=\"preserve\">") + ("<text xml:space=\"preserve\">").Length;
			//                    int end = tmp.IndexOf("</text>", start) - start;
			//                    tmp = tmp.Substring(start, end);
			//                    this.ReOpenDialog(WikipediaParser.ParseRedirect(tmp));
			//                }
			//                else {
			//                    this.txtResponse.Text = str.ToString();
			//                }
			//            }
			//            else {
			//                this.btnOpenEditor.Enabled = false;

			//                this.Cursor = Cursors.WaitCursor;

			//                this.ParseWikipedia(
			//                    searchWord,
			//                    WikiOutputType.Html,
			//                    WikiParseType.Search
			//                );

			//                this.Cursor = Cursors.Default;
			//            }
			//            break;

			//        case WikiParseType.Search:
			//            this.btnOpenEditor.Enabled = false;

			//            this.lbResult.Visible = true;
			//            this.txtResponse.Visible = false;
			//            this.pBrowser.Visible = false;
			//            //this.wbResponse.Visible = false;
			//            this.wbResponse.Hide();

			//            //<!-- Search results fetched via search=[search6], highlight=[search7], interwiki=[search10] in 104 ms -->
			//            //<ul class='mw-search-results'>

			//            string buffer = str.ToString();

			//            if(buffer.Contains("<ul class='mw-search-results'>")) {
			//                int start = buffer.IndexOf("<ul class='mw-search-results'>") + ( "<ul class='mw-search-results'>" ).Length;
			//                int end = buffer.IndexOf("</div><p class='mw-search-pager-bottom'>") - start;

			//                string output = buffer.Substring(start, end);

			//                //</ul>
			//                //</div><p class='mw-search-pager-bottom'>

			//                string[] items = output.Split(new string[] { "<li>" }, StringSplitOptions.RemoveEmptyEntries);

			//                foreach(string item in items) {
			//                    if(item != "\r\n") {
			//                        if(item.Contains("<a href=\"/wiki/")) {
			//                            int linkStart = item.IndexOf("<a href=\"/wiki/") + ( "<a href=\"/wiki/" ).Length;
			//                            int linkEnd = item.IndexOf("\" title=\"") - linkStart;

			//                            int titleStart = item.IndexOf("\">", linkEnd) + ( "\">" ).Length;
			//                            int titleEnd = item.IndexOf("</a>", linkEnd) - titleStart;

			//                            string link = item.Substring(linkStart, linkEnd);
			//                            string title = item.Substring(titleStart, titleEnd);

			//                            Regex textreplace = new Regex("(<[^>]*>)");
			//                            title = textreplace.Replace(title, "");

			//                            this.lbResult.Items.Add(title + " (Wiki Titel: " + link + ")");
			//                        }
			//                    }
			//                }

			//                //<li><a href="/wiki/American_History_X" title="American History X">
			//                //<span class='searchmatch'>American</span> <span class='searchmatch'>History</span> X</a>
			//                //<div class='searchresult'><span class='searchmatch'>American</span> <span class='searchmatch'>History</span> X ist ein Film aus dem Jahre 1998 .  Er beschäftigt sich mit der US-amerikanischen  Neonazi -Szene. Tony Kaye  führte  <b>…</b> 
			//                //</div>
			//                //</li>

			//                //this.txtResponse.Text = output;
			//            }
			//            else {
			//                StaticWindows.InfoBox(
			//                    "Es konnte kein Eintrag gefunden werden, bitte ändern Sie Ihre Suchanfrage."
			//                );
			//            }
			//            break;

			//        default:
			//            break;
			//    }
			//}
		}

		private void ReOpenDialog(string searchWord) {
			this._searchWord = searchWord;
			this._wikiParseType = WikiParseType.Article;

			this.DialogResult = DialogResult.Retry;

			this.Close();

			this.DialogResult = DialogResult.Retry;
		}
	}
}
