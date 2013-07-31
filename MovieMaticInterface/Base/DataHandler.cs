using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Toenda.MovieMaticInterface.Bean;

using Toenda.Foundation;
using Toenda.Foundation.Utility;
using Toenda.Foundation.Data;
using Toenda.Foundation.Types;

namespace Toenda.MovieMaticInterface.Base {
	/// <summary>
	/// Enum MovieObjectType
	/// </summary>
	public enum MovieObjectType {
		Genre = 0,
		Actor = 1,
		Director = 2,
		Producer = 3, 
		Category = 4,
		All = 5,
		Musician = 6,
		Cameraman = 7,
		Cutter = 8,
		Writer = 9
	}

	/// <summary>
	/// Enum NumberOperator
	/// </summary>
	public enum NumberOperator {
		/// <summary>
		/// 
		/// </summary>
		Min = 0,
		/// <summary>
		/// 
		/// </summary>
		Max = 1
	}

	/// <summary>
	/// SaveMethod
	/// </summary>
	public enum SaveMethod {
		SaveChanges = 0,
		CreateNew = 1, 
		CreateNewWithoutID = 2
	}

	/// <summary>
	/// Enum ImportType
	/// </summary>
	public enum ImportType {
		Excel2003 = 0,
		OpenDocument = 1
	}

	/// <summary>
	/// Enum FilterType
	/// </summary>
	public enum FilterType {
		NoFilter = 0,
		ResetFilter = 1,
		AllConferred = 2,
		AllOriginals = 3, 
		Codec = 4, 
		Genre = 5, 
		Name = 6,
		WithoutGenre = 7, 
		Actor = 8, 
		Director = 9, 
		Producer = 10,
		NameAndAllConferred = 11,
		NameAndAllOriginals = 12,
		NameAndCodec = 13,
		NameAndGenre = 14,
		ActorAndAllConferred = 15,
		ActorAndAllOriginals = 16,
		ActorAndCodec = 17,
		ActorAndGenre = 18,
		DirectorAndAllConferred = 19,
		DirectorAndAllOriginals = 20,
		DirectorAndCodec = 21,
		DirectorAndGenre = 22,
		ProducerAndAllConferred = 23,
		ProducerAndAllOriginals = 24,
		ProducerAndCodec = 25,
		ProducerAndGenre = 26,
		Country = 27,
		CountryAndAllConferred = 28,
		CountryAndAllOriginals = 29,
		Category = 30,
		WithoutCategory = 31,
		Musician = 32,
		Cutter = 33,
		Cameraman = 34,
		MusicianAndAllConferred = 35,
		MusicianAndAllOriginals = 36,
		MusicianAndCodec = 37,
		MusicianAndGenre = 38,
		CutterAndAllConferred = 39,
		CutterAndAllOriginals = 40,
		CutterAndCodec = 41,
		CutterAndGenre = 42,
		CameramanAndAllConferred = 43,
		CameramanAndAllOriginals = 44,
		CameramanAndCodec = 45,
		CameramanAndGenre = 46,
		Writer = 47,
		WriterAndAllConferred = 48,
		WriterAndAllOriginals = 49,
		WriterAndCodec = 50,
		WriterAndGenre = 51,
	}

	/// <summary>
	/// Class DataHandler
	/// </summary>
	public class DataHandler {
		private DALSettings _cfg;
		private DAL _db;

		// -------------------------------------------------------
		// CONSTRUCTORS
		// -------------------------------------------------------

		/// <summary>
		/// Default Ctor
		/// </summary>
		public DataHandler(string connectionString) {
			this._cfg = UdlParser.ParseConnectionString(connectionString);
			this._db = new DAL(this._cfg);
		}

