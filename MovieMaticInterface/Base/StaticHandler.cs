using MSSystem = System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using Toenda.MovieMaticInterface.Bean;

using Toenda.Foundation;
using Toenda.Foundation.Data;

namespace Toenda.MovieMaticInterface.Base {
	/// <summary>
	/// The order of the static items list
	/// </summary>
	public enum OrderStaticsBy {
		/// <summary> Undefined </summary>
		Undefined = 0,
		/// <summary> Order by value desc </summary>
		ValueDESC = 1,
		/// <summary> Order by value asc </summary>
		ValueASC = 2,
		/// <summary> Order by content desc </summary>
		ContentDESC = 3,
		/// <summary> Order by content asc </summary>
		ContentASC = 4
	}

	/// <summary>
	/// Class DBStatic
	/// </summary>
	public class StaticHandler {
		private DALSettings _cfg;
		private DAL _db;

		// -------------------------------------------------------
		// CONSTRUCTORS
		// -------------------------------------------------------

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="connectionString"></param>
		public StaticHandler(string connectionString) {
			this._cfg = UdlParser.ParseConnectionString(connectionString);
			this._db = new DAL(this._cfg);
		}

		// ----------------------------------------------
		// PRIVATE MEMBERS
		// ----------------------------------------------

		/// <summary>
		/// [PRIVATE] Fill a static from table reader
		/// </summary>
		/// <param name="oDT"></param>
		/// <returns></returns>
		private Static _FillStaticFromTableReader(DataTableReader oDT) {
			Static stc = new Static();

			stc.Tag = (oDT.IsDBNull(oDT.GetOrdinal("tag")) ? "" : oDT["tag"].ToString());
			stc.Type = (oDT.IsDBNull(oDT.GetOrdinal("type")) ? "" : oDT["type"].ToString());
			stc.Content = (oDT.IsDBNull(oDT.GetOrdinal("content")) ? "" : oDT["content"].ToString());
			stc.Value = (oDT.IsDBNull(oDT.GetOrdinal("value")) ? "" : oDT["value"].ToString());
			stc.Length = oDT.IsDBNull(oDT.GetOrdinal("length")) ? 0 : oDT.GetInt32(oDT.GetOrdinal("length"));

			return stc;
		}

		/// <summary>
		/// _s the fill static from table reader.
		/// </summary>
		/// <param name="oDT">The o DT.</param>
		/// <returns></returns>
		private Static _FillStaticFromTableReader(IDataReader oDT) {
			Static stc = new Static();

			stc.Tag = ( oDT.IsDBNull(oDT.GetOrdinal("tag")) ? "" : oDT["tag"].ToString() );
			stc.Type = ( oDT.IsDBNull(oDT.GetOrdinal("type")) ? "" : oDT["type"].ToString() );
			stc.Content = ( oDT.IsDBNull(oDT.GetOrdinal("content")) ? "" : oDT["content"].ToString() );
			stc.Value = ( oDT.IsDBNull(oDT.GetOrdinal("value")) ? "" : oDT["value"].ToString() );
			stc.Length = oDT.IsDBNull(oDT.GetOrdinal("length")) ? 0 : oDT.GetInt32(oDT.GetOrdinal("length"));

			return stc;
		}

		/// <summary>
		/// Get statics
		/// </summary>
		/// <param name="Tag"></param>
		/// <param name="Value"></param>
		/// <returns></returns>
		private DataSet _GetStatic(string Tag, string Value) {
			StringBuilder str = new StringBuilder();

			str.Append("SELECT *");
			str.Append(" FROM [tbl_static]");
			str.Append(" WHERE [tag] = '" + Tag + "'");
			str.Append(" AND [value] = '" + Value + "'");
			str.Append(" ORDER BY [value] ASC");

			return this._db.ExecuteQuery(str.ToString());
		}

		/// <summary>
		/// Get statics
		/// </summary>
		/// <param name="Tag"></param>
		/// <param name="osb"></param>
		/// <returns></returns>
		private DataSet _GetStatics(string Tag, OrderStaticsBy osb) {
			StringBuilder str = new StringBuilder();

			str.Append("SELECT *");
			str.Append(" FROM [tbl_static]");
			str.Append(" WHERE [tag] = '" + Tag + "'");

			switch(osb) {
				case OrderStaticsBy.Undefined:
					str.Append(" ORDER BY [value] ASC");
					break;

				case OrderStaticsBy.ValueASC:
					str.Append(" ORDER BY [value] ASC");
					break;

				case OrderStaticsBy.ValueDESC:
					str.Append(" ORDER BY [value] DESC");
					break;

				case OrderStaticsBy.ContentASC:
					str.Append(" ORDER BY [content] ASC");
					break;

				case OrderStaticsBy.ContentDESC:
					str.Append(" ORDER BY [content] DESC");
					break;
			}

			return this._db.ExecuteQuery(str.ToString());
		}

