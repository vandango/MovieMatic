using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMatic {
	public partial class NonUsedIdsForm : Form {
		private DataHandler _db = new DataHandler(
			Configuration.ConnectionString
		);

		private List<Movie> _movies;

		public NonUsedIdsForm() {
			InitializeComponent();

			this._LoadData();
		}
		
		private void _LoadData() {
			this._movies = this._db.GetMovieList("", Foundation.DataSortDirection.Unspecified);
			int maxNumber = this._movies.Max(item => item.Number);

			List<int> unusedNumbers = new List<int>();

			for(int i = 1; i <= maxNumber; i++) {
				if(!this._Exist(i)) {
					unusedNumbers.Add(i);
				}
			}

			foreach(int number in unusedNumbers) {
				ListViewItem lvi = new ListViewItem();
				lvi.Text = number.ToString();

				lvi.Name = number.ToString();

				this.lvNumbers.Items.Add(lvi);
			}
		}

		private bool _Exist(int number) {
			var result =
				from item in this._movies
				where item.Number == number
				select item;

			if(result == null
			|| result.Count() == 0) {
				return false;
			}
			else {
				return true;
			}
		}
	}
}
