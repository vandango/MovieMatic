using System;
using System.Collections.Generic;
using System.Text;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Class Genre
	/// </summary>
	public class Genre : IComparable {
		private string _id;
		private string _name;
		private int _movieCount;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public Genre() {
		}

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="name">The name.</param>
		public Genre(string id, string name) {
			this._id = id;
			this._name = name;
		}

		/// <summary>
		/// Default Ctor
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="name">The name.</param>
		/// <param name="movieCount">The movie count.</param>
		public Genre(string id, string name, int movieCount) {
			this._id = id;
			this._name = name;
			this._movieCount = movieCount;
		}

		// -------------------------------------------------------
		// INTERFACE
		// -------------------------------------------------------

		/// <summary>
		/// CompareTo
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object obj) {
			return 0;
		}

		/// <summary>
		/// Sort class for the name
		/// </summary>
		public class SortByName : IComparer<Genre> {
			public int Compare(Genre obj1, Genre obj2) {
				if(obj1 == null && obj2 == null) {
					return 0;
				}

				if(obj1 == null) {
					return 1;
				}

				if(obj2 == null) {
					return -1;
				}

				return String.Compare(obj1.Name, obj2.Name);
			}
		}

		/// <summary>
		/// Sort class for the ID
		/// </summary>
		public class SortById : IComparer<Genre> {
			public int Compare(Genre obj1, Genre obj2) {
				if(obj1 == null && obj2 == null) {
					return 0;
				}

				if(obj1 == null) {
					return 1;
				}

				if(obj2 == null) {
					return -1;
				}

				return String.Compare(obj1.ID, obj2.ID);
			}
		}

		/// <summary>
		/// Sort class for the MovieCount
		/// </summary>
		public class SortByMovieCount : IComparer<Genre> {
			public int Compare(Genre obj1, Genre obj2) {
				if(obj2.MovieCount < obj1.MovieCount) {
					return 1;
				}
				if(obj2.MovieCount == obj1.MovieCount) {
					return 0;
				}
				else {
					return -1;
				}
			}
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Get or set the ID
		/// </summary>
		public string ID {
			get { return this._id; }
			set { this._id = value; }
		}

		/// <summary>
		/// Get or set the Name
		/// </summary>
		public string Name {
			get { return this._name; }
			set { this._name = value; }
		}

		/// <summary>
		/// Gets or sets the movie count.
		/// </summary>
		/// <value>The movie count.</value>
		public int MovieCount {
			get { return this._movieCount; }
			set { this._movieCount = value; }
		}

		// -------------------------------------------------------
		// OVERRIDE
		// -------------------------------------------------------

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString() {
			return this._name;
		}
	}
}
