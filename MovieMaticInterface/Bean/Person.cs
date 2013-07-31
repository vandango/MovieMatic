using System;
using System.Collections.Generic;
using System.Text;

using Toenda.Foundation;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Class Person
	/// </summary>
	public class Person : IComparable {
		private string _id;
		private string _firstname;
		private string _lastname;
		private bool _is_actor;
		private bool _is_director;
		private bool _is_producer;
		private bool _is_cameraman;
		private bool _is_cutter;
		private bool _is_musician;
		private bool _is_writer;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public Person() {
		}

		/// <summary>
		/// Spezi Ctor
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="firstname">The firstname.</param>
		/// <param name="lastname">The lastname.</param>
		/// <param name="isActor">if set to <c>true</c> [is actor].</param>
		/// <param name="isDirector">if set to <c>true</c> [is director].</param>
		/// <param name="isProducer">if set to <c>true</c> [is producer].</param>
		/// <param name="isCameraman">if set to <c>true</c> [is cameraman].</param>
		/// <param name="isCutter">if set to <c>true</c> [is cutter].</param>
		/// <param name="isMusician">if set to <c>true</c> [is musician].</param>
		/// <param name="isWriter">if set to <c>true</c> [is writer].</param>
		public Person(
			string id,
			string firstname,
			string lastname, 
			bool isActor, 
			bool isDirector,
			bool isProducer,
			bool isCameraman,
			bool isCutter,
			bool isMusician,
			bool isWriter) {
			this._id = id;
			this._firstname = firstname;
			this._lastname = lastname;
			this._is_actor = isActor;
			this._is_director = isDirector;
			this._is_producer = isProducer;
			this._is_cameraman = isCameraman;
			this._is_cutter = isCutter;
			this._is_musician = isMusician;
			this._is_writer = isWriter;
		}

		// -------------------------------------------------------
		// INTERFACE
		// -------------------------------------------------------

		/// <summary>
		/// CompareTo
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public int CompareTo(object o) {
			return 0;
		}

		/// <summary>
		/// Sort class for the first name
		/// </summary>
		public class SortByFirstname : IComparer<Person> {
			public int Compare(Person obj1, Person obj2) {
				if(obj1 == null && obj2 == null) {
					return 0;
				}

				if(obj1 == null) {
					return 1;
				}

				if(obj2 == null) {
					return -1;
				}

				return String.Compare(obj1.Firstname, obj2.Firstname);
			}
		}

		/// <summary>
		/// Sort class for the last name
		/// </summary>
		public class SortByLastname : IComparer<Person> {
			public int Compare(Person obj1, Person obj2) {
				if(obj1 == null && obj2 == null) {
					return 0;
				}

				if(obj1 == null) {
					return 1;
				}

				if(obj2 == null) {
					return -1;
				}

				return String.Compare(obj1.Lastname, obj2.Lastname);
			}
		}

		// -------------------------------------------------------
		// PROPERTIES
		// -------------------------------------------------------

		/// <summary>
		/// Gets or sets the movie quantity as actor.
		/// </summary>
		/// <value>The movie quantity as actor.</value>
		public int MovieQuantityAsActor { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as director.
		/// </summary>
		/// <value>The movie quantity as director.</value>
		public int MovieQuantityAsDirector { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as producer.
		/// </summary>
		/// <value>The movie quantity as producer.</value>
		public int MovieQuantityAsProducer { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as musician.
		/// </summary>
		/// <value>The movie quantity as musician.</value>
		public int MovieQuantityAsMusician { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as cutter.
		/// </summary>
		/// <value>The movie quantity as cutter.</value>
		public int MovieQuantityAsCutter { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as cameraman.
		/// </summary>
		/// <value>The movie quantity as cameraman.</value>
		public int MovieQuantityAsCameraman { get; set; }

		/// <summary>
		/// Gets or sets the movie quantity as writer.
		/// </summary>
		/// <value>The movie quantity as writer.</value>
		public int MovieQuantityAsWriter { get; set; }

		/// <summary>
		/// Gets the movie quantity.
		/// </summary>
		/// <value>The movie quantity.</value>
		public int MovieQuantity {
			get;
			set;
			//get {
			//    return this.MovieQuantityAsActor + this.MovieQuantityAsDirector + this.MovieQuantityAsProducer;
			//}
		}

		/// <summary>
		/// Gets the fullname.
		/// </summary>
		/// <value>The fullname.</value>
		public string Fullname {
			get {
				StringBuilder str = new StringBuilder();

				str.Append(this._firstname);

				if(!this._firstname.IsNullOrTrimmedEmpty()
				&& !this._lastname.IsNullOrTrimmedEmpty()) {
					str.Append(" ");
				}

				str.Append(this._lastname);

				return str.ToString();
			}
		}

		/// <summary>
		/// Get or set the ID
		/// </summary>
		public string ID {
			get { return this._id; }
			set { this._id = value; }
		}

		/// <summary>
		/// Get or set the firstname
		/// </summary>
		public string Firstname {
			get { return this._firstname; }
			set { this._firstname = value; }
		}

		/// <summary>
		/// Get or set the lastname
		/// </summary>
		public string Lastname {
			get { return this._lastname; }
			set { this._lastname = value; }
		}

		/// <summary>
		/// Get or set a value that indicates that this person is a actor
		/// </summary>
		public bool IsActor {
			get { return this._is_actor; }
			set { this._is_actor = value; }
		}

		/// <summary>
		/// Get or set a value that indicates that this person is a director
		/// </summary>
		public bool IsDirector {
			get { return this._is_director; }
			set { this._is_director = value; }
		}

		/// <summary>
		/// Get or set a value that indicates that this person is a producer
		/// </summary>
		public bool IsProducer {
			get { return this._is_producer; }
			set { this._is_producer = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is cameraman.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is cameraman; otherwise, <c>false</c>.
		/// </value>
		public bool IsCameraman {
			get { return this._is_cameraman; }
			set { this._is_cameraman = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is cutter.
		/// </summary>
		/// <value><c>true</c> if this instance is cutter; otherwise, <c>false</c>.</value>
		public bool IsCutter {
			get { return this._is_cutter; }
			set { this._is_cutter = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is musician.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is musician; otherwise, <c>false</c>.
		/// </value>
		public bool IsMusician {
			get { return this._is_musician; }
			set { this._is_musician = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is writer.
		/// </summary>
		/// <value><c>true</c> if this instance is writer; otherwise, <c>false</c>.</value>
		public bool IsWriter {
			get { return this._is_writer; }
			set { this._is_writer = value; }
		}

		/// <summary>
		/// Gets or sets the rolename.
		/// </summary>
		/// <value>The rolename.</value>
		public string Rolename { get; set; }

		/// <summary>
		/// Gets or sets the roletype.
		/// </summary>
		/// <value>The roletype.</value>
		public string Roletype { get; set; }
	}
}
