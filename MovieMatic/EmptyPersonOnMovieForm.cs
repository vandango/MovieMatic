using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.Types;
using Toenda.Foundation.Windows.Forms;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Import;

namespace Toenda.MovieMatic {
	public partial class EmptyPersonOnMovieForm : Form {
		private DataHandler _db = new DataHandler(
			Configuration.ConnectionString
		);

		private List<Movie> _movies;
		private int _current_amount;

		public EmptyPersonOnMovieForm() {
			InitializeComponent();

			this.DialogResult = DialogResult.OK;

			this._LoadData();
		}

		private void _LoadData() {
			this.dgvMovies.DataSource = null;
			this._movies = new List<Movie>();

			this._movies = this._db.GetEmptyPersonOnMovieList();

			this._current_amount = this._movies.Count;

			this.dgvMovies.AutoGenerateColumns = false;
			this.dgvMovies.DataSource = this._movies;

			this.toolStripStatusLabel1.Text = "Gefundene Filme: " + this._current_amount.ToString();
		}
	}
}
