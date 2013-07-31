using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Implements PersonCollection
	/// </summary>
	public class PersonCollection : CollectionBase, IBindingList {//, ICollection, IList {
		private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
		private ListChangedEventHandler onListChanged;

		//List<Person> _internal = null;

		///// <summary>
		///// Initializes a new instance of the <see cref="PersonList"/> class.
		///// </summary>
		//public PersonCollection() {
		//    //this._internal = new List<Person>();
		//}

		/// <summary>
		/// Gets the enmerator.
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnmerator() {
			return List.GetEnumerator();
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public void Insert(int index, Person value) {
			( (IList)this ).Insert(index, (object)value);
		}

		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int Add(Person value) {
			return ( (IList)this ).Add(value);
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Remove(Person value) {
			( (IList)this ).Remove(value);
			//return true;
		}

		//public void Sort(IComparer<Person> comparer) {
		//}

		/// <summary>
		/// Gets or sets the <see cref="Toenda.MovieMaticInterface.Bean.Person"/> at the specified index.
		/// </summary>
		/// <value></value>
		public Person this[int index] {
			get {
				return (Person)( (IList<Person>)this )[index];
			}
			set {
				( (IList<Person>)this )[index] = value;
			}
		}

		//#region Implements IList.

		///// <summary>
		///// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> is read-only.
		///// </summary>
		///// <value></value>
		///// <returns>true if the <see cref="T:System.Collections.IList"/> is read-only; otherwise, false.
		///// </returns>
		//public bool IsReadOnly {
		//    get { return false; }
		//}

		//#endregion

		#region Implements IBindingList.

		/// <summary>
		/// Gets whether you can update items in the list.
		/// </summary>
		/// <value></value>
		/// <returns>true if you can update the items in the list; otherwise, false.
		/// </returns>
		bool IBindingList.AllowEdit {
			get { return true; }
		}

		/// <summary>
		/// Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>.
		/// </summary>
		/// <value></value>
		/// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew"/>; otherwise, false.
		/// </returns>
		bool IBindingList.AllowNew {
			get { return true; }
		}

		/// <summary>
		/// Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)"/> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)"/>.
		/// </summary>
		/// <value></value>
		/// <returns>true if you can remove items from the list; otherwise, false.
		/// </returns>
		bool IBindingList.AllowRemove {
			get { return true; }
		}

		/// <summary>
		/// Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or an item in the list changes.
		/// </summary>
		/// <value></value>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged"/> event is raised when the list changes or when an item changes; otherwise, false.
		/// </returns>
		bool IBindingList.SupportsChangeNotification {
			get { return true; }
		}

		/// <summary>
		/// Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method.
		/// </summary>
		/// <value></value>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)"/> method; otherwise, false.
		/// </returns>
		bool IBindingList.SupportsSearching {
			get { return false; }
		}

		/// <summary>
		/// Gets whether the list supports sorting.
		/// </summary>
		/// <value></value>
		/// <returns>true if the list supports sorting; otherwise, false.
		/// </returns>
		bool IBindingList.SupportsSorting {
			get { return false; }
		}

		#endregion

		#region Events.

		/// <summary>
		/// Occurs when the list changes or an item in the list changes.
		/// </summary>
		public event ListChangedEventHandler ListChanged {
			add {
				onListChanged += value;
			}
			remove {
				onListChanged -= value;
			}
		}

		#endregion

		#region Methods.

		/// <summary>
		/// Adds a new item to the list.
		/// </summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.AllowNew"/> is false.
		/// </exception>
		object IBindingList.AddNew() {
			Person person = new Person();
			List.Add(person);
			return person;
		}

		#endregion

		#region Unsupported properties.

		/// <summary>
		/// Gets the direction of the sort.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
		/// </exception>
		ListSortDirection IBindingList.SortDirection {
			get { throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the <see cref="T:System.ComponentModel.PropertyDescriptor"/> that is being used for sorting.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.ComponentModel.PropertyDescriptor"/> that is being used for sorting.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
		/// </exception>
		PropertyDescriptor IBindingList.SortProperty {
			get { throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets whether the items in the list are sorted.
		/// </summary>
		/// <value></value>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort"/> has not been called; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
		/// </exception>
		bool IBindingList.IsSorted {
			get { throw new NotSupportedException(); }
		}

		#endregion

		#region Unsupported properties.

		/// <summary>
		/// Adds the <see cref="T:System.ComponentModel.PropertyDescriptor"/> to the indexes used for searching.
		/// </summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to add to the indexes used for searching.</param>
		void IBindingList.AddIndex(PropertyDescriptor property) {
			throw new NotSupportedException();
		}

		/// <summary>
		/// Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor"/> and a <see cref="T:System.ComponentModel.ListSortDirection"/>.
		/// </summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values.</param>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
		/// </exception>
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {
			throw new NotSupportedException();
		}

		/// <summary>
		/// Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"/>.
		/// </summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to search on.</param>
		/// <param name="key">The value of the <paramref name="property"/> parameter to search for.</param>
		/// <returns>
		/// The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor"/>.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSearching"/> is false.
		/// </exception>
		int IBindingList.Find(PropertyDescriptor property, object key) {
			throw new NotSupportedException();
		}

		/// <summary>
		/// Removes the <see cref="T:System.ComponentModel.PropertyDescriptor"/> from the indexes used for searching.
		/// </summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor"/> to remove from the indexes used for searching.</param>
		void IBindingList.RemoveIndex(PropertyDescriptor property) {
			throw new NotSupportedException();
		}

		/// <summary>
		/// Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// 	<see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> is false.
		/// </exception>
		void IBindingList.RemoveSort() {
			throw new NotSupportedException();
		}

		#endregion
	}
}