		#region Public Members
		// -------------------------------------------------------
		// PUBLIC MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Checks the database version.
		/// </summary>
		public void CheckDatabaseVersion() {
			if(this._cfg.ProviderType != ProviderType.SQLite) {
				using(DAL dal = new DAL(this._cfg)) {
					dal.OpenConnection();

					/*
					 * for version 1.1.4
					 */
					bool hasSortValue = false;

					DbCommand cmd114 = dal.CreateCommand();

					if(this._cfg.ProviderType == ProviderType.SQLite) {
						cmd114.CommandText = "SELECT* FROM tbl_movies LIMIT 1";
					}
					else {
						cmd114.CommandText = "SELECT TOP 1 * FROM tbl_movies";
					}

					try {
						using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd114)) {
							for(int i = 0; i < reader.FieldCount; i++) {
								if(reader.GetName(i) == "sort_value") {
									hasSortValue = true;
									break;
								}
							}
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					try {
						if(!hasSortValue) {
							cmd114.CommandText = "ALTER TABLE tbl_movies ";
							cmd114.CommandText += "ADD	sort_value varchar(10) NULL;";
							cmd114.CommandText += "ALTER TABLE tbl_movies ";
							cmd114.CommandText += "ADD CONSTRAINT DF_tbl_movies_sort_value DEFAULT '' FOR sort_value;";

							dal.ExecuteNonQuery(cmd114);
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					/*
					 * for version 1.1.7
					 */
					bool hasRoleName = false;
					bool hasRoleType = false;

					DbCommand cmd117 = dal.CreateCommand();
					cmd117.CommandText = "SELECT TOP 1 * ";
					cmd117.CommandText += "FROM tbl_movies_to_persons";

					try {
						using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd117)) {
							for(int i = 0; i < reader.FieldCount; i++) {
								if(reader.GetName(i) == "role_name") {
									hasRoleName = true;
								}

								if(reader.GetName(i) == "role_type") {
									hasRoleType = true;
								}

								if(hasRoleName && hasRoleType) {
									break;
								}
							}
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					try {
						cmd117.CommandText = "";

						if(!hasRoleName) {
							cmd117.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd117.CommandText += "ADD role_name nvarchar(max) NULL;";
						}

						if(!hasRoleType) {
							cmd117.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd117.CommandText += "ADD role_type int NULL;";
						}

						if(!string.IsNullOrEmpty(cmd117.CommandText)) {
							dal.ExecuteNonQuery(cmd117);
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					/*
					 * for version 1.1.8
					 */
					//tbl_movies:language
					//tbl_movies_to_persons:as_musician
					//tbl_movies_to_persons:as_cutter
					//tbl_movies_to_persons:as_cameraman
					//tbl_movies_to_persons:as_writer
					//tbl_persons:is_cameraman
					//tbl_persons:is_musician
					//tbl_persons:is_cutter
					//tbl_persons:is_writer
					bool tbl_movies_language = false;

					DbCommand cmd118_tbl_movies = dal.CreateCommand();
					cmd118_tbl_movies.CommandText = "SELECT TOP 1 * ";
					cmd118_tbl_movies.CommandText += "FROM tbl_movies";

					try {
						using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd118_tbl_movies)) {
							for(int i = 0; i < reader.FieldCount; i++) {
								if(reader.GetName(i) == "language") {
									tbl_movies_language = true;
								}

								if(tbl_movies_language) {
									break;
								}
							}
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					try {
						if(!tbl_movies_language) {
							cmd118_tbl_movies.CommandText = "ALTER TABLE tbl_movies ";
							cmd118_tbl_movies.CommandText += "ADD language varchar(5) NOT NULL;";

							dal.ExecuteNonQuery(cmd118_tbl_movies);
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					bool tbl_movies_to_persons_as_musician = false;
					bool tbl_movies_to_persons_as_cutter = false;
					bool tbl_movies_to_persons_as_cameraman = false;
					bool tbl_movies_to_persons_as_writer = false;

					DbCommand cmd118_tbl_movies_to_persons = dal.CreateCommand();
					cmd118_tbl_movies_to_persons.CommandText = "SELECT TOP 1 * ";
					cmd118_tbl_movies_to_persons.CommandText += "FROM tbl_movies_to_persons";

					try {
						using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd118_tbl_movies_to_persons)) {
							for(int i = 0; i < reader.FieldCount; i++) {
								if(reader.GetName(i) == "as_musician") {
									tbl_movies_to_persons_as_musician = true;
								}

								if(reader.GetName(i) == "as_cutter") {
									tbl_movies_to_persons_as_cutter = true;
								}

								if(reader.GetName(i) == "as_cameraman") {
									tbl_movies_to_persons_as_cameraman = true;
								}

								if(reader.GetName(i) == "as_writer") {
									tbl_movies_to_persons_as_writer = true;
								}

								if(tbl_movies_to_persons_as_musician
								&& tbl_movies_to_persons_as_cutter
								&& tbl_movies_to_persons_as_cameraman
								&& tbl_movies_to_persons_as_writer) {
									break;
								}
							}
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					try {
						cmd118_tbl_movies_to_persons.CommandText = "";

						if(!tbl_movies_to_persons_as_musician) {
							cmd118_tbl_movies_to_persons.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd118_tbl_movies_to_persons.CommandText += "ADD as_musician bit NULL;";
						}

						if(!tbl_movies_to_persons_as_cutter) {
							cmd118_tbl_movies_to_persons.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd118_tbl_movies_to_persons.CommandText += "ADD as_cutter bit NULL;";
						}

						if(!tbl_movies_to_persons_as_cameraman) {
							cmd118_tbl_movies_to_persons.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd118_tbl_movies_to_persons.CommandText += "ADD as_cameraman bit NULL;";
						}

						if(!tbl_movies_to_persons_as_writer) {
							cmd118_tbl_movies_to_persons.CommandText += "ALTER TABLE tbl_movies_to_persons ";
							cmd118_tbl_movies_to_persons.CommandText += "ADD as_writer bit NULL;";
						}

						if(!string.IsNullOrEmpty(cmd118_tbl_movies_to_persons.CommandText)) {
							dal.ExecuteNonQuery(cmd118_tbl_movies_to_persons);
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					bool tbl_persons_is_cameraman = false;
					bool tbl_persons_is_musician = false;
					bool tbl_persons_is_cutter = false;
					bool tbl_persons_is_writer = false;

					DbCommand cmd118_tbl_persons = dal.CreateCommand();
					cmd118_tbl_persons.CommandText = "SELECT TOP 1 * ";
					cmd118_tbl_persons.CommandText += "FROM tbl_persons";

					try {
						using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd118_tbl_persons)) {
							for(int i = 0; i < reader.FieldCount; i++) {
								if(reader.GetName(i) == "is_musician") {
									tbl_persons_is_cameraman = true;
								}

								if(reader.GetName(i) == "is_cutter") {
									tbl_persons_is_musician = true;
								}

								if(reader.GetName(i) == "is_cameraman") {
									tbl_persons_is_cutter = true;
								}

								if(reader.GetName(i) == "is_writer") {
									tbl_persons_is_writer = true;
								}

								if(tbl_persons_is_cameraman
								&& tbl_persons_is_musician
								&& tbl_persons_is_cutter
								&& tbl_persons_is_writer) {
									break;
								}
							}
						}
					}
					catch(Exception ex) {
						throw ex;
					}

					try {
						cmd118_tbl_persons.CommandText = "";

						if(!tbl_persons_is_cameraman) {
							cmd118_tbl_persons.CommandText += "ALTER TABLE tbl_persons ";
							cmd118_tbl_persons.CommandText += "ADD is_musician bit NULL;";
						}

						if(!tbl_persons_is_musician) {
							cmd118_tbl_persons.CommandText += "ALTER TABLE tbl_persons ";
							cmd118_tbl_persons.CommandText += "ADD is_cutter bit NULL;";
						}

						if(!tbl_persons_is_cutter) {
							cmd118_tbl_persons.CommandText += "ALTER TABLE tbl_persons ";
							cmd118_tbl_persons.CommandText += "ADD is_cameraman bit NULL;";
						}

						if(!tbl_persons_is_writer) {
							cmd118_tbl_persons.CommandText += "ALTER TABLE tbl_persons ";
							cmd118_tbl_persons.CommandText += "ADD is_writer bit NULL;";
						}

						if(!string.IsNullOrEmpty(cmd118_tbl_persons.CommandText)) {
							dal.ExecuteNonQuery(cmd118_tbl_persons);
						}
					}
					catch(Exception ex) {
						throw ex;
					}
				}
			}
		}

		/// <summary>
		/// Get a movie
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Movie GetMovie(string id) {
			string tmp = "";
			Movie ret = null;
			List<Genre> glist = new List<Genre>();
			List<Person> alist = new List<Person>();
			List<Person> dlist = new List<Person>();
			List<Person> plist = new List<Person>();
			List<Person> mlist = new List<Person>();
			List<Person> calist = new List<Person>();
			List<Person> culist = new List<Person>();
			List<Person> wlist = new List<Person>();
			List<Category> clist = new List<Category>();
			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE pkid = '" + id + "'");
			}
			else {
				str.Append(" WHERE lower(pkid) = lower('{" + id + "}')");
			}

			//DataTableReader dtr = null;

			//this._db.OpenConnection();

			//SqlDataReader reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
			//    str.ToString(), 
			//    CommandType.Text
			//);

			using(DAL dal = new DAL(this._cfg)) {
				IDbCommand cmd = dal.CreateCommand();
				cmd.CommandText = str.ToString();

				dal.OpenConnection();

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					if(reader.Read()) {
						tmp = reader["pkid"].ToString();

						// movie
						ret = new Movie();

						ret.Id = reader.GetSafeValue<Guid>("pkid").ToString();
						ret.Number = reader.GetSafeValue<int>("number");
						ret.Name = reader.GetSafeValue<string>("name");
						ret.Note = reader.GetSafeValue<string>("note");
						ret.HasCover = reader.GetSafeValue<bool>("has_cover");
						ret.IsOriginal = reader.GetSafeValue<bool>("is_original");
						ret.IsConferred = reader.GetSafeValue<bool>("is_conferred");
						ret.ConferredTo = reader.GetSafeValue<string>("conferred_to");
						ret.Codec = (Codec)reader.GetSafeValue<int>("codec");
						ret.Actors = new List<Person>();
						ret.Directors = new List<Person>();
						ret.Producers = new List<Person>();
						ret.Musicians = new List<Person>();
						ret.Cameramans = new List<Person>();
						ret.Cutters = new List<Person>();
						ret.Writers = new List<Person>();
						ret.Genres = new List<Genre>();
						ret.DiscAmount = reader.GetSafeValue<int>("disc_amount");
						ret.Year = reader.GetSafeValue<int>("year");
						ret.Country = reader.GetSafeValue<string>("country");
						ret.Quality = (Quality)reader.GetSafeValue<int>("quality");
						ret.SortValue = ( reader["sort_value"] == DBNull.Value ? "" : reader.GetSafeValue<string>("sort_value") );
						ret.Language = ( reader["language"] == DBNull.Value ? "" : reader.GetSafeValue<string>("language") );

						//ret = new Movie() {
						//    Id = tmp,
						//    Number = reader["number"),
						//    Name = reader.GetSafeValue<string>("name"),
						//    Note = reader.GetSafeValue<string>("note"),
						//    HasCover = reader.GetSafeValue<bool>("has_cover"),
						//    IsOriginal = reader.GetSafeValue<bool>("is_original"),
						//    IsConferred = reader.GetSafeValue<bool>("is_conferred"),
						//    ConferredTo = reader.GetSafeValue<string>("conferred_to"),
						//    Codec = (Codec)reader.GetSafeValue<int>("codec"),
						//    Actors = alist,
						//    Directors = dlist,
						//    Producers = plist,
						//    Musicians = mlist,
						//    Cameramans = calist,
						//    Cutters = culist,
						//    Writers = wlist,
						//    Genres = glist,
						//    Categories = clist,
						//    DiscAmount = reader.GetSafeValue<int>("disc_amount"),
						//    Year = reader.GetSafeValue<int>("year"),
						//    Country = reader.GetSafeValue<string>("country"),
						//    Quality = (Quality)reader.GetSafeValue<int>("quality"),
						//    SortValue = ( reader["sort_value"] == DBNull.Value ? "" : reader.GetSafeValue<string>("sort_value") ),
						//    Language = ( reader["language"] == DBNull.Value ? "" : reader.GetSafeValue<string>("language") )
						//};
					}
				}

				// load additionals
				if(ret != null) {
					// genres
					str.Remove(0, str.Length);
					str.Append("SELECT g.*");
					str.Append(" FROM tbl_genres AS g");
					str.Append(" INNER JOIN tbl_movies_to_genres AS mg");
					str.Append(" ON mg.genre_pkid = g.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mg.movie_pkid = '" + tmp + "'");
					}
					else {
						str.Append(" WHERE lower(mg.movie_pkid) = lower('{" + tmp + "}')");
					}

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdGenre = dal.CreateCommand();
					cmdGenre.CommandText = str.ToString();

					using(IDataReader genreReader = dal.ExecuteQueryForDataReader(cmdGenre)) {
						while(genreReader.Read()) {
							glist.Add(
								new Genre(
									genreReader.GetSafeValue<Guid>("pkid").ToString(),
									genreReader.GetSafeValue<string>("name")
								)
							);
						}
					}

					// categories
					str.Remove(0, str.Length);
					str.Append("SELECT c.*");
					str.Append(" FROM tbl_categories AS c");
					str.Append(" INNER JOIN tbl_movies_to_categories AS mc");
					str.Append(" ON mc.category_pkid = c.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mc.movie_pkid = '" + tmp + "'");
					}
					else {
						str.Append(" WHERE lower(mc.movie_pkid) = lower('{" + tmp + "}')");
					}

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdCategories = dal.CreateCommand();
					cmdCategories.CommandText = str.ToString();

					using(IDataReader categoriesReader = dal.ExecuteQueryForDataReader(cmdCategories)) {
						while(categoriesReader.Read()) {
							clist.Add(
								new Category(
									categoriesReader.GetSafeValue<Guid>("pkid").ToString(),
									categoriesReader.GetSafeValue<string>("name")
								)
							);
						}
					}

					// actors
					str.Remove(0, str.Length);
					str.Append("SELECT p.*, mp.role_name, mp.role_type ");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_actor = 1");
						str.Append(" AND mp.as_actor = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_actor = 'True'");
						str.Append(" AND mp.as_actor = 'True'");
					}

					str.Append(" ORDER BY mp.role_type ASC, p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdActors = dal.CreateCommand();
					cmdActors.CommandText = str.ToString();

					using(IDataReader actorsReader = dal.ExecuteQueryForDataReader(cmdActors)) {
						while(actorsReader.Read()) {
							alist.Add(
								new Person(
									actorsReader.GetSafeValue<Guid>("pkid").ToString(),
									actorsReader.GetSafeValue<string>("firstname"),
									actorsReader.GetSafeValue<string>("lastname"),
									actorsReader.GetSafeValue<bool>("is_actor"),
									actorsReader.GetSafeValue<bool>("is_director"),
									actorsReader.GetSafeValue<bool>("is_producer"),
									( actorsReader["is_cameraman"] == DBNull.Value ? false : actorsReader.GetSafeValue<bool>("is_cameraman") ),
									( actorsReader["is_cutter"] == DBNull.Value ? false : actorsReader.GetSafeValue<bool>("is_cutter") ),
									( actorsReader["is_musician"] == DBNull.Value ? false : actorsReader.GetSafeValue<bool>("is_musician") ),
									( actorsReader["is_writer"] == DBNull.Value ? false : actorsReader.GetSafeValue<bool>("is_writer") )
								) {
									Rolename = actorsReader.GetSafeValue<string>("role_name"),
									Roletype = (
										actorsReader["role_type"] == DBNull.Value 
										? "" 
										: (actorsReader.GetSafeValue<int>("role_type")).ToString()
									)
								}
							);
						}
					}

					// directors
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_director = 1");
						str.Append(" AND mp.as_director = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_director = 'True'");
						str.Append(" AND mp.as_director = 'True'");
					}
					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdDirectors = dal.CreateCommand();
					cmdDirectors.CommandText = str.ToString();

					using(IDataReader directorsReader = dal.ExecuteQueryForDataReader(cmdDirectors)) {
						while(directorsReader.Read()) {
							dlist.Add(
								new Person(
									directorsReader.GetSafeValue<Guid>("pkid").ToString(),
									directorsReader.GetSafeValue<string>("firstname"),
									directorsReader.GetSafeValue<string>("lastname"),
									directorsReader.GetSafeValue<bool>("is_actor"),
									directorsReader.GetSafeValue<bool>("is_director"),
									directorsReader.GetSafeValue<bool>("is_producer"),
									( directorsReader["is_cameraman"] == DBNull.Value ? false : directorsReader.GetSafeValue<bool>("is_cameraman") ),
									( directorsReader["is_cutter"] == DBNull.Value ? false : directorsReader.GetSafeValue<bool>("is_cutter") ),
									( directorsReader["is_musician"] == DBNull.Value ? false : directorsReader.GetSafeValue<bool>("is_musician") ),
									( directorsReader["is_writer"] == DBNull.Value ? false : directorsReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					// producers
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_producer = 1");
						str.Append(" AND mp.as_producer = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_producer = 'True'");
						str.Append(" AND mp.as_producer = 'True'");
					}

					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdProducers = dal.CreateCommand();
					cmdProducers.CommandText = str.ToString();

					using(IDataReader producersReader = dal.ExecuteQueryForDataReader(cmdProducers)) {
						while(producersReader.Read()) {
							plist.Add(
								new Person(
									producersReader.GetSafeValue<Guid>("pkid").ToString(),
									producersReader.GetSafeValue<string>("firstname"),
									producersReader.GetSafeValue<string>("lastname"),
									producersReader.GetSafeValue<bool>("is_actor"),
									producersReader.GetSafeValue<bool>("is_director"),
									producersReader.GetSafeValue<bool>("is_producer"),
									( producersReader["is_cameraman"] == DBNull.Value ? false : producersReader.GetSafeValue<bool>("is_cameraman") ),
									( producersReader["is_cutter"] == DBNull.Value ? false : producersReader.GetSafeValue<bool>("is_cutter") ),
									( producersReader["is_musician"] == DBNull.Value ? false : producersReader.GetSafeValue<bool>("is_musician") ),
									( producersReader["is_writer"] == DBNull.Value ? false : producersReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					// musician
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_musician = 1");
						str.Append(" AND mp.as_musician = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_musician = 'True'");
						str.Append(" AND mp.as_musician = 'True'");
					}

					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdMusician = dal.CreateCommand();
					cmdMusician.CommandText = str.ToString();

					using(IDataReader musicianReader = dal.ExecuteQueryForDataReader(cmdMusician)) {
						while(musicianReader.Read()) {
							mlist.Add(
								new Person(
									musicianReader.GetSafeValue<Guid>("pkid").ToString(),
									musicianReader.GetSafeValue<string>("firstname"),
									musicianReader.GetSafeValue<string>("lastname"),
									musicianReader.GetSafeValue<bool>("is_actor"),
									musicianReader.GetSafeValue<bool>("is_director"),
									musicianReader.GetSafeValue<bool>("is_producer"),
									( musicianReader["is_cameraman"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_cameraman") ),
									( musicianReader["is_cutter"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_cutter") ),
									( musicianReader["is_musician"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_musician") ),
									( musicianReader["is_writer"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					// cameraman
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_cameraman = 1");
						str.Append(" AND mp.as_cameraman = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_cameraman = 'True'");
						str.Append(" AND mp.as_cameraman = 'True'");
					}

					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdCameraman = dal.CreateCommand();
					cmdCameraman.CommandText = str.ToString();

					using(IDataReader cameramanReader = dal.ExecuteQueryForDataReader(cmdCameraman)) {
						while(cameramanReader.Read()) {
							calist.Add(
								new Person(
									cameramanReader.GetSafeValue<Guid>("pkid").ToString(),
									cameramanReader.GetSafeValue<string>("firstname"),
									cameramanReader.GetSafeValue<string>("lastname"),
									cameramanReader.GetSafeValue<bool>("is_actor"),
									cameramanReader.GetSafeValue<bool>("is_director"),
									cameramanReader.GetSafeValue<bool>("is_producer"),
									( cameramanReader["is_cameraman"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_cameraman") ),
									( cameramanReader["is_cutter"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_cutter") ),
									( cameramanReader["is_musician"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_musician") ),
									( cameramanReader["is_writer"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					// cutter
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_cutter = 1");
						str.Append(" AND mp.as_cutter = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_cutter = 'True'");
						str.Append(" AND mp.as_cutter = 'True'");
					}

					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdCutter = dal.CreateCommand();
					cmdCutter.CommandText = str.ToString();

					using(IDataReader cutterReader = dal.ExecuteQueryForDataReader(cmdCutter)) {
						while(cutterReader.Read()) {
							culist.Add(
								new Person(
									cutterReader.GetSafeValue<Guid>("pkid").ToString(),
									cutterReader.GetSafeValue<string>("firstname"),
									cutterReader.GetSafeValue<string>("lastname"),
									cutterReader.GetSafeValue<bool>("is_actor"),
									cutterReader.GetSafeValue<bool>("is_director"),
									cutterReader.GetSafeValue<bool>("is_producer"),
									( cutterReader["is_cameraman"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_cameraman") ),
									( cutterReader["is_cutter"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_cutter") ),
									( cutterReader["is_musician"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_musician") ),
									( cutterReader["is_writer"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					// writer
					str.Remove(0, str.Length);
					str.Append("SELECT p.*");
					str.Append(" FROM tbl_persons AS p");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.person_pkid = p.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
						str.Append(" AND p.is_writer = 1");
						str.Append(" AND mp.as_writer = 1");
					}
					else {
						str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
						str.Append(" AND p.is_writer = 'True'");
						str.Append(" AND mp.as_writer = 'True'");
					}

					str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");

					//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();
					IDbCommand cmdWriter = dal.CreateCommand();
					cmdWriter.CommandText = str.ToString();

					using(IDataReader writerReader = dal.ExecuteQueryForDataReader(cmdWriter)) {
						while(writerReader.Read()) {
							wlist.Add(
								new Person(
									writerReader.GetSafeValue<Guid>("pkid").ToString(),
									writerReader.GetSafeValue<string>("firstname"),
									writerReader.GetSafeValue<string>("lastname"),
									writerReader.GetSafeValue<bool>("is_actor"),
									writerReader.GetSafeValue<bool>("is_director"),
									writerReader.GetSafeValue<bool>("is_producer"),
									( writerReader["is_cameraman"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_cameraman") ),
									( writerReader["is_cutter"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_cutter") ),
									( writerReader["is_musician"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_musician") ),
									( writerReader["is_writer"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_writer") )
								)
							);
						}
					}

					ret.Genres = glist;
					ret.Categories = clist;
					ret.Actors = alist;
					ret.Directors = dlist;
					ret.Producers = plist;
					ret.Cameramans = calist;
					ret.Cutters = culist;
					ret.Musicians = mlist;
					ret.Writers = wlist;
				}
			}

			return ret;
		}

		/// <summary>
		/// Gets the movie list with no added persons.
		/// </summary>
		/// <returns></returns>
		public List<Movie> GetEmptyPersonOnMovieList() {
			List<Movie> list = new List<Movie>();

			list = this.GetMovieListBySqlQuery(
				SqlResources.GetEmptyPersonOnMovieList, 
				FilterType.NoFilter, 
				true
			);

			return list;
		}

		/// <summary>
		/// Get a list of movies
		/// </summary>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="sortDirection">The sort direction.</param>
		/// <returns></returns>
		public List<Movie> GetMovieList(string sortExpression, DataSortDirection sortDirection) {
			return this.GetMovieList(FilterType.NoFilter, "", 0, false, sortExpression, sortDirection);
		}

		/// <summary>
		/// Get a list of movies
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="sortDirection">The sort direction.</param>
		/// <returns></returns>
		public List<Movie> GetMovieList(
			FilterType filter,
			string sortExpression,
			DataSortDirection sortDirection) {
			return this.GetMovieList(filter, "", 0, false, sortExpression, sortDirection);
		}

		/// <summary>
		/// Get a list of movies
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="value">The value.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="sortDirection">The sort direction.</param>
		/// <returns></returns>
		public List<Movie> GetMovieList(
			FilterType filter, 
			string value,
			string sortExpression,
			DataSortDirection sortDirection) {
			return this.GetMovieList(filter, value, 0, false, sortExpression, sortDirection);
		}

		/// <summary>
		/// Get a list of movies
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="value">The value.</param>
		/// <param name="number">The number.</param>
		/// <param name="withAdditionals">if set to <c>true</c> [with additionals].</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="sortDirection">The sort direction.</param>
		/// <returns></returns>
		public List<Movie> GetMovieList(
			FilterType filter, 
			string value, 
			int number, 
			bool withAdditionals, 
			string sortExpression,
			Toenda.Foundation.DataSortDirection sortDirection) {
			List<Movie> list = new List<Movie>();

			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies AS m");
			str.Append(" ");
			str.Append(" INNER JOIN tbl_static AS s");
			str.Append(" ON s.value = m.country AND s.tag = 'C002'");
			str.Append(" ");

			str = this.GetMovieListSqlString(str, filter, value, number);

			//str.Append(" ORDER BY m.sort_value ASC, m.number ASC");

			// add sort expression
			if(!string.IsNullOrEmpty(sortExpression)) {
				str.Append("\r\n");
				str.AppendFormat(
					SqlResources.GetMovieList_SortOrder,
					"m." + sortExpression,
					( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
				);

				if(sortExpression == "sort_value") {
					str.AppendFormat(
						", m.number {0}",
						( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
					);
				}
				else {
					str.AppendFormat(
						", m.sort_value {0}",
						( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
					);
				}
			}

			list = this.GetMovieListBySqlQuery(str.ToString(), filter, withAdditionals);

			return list;
		}

		/// <summary>
		/// Adds the movie additionals.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public List<Movie> AddMovieAdditionals(List<Movie> list) {
			DataTableReader dtr = null;

			try {
				this._db.OpenConnection();
			}
			catch(Exception ex) {
				throw new Exception("Error on opening a connection!", ex);
			}

			StringBuilder str = new StringBuilder();

			foreach(Movie mov in list) {
				string tmp = mov.Id;

				//List<Genre> glist = new List<Genre>();
				List<Person> alist = new List<Person>();
				List<Person> dlist = new List<Person>();
				List<Person> plist = new List<Person>();
				List<Person> mlist = new List<Person>();
				List<Person> calist = new List<Person>();
				List<Person> culist = new List<Person>();
				List<Person> wlist = new List<Person>();

				//// genres
				//str.Remove(0, str.Length);
				//str.Append("SELECT g.*");
				//str.Append(" FROM tbl_genres AS g");
				//str.Append(" INNER JOIN tbl_movies_to_genres AS mg");
				//str.Append(" ON mg.genre_pkid = g.pkid");
				//str.Append(" WHERE mg.movie_pkid = '" + tmp + "'");

				//dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				//if(dtr != null && dtr.HasRows) {
				//    while(dtr.Read()) {
				//        glist.Add(
				//            new Genre(
				//                dtr["pkid"].ToString(),
				//                dtr["name"].ToString()
				//            )
				//        );
				//    }
				//}

				//dtr = null;

				// actors
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_actor = 1");
					str.Append(" AND mp.as_actor = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_actor = 'True'");
					str.Append(" AND mp.as_actor = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						alist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// directors
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_director = 1");
					str.Append(" AND mp.as_director = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_director = 'True'");
					str.Append(" AND mp.as_director = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						dlist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// producers
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_producer = 1");
					str.Append(" AND mp.as_producer = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_producer = 'True'");
					str.Append(" AND mp.as_producer = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						plist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// musician
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_musician = 1");
					str.Append(" AND mp.as_musician = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_musician = 'True'");
					str.Append(" AND mp.as_musician = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						mlist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// cameraman
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_cameraman = 1");
					str.Append(" AND mp.as_cameraman = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_cameraman = 'True'");
					str.Append(" AND mp.as_cameraman = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						calist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// cutter
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_cutter = 1");
					str.Append(" AND mp.as_cutter = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_cutter = 'True'");
					str.Append(" AND mp.as_cutter = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						culist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				// writer
				str.Remove(0, str.Length);
				str.Append("SELECT p.*");
				str.Append(" FROM tbl_persons AS p");
				str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
				str.Append(" ON mp.person_pkid = p.pkid");

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					str.Append(" WHERE mp.movie_pkid = '" + tmp + "'");
					str.Append(" AND p.is_writer = 1");
					str.Append(" AND mp.as_writer = 1");
				}
				else {
					str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + tmp + "}')");
					str.Append(" AND p.is_writer = 'True'");
					str.Append(" AND mp.as_writer = 'True'");
				}

				dtr = this._db.ExecuteQuery(str.ToString()).CreateDataReader();

				if(dtr != null && dtr.HasRows) {
					while(dtr.Read()) {
						wlist.Add(
							new Person(
								dtr.GetSafeValue<Guid>("pkid").ToString(),
								dtr.GetSafeValue<string>("firstname"),
								dtr.GetSafeValue<string>("lastname"),
								dtr.GetSafeValue<bool>("is_actor"),
								dtr.GetSafeValue<bool>("is_director"),
								dtr.GetSafeValue<bool>("is_producer"),
								( dtr["is_cameraman"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cameraman") ),
								( dtr["is_cutter"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_cutter") ),
								( dtr["is_musician"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_musician") ),
								( dtr["is_writer"] == DBNull.Value ? false : dtr.GetSafeValue<bool>("is_writer") )
							)
						);
					}
				}

				dtr = null;

				mov.Actors = alist;
				mov.Directors = dlist;
				mov.Producers = plist;
				mov.Musicians = mlist;
				mov.Cameramans = calist;
				mov.Cutters = culist;
				mov.Writers = wlist;
			}

			this._db.CloseConnection();

			return list;
		}

		/// <summary>
		/// Get a list of genres
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public Genre GetGenreByName(string name) {
			Genre genre = new Genre();

			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_genres");
			str.Append(" WHERE name = '" + name + "'");

			this._db.OpenConnection();

			SqlDataReader reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
				str.ToString(),
				CommandType.Text
			);

			if(reader != null && reader.HasRows) {
				reader.Read();

				genre = new Genre(
					reader.GetSafeValue<Guid>("pkid").ToString(),
					reader.GetSafeValue<string>("name")
				);
			}
			else {
				genre = null;
			}

			this._db.CloseConnection();

			return genre;
		}

		/// <summary>
		/// Get a list of genres
		/// </summary>
		/// <returns></returns>
		public List<Genre> GetGenreList() {
			List<Genre> list = new List<Genre>();

			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				cmd.CommandText = "SELECT g.*, ";
				cmd.CommandText += " (";
				cmd.CommandText += "	SELECT COUNT(pkid)";
				cmd.CommandText += "	FROM tbl_movies_to_genres";
				cmd.CommandText += "	WHERE genre_pkid = g.pkid";
				cmd.CommandText += " ) AS count";
				cmd.CommandText += " FROM tbl_genres AS g";
				cmd.CommandText += " ORDER BY g.name ASC";

				//cmd.AddParameter("invoiceId", order.Id);

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						Genre genre = new Genre();

						genre.ID = reader.GetSafeValue<Guid>("pkid").ToString();
						genre.Name = reader.GetSafeValue<string>("name");
						genre.MovieCount = reader["count"].ToString().ToInt32();

						list.Add(genre);
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Cleans the database.
		/// </summary>
		/// <returns></returns>
		public bool CleanDatabase() {
			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				// clean empty genre-link entries
				cmd.CommandText = "DELETE ";
				cmd.CommandText += "FROM tbl_movies_to_genres ";
				cmd.CommandText += "WHERE pkid IN ( ";
				cmd.CommandText += "	SELECT mg.pkid ";
				cmd.CommandText += "	FROM tbl_movies_to_genres AS mg ";
				cmd.CommandText += "	LEFT JOIN tbl_movies AS m ";
				cmd.CommandText += "	ON mg.movie_pkid = m.pkid ";
				cmd.CommandText += "	WHERE m.pkid IS NULL";
				cmd.CommandText += ") ";

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}

				// clean empty category-link entries
				cmd.CommandText = "DELETE ";
				cmd.CommandText += "FROM tbl_movies_to_categories ";
				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += "WHERE (CAST(movie_pkid AS VARCHAR(MAX)) + CAST(category_pkid AS VARCHAR(MAX))) IN ( ";
					cmd.CommandText += "	SELECT (CAST(mc.movie_pkid AS VARCHAR(MAX)) + CAST(mc.category_pkid AS VARCHAR(MAX))) ";
				}
				else {
					cmd.CommandText += "WHERE (CAST(movie_pkid AS VARCHAR) + CAST(category_pkid AS VARCHAR)) IN ( ";
					cmd.CommandText += "	SELECT (CAST(mc.movie_pkid AS VARCHAR) + CAST(mc.category_pkid AS VARCHAR)) ";
				}
				cmd.CommandText += "	FROM tbl_movies_to_categories AS mc ";
				cmd.CommandText += "	LEFT JOIN tbl_movies AS m ";
				cmd.CommandText += "	ON mc.movie_pkid = m.pkid ";
				cmd.CommandText += "	WHERE m.pkid IS NULL ";
				cmd.CommandText += ") ";

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}
			}

			return true;
		}

		/// <summary>
		/// Gets the category list.
		/// </summary>
		/// <returns></returns>
		public List<Category> GetCategoryList() {
			List<Category> list = new List<Category>();

			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				cmd.CommandText = "SELECT c.*, ";
				cmd.CommandText += " (";
				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += "	SELECT CAST(COUNT(CAST(movie_pkid AS VARCHAR(MAX)) + CAST(category_pkid AS VARCHAR(MAX))) AS int)";
				}
				else {
					cmd.CommandText += "	SELECT CAST(COUNT(CAST(movie_pkid AS VARCHAR) + CAST(category_pkid AS VARCHAR)) AS int)";
				}
				cmd.CommandText += "	FROM tbl_movies_to_categories";
				cmd.CommandText += "	WHERE category_pkid = c.pkid";
				cmd.CommandText += " ) AS count";
				cmd.CommandText += " FROM tbl_categories AS c";
				cmd.CommandText += " ORDER BY c.name ASC";

				//cmd.AddParameter("invoiceId", order.Id);

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						list.Add(
							new Category(
								reader.GetSafeValue<Guid>("pkid").ToString(),
								reader.GetSafeValue<string>("name"),
								reader["count"].ToString().ToInt32()
							)
						);
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Get a person
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Person GetPerson(string id) {
			Person person = new Person();

			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_persons");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE pkid = '" + id + "'");
			}
			else {
				str.Append(" WHERE lower(pkid) = lower('{" + id + "}')");
			}


			this._db.OpenConnection();

			SqlDataReader reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
				str.ToString(),
				CommandType.Text
			);

			if(reader != null && reader.HasRows) {
				reader.Read();

				person = new Person(
					reader.GetSafeValue<Guid>("pkid").ToString(),
					reader.GetSafeValue<string>("firstname"),
					reader.GetSafeValue<string>("lastname"),
					reader.GetSafeValue<bool>("is_actor"),
					reader.GetSafeValue<bool>("is_director"),
					reader.GetSafeValue<bool>("is_producer"),
					( reader["is_cameraman"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_cameraman") ),
					( reader["is_cutter"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_cutter") ),
					( reader["is_musician"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_musician") ),
					( reader["is_writer"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_writer") )
				);
			}

			this._db.CloseConnection();

			return person;
		}

		/// <summary>
		/// Gets the person list.
		/// </summary>
		/// <param name="mot">The MovieObjectType.</param>
		/// <returns></returns>
		public List<Person> GetPersonList(MovieObjectType mot) {
			return this.GetPersonList(mot, "", "", DataSortDirection.Ascending);
		}

		/// <summary>
		/// Get a list of persons
		/// </summary>
		/// <param name="mot">The MovieObjectType.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="sortDirection">The sort direction.</param>
		/// <returns></returns>
		public List<Person> GetPersonList(MovieObjectType mot, string sortExpression, DataSortDirection sortDirection) {
			return this.GetPersonList(mot, "", sortExpression, sortDirection);
		}

		/// <summary>
		/// Get a list of persons
		/// </summary>
		/// <param name="mot">The MovieObjectType.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public List<Person> GetPersonList(MovieObjectType mot, string name, string sortExpression, DataSortDirection sortDirection) {
			List<Person> list = new List<Person>();
			//PersonCollection list = new PersonCollection();
			
			StringBuilder str = new StringBuilder();

			str.Append("SELECT p.*, ");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_actor = 1");
			str.Append(" ) AS actorQuantity,");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_director = 1");
			str.Append(" ) AS directorQuantity,");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_producer = 1");
			str.Append(" ) AS producerQuantity, ");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_musician = 1");
			str.Append(" ) AS musicianQuantity, ");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_cameraman = 1");
			str.Append(" ) AS cameramanQuantity, ");
			str.Append(" (");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_cutter = 1");
			str.Append(" ) AS cutterQuantity, ");
			str.Append("(");
			str.Append(" 	SELECT CAST(COUNT(pkid) AS int)");
			str.Append(" 	FROM tbl_movies_to_persons");
			str.Append(" 	WHERE person_pkid = p.pkid");
			str.Append(" 	AND as_writer = 1");
			str.Append(" ) AS writerQuantity, ");
			str.Append("(");
			str.Append("	SELECT CAST(COUNT(DISTINCT movie_pkid) AS int)	");
			str.Append("	FROM tbl_movies_to_persons 	");
			str.Append("	WHERE person_pkid = p.pkid 	");
			str.Append("	AND ( as_director = 1 OR as_producer = 1 OR as_actor = 1 OR as_musician = 1 OR as_cameraman = 1 OR as_cutter = 1 OR as_writer = 1 )");
			str.Append(") AS movieQuantity ");
			str.Append(" FROM tbl_persons AS p");

			switch(mot) {
				case MovieObjectType.Genre:
					break;

				case MovieObjectType.Actor:
					str.Append(" WHERE p.is_actor = 1");
					break;

				case MovieObjectType.Director:
					str.Append(" WHERE p.is_director = 1");
					break;

				case MovieObjectType.Producer:
					str.Append(" WHERE p.is_producer = 1");
					break;

				case MovieObjectType.Musician:
					str.Append(" WHERE p.is_musician = 1");
					break;

				case MovieObjectType.Cameraman:
					str.Append(" WHERE p.is_cameraman = 1");
					break;

				case MovieObjectType.Cutter:
					str.Append(" WHERE p.is_cutter = 1");
					break;

				case MovieObjectType.Writer:
					str.Append(" WHERE p.is_writer = 1");
					break;

				case MovieObjectType.All:
					str.Append(" WHERE ( NOT (p.pkid IS NULL) )");
					break;
			}

			if(name != null
			&& name.Trim() != "") {
				if(this._cfg.ProviderType == ProviderType.SQLite) {
					str.Append(" AND ( ");
					str.Append(" ( LOWER(p.firstname) LIKE LOWER('{" + name + "}') AND LOWER(p.lastname) LIKE LOWER('{" + name + "}') ) ");
					str.Append(" OR ( LOWER(p.firstname) + ' ' + LOWER(p.lastname) LIKE LOWER('{" + name + "}') ) ");
					str.Append(" OR ( LOWER(p.lastname) + ' ' + LOWER(p.firstname) LIKE LOWER('{" + name + "}') ) ");
					str.Append(" ) ");
				}
				else {
					str.Append(" AND ( ");
					str.Append(" ( LOWER(p.firstname) LIKE LOWER('%" + name + "%') AND LOWER(p.lastname) LIKE LOWER('%" + name + "%') ) ");
					str.Append(" OR ( LOWER(p.firstname) + ' ' + LOWER(p.lastname) LIKE LOWER('%" + name + "%') ) ");
					str.Append(" OR ( LOWER(p.lastname) + ' ' + LOWER(p.firstname) LIKE LOWER('%" + name + "%') ) ");
					str.Append(" ) ");
				}
			}

			// add sort expression
			if(!string.IsNullOrEmpty(sortExpression)) {
				switch(sortExpression) {
					case "MovieQuantity":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"movieQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsActor":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"actorQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsDirector":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"directorQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsProducer":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"producerQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsMusician":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"musicianQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsCameraman":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"cameramanQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsCutter":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"cutterQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "MovieQuantityAsWriter":
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"writerQuantity",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.Append("\r\n");
						str.AppendFormat(
							", p.firstname {0}, p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;

					case "Fullname":
					default:
						str.Append("\r\n");
						str.AppendFormat(
							SqlResources.GetPersonList_SortOrder,
							"p.firstname",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);

						str.AppendFormat(
							", p.lastname {0}",
							( sortDirection == DataSortDirection.Ascending ? "ASC" : "DESC" )
						);
						break;
				}
			}
			else {
				str.Append(" ORDER BY p.firstname ASC, p.lastname ASC");
			}

			if(this._cfg.ProviderType == ProviderType.SQLite) {
				str = str.Replace(" = 1", " = 'True'");
			}

			// TODO: Optimize for SQLite

			// new way
			using(DAL dal = new DAL(this._cfg)) {
				IDbCommand cmd = dal.CreateCommand();
				cmd.CommandText = str.ToString();

				dal.OpenConnection();

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						list.Add(
							new Person(
								reader.GetSafeValue<Guid>("pkid").ToString(),
								reader.GetSafeValue<string>("firstname"),
								reader.GetSafeValue<string>("lastname"),
								reader.GetSafeValue<bool>("is_actor"),
								reader.GetSafeValue<bool>("is_director"),
								reader.GetSafeValue<bool>("is_producer"),
								( reader["is_cameraman"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_cameraman") ),
								( reader["is_cutter"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_cutter") ),
								( reader["is_musician"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_musician") ),
								( reader["is_writer"] == DBNull.Value ? false : reader.GetSafeValue<bool>("is_writer") )
							) {
								MovieQuantityAsActor = reader["actorQuantity"].ToString().ToInt32(),
								MovieQuantityAsDirector = reader["directorQuantity"].ToString().ToInt32(),
								MovieQuantityAsProducer = reader["producerQuantity"].ToString().ToInt32(),
								MovieQuantityAsCameraman = reader["cameramanQuantity"].ToString().ToInt32(),
								MovieQuantityAsCutter = reader["cutterQuantity"].ToString().ToInt32(),
								MovieQuantityAsMusician = reader["musicianQuantity"].ToString().ToInt32(),
								MovieQuantityAsWriter = reader["writerQuantity"].ToString().ToInt32(),
								MovieQuantity = reader["movieQuantity"].ToString().ToInt32()
							}
						);
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Save a list of genres
		/// </summary>
		/// <param name="list"></param>
		public void SaveNewGenreList(List<Genre> list) {
			StringBuilder str = new StringBuilder();
			int res;

			// first delete
			if(list.Count > 0) {
				str.Append("DELETE ");
				str.Append(" FROM tbl_genres");
				str.Append(" WHERE ");

				foreach(Genre g in list) {
					if(g.ID != null && g.ID != "") {
						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" (pkid = '" + g.ID + "') AND ");
						}
						else {
							str.Append(" (lower(pkid) = lower('{" + g.ID + "}')) AND ");
						}
					}
				}

				str.Remove(str.Length - 5, 5);

				if(str.ToString().Contains("pkid")) {
					try {
						res = this._db.ExecuteNonQuery(str.ToString());
					}
					catch(Exception ex) {
						throw new Exception("Error on loading genres from table!", ex);
					}
				}
			}

			// save now
			DataSet data;

			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_genres");

			try {
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading genres from table!", ex);
			}

			foreach(Genre g in list) {
				DataRow row = data.Tables[0].NewRow();

				row["pkid"] = g.ID;
				row["name"] = g.Name;

				data.Tables[0].Rows.Add(row);
			}

			try {
				res = this._db.ExecuteUpdate(data, "tbl_genres");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating genres to table!", ex);
			}
		}

		/// <summary>
		/// Save a list of genres
		/// </summary>
		/// <param name="list"></param>
		public void SaveGenreList(List<Genre> list) {
			this.SaveGenreList(list, false);
		}

		/// <summary>
		/// Save a list of genres
		/// </summary>
		/// <param name="list"></param>
		/// <param name="asUpdate"></param>
		public void SaveGenreList(List<Genre> list, bool asUpdate) {
			StringBuilder str = new StringBuilder();
			int res;
			DataSet data;

			bool isCorrectSql = false;

			// first delete
			str.Append("DELETE ");
			str.Append(" FROM tbl_movies_to_genres");

			if(list.Count > 0) {
				str.Append(" WHERE ");

				foreach(Genre g in list) {
					if(g.ID != null && g.ID != "") {
						str.Append(" (genre_pkid <> '" + g.ID + "') AND ");

						isCorrectSql = true;
					}
				}

				if(isCorrectSql) {
					str.Remove(str.Length - 5, 5);
				}
			}

			if(isCorrectSql) {
				try {
					res = this._db.ExecuteNonQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on deleting genres from movie_to_genre index table!", ex);
				}
			}

			isCorrectSql = false;

			str.Remove(0, str.Length);

			if(asUpdate) {
				// first delete
				str.Append("DELETE FROM tbl_genres");

				if(list.Count > 0) {
					str.Append(" WHERE ");

					foreach(Genre g in list) {
						if(g.ID != null && g.ID != "") {
							str.Append(" (pkid <> '" + g.ID + "') AND ");

							isCorrectSql = true;
						}
					}

					if(isCorrectSql) {
						str.Remove(str.Length - 5, 5);
					}
				}

				if(isCorrectSql) {
					try {
						res = this._db.ExecuteNonQuery(str.ToString());
					}
					catch(Exception ex) {
						throw new Exception("Error on loading genres from table!", ex);
					}
				}

				isCorrectSql = false;

				// load
				str.Remove(0, str.Length);
				str.Append("SELECT * ");
				str.Append(" FROM tbl_genres");

				if(list.Count > 0) {
					str.Append(" WHERE ");

					foreach(Genre g in list) {
						if(g.ID != null && g.ID != "") {
							if(this._cfg.ProviderType != ProviderType.SQLite) {
								str.Append(" (pkid = '" + g.ID + "') AND ");
							}
							else {
								str.Append(" (lower(pkid) = lower('{" + g.ID + "}')) AND ");
							}
						}
					}

					str.Remove(str.Length - 4, 4);
				}

				try {
					data = this._db.ExecuteQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on loading genres from table!", ex);
				}

				if(data.Tables[0].Rows.Count > 0) {
					foreach(DataRow dr in data.Tables[0].Rows) {
						dr["name"] = Tools.GetCleanGenreOrCategoryName(
							Tools.GetGenreNameFromList(
								list, 
								dr["pkid"].ToString()
							)
						);
					}
				}
				else {
					foreach(Genre g in list) {
						DataRow row = data.Tables[0].NewRow();

						row["pkid"] = Guid.NewGuid().ToString();
						row["name"] = Tools.GetCleanGenreOrCategoryName(g.Name);

						data.Tables[0].Rows.Add(row);
					}
				}

				try {
					res = this._db.ExecuteUpdate(data, "tbl_genres");
				}
				catch(Exception ex) {
					throw new Exception("Error on updating genres to table!", ex);
				}
			}
			else {
				// first delete
				str.Append("DELETE ");
				str.Append(" FROM tbl_genres");

				res = this._db.ExecuteNonQuery(str.ToString());

				// load
				str.Remove(0, str.Length);
				str.Append("SELECT * ");
				str.Append(" FROM tbl_genres");

				try {
					data = this._db.ExecuteQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on loading genres from table!", ex);
				}

				foreach(Genre g in list) {
					DataRow row = data.Tables[0].NewRow();

					if(g.ID == null || g.ID.Trim() == "") {
						row["pkid"] = Guid.NewGuid().ToString();
					}
					else {
						row["pkid"] = g.ID;
					}

					row["name"] = Tools.GetCleanGenreOrCategoryName(g.Name);

					data.Tables[0].Rows.Add(row);
				}

				try {
					res = this._db.ExecuteUpdate(data, "tbl_genres");
				}
				catch(Exception ex) {
					throw new Exception("Error on updating genres to table!", ex);
				}
			}
		}

		/// <summary>
		/// Saves the category list.
		/// </summary>
		/// <param name="list">The list.</param>
		public void SaveCategoryList(List<Category> list) {
			this.SaveCategoryList(list, false);
		}

		/// <summary>
		/// Saves the category list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="asUpdate">if set to <c>true</c> [as update].</param>
		public void SaveCategoryList(List<Category> list, bool asUpdate) {
			StringBuilder str = new StringBuilder();
			int res;
			DataSet data;

			bool isCorrectSql = false;

			// first delete
			str.Append("DELETE ");
			str.Append(" FROM tbl_movies_to_categories");

			if(list.Count > 0) {
				str.Append(" WHERE ");

				foreach(Category g in list) {
					if(g.ID != null && g.ID != "") {
						str.Append(" (category_pkid <> '" + g.ID + "') AND ");

						isCorrectSql = true;
					}
				}

				if(isCorrectSql) {
					str.Remove(str.Length - 5, 5);
				}
			}

			if(isCorrectSql) {
				try {
					res = this._db.ExecuteNonQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on deleting categories from movies_to_categories index table!", ex);
				}
			}

			isCorrectSql = false;

			str.Remove(0, str.Length);

			if(asUpdate) {
				// first delete
				str.Append("DELETE FROM tbl_categories");

				if(list.Count > 0) {
					str.Append(" WHERE ");

					foreach(Category g in list) {
						if(g.ID != null && g.ID != "") {
							str.Append(" (pkid <> '" + g.ID + "') AND ");

							isCorrectSql = true;
						}
					}

					if(isCorrectSql) {
						str.Remove(str.Length - 5, 5);
					}
				}

				if(isCorrectSql) {
					try {
						res = this._db.ExecuteNonQuery(str.ToString());
					}
					catch(Exception ex) {
						throw new Exception("Error on loading categories from table!", ex);
					}
				}

				isCorrectSql = false;

				// load
				str.Remove(0, str.Length);
				str.Append("SELECT * ");
				str.Append(" FROM tbl_categories");

				if(list.Count > 0) {
					str.Append(" WHERE ");

					foreach(Category g in list) {
						if(g.ID != null && g.ID != "") {
							if(this._cfg.ProviderType != ProviderType.SQLite) {
								str.Append(" (pkid = '" + g.ID + "') AND ");
							}
							else {
								str.Append(" (lower(pkid) = lower('{" + g.ID + "}')) AND ");
							}
						}
					}

					str.Remove(str.Length - 4, 4);
				}

				try {
					data = this._db.ExecuteQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on loading categories from table!", ex);
				}

				if(data.Tables[0].Rows.Count > 0) {
					foreach(DataRow dr in data.Tables[0].Rows) {
						dr["name"] = Tools.GetCleanGenreOrCategoryName(
							Tools.GetCategoryNameFromList(
								list,
								dr["pkid"].ToString()
							)
						);
					}
				}
				else {
					foreach(Category g in list) {
						DataRow row = data.Tables[0].NewRow();
						
						row["pkid"] = Guid.NewGuid().ToString();
						row["name"] = Tools.GetCleanGenreOrCategoryName(g.Name);

						data.Tables[0].Rows.Add(row);
					}
				}

				try {
					res = this._db.ExecuteUpdate(data, "tbl_categories");
				}
				catch(Exception ex) {
					throw new Exception("Error on updating categories to table!", ex);
				}
			}
			else {
				// first delete
				str.Append("DELETE ");
				str.Append(" FROM tbl_categories");

				res = this._db.ExecuteNonQuery(str.ToString());

				// load
				str.Remove(0, str.Length);
				str.Append("SELECT * ");
				str.Append(" FROM tbl_categories");

				try {
					data = this._db.ExecuteQuery(str.ToString());
				}
				catch(Exception ex) {
					throw new Exception("Error on loading categories from table!", ex);
				}

				foreach(Category g in list) {
					DataRow row = data.Tables[0].NewRow();

					if(g.ID == null || g.ID.Trim() == "") {
						row["pkid"] = Guid.NewGuid().ToString();
					}
					else {
						row["pkid"] = g.ID;
					}

					row["name"] = Tools.GetCleanGenreOrCategoryName(g.Name);

					data.Tables[0].Rows.Add(row);
				}

				try {
					res = this._db.ExecuteUpdate(data, "tbl_categories");
				}
				catch(Exception ex) {
					throw new Exception("Error on updating categories to table!", ex);
				}
			}
		}

		/// <summary>
		/// Save a movie
		/// </summary>
		/// <param name="movie"></param>
		/// <param name="method"></param>
		public void SaveMovie(Movie movie, SaveMethod method) {
			StringBuilder str = new StringBuilder();
			DataSet data;
			DataRow row;
			int res;
			bool isEmpty = false;

			if(method == SaveMethod.CreateNew) {
				movie.Id = Helper.NewGuid;

				if(movie.Number == default(int)) {
					movie.Number = this.GetNextMovieNumber();
				}
			}

			// load
			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE pkid = '" + movie.Id + "'");
			}
			else {
				str.Append(" WHERE lower(pkid) = lower('{" + movie.Id + "}')");
			}

			// load data
			try {
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading movie from table!", ex);
			}

			// get row
			if(method == SaveMethod.CreateNew) {
				row = data.Tables[0].NewRow();
				isEmpty = true;
			}
			else {
				row = data.Tables[0].Rows[0];
			}

			// data
			row["pkid"] = movie.Id;
			row["sort_value"] = ( movie.SortValue.IsNullOrTrimmedEmpty() ? null : movie.SortValue );
			row["language"] = ( movie.Language.IsNullOrTrimmedEmpty() ? null : movie.Language );
			row["number"] = movie.Number;
			row["name"] = movie.Name + "";
			row["note"] = movie.Note + "";
			row["has_cover"] = movie.HasCover;
			row["is_original"] = movie.IsOriginal;
			row["is_conferred"] = movie.IsConferred;
			row["codec"] = (int)movie.Codec;
			row["conferred_to"] = movie.ConferredTo + "";
			row["disc_amount"] = movie.DiscAmount;
			row["year"] = movie.Year;
			row["country"] = movie.Country + "";
			row["quality"] = (int)movie.Quality;

			// add row
			if(isEmpty) {
				data.Tables[0].Rows.Add(row);
			}

			// save
			try {
				res = this._db.ExecuteUpdate(data, "tbl_movies");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating movie to table!", ex);
			}

			// save genres
			str.Remove(0, str.Length);
			str.Append("DELETE ");
			str.Append(" FROM tbl_movies_to_genres");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE movie_pkid = '" + movie.Id + "'");
			}
			else {
				str.Append(" WHERE lower(movie_pkid) = lower('{" + movie.Id + "}')");
			}

			res = this._db.ExecuteNonQuery(str.ToString());

			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies_to_genres");

			try {
				data = null;
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading genres from table!", ex);
			}

			foreach(Genre g in movie.Genres) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["genre_pkid"] = g.ID;
				hrow["movie_pkid"] = movie.Id;

				data.Tables[0].Rows.Add(hrow);
			}

			try {
				res = this._db.ExecuteUpdate(data, "tbl_movies_to_genres");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating genres to table!", ex);
			}

			// save categories
			str.Remove(0, str.Length);
			str.Append("DELETE ");
			str.Append(" FROM tbl_movies_to_categories");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE movie_pkid = '" + movie.Id + "'");
			}
			else {
				str.Append(" WHERE lower(movie_pkid) = lower('{" + movie.Id + "}')");
			}

			res = this._db.ExecuteNonQuery(str.ToString());

			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies_to_categories");

			try {
				data = null;
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading categories from table!", ex);
			}

			foreach(Category c in movie.Categories) {
				DataRow hrow = data.Tables[0].NewRow();

				//hrow["pkid"] = Helper.NewGuid;
				hrow["category_pkid"] = c.ID;
				hrow["movie_pkid"] = movie.Id;

				data.Tables[0].Rows.Add(hrow);
			}

			try {
				res = this._db.ExecuteUpdate(data, "tbl_movies_to_categories");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating categories to table!", ex);
			}

			// save persons
			//List<Person> list = new List<Person>();
			//list.AddRange(movie.Actors);
			//list.AddRange(movie.Directors);
			//list.AddRange(movie.Producers);

			str.Remove(0, str.Length);
			str.Append("DELETE ");
			str.Append(" FROM tbl_movies_to_persons");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE movie_pkid = '" + movie.Id + "'");
			}
			else {
				str.Append(" WHERE lower(movie_pkid) = lower('{" + movie.Id + "}')");
			}

			res = this._db.ExecuteNonQuery(str.ToString());

			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_movies_to_persons");

			try {
				data = null;
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading persons from table!", ex);
			}

			// actors
			foreach(Person p in movie.Actors) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_actor"] = true;

				if(!p.Rolename.IsNullOrTrimmedEmpty()) {
					hrow["role_name"] = p.Rolename;
				}

				if(!p.Roletype.IsNullOrTrimmedEmpty()) {
					hrow["role_type"] = p.Roletype.ToInt32();
				}

				data.Tables[0].Rows.Add(hrow);
			}

			// directors
			foreach(Person p in movie.Directors) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_director"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			// producer
			foreach(Person p in movie.Producers) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_producer"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			// musician
			foreach(Person p in movie.Musicians) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_musician"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			// cameraman
			foreach(Person p in movie.Cameramans) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_cameraman"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			// cutter
			foreach(Person p in movie.Cutters) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_cutter"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			// writer
			foreach(Person p in movie.Writers) {
				DataRow hrow = data.Tables[0].NewRow();

				hrow["pkid"] = Helper.NewGuid;
				hrow["person_pkid"] = p.ID;
				hrow["movie_pkid"] = movie.Id;
				hrow["as_writer"] = true;

				data.Tables[0].Rows.Add(hrow);
			}

			try {
				res = this._db.ExecuteUpdate(data, "tbl_movies_to_persons");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating persons to table!", ex);
			}
		}

		/// <summary>
		/// Save a list of Persons
		/// </summary>
		/// <param name="list"></param>
		public void SaveNewPersonList(List<Person> list) {
			StringBuilder str = new StringBuilder();
			int res;

			// first delete
			if(list.Count > 0) {
				str.Append("DELETE ");
				str.Append(" FROM tbl_persons");
				str.Append(" WHERE ");

				foreach(Person p in list) {
					if(p.ID != null && p.ID != "") {
						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" (pkid = '" + p.ID + "') AND ");
						}
						else {
							str.Append(" (lower(pkid) = lower('{" + p.ID + "}')) AND ");
						}
					}
				}

				str.Remove(str.Length - 5, 5);

				if(str.ToString().Contains("pkid")) {
					try {
						res = this._db.ExecuteNonQuery(str.ToString());
					}
					catch(Exception ex) {
						throw new Exception("Error on deleting Persons from table!", ex);
					}
				}
			}

			// save now
			DataSet data;

			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_persons");

			try {
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading Persons from table!", ex);
			}

			foreach(Person p in list) {
				DataRow row = data.Tables[0].NewRow();

				row["pkid"] = p.ID;
				row["firstname"] = p.Firstname;
				row["lastname"] = p.Lastname;
				row["is_actor"] = p.IsActor;
				row["is_director"] = p.IsDirector;
				row["is_producer"] = p.IsProducer;
				row["is_musician"] = p.IsMusician;
				row["is_cameraman"] = p.IsCameraman;
				row["is_cutter"] = p.IsCutter;

				data.Tables[0].Rows.Add(row);
			}

			try {
				res = this._db.ExecuteUpdate(data, "tbl_persons");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating persons to table!", ex);
			}
		}

		/// <summary>
		/// Updates the person role.
		/// </summary>
		/// <param name="movieId">The movie id.</param>
		/// <param name="personId">The person id.</param>
		/// <param name="roleName">Name of the role.</param>
		/// <param name="roleType">Type of the role.</param>
		public void UpdatePersonRole(string movieId, string personId, string roleName, string roleType) {
			StringBuilder str = new StringBuilder();
			str.Append(" UPDATE tbl_movies_to_persons");
			str.Append(" SET role_name = @roleName, role_type = @roleType");
			str.Append(" WHERE movie_pkid = @movieId");
			str.Append(" AND person_pkid = @personId");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" AND as_actor = 1");
			}
			else {
				str.Append(" AND as_actor = 'True'");
			}

			using(DAL dal = new DAL(this._cfg)) {
				DbCommand cmd = dal.CreateCommand();
				cmd.CommandText = str.ToString();
				cmd.AddParameter("roleName", roleName);
				cmd.AddParameter("roleType", roleType.ToInt32());
				cmd.AddParameter("movieId", movieId);
				cmd.AddParameter("personId", personId);

				dal.ExecuteNonQuery(cmd);
			}
		}

		/// <summary>
		/// Save a person
		/// </summary>
		/// <param name="person"></param>
		/// <param name="method"></param>
		public void SavePerson(Person person, SaveMethod method) {
			StringBuilder str = new StringBuilder();
			DataSet data;
			DataRow row;
			int res;
			bool isEmpty = false;

			if(method == SaveMethod.CreateNew) {
				person.ID = Helper.NewGuid;
			}

			// load
			str.Remove(0, str.Length);
			str.Append("SELECT * ");
			str.Append(" FROM tbl_persons");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE pkid = '" + person.ID + "'");
			}
			else {
				str.Append(" WHERE lower(pkid) = lower('{" + person.ID + "}')");
			}

			// load data
			try {
				data = this._db.ExecuteQuery(str.ToString());
			}
			catch(Exception ex) {
				throw new Exception("Error on loading person from table!", ex);
			}

			// get row
			if(method == SaveMethod.CreateNew
			|| method == SaveMethod.CreateNewWithoutID) {
				row = data.Tables[0].NewRow();
				isEmpty = true;
			}
			else {
				row = data.Tables[0].Rows[0];
			}

			// data
			row["pkid"] = person.ID;
			row["firstname"] = person.Firstname + "";
			row["lastname"] = person.Lastname + "";
			row["is_actor"] = person.IsActor;
			row["is_director"] = person.IsDirector;
			row["is_producer"] = person.IsProducer;
			row["is_musician"] = person.IsMusician;
			row["is_cameraman"] = person.IsCameraman;
			row["is_cutter"] = person.IsCutter;
			row["is_writer"] = person.IsWriter;

			// add row
			if(isEmpty) {
				data.Tables[0].Rows.Add(row);
			}

			// save
			try {
				res = this._db.ExecuteUpdate(data, "tbl_persons");
			}
			catch(Exception ex) {
				throw new Exception("Error on updating person to table!", ex);
			}
		}

		/// <summary>
		/// Delete a person
		/// </summary>
		/// <param name="id"></param>
		public void DeletePerson(string id) {
			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				// delete person
				cmd.CommandText = "DELETE ";
				cmd.CommandText += " FROM tbl_persons ";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}

				// delete person-link
				cmd.CommandText = "DELETE ";
				cmd.CommandText += " FROM tbl_movies_to_persons ";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE person_pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(person_pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}
			}
		}

		/// <summary>
		/// Delete a person
		/// </summary>
		/// <param name="id"></param>
		public void DeleteMovie(string id) {
			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				// delete movie
				cmd.CommandText = "DELETE";
				cmd.CommandText += " FROM tbl_movies";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}

				// delete movie-category-link
				cmd.CommandText = "DELETE ";
				cmd.CommandText += " FROM tbl_movies_to_categories";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE movie_pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(movie_pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}

				// delete movie-genre-link
				cmd.CommandText = "DELETE ";
				cmd.CommandText += " FROM tbl_movies_to_genres";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE movie_pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(movie_pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}

				// delete movie-person-link
				cmd.CommandText = "DELETE ";
				cmd.CommandText += " FROM tbl_movies_to_persons";

				if(this._cfg.ProviderType != ProviderType.SQLite) {
					cmd.CommandText += " WHERE movie_pkid = '" + id + "'";
				}
				else {
					cmd.CommandText += " WHERE lower(movie_pkid) = lower('{" + id + "}'";
				}

				try {
					dal.ExecuteNonQuery(cmd);
				}
				catch(Exception ex) {
					throw ex;
				}
			}
		}

		/// <summary>
		/// Gets the previous movie id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="number">The number.</param>
		/// <returns></returns>
		public string GetPreviousMovieId(string id, int number) {
			StringBuilder str = new StringBuilder();

			str.Append("SELECT pkid");
			str.Append(" FROM tbl_movies");
			str.Append(" WHERE pkid <> '" + id + "'");
			str.Append(" AND (number = " + number.ToString() + " OR number = " + (number - 1).ToString() + ")");
			str.Append(" ORDER BY number ASC, sort_value ASC");

			this._db.OpenConnection();

			SqlDataReader reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
				str.ToString(),
				CommandType.Text
			);

			string result = "";

			if(reader != null && reader.HasRows) {
				reader.Read();

				result = reader["pkid"].ToString();

				reader.Close();
			}

			this._db.CloseConnection();

			return result;
		}

		/// <summary>
		/// Gets the next movie id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="number">The number.</param>
		/// <returns></returns>
		public string GetNextMovieId(string id, int number) {
			StringBuilder str = new StringBuilder();

			str.Append("SELECT pkid");
			str.Append(" FROM tbl_movies");
			str.Append(" WHERE pkid <> '" + id + "'");
			str.Append(" AND (number = " + number.ToString() + " OR number = " + (number + 1).ToString() + ")");
			str.Append(" ORDER BY number ASC, sort_value ASC");

			this._db.OpenConnection();

			SqlDataReader reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
				str.ToString(),
				CommandType.Text
			);

			string result = "";

			if(reader != null && reader.HasRows) {
				reader.Read();

				result = reader["pkid"].ToString();

				reader.Close();
			}

			this._db.CloseConnection();

			return result;
		}

		/// <summary>
		/// Get the next movie number
		/// </summary>
		/// <param name="op">The op.</param>
		/// <returns></returns>
		public int GetMovieNumber(NumberOperator op) {
			int numCount = 0;
			//int numMax = 0;

			SqlDataReader reader;
			StringBuilder str = new StringBuilder();

			// get count
			try {
				if(op == NumberOperator.Min) {
					str.Append("SELECT MIN(number) FROM tbl_movies");
				}
				else if(op == NumberOperator.Max) {
					str.Append("SELECT MAX(number) FROM tbl_movies");
				}

				this._db.OpenConnection();

				reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
					str.ToString(),
					CommandType.Text
				);

				if(reader != null && reader.HasRows) {
					reader.Read();

					numCount = reader.GetInt32(0);
				}
			}
			catch(Exception) {
				numCount = 0;
			}
			finally {
				this._db.CloseConnection();
			}

			return numCount;//+1;
		}

		/// <summary>
		/// Get the next movie number
		/// </summary>
		/// <returns></returns>
		public int GetNextMovieNumber() {
			int numCount = 0;

			SqlDataReader reader;
			StringBuilder str = new StringBuilder();

			// get count
			try {
				str.Append("SELECT COUNT(pkid) FROM tbl_movies");

				this._db.OpenConnection();

				reader = (SqlDataReader)this._db.ExecuteQueryForDataReader(
					str.ToString(),
					CommandType.Text
				);

				if(reader != null && reader.HasRows) {
					reader.Read();

					numCount = reader[0].ToString().ToInt32();
				}
			}
			catch(Exception) {
				numCount = 0;
			}
			finally {
				this._db.CloseConnection();
			}

			return numCount + 1;
		}

		/// <summary>
		/// Check if a person exist
		/// </summary>
		/// <param name="firstname"></param>
		/// <param name="lastname"></param>
		/// <param name="mot"></param>
		/// <returns></returns>
		public bool CheckPersonExist(string firstname, string lastname, MovieObjectType mot) {
			bool ret = false;

			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_persons");
			//str.Append(" WHERE firstname LIKE = '{" + firstname + "}')");
			//str.Append(" AND lastname LIKE = '{" + lastname + "}')");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE (");
				str.Append(" (firstname LIKE '%" + firstname + "%'");
				str.Append(" AND lastname LIKE '%" + lastname + "%')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + firstname + "}')");
				str.Append(" OR (firstname + ' ' + lastname LIKE '%" + firstname + ' ' + lastname + "%')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + ' ' + firstname + "}')");
				str.Append(" )");
			}
			else {
				str.Append(" WHERE (");
				str.Append(" (firstname LIKE '{" + firstname + "}'");
				str.Append(" AND lastname LIKE '{" + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + firstname + "}')");
				str.Append(" OR (firstname + ' ' + lastname LIKE '{" + firstname + ' ' + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + ' ' + firstname + "}')");
				str.Append(" )");
			}

			switch(mot) {
				case MovieObjectType.Genre:
					break;

				case MovieObjectType.Actor:
					str.Append(" AND is_actor = 1");
					break;

				case MovieObjectType.Director:
					str.Append(" AND is_director = 1");
					break;

				case MovieObjectType.Producer:
					str.Append(" AND is_producer = 1");
					break;

				case MovieObjectType.Musician:
					str.Append(" AND is_musician = 1");
					break;

				case MovieObjectType.Cameraman:
					str.Append(" AND is_cameraman = 1");
					break;

				case MovieObjectType.Cutter:
					str.Append(" AND is_cutter = 1");
					break;

				case MovieObjectType.Writer:
					str.Append(" AND is_writer = 1");
					break;

				case MovieObjectType.All:
					break;
			}

			if(this._cfg.ProviderType == ProviderType.SQLite) {
				str = str.Replace(" = 1", " = 'True'");
			}

			DataSet data = this._db.ExecuteQuery(str.ToString());

			if(data.Tables[0].Rows.Count > 0) {
				ret = true;
			}
			else {
				ret = false;
			}

			return ret;
		}

		/// <summary>
		/// Gets the person by its name
		/// </summary>
		/// <param name="firstname">The firstname.</param>
		/// <param name="lastname">The lastname.</param>
		/// <param name="mot">The mot.</param>
		/// <returns></returns>
		public Person GetPersonByName(string firstname, string lastname, MovieObjectType mot) {
			StringBuilder str = new StringBuilder();

			str.Append("SELECT * ");
			str.Append(" FROM tbl_persons");

			if(this._cfg.ProviderType != ProviderType.SQLite) {
				str.Append(" WHERE (");
				str.Append(" (firstname LIKE '%" + firstname.Trim() + "%'");
				str.Append(" AND lastname LIKE '%" + lastname.Trim() + "%')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + firstname + "}')");
				str.Append(" OR (firstname + ' ' + lastname LIKE '%" + (firstname + " " + lastname).Trim() + "%')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + ' ' + firstname + "}')");
				str.Append(" )");
			}
			else {
				str.Append(" WHERE (");
				str.Append(" (firstname LIKE '{" + firstname.Trim() + "}'");
				str.Append(" AND lastname LIKE '{" + lastname.Trim() + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + firstname + "}')");
				str.Append(" OR (firstname + ' ' + lastname LIKE '{" + (firstname + " " + lastname).Trim() + "}')");
				//str.Append(" OR (firstname + ' ' + lastname LIKE '{" + lastname + ' ' + firstname + "}')");
				str.Append(" )");
			}

			switch(mot) {
				case MovieObjectType.Genre:
					break;

				case MovieObjectType.Actor:
					str.Append(" AND is_actor = 1");
					break;

				case MovieObjectType.Director:
					str.Append(" AND is_director = 1");
					break;

				case MovieObjectType.Producer:
					str.Append(" AND is_producer = 1");
					break;

				case MovieObjectType.Musician:
					str.Append(" AND is_musician = 1");
					break;

				case MovieObjectType.Cameraman:
					str.Append(" AND is_cameraman = 1");
					break;

				case MovieObjectType.Cutter:
					str.Append(" AND is_cutter = 1");
					break;

				case MovieObjectType.Writer:
					str.Append(" AND is_writer = 1");
					break;

				case MovieObjectType.All:
					break;
			}

			if(this._cfg.ProviderType == ProviderType.SQLite) {
				str = str.Replace(" = 1", " = 'True'");
			}

			DataSet data = this._db.ExecuteQuery(str.ToString());

			if(data.Tables[0].Rows.Count > 0) {
				DataTableReader reader = data.CreateDataReader();
				reader.Read();

				return this.GetPerson(reader["pkid"].ToString());
			}
			else {
				return null;
			}
		}

		#endregion

		#region Private Members
		// -------------------------------------------------------
		// PRIVATE MEMBERS
		// -------------------------------------------------------

		/// <summary>
		/// Gets the movie list SQL string.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="value">The value.</param>
		/// <param name="number">The number.</param>
		/// <returns></returns>
		private StringBuilder GetMovieListSqlString(StringBuilder str, FilterType filter, string value, int number) {
			QueryParser parser = new QueryParser();

			switch(filter) {
				case FilterType.NoFilter:
				case FilterType.ResetFilter:
					break;

				case FilterType.AllConferred:
					str.Append(" WHERE is_conferred = 1");
					break;

				case FilterType.AllOriginals:
					str.Append(" WHERE is_original = 1");
					break;

				case FilterType.Codec:
					str.Append(" WHERE codec = " + number.ToString());
					break;

				case FilterType.WithoutGenre:
					str.Remove(0, str.Length);
					str.Append("SELECT DISTINCT m.*, s.Content");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" LEFT JOIN tbl_movies_to_genres AS mg");
					str.Append(" ON mg.movie_pkid = m.pkid");
					str.Append(" LEFT JOIN tbl_genres AS g");
					str.Append(" ON g.pkid = mg.genre_pkid");
					str.Append(" WHERE g.pkid IS NULL");
					break;

				case FilterType.Genre:
					str.Remove(0, str.Length);
					str.Append("SELECT DISTINCT m.*, s.Content");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_movies_to_genres AS mg");
					str.Append(" ON mg.movie_pkid = m.pkid");
					str.Append(" INNER JOIN tbl_genres AS g");
					str.Append(" ON g.pkid = mg.genre_pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE g.pkid = '" + value + "'");
					}
					else {
						str.Append(" WHERE lower(g.pkid) = lower('{" + value + "}')");
					}
					break;

				case FilterType.Name:
					//str.Append(" WHERE (LOWER(name) LIKE LOWER('{" + value + "}')");
					//str.Append(" OR number LIKE '{" + value + "}')");
					//str.Append(" OR LOWER(note) LIKE LOWER('{" + value + "}')");

					if(!value.IsNullOrTrimmedEmpty()) {
						parser.DefaultOperator = QueryOperator.AND;
						parser.QueryType = QueryType.Like;
						value = parser.Parse(value, new string[] { "name", "number", "note" });

						str.Append(" WHERE (" + value + ")");
					}
					break;

				case FilterType.NameAndAllConferred:
					//str.Append(" WHERE (LOWER(name) LIKE LOWER('{" + value + "}')");
					//str.Append(" OR number LIKE '{" + value + "}')");

					parser.DefaultOperator = QueryOperator.AND;
					parser.QueryType = QueryType.Like;
					value = parser.Parse(value, new string[] { "name", "number", "note" });

					str.Append(" WHERE (" + value + ")");
					str.Append(" AND is_conferred = 1");
					break;

				case FilterType.NameAndAllOriginals:
					//str.Append(" WHERE (LOWER(name) LIKE LOWER('{" + value + "}')");
					//str.Append(" OR number LIKE '{" + value + "}')");

					parser.DefaultOperator = QueryOperator.AND;
					parser.QueryType = QueryType.Like;
					value = parser.Parse(value, new string[] { "name", "number", "note" });

					str.Append(" WHERE (" + value + ")");
					str.Append(" AND is_original = 1");
					break;

				case FilterType.Actor:
				case FilterType.ActorAndAllConferred:
				case FilterType.ActorAndAllOriginals:
				case FilterType.Director:
				case FilterType.DirectorAndAllConferred:
				case FilterType.DirectorAndAllOriginals:
				case FilterType.Producer:
				case FilterType.ProducerAndAllConferred:
				case FilterType.ProducerAndAllOriginals:
				case FilterType.Cutter:
				case FilterType.CutterAndAllConferred:
				case FilterType.CutterAndAllOriginals:
				case FilterType.Cameraman:
				case FilterType.CameramanAndAllConferred:
				case FilterType.CameramanAndAllOriginals:
				case FilterType.Musician:
				case FilterType.MusicianAndAllConferred:
				case FilterType.MusicianAndAllOriginals:
				case FilterType.Writer:
				case FilterType.WriterAndAllConferred:
				case FilterType.WriterAndAllOriginals:
					str.Remove(0, str.Length);
					str.Append(" SELECT m.*, s.*");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
					str.Append(" ON mp.movie_pkid = m.pkid");
					str.Append(" INNER JOIN tbl_persons AS p");
					str.Append(" ON p.pkid = mp.person_pkid");
					str.Append(" WHERE NOT m.pkid IS NULL");
					str.Append(" AND ( ");

					if(this._cfg.ProviderType == ProviderType.SQLite) {
						str.Append(" ( LOWER(firstname) LIKE LOWER('{" + value + "}') AND LOWER(lastname) LIKE LOWER('{" + value + "}') )");
						str.Append(" OR ( LOWER(firstname) + ' ' + LOWER(lastname) LIKE LOWER('{" + value + "}') ) ");
						str.Append(" OR ( LOWER(lastname) + ' ' + LOWER(firstname) LIKE LOWER('{" + value + "}') )");
						str.Append(" )");
					}
					else {
						str.Append(" ( LOWER(firstname) LIKE LOWER('%" + value + "%') AND LOWER(lastname) LIKE LOWER('%" + value + "%') )");
						str.Append(" OR ( LOWER(firstname) + ' ' + LOWER(lastname) LIKE LOWER('%" + value + "%') ) ");
						str.Append(" OR ( LOWER(lastname) + ' ' + LOWER(firstname) LIKE LOWER('%" + value + "%') )");
						str.Append(" )");
					}
					break;

				case FilterType.Country:
					str.Remove(0, str.Length);
					str.Append(" SELECT m.*, s.*");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" WHERE NOT m.pkid IS NULL");
					str.Append(" AND s.tag = 'C002'");
					str.Append(" AND ( ");

					if(this._cfg.ProviderType == ProviderType.SQLite) {
						str.Append(" s.content LIKE '{" + value + "}')");
						str.Append(" OR s.value LIKE '{" + value + "}')");
					}
					else {
						str.Append(" s.content LIKE '%" + value + "%')");
						str.Append(" OR s.value LIKE '%" + value + "%')");
					}
					
					str.Append(" )");
					break;

				case FilterType.CountryAndAllConferred:
					str.Remove(0, str.Length);
					str.Append(" SELECT m.*, s.*");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" WHERE NOT m.pkid IS NULL");
					str.Append(" AND is_conferred = 1");
					str.Append(" AND s.tag = 'C002'");
					str.Append(" AND ( ");

					if(this._cfg.ProviderType == ProviderType.SQLite) {
						str.Append(" s.content LIKE '{" + value + "}')");
						str.Append(" OR s.value LIKE '{" + value + "}')");
					}
					else {
						str.Append(" s.content LIKE '%" + value + "%')");
						str.Append(" OR s.value LIKE '%" + value + "%')");
					}

					str.Append(" )");
					break;

				case FilterType.CountryAndAllOriginals:
					str.Remove(0, str.Length);
					str.Append(" SELECT m.*, s.*");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" WHERE NOT m.pkid IS NULL");
					str.Append(" AND is_original = 1");
					str.Append(" AND s.tag = 'C002'");
					str.Append(" AND ( ");

					if(this._cfg.ProviderType == ProviderType.SQLite) {
						str.Append(" s.content LIKE '{" + value + "}')");
						str.Append(" OR s.value LIKE '{" + value + "}')");
					}
					else {
						str.Append(" s.content LIKE '%" + value + "%')");
						str.Append(" OR s.value LIKE '%" + value + "%')");
					}

					str.Append(" )");
					break;

				case FilterType.WithoutCategory:
					str.Remove(0, str.Length);
					str.Append("SELECT DISTINCT m.*, s.Content");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" LEFT JOIN tbl_movies_to_categories AS mc");
					str.Append(" ON mc.movie_pkid = m.pkid");
					str.Append(" LEFT JOIN tbl_categories AS c");
					str.Append(" ON c.pkid = mc.category_pkid");
					str.Append(" WHERE c.pkid IS NULL");
					break;

				case FilterType.Category:
					str.Remove(0, str.Length);
					str.Append("SELECT DISTINCT m.*, s.Content");
					str.Append(" FROM tbl_movies AS m");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_static AS s");
					str.Append(" ON s.value = m.country AND s.tag = 'C002'");
					str.Append(" ");
					str.Append(" INNER JOIN tbl_movies_to_categories AS mc");
					str.Append(" ON mc.movie_pkid = m.pkid");
					str.Append(" INNER JOIN tbl_categories AS c");
					str.Append(" ON c.pkid = mc.category_pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE c.pkid = '" + value + "'");
					}
					else {
						str.Append(" WHERE lower(c.pkid) = lower('{" + value + "}')");
					}
					break;
			}

			switch(filter) {
				case FilterType.Actor:
				case FilterType.ActorAndAllConferred:
				case FilterType.ActorAndAllOriginals:
					str.Append(" AND p.is_actor = 1");
					str.Append(" AND mp.as_actor = 1");
					break;

				case FilterType.Director:
				case FilterType.DirectorAndAllConferred:
				case FilterType.DirectorAndAllOriginals:
					str.Append(" AND p.is_director = 1");
					str.Append(" AND mp.as_director = 1");
					break;

				case FilterType.Producer:
				case FilterType.ProducerAndAllConferred:
				case FilterType.ProducerAndAllOriginals:
					str.Append(" AND p.is_producer = 1");
					str.Append(" AND mp.as_producer = 1");
					break;

				case FilterType.Cutter:
				case FilterType.CutterAndAllConferred:
				case FilterType.CutterAndAllOriginals:
					str.Append(" AND p.is_cutter = 1");
					str.Append(" AND mp.as_cutter = 1");
					break;

				case FilterType.Cameraman:
				case FilterType.CameramanAndAllConferred:
				case FilterType.CameramanAndAllOriginals:
					str.Append(" AND p.is_cameraman = 1");
					str.Append(" AND mp.as_cameraman = 1");
					break;

				case FilterType.Musician:
				case FilterType.MusicianAndAllConferred:
				case FilterType.MusicianAndAllOriginals:
					str.Append(" AND p.is_musician = 1");
					str.Append(" AND mp.as_musician = 1");
					break;

				case FilterType.Writer:
				case FilterType.WriterAndAllConferred:
				case FilterType.WriterAndAllOriginals:
					str.Append(" AND p.is_writer = 1");
					str.Append(" AND mp.as_writer = 1");
					break;
			}

			switch(filter) {
				case FilterType.ActorAndAllConferred:
				case FilterType.DirectorAndAllConferred:
				case FilterType.ProducerAndAllConferred:
				case FilterType.CutterAndAllConferred:
				case FilterType.CameramanAndAllConferred:
				case FilterType.MusicianAndAllConferred:
				case FilterType.WriterAndAllConferred:
					str.Append(" AND m.is_conferred = 1");
					break;

				case FilterType.ActorAndAllOriginals:
				case FilterType.DirectorAndAllOriginals:
				case FilterType.ProducerAndAllOriginals:
				case FilterType.CutterAndAllOriginals:
				case FilterType.CameramanAndAllOriginals:
				case FilterType.MusicianAndAllOriginals:
				case FilterType.WriterAndAllOriginals:
					str.Append(" AND m.is_original = 1");
					break;
			}

			if(this._cfg.ProviderType == ProviderType.SQLite) {
				str = str.Replace(" = 1", " = 'True'");
			}

			return str;
		}

		/// <summary>
		/// Get a list of movies by a sql query string
		/// </summary>
		/// <param name="query">The Sql query string.</param>
		/// <param name="filter"></param>
		/// <param name="withAdditionals"></param>
		/// <returns></returns>
		private List<Movie> GetMovieListBySqlQuery(string query, FilterType filter, bool withAdditionals) {
			List<Movie> list = new List<Movie>();
			StringBuilder str = new StringBuilder();

			// new way
			using(DAL dal = new DAL(this._cfg)) {
				IDbCommand cmd = dal.CreateCommand();
				cmd.CommandText = query;

				dal.OpenConnection();

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						Movie mov = new Movie();

						mov.Id = reader.GetSafeValue<Guid>("pkid").ToString();
						mov.Number = reader.GetSafeValue<int>("number");
						mov.Name = reader.GetSafeValue<string>("name");
						mov.Note = reader.GetSafeValue<string>("note");
						mov.HasCover = reader.GetSafeValue<bool>("has_cover");
						mov.IsOriginal = reader.GetSafeValue<bool>("is_original");
						mov.IsConferred = reader.GetSafeValue<bool>("is_conferred");
						mov.ConferredTo = reader.GetSafeValue<string>("conferred_to");
						mov.Codec = (Codec)reader.GetSafeValue<int>("codec");
						mov.Actors = new List<Person>();
						mov.Directors = new List<Person>();
						mov.Producers = new List<Person>();
						mov.Musicians = new List<Person>();
						mov.Cameramans = new List<Person>();
						mov.Cutters = new List<Person>();
						mov.Writers = new List<Person>();
						mov.Genres = new List<Genre>();
						mov.DiscAmount = reader.GetSafeValue<int>("disc_amount");
						mov.Year = reader.GetSafeValue<int>("year");
						mov.Country = reader.GetSafeValue<string>("country");
						mov.Quality = (Quality)reader.GetSafeValue<int>("quality");
						mov.SortValue = ( reader["sort_value"] == DBNull.Value ? "" : reader.GetSafeValue<string>("sort_value") );
						mov.Language = ( reader["language"] == DBNull.Value ? "" : reader.GetSafeValue<string>("language") );
						mov.CountryString = reader.GetSafeValue<string>("content");

						list.Add(mov);
					}
				}

				foreach(Movie mov in list) {
					// genre
					List<Genre> glist = new List<Genre>();

					str.Remove(0, str.Length);
					str.Append("SELECT g.*");
					str.Append(" FROM tbl_genres AS g");
					str.Append(" INNER JOIN tbl_movies_to_genres AS mg");
					str.Append(" ON mg.genre_pkid = g.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mg.movie_pkid = '" + mov.Id + "'");
					}
					else {
						str.Append(" WHERE lower(mg.movie_pkid) = lower('{" + mov.Id + "}')");
					}

					IDbCommand cmdGenre = dal.CreateCommand();
					cmdGenre.CommandText = str.ToString();

					using(IDataReader genreReader = dal.ExecuteQueryForDataReader(cmdGenre)) {
						while(genreReader.Read()) {
							glist.Add(
								new Genre(
									genreReader.GetSafeValue<Guid>("pkid").ToString(),
									genreReader.GetSafeValue<string>("name")
								)
							);
						}
					}

					mov.Genres.AddRange(glist);

					// categories
					List<Category> clist = new List<Category>();

					str.Remove(0, str.Length);
					str.Append("SELECT c.*");
					str.Append(" FROM tbl_categories AS c");
					str.Append(" INNER JOIN tbl_movies_to_categories AS mc");
					str.Append(" ON mc.category_pkid = c.pkid");

					if(this._cfg.ProviderType != ProviderType.SQLite) {
						str.Append(" WHERE mc.movie_pkid = '" + mov.Id + "'");
					}
					else {
						str.Append(" WHERE lower(mc.movie_pkid) = lower('{" + mov.Id + "}')");
					}

					IDbCommand cmdCat = dal.CreateCommand();
					cmdCat.CommandText = str.ToString();

					using(IDataReader catReader = dal.ExecuteQueryForDataReader(cmdCat)) {
						while(catReader.Read()) {
							clist.Add(
								new Category(
									catReader.GetSafeValue<Guid>("pkid").ToString(),
									catReader.GetSafeValue<string>("name")
								)
							);
						}
					}

					mov.Categories.AddRange(clist);

					if(withAdditionals) {
						// actors
						List<Person> alist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_actor = 1");
							str.Append(" AND mp.as_actor = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_actor = 'True'");
							str.Append(" AND mp.as_actor = 'True'");
						}

						IDbCommand cmdActor = dal.CreateCommand();
						cmdActor.CommandText = str.ToString();

						using(IDataReader actorReader = dal.ExecuteQueryForDataReader(cmdActor)) {
							while(actorReader.Read()) {
								alist.Add(
									new Person(
										actorReader.GetSafeValue<Guid>("pkid").ToString(),
										actorReader.GetSafeValue<string>("firstname"),
										actorReader.GetSafeValue<string>("lastname"),
										actorReader.GetSafeValue<bool>("is_actor"),
										actorReader.GetSafeValue<bool>("is_director"),
										actorReader.GetSafeValue<bool>("is_producer"),
										( actorReader["is_cameraman"] == DBNull.Value ? false : actorReader.GetSafeValue<bool>("is_cameraman") ),
										( actorReader["is_cutter"] == DBNull.Value ? false : actorReader.GetSafeValue<bool>("is_cutter") ),
										( actorReader["is_musician"] == DBNull.Value ? false : actorReader.GetSafeValue<bool>("is_musician") ),
										( actorReader["is_writer"] == DBNull.Value ? false : actorReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Actors.AddRange(alist);

						// directors
						List<Person> dlist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_director = 1");
							str.Append(" AND mp.as_director = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_director = 'True'");
							str.Append(" AND mp.as_director = 'True'");
						}

						IDbCommand cmdDirector = dal.CreateCommand();
						cmdDirector.CommandText = str.ToString();

						using(IDataReader directorReader = dal.ExecuteQueryForDataReader(cmdDirector)) {
							while(directorReader.Read()) {
								dlist.Add(
									new Person(
										directorReader.GetSafeValue<Guid>("pkid").ToString(),
										directorReader.GetSafeValue<string>("firstname"),
										directorReader.GetSafeValue<string>("lastname"),
										directorReader.GetSafeValue<bool>("is_actor"),
										directorReader.GetSafeValue<bool>("is_director"),
										directorReader.GetSafeValue<bool>("is_producer"),
										( directorReader["is_cameraman"] == DBNull.Value ? false : directorReader.GetSafeValue<bool>("is_cameraman") ),
										( directorReader["is_cutter"] == DBNull.Value ? false : directorReader.GetSafeValue<bool>("is_cutter") ),
										( directorReader["is_musician"] == DBNull.Value ? false : directorReader.GetSafeValue<bool>("is_musician") ),
										( directorReader["is_writer"] == DBNull.Value ? false : directorReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Directors.AddRange(dlist);

						// producers
						List<Person> plist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_producer = 1");
							str.Append(" AND mp.as_producer = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_producer = 'True'");
							str.Append(" AND mp.as_producer = 'True'");
						}

						IDbCommand cmdProducer = dal.CreateCommand();
						cmdProducer.CommandText = str.ToString();

						using(IDataReader producerReader = dal.ExecuteQueryForDataReader(cmdProducer)) {
							while(producerReader.Read()) {
								plist.Add(
									new Person(
										producerReader.GetSafeValue<Guid>("pkid").ToString(),
										producerReader.GetSafeValue<string>("firstname"),
										producerReader.GetSafeValue<string>("lastname"),
										producerReader.GetSafeValue<bool>("is_actor"),
										producerReader.GetSafeValue<bool>("is_director"),
										producerReader.GetSafeValue<bool>("is_producer"),
										( producerReader["is_cameraman"] == DBNull.Value ? false : producerReader.GetSafeValue<bool>("is_cameraman") ),
										( producerReader["is_cutter"] == DBNull.Value ? false : producerReader.GetSafeValue<bool>("is_cutter") ),
										( producerReader["is_musician"] == DBNull.Value ? false : producerReader.GetSafeValue<bool>("is_musician") ),
										( producerReader["is_writer"] == DBNull.Value ? false : producerReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Producers.AddRange(plist);

						// musician
						List<Person> mlist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_musician = 1");
							str.Append(" AND mp.as_musician = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_musician = 'True'");
							str.Append(" AND mp.as_musician = 'True'");
						}

						IDbCommand cmdMusician = dal.CreateCommand();
						cmdMusician.CommandText = str.ToString();

						using(IDataReader musicianReader = dal.ExecuteQueryForDataReader(cmdMusician)) {
							while(musicianReader.Read()) {
								mlist.Add(
									new Person(
										musicianReader.GetSafeValue<Guid>("pkid").ToString(),
										musicianReader.GetSafeValue<string>("firstname"),
										musicianReader.GetSafeValue<string>("lastname"),
										musicianReader.GetSafeValue<bool>("is_actor"),
										musicianReader.GetSafeValue<bool>("is_director"),
										musicianReader.GetSafeValue<bool>("is_producer"),
										( musicianReader["is_cameraman"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_cameraman") ),
										( musicianReader["is_cutter"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_cutter") ),
										( musicianReader["is_musician"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_musician") ),
										( musicianReader["is_writer"] == DBNull.Value ? false : musicianReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Musicians.AddRange(mlist);

						// cameraman
						List<Person> calist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_cameraman = 1");
							str.Append(" AND mp.as_cameraman = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_cameraman = 'True'");
							str.Append(" AND mp.as_cameraman = 'True'");
						}

						IDbCommand cmdCameraman = dal.CreateCommand();
						cmdCameraman.CommandText = str.ToString();

						using(IDataReader cameramanReader = dal.ExecuteQueryForDataReader(cmdCameraman)) {
							while(cameramanReader.Read()) {
								calist.Add(
									new Person(
										cameramanReader.GetSafeValue<Guid>("pkid").ToString(),
										cameramanReader.GetSafeValue<string>("firstname"),
										cameramanReader.GetSafeValue<string>("lastname"),
										cameramanReader.GetSafeValue<bool>("is_actor"),
										cameramanReader.GetSafeValue<bool>("is_director"),
										cameramanReader.GetSafeValue<bool>("is_producer"),
										( cameramanReader["is_cameraman"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_cameraman") ),
										( cameramanReader["is_cutter"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_cutter") ),
										( cameramanReader["is_musician"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_musician") ),
										( cameramanReader["is_writer"] == DBNull.Value ? false : cameramanReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Cameramans.AddRange(calist);

						// cutter
						List<Person> culist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_cutter = 1");
							str.Append(" AND mp.as_cutter = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_cutter = 'True'");
							str.Append(" AND mp.as_cutter = 'True'");
						}

						IDbCommand cmdCutter = dal.CreateCommand();
						cmdCutter.CommandText = str.ToString();

						using(IDataReader cutterReader = dal.ExecuteQueryForDataReader(cmdCutter)) {
							while(cutterReader.Read()) {
								culist.Add(
									new Person(
										cutterReader.GetSafeValue<Guid>("pkid").ToString(),
										cutterReader.GetSafeValue<string>("firstname"),
										cutterReader.GetSafeValue<string>("lastname"),
										cutterReader.GetSafeValue<bool>("is_actor"),
										cutterReader.GetSafeValue<bool>("is_director"),
										cutterReader.GetSafeValue<bool>("is_producer"),
										( cutterReader["is_cameraman"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_cameraman") ),
										( cutterReader["is_cutter"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_cutter") ),
										( cutterReader["is_musician"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_musician") ),
										( cutterReader["is_writer"] == DBNull.Value ? false : cutterReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Cameramans.AddRange(calist);

						// writer
						List<Person> wlist = new List<Person>();

						str.Remove(0, str.Length);
						str.Append("SELECT p.*");
						str.Append(" FROM tbl_persons AS p");
						str.Append(" INNER JOIN tbl_movies_to_persons AS mp");
						str.Append(" ON mp.person_pkid = p.pkid");

						if(this._cfg.ProviderType != ProviderType.SQLite) {
							str.Append(" WHERE mp.movie_pkid = '" + mov.Id + "'");
							str.Append(" AND p.is_writer = 1");
							str.Append(" AND mp.as_writer = 1");
						}
						else {
							str.Append(" WHERE lower(mp.movie_pkid) = lower('{" + mov.Id + "}')");
							str.Append(" AND p.is_writer = 'True'");
							str.Append(" AND mp.as_writer = 'True'");
						}

						IDbCommand cmdWriter = dal.CreateCommand();
						cmdCutter.CommandText = str.ToString();

						using(IDataReader writerReader = dal.ExecuteQueryForDataReader(cmdCutter)) {
							while(writerReader.Read()) {
								wlist.Add(
									new Person(
										writerReader.GetSafeValue<Guid>("pkid").ToString(),
										writerReader.GetSafeValue<string>("firstname"),
										writerReader.GetSafeValue<string>("lastname"),
										writerReader.GetSafeValue<bool>("is_actor"),
										writerReader.GetSafeValue<bool>("is_director"),
										writerReader.GetSafeValue<bool>("is_producer"),
										( writerReader["is_cameraman"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_cameraman") ),
										( writerReader["is_cutter"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_cutter") ),
										( writerReader["is_musician"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_musician") ),
										( writerReader["is_writer"] == DBNull.Value ? false : writerReader.GetSafeValue<bool>("is_writer") )
									)
								);
							}
						}

						mov.Cameramans.AddRange(calist);
					}
				}
			}

			return list;
		}

		#endregion
	}
}
