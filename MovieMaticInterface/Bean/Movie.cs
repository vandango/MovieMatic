using System;
using System.Collections.Generic;
using System.Text;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Class Movie
	/// </summary>
	public class Movie : IComparable {
		/// <summary>
		/// Default Ctor
		/// </summary>
		public Movie() {
			this.Genres = new List<Genre>();
			this.Categories = new List<Category>();
			this.Actors = new List<Person>();
			this.Directors = new List<Person>();
			this.Producers = new List<Person>();
			this.Musicians = new List<Person>();
			this.Cameramans = new List<Person>();
			this.Cutters = new List<Person>();
			this.Writers = new List<Person>();
		}

		/// <summary>
		/// CompareTo
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object obj) {
			Movie val = (Movie)obj;

			if(val.Number < this.Number) {
				return 1;
			}
			else if(val.Number == this.Number) {
				return 0;
			}
			else { 
				return -1;
			}
		}

		/// <summary>
		/// Sort class for the name
		/// </summary>
		public class SortByName : IComparer<Movie> {
			public int Compare(Movie obj1, Movie obj2) {
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
		/// Sort class for the sort value
		/// </summary>
		public class SortBySortValue : IComparer<Movie> {
			public int Compare(Movie obj1, Movie obj2) {
				if(obj1 == null && obj2 == null) {
					return 0;
				}

				if(obj1 == null) {
					return 1;
				}

				if(obj2 == null) {
					return -1;
				}

				return String.Compare(obj1.SortValue, obj2.SortValue);
			}
		}

		/// <summary>
		/// Get or set the ID
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Get or set the number
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Gets or sets the sort value.
		/// </summary>
		/// <value>The sort value.</value>
		public string SortValue { get; set; }

		/// <summary>
		/// Get or set the Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Get or set the Note
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		/// Get or set the HasCover
		/// </summary>
		public bool HasCover { get; set; }

		/// <summary>
		/// Get or set the IsOriginal
		/// </summary>
		public bool IsOriginal { get; set; }

		/// <summary>
		/// Get or set the is conferred
		/// </summary>
		public bool IsConferred { get; set; }

		/// <summary>
		/// Get or set the is conferred to
		/// </summary>
		public string ConferredTo { get; set; }

		/// <summary>
		/// Get or set the Codec
		/// </summary>
		public Codec Codec { get; set; }

		/// <summary>
		/// ActorsList
		/// </summary>
		public List<Person> Actors { get; set; }

		/// <summary>
		/// DirectorsList
		/// </summary>
		public List<Person> Directors { get; set; }

		/// <summary>
		/// ProducersList
		/// </summary>
		public List<Person> Producers { get; set; }

		/// <summary>
		/// Gets or sets the musician.
		/// </summary>
		/// <value>The musician.</value>
		public List<Person> Musicians { get; set; }

		/// <summary>
		/// Gets or sets the cameraman.
		/// </summary>
		/// <value>The cameraman.</value>
		public List<Person> Cameramans { get; set; }

		/// <summary>
		/// Gets or sets the cutter.
		/// </summary>
		/// <value>The cutter.</value>
		public List<Person> Cutters { get; set; }

		/// <summary>
		/// Gets or sets the writer.
		/// </summary>
		/// <value>The writer.</value>
		public List<Person> Writers { get; set; }

		/// <summary>
		/// GenresList
		/// </summary>
		public List<Genre> Genres { get; set; }

		/// <summary>
		/// Gets or sets the categories.
		/// </summary>
		/// <value>The categories.</value>
		public List<Category> Categories { get; set; }

		/// <summary>
		/// Get or set the disc amount
		/// </summary>
		public int DiscAmount { get; set; }

		/// <summary>
		/// Get or set the year
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// Get or set the country
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language { get; set; }

		/// <summary>
		/// Get or set the quality
		/// </summary>
		public Quality Quality { get; set; }

		/// <summary>
		/// Gets or sets the country string.
		/// </summary>
		/// <value>The country string.</value>
		public string CountryString { get; set; }

		/// <summary>
		/// Gets the quality string.
		/// </summary>
		/// <value>The quality string.</value>
		public string GenerateQualityString {
			get {
				string tmp = this.Quality.ToString();
				int quali = 0;
				int.TryParse(tmp.Substring(tmp.LastIndexOf("_") + 1), out quali);

				return "(" + quali.ToString() + ") " + this.Quality.ToString().Substring(0, this.Quality.ToString().IndexOf("_"));
			}
		}

		/// <summary>
		/// Get Actors as list string
		/// </summary>
		public string GenerateActorsString {
			get { return this.GetPersonString(this.Actors); }
		}

		/// <summary>
		/// Get Directors as list string
		/// </summary>
		public string GenerateDirectorsString {
			get { return this.GetPersonString(this.Directors); }
		}

		/// <summary>
		/// Get Producer as list string
		/// </summary>
		public string GenerateProducersString {
			get { return this.GetPersonString(this.Producers); }
		}

		/// <summary>
		/// Gets the musician string.
		/// </summary>
		/// <value>The musician string.</value>
		public string GenerateMusicianString {
			get { return this.GetPersonString(this.Musicians); }
		}

		/// <summary>
		/// Gets the cameraman string.
		/// </summary>
		/// <value>The cameraman string.</value>
		public string GenerateCameramanString {
			get { return this.GetPersonString(this.Cameramans); }
		}

		/// <summary>
		/// Gets the cutter string.
		/// </summary>
		/// <value>The cutter string.</value>
		public string GenerateCutterString {
			get { return this.GetPersonString(this.Cutters); }
		}

		/// <summary>
		/// Gets the writer string.
		/// </summary>
		/// <value>The writer string.</value>
		public string GenerateWriterString {
			get { return this.GetPersonString(this.Writers); }
		}

		/// <summary>
		/// Get Genres as list string
		/// </summary>
		public string GenerateGenresString {
			get {
				string genres = "";

				if(this.Genres != null) {
					foreach(Genre g in this.Genres) {
						genres += g.Name + ", ";
					}

					if(genres.Length > 3) {
						return genres.Substring(0, genres.Length - 2);
					}
					else {
						return "";
					}
				}
				else {
					return "";
				}
			}
		}

		/// <summary>
		/// Gets the categories string.
		/// </summary>
		/// <value>The categories string.</value>
		public string GenerateCategoriesString {
			get {
				string categories = "";

				if(this.Categories != null) {
					foreach(Category c in this.Categories) {
						categories += c.Name + ", ";
					}

					if(categories.Length > 3) {
						return categories.Substring(0, categories.Length - 2);
					}
					else {
						return "";
					}
				}
				else {
					return "";
				}
			}
		}

		/// <summary>
		/// Gets the person string.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public string GetPersonString(List<Person> list) {
			string names = "";

			if(list != null) {
				foreach(Person p in list) {
					//if(p.IsProducer) {
					names += p.Firstname + " " + p.Lastname + ", ";
					//}
				}

				if(names.Length > 3) {
					return names.Substring(0, names.Length - 2);
				}
				else {
					return "";
				}
			}
			else {
				return "";
			}
		}
	}
}
