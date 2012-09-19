using System;
using System.Collections.Generic;
using System.Text;

using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Base {
	/// <summary>
	/// Class Tools
	/// </summary>
	public class Tools {
		/// <summary>
		/// Format a year value
		/// </summary>
		/// <param name="year"></param>
		/// <returns></returns>
		public static string FormatYear(int year) {
			return year.ToString().PadLeft(4, '0');
		}

		/// <summary>
		/// Splits the name by last space.
		/// </summary>
		/// <param name="fullname">The fullname.</param>
		/// <returns></returns>
		public static PersonName SplitNameByLastSpace(string fullname) {
			if(fullname.Length > 6
			&& fullname.Contains(" ")) {
				int last = fullname.LastIndexOf(' ');

				PersonName name = new PersonName();

				name.Firstname = fullname.Substring(0, last).Trim();
				name.Lastname = fullname.Substring(last).Trim();
				name.Fullname = fullname.Trim();

				return name;
			}
			else {
				return null;
			}
		}
		
		/// <summary>
		/// Get a name of a genre by its id from a list
		/// </summary>
		/// <param name="list"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetGenreNameFromList(List<Genre> list, string id) {
			foreach(Genre g in list) {
				if(g.ID == id) {
					return g.Name;
				}
			}

			return "";
		}

		/// <summary>
		/// Gets the category name from list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public static string GetCategoryNameFromList(List<Category> list, string id) {
			foreach(Category g in list) {
				if(g.ID == id) {
					return g.Name;
				}
			}

			return "";
		}

		/// <summary>
		/// Gets the name of the genre or category and clean it.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static string GetCleanGenreOrCategoryName(string name) {
			if(name.Contains("(") && name.Contains(")")) {
				return name.Substring(0, name.LastIndexOf("(")).Trim();
			}
			else {
				return name;
			}
		}
	}

	/// <summary>
	/// Class PersonName
	/// </summary>
	public class PersonName {
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Fullname { get; set; }
	}
}
