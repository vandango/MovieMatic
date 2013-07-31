using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Toenda.Foundation;
using Toenda.Foundation.Data;

using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Base {
	/// <summary>
	/// Class WikipediaParser
	/// </summary>
	public class WikipediaParser {
		private DataHandler _dh;
		private StaticHandler _sh;
		private DALSettings _cfg;
		private DAL _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="WikipediaParser"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		public WikipediaParser(string connectionString) {
			if(connectionString == null
			|| connectionString.Trim() == "") {
				throw new ArgumentNullException(
					"connectionString",
					"The connectionString parameter cannot be null."
				);
			}

			this._cfg = UdlParser.ParseConnectionString(connectionString);
			this._db = new DAL(this._cfg);
			this._dh = new DataHandler(connectionString);
			this._sh = new StaticHandler(connectionString);
		}

		public Movie Parse(string buffer) {
			Movie mov = new Movie();

			//mov.Note = buffer;
			
			if(buffer.Contains("{{Infobox_Film")) {
				buffer = buffer.Replace("{{Infobox_Film", "{{Infobox Film");
			}

			if(buffer.Contains("&lt;br /&gt;")) {
				buffer = buffer.Replace("&lt;br /&gt;", ",");
			}

			if(buffer.Contains("&quot;")) {
				buffer = buffer.Replace("&quot;", "");
			}

			if(buffer.Contains("'")) {
				buffer = buffer.Replace("'", "");
			}

			// remove :: {{Internetquelle
			if(buffer.Contains("{{Internetquelle")) {
				int start = buffer.IndexOf("{{Internetquelle");
				int end = buffer.IndexOf("}}", start) + 2;

				string buffer1 = buffer.Substring(0, start);
				string buffer2 = buffer.Substring(end);

				buffer = buffer1 + buffer2;
			}

			if(buffer.Contains("{{Infobox Film")) {
				int start = buffer.IndexOf("{{Infobox Film") + ( "{{Infobox Film" ).Length;
				int end = buffer.IndexOf("}}", start) - start;

				buffer = buffer.Substring(start, end);

				//{{Infobox_Film
				//|DT = American History X
				//|OT = American History X
				//|PL = [[USA]]
				//|PJ = 1998
				//|AF = 16
				//|LEN = 114
				//|OS = [[Englische Sprache|Englisch]]
				//|REG = [[Tony Kaye (Regisseur)|Tony Kaye]]
				//|DRB = [[David McKenna]]
				//|PRO = [[John Morrissey]], [[Michael De Luca]]
				//|MUSIK = [[Anne Dudley]]
				//|KAMERA = [[Tony Kaye (Regisseur)|Tony Kaye]], [[Gerald B. Greenberg]], [[Gerald B. Greenberg|G. B. Greenberg]]
				//|SCHNITT = [[Gerald B. Greenberg]], [[Alan Heim]]
				//|DS =
				//* [[Edward Norton]]: Derek Vinyard
				//* [[Edward Furlong]]: Danny Vinyard
				//* [[Beverly D’Angelo]]: Doris Vinyard
				//* [[Avery Brooks]]: Dr. Dr. Bob Sweeney
				//* [[Jennifer Lien]]: Davina Vinyard
				//* [[Elliott Gould]]: Murray
				//* [[Stacy Keach]]: Cameron Alexander
				//* [[Ethan Suplee]]: Seth Ryan
				//* [[Fairuza Balk]]: Stacey
				//* [[Guy Torry]]: Lamont
				//}}
				string[] lines;

				if(buffer.Contains("\r\n")) {
					lines = buffer.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				}
				else {
					lines = buffer.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
				}

				foreach(string line in lines) {
					// Deutscher Titel
					if((line.Contains("|") && line.Contains("DT") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Deutscher Titel") && line.Contains("="))) {
						mov.Name = line.Replace("|", "")
							.Replace("DT", "")
							.Replace("Dutscher Titel", "")
							.Replace("=", "").Trim();
					}
					// Original Titel
					else if((line.Contains("|") && line.Contains("OT") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Originaltitel") && line.Contains("="))) {
						if(mov.Name.IsNullOrTrimmedEmpty()) {
							mov.Name = line.Replace("|", "")
								.Replace("OT", "")
								.Replace("Originaltitel", "")
								.Replace("=", "").Trim();
						}
					}
					// Produktionsland
					else if((line.Contains("|") && line.Contains("PL") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Produktionsland") && line.Contains("="))) {
						string country = line.Replace("|", "")
							.Replace("PL", "")
							.Replace("Produktionsland", "")
							.Replace("=", "")
							.Replace("[", "")
							.Replace("]", "").Trim();

						if(country.Contains("USA")) {
							country = "Vereinigte Staaten von Amerika";
						}

						if(country.Contains(",")) {
							country = country.Substring(0, country.IndexOf(","));
						}

						List<Static> list = this._sh.SearchStaticItem("C002", country);

						if(list.Count == 1) {
							mov.CountryString = list[0].Content;
							mov.Country = list[0].Value;
						}
					}
					// Produktionsjahr
					else if((line.Contains("|") && line.Contains("PJ") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Erscheinungsjahr") && line.Contains("="))) {
						mov.Year = ParseTagYear(line).ToInt32();
						//mov.Year = line.Replace("|", "")
						//    .Replace("PJ", "")
						//    .Replace("=", "").Trim().ToInt32();
					}
					// Altersfreigabe
					else if(line.StartsWith("|AF = ")) {
					}
					// Länge
					else if(line.StartsWith("|LEN = ")) {
					}
					// Original Sprache
					else if(line.StartsWith("|OS = ")) {
					}
					// Regie
					else if((line.Contains("|") && line.Contains("REG") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Regie") && line.Contains("="))) {
						//string reg = "";
						//string tmp = "";
						//int regStart = 0;
						//int regEnd = 0;

						//if(line.Contains(")|")) {
						//    regStart = line.IndexOf(")|") + ( ")|" ).Length;
						//    regEnd = line.IndexOf("]]") - regStart;

						//    reg = line.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "");
						//}
						//else if(line.Contains("|")) {
						//    tmp = line.Replace("| REG", "").Replace("|REG", "");

						//    //regStart = tmp.IndexOf("|") + ( "|" ).Length;
						//    //regEnd = tmp.IndexOf("]]") - regStart;

						//    regStart = tmp.IndexOf("[[") + 2;
						//    regEnd = tmp.IndexOf("|") - regStart;

						//    reg = tmp.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}
						//else {
						//    reg = line.Replace("|", "")
						//        .Replace("REG", "")
						//        .Replace("=", "")
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}

						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsDirector) {
								mov.Directors.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsDirector = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsDirector = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Directors.Add(p);
							}
						}
					}
					// Drehbuch
					else if((line.Contains("|") && line.Contains("DRB") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Drehbuch") && line.Contains("="))) {
						//string drb = "";
						//string tmp = line.Replace("| DRB", "").Replace("|DRB", "");
						//int regStart = 0;
						//int regEnd = 0;

						//if(tmp.Contains(")|")) {
						//    regStart = tmp.IndexOf(")|") + ( ")|" ).Length;
						//    regEnd = tmp.IndexOf("]]") - regStart;

						//    drb = tmp.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "");
						//}
						//else if(tmp.Contains("|")) {
						//    //regStart = tmp.IndexOf("|") + ( "|" ).Length;
						//    //regEnd = tmp.IndexOf("]]") - regStart;

						//    regStart = tmp.IndexOf("[[") + 2;
						//    regEnd = tmp.IndexOf("|") - regStart;

						//    drb = tmp.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}
						//else {
						//    drb = tmp.Replace("|", "")
						//        .Replace("DRB", "")
						//        .Replace("=", "")
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}

						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsWriter) {
								mov.Writers.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsWriter = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsWriter = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Writers.Add(p);
							}
						}
					}
					// Produktion
					else if((line.Contains("|") && line.Contains("PRO") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Produzent") && line.Contains("="))) {
						//string pro = "";
						//string tmp = "";
						//int regStart = 0;
						//int regEnd = 0;

						//if(line.Contains(")|")) {
						//    regStart = line.IndexOf(")|") + ( ")|" ).Length;
						//    regEnd = line.IndexOf("]]") - regStart;

						//    pro = line.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "");
						//}
						//else if(line.Contains("|")) {
						//    tmp = line.Replace("| PRO", "").Replace("|PRO", "");

						//    //regStart = tmp.IndexOf("|") + ( "|" ).Length;
						//    //regEnd = tmp.IndexOf("]]") - regStart;

						//    regStart = tmp.IndexOf("[[") + 2;
						//    regEnd = tmp.IndexOf("|") - regStart;

						//    pro = tmp.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}
						//else {
						//    pro = line.Replace("|", "")
						//        .Replace("PRO", "")
						//        .Replace("=", "")
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}

						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsProducer) {
								mov.Producers.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsProducer = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsProducer = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Producers.Add(p);
							}
						}
					}
					// Musik
					else if((line.Contains("|") && line.Contains("MUSIK") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Musik") && line.Contains("="))) {
						string music = "";
						//string tmp = "";
						//int regStart = 0;
						//int regEnd = 0;

						//if(line.Contains(")|")) {
						//    regStart = line.IndexOf(")|") + ( ")|" ).Length;
						//    regEnd = line.IndexOf("]]") - regStart;

						//    music = line.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "");
						//}
						//else if(line.Contains("|")) {
						//    tmp = line.Replace("| MUSIK", "").Replace("|MUSIK", "");

						//    //regStart = tmp.IndexOf("|") + ( "|" ).Length;
						//    //regEnd = tmp.IndexOf("]]") - regStart;

						//    regStart = tmp.IndexOf("[[") + 2;
						//    regEnd = tmp.IndexOf("|") - regStart;

						//    music = tmp.Substring(regStart, regEnd)
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}
						//else {
						//    music = line.Replace("|", "")
						//        .Replace("MUSIK", "")
						//        .Replace("=", "")
						//        .Replace("[", "")
						//        .Replace("]", "").Trim();
						//}

						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsMusician) {
								mov.Musicians.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsMusician = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsMusician = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Musicians.Add(p);
							}
						}
					}
					// Kamera
					else if((line.Contains("|") && line.Contains("KAMERA") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Kamera") && line.Contains("="))) {
						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsCameraman) {
								mov.Cameramans.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsCameraman = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsCameraman = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Cameramans.Add(p);
							}
						}
					}
					// Schnitt
					else if((line.Contains("|") && line.Contains("SCHNITT") && line.Contains("="))
					|| (line.Contains("|") && line.Contains("Schnitt") && line.Contains("="))) {
						string tagList = ParseTagCrew(line);
						string[] names = tagList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

						foreach(string nameTemp in names) {
							string name = nameTemp.Trim();

							Person p = this._dh.GetPersonByName(name, "", MovieObjectType.All);

							if(p != null && p.IsCutter) {
								mov.Cutters.Add(p);
							}
							else {
								if(p == null) {
									// erstellen !!!!!!!!!

									p = new Person();

									if(name.Contains(" ") && name.IndexOf(" ") > 0) {
										p.Firstname = name.Substring(0, name.LastIndexOf(" ")).Trim();
										p.Lastname = name.Substring(name.LastIndexOf(" ")).Trim();
									}
									else {
										p.Lastname = name;
									}

									p.IsCutter = true;

									this._dh.SavePerson(p, SaveMethod.CreateNew);
								}
								else {
									p.IsCutter = true;

									this._dh.SavePerson(p, SaveMethod.SaveChanges);
								}

								mov.Cutters.Add(p);
							}
						}
					}
					// Darsteller / Schauspieler
					else if(line.Contains("|") && line.Contains("DS") && line.Contains("=")) {
					}
					else if(line.StartsWith("*")) {
						// Format: * [[Edward Norton]]: Derek Vinyard
						// Format: * [[Edward Norton|E. Norton]]: Derek Vinyard

						string actor = ParseTagActor(line);

						//string actor = iline.Replace("* ", "")
						//    .Replace("[", "")
						//    .Replace("]", "");

						string[] names = actor.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

						string[] actorNames = names[0].Split(new char[] { '|' });
						string actorName = "";

						if(actorNames.Length > 1) {
							actorName = actorNames[1];
						}
						else {
							actorName = actorNames[0];
						}

						string[] roleNames;
						string roleName = "";
						if(names.Length > 1) {
							roleNames = names[1].Split(new char[] { '|' });

							//if(roleNames.Length > 1) {
							//    roleName = roleNames[1];
							//}
							//else {
							//    roleName = roleNames[0];
							//}

							roleName = roleNames[0].Replace("[", "").Replace("]", "");
						}

						actorName = actorName.Trim();
						Person p = this._dh.GetPersonByName(actorName, "", MovieObjectType.All);

						if(p != null && p.IsActor) {
							if(names.Length > 1) {
								p.Rolename = roleName.Trim();
							}

							mov.Actors.Add(p);
						}
						else {
							if(p == null) {
								// erstellen !!!!!!!!!

								p = new Person();

								if(actorName.Contains(" ") && actorName.IndexOf(" ") > 0) {
									p.Firstname = actorName.Substring(0, actorName.LastIndexOf(" ")).Trim();
									p.Lastname = actorName.Substring(actorName.LastIndexOf(" ")).Trim();
								}
								else {
									p.Lastname = actorName;
								}

								p.IsActor = true;

								this._dh.SavePerson(p, SaveMethod.CreateNew);
							}
							else {
								p.IsActor = true;

								this._dh.SavePerson(p, SaveMethod.SaveChanges);
							}

							if(names.Length > 1) {
								p.Rolename = roleName.Trim();
							}

							mov.Actors.Add(p);
						}
					}
				}

				// default settings
				mov.Codec = Codec.Unknown;
				mov.Quality = Quality.Good_5;
				mov.Language = "de";
				mov.DiscAmount = 1;
				mov.Number = this._dh.GetNextMovieNumber();
			}
			else {
				mov = null;
			}

			return mov;
		}

		public static string ParseTagActor(string tag) {
			// * [[Edward Norton|E. Norton]]: Derek Vinyard

			tag = tag.Substring(tag.IndexOf("*"));

			string tmp = "";

			int regStart = 0;
			int regEnd = 0;

			if(tag.Contains("|")) {
				regStart = tag.IndexOf("[[") + 2;
				regEnd = tag.IndexOf("|") - regStart;
			}
			else if(tag.Contains("[")) {
				regStart = tag.IndexOf("[[") + 2;
				regEnd = tag.IndexOf("]]") - regStart;
			}
			else {
				regStart = 0;
				regEnd = tag.Length;
			}

			tmp = tag.Substring(regStart, regEnd)
				.Replace("=", "")
				.Replace("[", "")
				.Replace("]", "")
				.Replace("*", "").Trim();

			StringBuilder name = new StringBuilder();

			name.Append(tmp);

			if(tag.Contains(":")) {
				name.Append(tag.Substring(tag.IndexOf(":")));
			}

			return name.ToString();
		}

		public static string ParseTagCrew(string tag) {
			// |KAMERA = [[Tony Kaye (Regisseur)|Tony Kaye]], [[Gerald B. Greenberg]], [[Gerald B. Greenberg|G. B. Greenberg]]
			// = [[Jason Blum]],,Oren Peli

			tag = tag.Substring(tag.IndexOf("="));

			string[] links = tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			string tmp = "";

			int regStart = 0;
			int regEnd = 0;

			StringBuilder name = new StringBuilder();

			foreach(string link in links) {
				if(link.Contains("|")) {
					regStart = link.IndexOf("[[") + 2;
					regEnd = link.IndexOf("|") - regStart;
				}
				else if(link.Contains("[")) {
					regStart = link.IndexOf("[[") + 2;
					regEnd = link.IndexOf("]]") - regStart;
				}
				else {
					regStart = 0;
					regEnd = link.Length;
				}

				tmp = link.Substring(regStart, regEnd)
					.Replace("=", "")
					.Replace("[", "")
					.Replace("]", "").Trim();

				if(tmp.Contains("(")) {
					tmp = tmp.Substring(0, tmp.IndexOf("("));
				}

				name.Append(tmp.Trim());
				name.Append(",");
			}

			return name.ToString();
		}

		public static string ParseRedirect(string tag) {
			// #REDIRECT [[Star Trek (2009)]]

			tag = tag.Substring(tag.IndexOf("#REDIRECT"));

			int regStart = regStart = tag.IndexOf("[[") + 2;
			int regEnd = regEnd = tag.IndexOf("]]") - regStart;

			return tag.Substring(regStart, regEnd)
				.Replace("=", "")
				.Replace("[", "")
				.Replace("]", "").Trim();
		}

		public static string ParseTagYear(string tag) {
			// PJ = [[Filmjahr 1984|1984]]
			// PJ = [[1984]]
			//  [[2006]]|
			// PJ= [[2006]]|

			int regStart = 0;
			int regEnd = 0;

			if(tag.Contains("=")) {
				tag = tag.Substring(tag.IndexOf("=") + 1);

				if(tag.Contains("|") && tag.Contains("]") && !tag.EndsWith("|")) {
					regStart = tag.IndexOf("|") + 1;
					regEnd = tag.IndexOf("]]") - regStart;
				}
				else {
					regStart = 0; //tag.IndexOf("[[") + 2;
					regEnd = tag.Length; //tag.IndexOf("]]") - regStart;
				}
			}
			else {
				regStart = 0;
				regEnd = tag.Length;
			}

			return tag.Substring(regStart, regEnd)
				.Replace("=", "")
				.Replace("|", "")
				.Replace("[", "")
				.Replace("]", "").Trim();
		}
	}
}
