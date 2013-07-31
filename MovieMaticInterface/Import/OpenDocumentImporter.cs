using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.Utility;
using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Bean;

using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Tables;
using AODL.Document.SpreadsheetDocuments;

namespace Toenda.MovieMaticInterface.Import {
	/// <summary>
	/// Class OpenDocumentImporter ("is a" IImporter)
	/// </summary>
	public class OpenDocumentImporter : IImporter {
		private DataHandler _dh;
		private DALSettings _cfg;
		private DAL _db;
		private SpreadsheetDocument _doc = new SpreadsheetDocument();
		private string _filename;
		private int _allObjectsInFile = 0;

		// ---------------------------------------------------
		// CONSTRUCTORS
		// ---------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="filename">The filename including the path of the spreadsheet file.</param>
		/// <exception cref="ArgumentNullException">If the connectionString or filename parameter is null or empty.</exception>
		public OpenDocumentImporter(string connectionString, string filename) {
			if(connectionString == null
			|| connectionString.Trim() == "") {
				throw new ArgumentNullException(
					"connectionString",
					"The connectionString parameter canot be null."
				);
			}

			if(filename == null
			|| filename.Trim() == "") {
				throw new ArgumentNullException(
					"filename",
					"The filename parameter canot be null."
				);
			}

			this._filename = filename;

			this._cfg = UdlParser.ParseConnectionString(connectionString);
			this._db = new DAL(this._cfg);
			this._dh = new DataHandler(connectionString);
		}

		// ---------------------------------------------------
		// INTERFACE IMPLEMENTATIONS
		// ---------------------------------------------------

		/// <summary>
		/// Event ImportPercentEventHandler
		/// </summary>
		public event ImportPercentEventHandler PercentState;

		/// <summary>
		/// Load the file
		/// </summary>
		/// <exception cref="FileNotFoundException">If the file was not found or is not a file with the OpenDocument format.</exception>
		/// <returns>Returns true if the file was successfully open.</returns>
		public bool LoadFile() {
			this._doc.Load(this._filename);

			if(!this._doc.IsLoadedFile) {
				throw new FileNotFoundException(
					"The file [" + this._filename + "] does not exist or is not a file with the OpenDocument format.",
					this._filename
				);
			}

			return true;
		}

		/// <summary>
		/// Get a preview
		/// </summary>
		/// <returns></returns>
		public DataSet GetPreview() {
			if(this._doc.IsLoadedFile) {
				return this._ConvertSpreadsheetToDataset(false);
			}
			else {
				return null;
			}
		}

		/// <summary>
		/// Start the import
		/// </summary>
		/// <param name="allocation">The column allocation.</param>
		/// <returns>Returns true if the import was successfull.</returns>
		public bool Import(List<ColumnAllocation> allocation) {
			if(!this._doc.IsLoadedFile) {
				return false;
			}
			else {
				if(this._doc.TableCount > 0) {
					int percentMax = 50;
					double addValue = addValue = Convert.ToDouble(
						Convert.ToDouble(percentMax) / Convert.ToDouble(this._allObjectsInFile)
					);
					double throwValue = percentMax;
					int nextMovieNumber = this._dh.GetNextMovieNumber();

					DataSet data = this._ConvertSpreadsheetToDataset(true);
					
					DataSet content = this._db.ExecuteQuery("SELECT * FROM tbl_movies");

					ImportHelper ih = new ImportHelper();

					if(data.Tables[0].Rows.Count > 0) {
						DataTableReader reader = data.CreateDataReader();
						DataRow row;
						string tmp_str = "";
						bool tmp_bool = false;
						int tmp_int = 0;
						string newPkid = "";
						StringBuilder str = new StringBuilder();
						DataSet eData;

						try {
							while(reader.Read()) {
								row = content.Tables[0].NewRow();

								// data
								newPkid = Helper.NewGuid;
								row["pkid"] = newPkid;

								// number
								if(ih.CheckForAllocatedColumn(allocation, "number")) {
									if(!int.TryParse(reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "number")
										)
									).ToString(), out tmp_int)) {
										tmp_int = 0;
									}

									row["number"] = tmp_int;
								}
								else {
									row["number"] = nextMovieNumber;
								}

								// name
								if(ih.CheckForAllocatedColumn(allocation, "name")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "name")
										)
									).ToString();

									row["name"] = tmp_str + "";
								}
								else {
									row["name"] = "";
								}