		// ----------------------------------------------
		// PUBLIC MEMBERS
		// ----------------------------------------------

		/// <summary>
		/// Get a arraylist of static items
		/// </summary>
		/// <param name="Tag"></param>
		/// <returns></returns>
		public List<Static> GetStaticItemList(string Tag) {
			List<Static> arr = new List<Static>();
			DataSet dsData = this._GetStatics(Tag, OrderStaticsBy.Undefined);

			if(dsData.Tables[0].Rows.Count > 0){
				DataTableReader oDT = dsData.CreateDataReader();
				
				while(oDT.Read()){
					Static stc = this._FillStaticFromTableReader(oDT);
					arr.Add(stc);
				}
			}

			return arr;
		}

		/// <summary>
		/// Get a arraylist of static items
		/// </summary>
		/// <param name="Tag"></param>
		/// <param name="osb"></param>
		/// <returns></returns>
		public List<Static> GetStaticItemList(string Tag, OrderStaticsBy osb) {
			List<Static> arr = new List<Static>();
			DataSet dsData = this._GetStatics(Tag, osb);

			if(dsData.Tables[0].Rows.Count > 0) {
				DataTableReader oDT = dsData.CreateDataReader();

				while(oDT.Read()) {
					Static stc = this._FillStaticFromTableReader(oDT);
					arr.Add(stc);
				}
			}

			return arr;
		}

		/// <summary>
		/// Get a single static item
		/// </summary>
		/// <param name="Tag"></param>
		/// <param name="Value"></param>
		/// <returns></returns>
		public Static GetStaticItem(string Tag, string Value) {
			DataSet dsData = this._GetStatic(Tag, Value);
			Static stc = new Static();

			if(dsData.Tables[0].Rows.Count > 0) {
				DataTableReader oDT = dsData.CreateDataReader();
				oDT.Read();

				stc = this._FillStaticFromTableReader(oDT);
			}

			return stc;
		}

		/// <summary>
		/// Searches the static item.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <param name="content">The content.</param>
		/// <returns></returns>
		public List<Static> SearchStaticItem(string tag, string content) {
			List<Static> list = new List<Static>();

			using(DAL dal = new DAL(this._cfg)) {
				dal.OpenConnection();

				DbCommand cmd = dal.CreateCommand();

				cmd.CommandText = "SELECT * ";
				cmd.CommandText += " FROM tbl_static";
				cmd.CommandText += " WHERE tag = @tag";
				cmd.CommandText += " AND content LIKE '%' + @content + '%'";

				cmd.AddParameter("tag", tag);
				cmd.AddParameter("content", content);

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						list.Add(
							this._FillStaticFromTableReader(reader)
						);
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Get the state code ba a complete state name
		/// </summary>
		/// <param name="StateString"></param>
		/// <returns></returns>
		public string GetStateCode(string StateString) {
			string strReturn = "";

			switch(StateString) {
				case "SchleswigHolstein": strReturn = "01"; break;
				case "Hamburg": strReturn = "02"; break;
				case "Niedersachsen": strReturn = "03"; break;
				case "Bremen": strReturn = "04"; break;
				case "NordrheinWestfalen": strReturn = "05"; break;
				case "Hessen": strReturn = "06"; break;
				case "RheinlandPfalz": strReturn = "07"; break;
				case "BadenWuerttemberg": strReturn = "08"; break;
				case "Bayern": strReturn = "09"; break;
				case "Saarland": strReturn = "10"; break;
				case "Berlin": strReturn = "11"; break;
				case "Brandenburg": strReturn = "12"; break;
				case "MecklenburgVorpommern": strReturn = "13"; break;
				case "Sachsen": strReturn = "14"; break;
				case "SachsenAnhalt": strReturn = "15"; break;
				case "Thueringen": strReturn = "16"; break;
				default: strReturn = "00"; break;
			}

			return strReturn;
		}

	}
}