								// note
								if(ih.CheckForAllocatedColumn(allocation, "note")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "note")
										)
									).ToString();

									row["note"] = tmp_str + "";
								}
								else {
									row["note"] = "";
								}

								// has_cover
								if(ih.CheckForAllocatedColumn(allocation, "has_cover")) {
									//if(!bool.TryParse(reader.GetValue(
									//    reader.GetOrdinal(
									//        ih.GetAllocatedColumnName(allocation, "has_cover")
									//    )
									//).ToString(), out tmp_bool)) {
									//    tmp_bool = false;
									//}

									//row["has_cover"] = tmp_bool;

									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "has_cover")
										)
									).ToString();

									if(tmp_str.ToLower().Contains("x")
									|| tmp_str.ToLower().IsExpressionTrue()) {
										row["has_cover"] = true;
									}
									else {
										row["has_cover"] = false;
									}
								}
								else {
									row["has_cover"] = false;
								}

								// is_original
								if(ih.CheckForAllocatedColumn(allocation, "is_original")) {
									//if(!bool.TryParse(reader.GetValue(
									//    reader.GetOrdinal(
									//        ih.GetAllocatedColumnName(allocation, "is_original")
									//    )
									//).ToString(), out tmp_bool)) {
									//    tmp_bool = false;
									//}

									//row["is_original"] = tmp_bool;

									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "is_original")
										)
									).ToString();

									if(tmp_str.ToLower().Contains("x")
									|| tmp_str.ToLower().IsExpressionTrue()) {
										row["is_original"] = true;
									}
									else {
										row["is_original"] = false;
									}
								}
								else {
									row["is_original"] = false;
								}

								// is_conferred
								if(ih.CheckForAllocatedColumn(allocation, "is_conferred")) {
									//if(!bool.TryParse(reader.GetValue(
									//    reader.GetOrdinal(
									//        ih.GetAllocatedColumnName(allocation, "is_conferred")
									//    )
									//).ToString(), out tmp_bool)) {
									//    tmp_bool = false;
									//}

									//row["is_conferred"] = tmp_bool;

									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "is_conferred")
										)
									).ToString();

									if(tmp_str.ToLower().Contains("x")
									|| tmp_str.ToLower().IsExpressionTrue()) {
										row["is_conferred"] = true;
									}
									else {
										row["is_conferred"] = false;
									}
								}
								else {
									row["is_conferred"] = false;
								}

								// codec
								Codec tmpCodec = Codec.Unknown;

								if(ih.CheckForAllocatedColumn(allocation, "codec")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "codec")
										)
									).ToString();

									if(tmp_str.ToLower().Contains("divx")) {
										tmpCodec = Codec.Divx;
										row["codec"] = (int)Codec.Divx;
									}
									else if(tmp_str.ToLower().Contains("dvd")) {
										tmpCodec = Codec.DVD;
										row["codec"] = (int)Codec.DVD;
									}
									else if(tmp_str.ToLower().Contains("mpeg")) {
										tmpCodec = Codec.MPEG;
										row["codec"] = (int)Codec.MPEG;
									}
									else if(tmp_str.ToLower().Contains("mvcd")) {
										tmpCodec = Codec.MVCD;
										row["codec"] = (int)Codec.MVCD;
									}
									else if(tmp_str.ToLower().Contains("svcd")) {
										tmpCodec = Codec.SVCD;
										row["codec"] = (int)Codec.SVCD;
									}
									else if(tmp_str.ToLower().Contains("vcd")) {
										tmpCodec = Codec.VCD;
										row["codec"] = (int)Codec.VCD;
									}
									else if(tmp_str.ToLower().Contains("wmv")) {
										tmpCodec = Codec.WMV;
										row["codec"] = (int)Codec.WMV;
									}
									else if(tmp_str.ToLower().Contains("xvid")) {
										tmpCodec = Codec.Xvid;
										row["codec"] = (int)Codec.Xvid;
									}
									else if(tmp_str.ToLower().Contains("vhs")) {
										tmpCodec = Codec.VHS;
										row["codec"] = (int)Codec.VHS;
									}
								}
								else {
									row["codec"] = 6;
								}

								// conferred_to
								if(ih.CheckForAllocatedColumn(allocation, "conferred_to")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "conferred_to")
										)
									).ToString();

									row["conferred_to"] = tmp_str + "";
								}
								else {
									row["conferred_to"] = "";
								}

								// disc_amount
								if(ih.CheckForAllocatedColumn(allocation, "disc_amount")) {
									if(!int.TryParse(reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "disc_amount")
										)
									).ToString(), out tmp_int)) {
										tmp_int = 0;
									}

									row["disc_amount"] = tmp_int;
								}
								else {
									row["disc_amount"] = 0;
								}

								// year
								if(ih.CheckForAllocatedColumn(allocation, "year")) {
									if(!int.TryParse(reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "year")
										)
									).ToString(), out tmp_int)) {
										tmp_int = 0;
									}

									row["year"] = tmp_int;
								}
								else {
									row["year"] = -1;
								}

								// country
								//????????? (string, but only the country-code)
								row["country"] = "";

								// quality
								//????????? (int)
								switch(tmpCodec) {
									case Codec.DVD:
										row["quality"] = 8;
										break;

									case Codec.VHS:
										row["quality"] = 3;
										break;

									default:
										row["quality"] = 5;
										break;
								}

								// GENRE
								if(ih.CheckForAllocatedColumn(allocation, "__GENRE")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "__GENRE")
										)
									).ToString();

									if(tmp_str.Length > 0) {
										string[] genres = tmp_str.Split(new char[] { ',' });

										List<Genre> lGenre = new List<Genre>();
										List<Genre> lNewGenre = new List<Genre>();

										foreach(string genre in genres) {
											Genre tmp = this._dh.GetGenreByName(genre);

											if(tmp != null) {
												lGenre.Add(tmp);
											}
											else {
												Genre g = new Genre();

												g.ID = Helper.NewGuid;
												g.Name = genre;

												lGenre.Add(g);
												lNewGenre.Add(g);
											}
										}

										// save genres
										this._dh.SaveNewGenreList(lNewGenre);

										// save genre link
										str.Remove(0, str.Length);
										str.Append("DELETE ");
										str.Append(" FROM tbl_movies_to_genres");
										str.Append(" WHERE movie_pkid = '" + newPkid + "'");

										this._db.ExecuteNonQuery(str.ToString());

										str.Remove(0, str.Length);
										str.Append("SELECT * ");
										str.Append(" FROM tbl_movies_to_genres");

										try {
											eData = null;
											eData = this._db.ExecuteQuery(str.ToString());
										}
										catch(Exception ex) {
											throw new Exception("Error on loading genres from table!", ex);
										}

										foreach(Genre g in lGenre) {
											DataRow hrow = eData.Tables[0].NewRow();

											hrow["pkid"] = Helper.NewGuid;
											hrow["genre_pkid"] = g.ID;
											hrow["movie_pkid"] = newPkid;

											eData.Tables[0].Rows.Add(hrow);
										}

										try {
											this._db.ExecuteUpdate(eData, "tbl_movies_to_genres");
										}
										catch(Exception ex) {
											throw new Exception("Error on updating genres to table!", ex);
										}
									}
								}

								// ACTOR
								if(ih.CheckForAllocatedColumn(allocation, "__ACTOR")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "__ACTOR")
										)
									).ToString();

									if(tmp_str.Length > 0) {
										string[] actors = tmp_str.Split(new char[] { ',' });

										List<Person> lActors = new List<Person>();
										List<Person> lNewActors = new List<Person>();

										foreach(string actor in actors) {
											PersonName name = Tools.SplitNameByLastSpace(actor);

											if(name != null) {
												Person person = this._dh.GetPersonByName(
													name.Firstname,
													name.Lastname,
													MovieObjectType.All
												);

												if(person != null) {
													person.IsActor = true;

													this._dh.SavePerson(
														person,
														SaveMethod.SaveChanges
													);

													lActors.Add(person);
												}
												else {
													Person p = new Person();

													p.ID = Helper.NewGuid;
													p.Firstname = name.Firstname;
													p.Lastname = name.Lastname;
													p.IsActor = true;

													this._dh.SavePerson(
														p, 
														SaveMethod.CreateNewWithoutID
													);

													lActors.Add(p);
													lNewActors.Add(p);
												}
											}
										}

										// save new persons
										//this._dh.SaveNewPersonList(lNewActors);

										// save person link
										//str.Remove(0, str.Length);
										//str.Append("DELETE ");
										//str.Append(" FROM tbl_movies_to_persons");
										//str.Append(" WHERE movie_pkid = '" + newPkid + "'");

										//this._db.ExecuteNonQuery(str.ToString());

										str.Remove(0, str.Length);
										str.Append("SELECT * ");
										str.Append(" FROM tbl_movies_to_persons");

										try {
											eData = null;
											eData = this._db.ExecuteQuery(str.ToString());
										}
										catch(Exception ex) {
											throw new Exception("Error on loading persons from table!", ex);
										}

										foreach(Person p in lActors) {
											DataRow hrow = eData.Tables[0].NewRow();

											hrow["pkid"] = Helper.NewGuid;
											hrow["person_pkid"] = p.ID;
											hrow["movie_pkid"] = newPkid;
											hrow["as_actor"] = true;

											eData.Tables[0].Rows.Add(hrow);
										}

										try {
											this._db.ExecuteUpdate(eData, "tbl_movies_to_persons");
										}
										catch(Exception ex) {
											throw new Exception("Error on updating persons to table!", ex);
										}
									}
								}

								// DIRECTOR
								if(ih.CheckForAllocatedColumn(allocation, "__DIRECTOR")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "__DIRECTOR")
										)
									).ToString();

									if(tmp_str.Length > 0) {
										string[] directors = tmp_str.Split(new char[] { ',' });

										List<Person> lDirectors = new List<Person>();
										List<Person> lNewDirectors = new List<Person>();

										foreach(string director in directors) {
											PersonName name = Tools.SplitNameByLastSpace(director);

											if(name != null) {
												Person person = this._dh.GetPersonByName(
													name.Firstname,
													name.Lastname,
													MovieObjectType.All
												);

												if(person != null) {
													person.IsDirector = true;

													this._dh.SavePerson(
														person,
														SaveMethod.SaveChanges
													);

													lDirectors.Add(person);
												}
												else {
													Person p = new Person();

													p.ID = Helper.NewGuid;
													p.Firstname = name.Firstname;
													p.Lastname = name.Lastname;
													p.IsDirector = true;

													this._dh.SavePerson(
														p,
														SaveMethod.CreateNewWithoutID
													);

													lDirectors.Add(p);
													lNewDirectors.Add(p);
												}
											}
										}

										// save new persons
										//this._dh.SaveNewPersonList(lNewDirectors);

										// save person link
										//str.Remove(0, str.Length);
										//str.Append("DELETE ");
										//str.Append(" FROM tbl_movies_to_persons");
										//str.Append(" WHERE movie_pkid = '" + newPkid + "'");

										//this._db.ExecuteNonQuery(str.ToString());

										str.Remove(0, str.Length);
										str.Append("SELECT * ");
										str.Append(" FROM tbl_movies_to_persons");

										try {
											eData = null;
											eData = this._db.ExecuteQuery(str.ToString());
										}
										catch(Exception ex) {
											throw new Exception("Error on loading persons from table!", ex);
										}

										foreach(Person p in lDirectors) {
											DataRow hrow = eData.Tables[0].NewRow();

											hrow["pkid"] = Helper.NewGuid;
											hrow["person_pkid"] = p.ID;
											hrow["movie_pkid"] = newPkid;
											hrow["as_director"] = true;

											eData.Tables[0].Rows.Add(hrow);
										}

										try {
											this._db.ExecuteUpdate(eData, "tbl_movies_to_persons");
										}
										catch(Exception ex) {
											throw new Exception("Error on updating persons to table!", ex);
										}
									}
								}

								// PRODUCER
								if(ih.CheckForAllocatedColumn(allocation, "__PRODUCER")) {
									tmp_str = reader.GetValue(
										reader.GetOrdinal(
											ih.GetAllocatedColumnName(allocation, "__PRODUCER")
										)
									).ToString();

									if(tmp_str.Length > 0) {
										string[] producers = tmp_str.Split(new char[] { ',' });

										List<Person> lProducers = new List<Person>();
										List<Person> lNewProducers = new List<Person>();

										foreach(string producer in producers) {
											PersonName name = Tools.SplitNameByLastSpace(producer);

											if(name != null) {
												Person person = this._dh.GetPersonByName(
													name.Firstname,
													name.Lastname,
													MovieObjectType.All
												);

												if(person != null) {
													person.IsProducer = true;

													this._dh.SavePerson(
														person,
														SaveMethod.SaveChanges
													);

													lProducers.Add(person);
												}
												else {
													Person p = new Person();

													p.ID = Helper.NewGuid;
													p.Firstname = name.Firstname;
													p.Lastname = name.Lastname;
													p.IsProducer = true;

													this._dh.SavePerson(
														p,
														SaveMethod.CreateNewWithoutID
													);

													lProducers.Add(p);
													lNewProducers.Add(p);
												}
											}
										}

										// save new persons
										//this._dh.SaveNewPersonList(lNewProducers);

										// save person link
										//str.Remove(0, str.Length);
										//str.Append("DELETE ");
										//str.Append(" FROM tbl_movies_to_persons");
										//str.Append(" WHERE movie_pkid = '" + newPkid + "'");

										//this._db.ExecuteNonQuery(str.ToString());

										str.Remove(0, str.Length);
										str.Append("SELECT * ");
										str.Append(" FROM tbl_movies_to_persons");

										try {
											eData = null;
											eData = this._db.ExecuteQuery(str.ToString());
										}
										catch(Exception ex) {
											throw new Exception("Error on loading persons from table!", ex);
										}

										foreach(Person p in lProducers) {
											DataRow hrow = eData.Tables[0].NewRow();

											hrow["pkid"] = Helper.NewGuid;
											hrow["person_pkid"] = p.ID;
											hrow["movie_pkid"] = newPkid;
											hrow["as_producer"] = true;

											eData.Tables[0].Rows.Add(hrow);
										}

										try {
											this._db.ExecuteUpdate(eData, "tbl_movies_to_persons");
										}
										catch(Exception ex) {
											throw new Exception("Error on updating persons to table!", ex);
										}
									}
								}

								throwValue += addValue;

								if(throwValue <= (percentMax * 2)) {
									this.PercentState(this, new ImportStateEventArgs(throwValue));
								}

								content.Tables[0].Rows.Add(row);

								nextMovieNumber++;
							}

							// save
							try {
								this._db.ExecuteUpdate(content, "tbl_movies");
							}
							catch(Exception ex) {
								throw new Exception("Error on updating movie to table (during import)!", ex);
							}
						}
						catch(Exception ex) {
							throw new Exception("Error on adding rows to the movies table (during import)!", ex);
						}
					}

					// send a last event with a value of 100 percent
					this.PercentState(this, new ImportStateEventArgs(percentMax + percentMax));
				}
			}

			return true;
		}

		/// <summary>
		/// Close the file and dispose all the resources
		/// </summary>
		public void Dispose() {
			this._doc.Dispose();
		}

		/// <summary>
		/// Convert the spreadsheet to a dataset
		/// </summary>
		/// <param name="withEventThrowing"></param>
		/// <returns></returns>
		private DataSet _ConvertSpreadsheetToDataset(bool withEventThrowing) {
			int count = 0;
			DataSet data = new DataSet();

			try {
				if(this._doc.TableCount > 0) {
					int percentMax = 50;
					double addValue = 0;
					double throwValue = 0;

					if(withEventThrowing) {
						addValue = Convert.ToDouble(
							Convert.ToDouble(percentMax) / Convert.ToDouble(this._allObjectsInFile)
						);
					}

					// only for my videos !!!!!!!!!!!!!!!!!!!!!!!
					bool isMy = false;
					FileInfo fi = new FileInfo(this._filename);
					if(fi.Name == "Videos.ods") {
						isMy = true;
					}

					TableCollection col = this._doc.TableCollection;

					foreach(Table tbl in col) {
						// add a table to the dataset for each
						// table in the document table collection
						data.Tables.Add(new DataTable());

						/*
						 * load column info																																																																			
						 * */
						List<ImportTableColumnSettings> list = new List<ImportTableColumnSettings>();

						RowCollection rc = tbl.RowCollection;

						foreach(Row r in rc) {
							if(count < 2) {
								CellCollection typecc = r.CellCollection;
								
								foreach(Cell c in typecc) {
									if(count == 0) {
										list.Add(new ImportTableColumnSettings());

										list[r.GetCellIndex(c)].Index = r.GetCellIndex(c);
										list[r.GetCellIndex(c)].Name = c.Node.InnerText;

										if(withEventThrowing) {
											throwValue += addValue;

											if(throwValue <= percentMax) {
												this.PercentState(this, new ImportStateEventArgs(throwValue));
											}
										}
										else {
											this._allObjectsInFile++;
										}
									}
									else if(count == 1) {
										//try {
										//    if(c.OfficeValueType != null) {
										//        switch(c.OfficeValueType.Trim()) {
										//            case "string":
										//                list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
										//                break;

										//            case "float":
										//                list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.Single");
										//                break;

										//            case "bool":
										//                list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.Boolean");
										//                break;

										//            default:
										//                list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
										//                break;
										//        }
										//    }
										//    else {
										//        list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
										//    }
										//}
										//catch(Exception) {
										//    list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
										//}

										if(isMy && ( r.GetCellIndex(c) == 3 || r.GetCellIndex(c) == 5 )) {
											list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.Boolean");
										}
										else {
											try {
												if(c.OfficeValueType != null) {
													switch(c.OfficeValueType.Trim()) {
														case "string":
															list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
															break;

														case "float":
															list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.Single");
															break;

														default:
															list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
															break;
													}
												}
												else {
													list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
												}
											}
											catch(Exception) {
												list[r.GetCellIndex(c)].CellDataType = Type.GetType("System.String");
											}
										}
									}
									else {
										break;
									}
								}
							}
							else {
								break;
							}

							count++;
						}

						//// genre
						//ImportTableColumnSettings genre = new ImportTableColumnSettings();
						//genre.CellDataType = Type.GetType("System.String");
						//genre.Name = "__GENRE";
						//list.Add(genre);

						//// actor
						//ImportTableColumnSettings actor = new ImportTableColumnSettings();
						//actor.CellDataType = Type.GetType("System.String");
						//actor.Name = "__ACTOR";
						//list.Add(actor);

						//// director
						//ImportTableColumnSettings director = new ImportTableColumnSettings();
						//director.CellDataType = Type.GetType("System.String");
						//director.Name = "__DIRECTOR";
						//list.Add(director);

						//// producer
						//ImportTableColumnSettings producer = new ImportTableColumnSettings();
						//producer.CellDataType = Type.GetType("System.String");
						//producer.Name = "__PRODUCER";
						//list.Add(producer);

						//if(withEventThrowing) {
						//    throwValue += addValue;

						//    if(throwValue <= percentMax) {
						//        this.PercentState(this, new ImportStateEventArgs(throwValue);
						//    }
						//}
						//else {
						//    this._allObjectsInFile++;
						//}

						count = 0;
						int amountOfRealColumns = 0;

						/*
						 * create dataset columns
						 * */
						foreach(ImportTableColumnSettings itcs in list) {
							if(itcs.IsRealObject) {
								data.Tables[0].Columns.Add(
									new DataColumn(
										itcs.Name,
										(itcs.CellDataType == null ? Type.GetType("System.String") : itcs.CellDataType)
									)
								);

								if(withEventThrowing) {
									throwValue += addValue;

									if(throwValue <= percentMax) {
										this.PercentState(this, new ImportStateEventArgs(throwValue));
									}
								}
								else {
									this._allObjectsInFile++;
								}

								amountOfRealColumns++;
							}
						}

						/*
						 * load data from rows
						 * */
						foreach(Row r in rc) {
							if(count > 0) {
								// cells
								CellCollection cc = r.CellCollection;

								try {
									// add rows to dataset
									DataRow row = data.Tables[0].NewRow();

									foreach(Cell c in cc) {
										if(r.GetCellIndex(c) < amountOfRealColumns) {
											// temp variables
											float tmp_float = 0;

											// save data
											if(isMy && (r.GetCellIndex(c) == 3 || r.GetCellIndex(c) == 5)) {
												if(c.Node.InnerText != null
												&& c.Node.InnerText.Trim() != ""
												&& (c.Node.InnerText.Trim().ToLower() == "x"
												|| c.Node.InnerText.Trim().IsExpressionTrue() )) {
													row[r.GetCellIndex(c)] = true;
												}
												else {
													row[r.GetCellIndex(c)] = false;
												}
											}
											else {
												try {
													if(c.OfficeValueType != null) {
														switch(c.OfficeValueType) {
															case "string":
																//row[r.GetCellIndex(c)] = c.Node.InnerText;
																foreach(Paragraph p in c.Content) {
																	foreach(SimpleText text in p.TextContent) {
																		row[r.GetCellIndex(c)] = text.Text;
																	}
																}
																break;

															case "float":
																foreach(Paragraph p in c.Content) {
																	foreach(SimpleText text in p.TextContent) {
																		//row[r.GetCellIndex(c)] = text.Text;
																		float.TryParse(text.Text, out tmp_float);
																	}
																}
																
																row[r.GetCellIndex(c)] = tmp_float;
																break;

															default:
																//row[r.GetCellIndex(c)] = c.Node.InnerText;
																foreach(Paragraph p in c.Content) {
																	foreach(SimpleText text in p.TextContent) {
																		row[r.GetCellIndex(c)] = text.Text;
																	}
																}
																break;
														}
													}
													else {
													    //row[r.GetCellIndex(c)] = c.Node.InnerText;

														foreach(Paragraph p in c.Content) {
															foreach(SimpleText text in p.TextContent) {
																row[r.GetCellIndex(c)] = text.Text;
															}
														}
													}
												}
												catch(Exception) {
													try {
														//row[r.GetCellIndex(c)] = (c.Node.InnerText != null ? c.Node.InnerText : "");
													}
													catch(Exception) {
														row[r.GetCellIndex(c)] = DBNull.Value;
													}
												}
											}

											if(withEventThrowing) {
												throwValue += addValue;

												if(throwValue <= percentMax) {
													this.PercentState(this, new ImportStateEventArgs(throwValue));
												}
											}
											else {
												this._allObjectsInFile++;
											}
										}
									}

									if(row[0] != DBNull.Value) {
										data.Tables[0].Rows.Add(row);
									}

									if(withEventThrowing) {
										throwValue += addValue;

										if(throwValue <= percentMax) {
											this.PercentState(this, new ImportStateEventArgs(throwValue));
										}
									}
									else {
										this._allObjectsInFile++;
									}
								}
								catch(Exception) {
								}
							}

							count++;
						}

						if(withEventThrowing) {
							// send a last event with a value of 100 percent
							this.PercentState(this, new ImportStateEventArgs(percentMax));
						}

						// break after the first table (for this time)
						break;
					}
				}
			}
			catch(Exception ex) {
				throw new Exception(
					"Error on loading data from OpenDocument spreadsheet!",
					ex
				);
			}

			return data;
		}
	}
}
