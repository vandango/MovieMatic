using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

using Toenda.Foundation;
using Toenda.Foundation.Data;
using Toenda.Foundation.Windows.WpfControls;
using Toenda.Foundation.Types;

using Toenda.MovieMaticInterface.Bean;
using Toenda.MovieMaticInterface.Base;
using Toenda.MovieMaticInterface.Import;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private bool _neverRendered = true;

		private DataHandler _db = new DataHandler(
			Configuration.Current.ConnectionString
		);
		private StaticHandler _sb = new StaticHandler(
			Configuration.Current.ConnectionString
		);

		private List<Movie> _movies;
		private int _current_amount;
		private DataSortDirection _sortDirection = DataSortDirection.Ascending;
		private string _sortExpression = "sort_value";
		private FilterType _searchFilter = FilterType.NoFilter;
		private string _searchValue = "";

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			//this.btnEdit.Focusable = false;

			this.LoadData(FilterType.NoFilter);
		}

		#region "Events"

		/// <summary>
		/// Handles the SourceInitialized event of the MainWindow control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void MainWindow_SourceInitialized(object sender, EventArgs e) {
			GlassHelper.ExtendGlassFrame(this, new Thickness(0, 35, 0, 0));
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Window.ContentRendered"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnContentRendered(EventArgs e) {
			if(this._neverRendered) {
				// The window takes the size of its content because SizeToContent
				// is set to WidthAndHeight in the markup. We then allow
				// it to be set by the user, and have the content take the size
				// of the window.

				this.SizeToContent = SizeToContent.Manual;

				FrameworkElement root = this.Content as FrameworkElement;

				if(root != null) {
					root.Width = double.NaN;
					root.Height = double.NaN;
				}

				this._neverRendered = false;
			}

			base.OnContentRendered(e);
		}

		/// <summary>
		/// Handles the TextChanged event of the TextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
		private void txtSearch_TextChanged(object sender, TextChangedEventArgs e) {
			this.UpdateSearchTextBox(false, false);
		}

		/// <summary>
		/// Handles the LostFocus event of the txtSearch control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void txtSearch_LostFocus(object sender, RoutedEventArgs e) {
			if(this.txtSearch.Text.IsNullOrTrimmedEmpty()) {
				this.txtSearch.Text = "Suchen";
			}

			this.UpdateSearchTextBox(false, false);
		}

		/// <summary>
		/// Handles the GotFocus event of the txtSearch control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void txtSearch_GotFocus(object sender, RoutedEventArgs e) {
			if(!this.txtSearch.Text.IsNullOrTrimmedEmpty()
			&& this.txtSearch.Text == "Suchen") {
				this.txtSearch.Text = "";
			}

			this.UpdateSearchTextBox(false, false);
		}

		/// <summary>
		/// Handles the MouseLeave event of the txtSearch control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		void txtSearch_MouseLeave(object sender, MouseEventArgs e) {
			this.UpdateSearchTextBox(true, true);
		}

		/// <summary>
		/// Handles the MouseEnter event of the txtSearch control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		void txtSearch_MouseEnter(object sender, MouseEventArgs e) {
			this.UpdateSearchTextBox(false, true);
		}

		/// <summary>
		/// Updates the search text box.
		/// </summary>
		/// <param name="lostFocus">if set to <c>true</c> [lost focus].</param>
		/// <param name="onlyMouseOver">if set to <c>true</c> [only mouse over].</param>
		private void UpdateSearchTextBox(bool lostFocus, bool onlyMouseOver) {
			if(this.txtSearch.Text == "Suchen") {
				if(onlyMouseOver && !lostFocus) {
					BrushConverter convert = new BrushConverter();
					Brush borderBrush = (Brush)convert.ConvertFromString("#FF3f3f3f");
					Brush fgBrush = (Brush)convert.ConvertFromString("#FF575757");

					this.txtSearch.BorderBrush = borderBrush;

					this.txtSearch.Background = Brushes.White;
					this.txtSearch.Foreground = fgBrush;

					this.txtSearch.FontStyle = FontStyles.Italic;
				}
				else {
					BrushConverter convert = new BrushConverter();
					Brush borderBrush = (Brush)convert.ConvertFromString("#FF3f3f3f");
					Brush bgBrush = (Brush)convert.ConvertFromString("#FFe8e8e8");
					Brush fgBrush = (Brush)convert.ConvertFromString("#FF575757");

					this.txtSearch.BorderBrush = borderBrush;

					this.txtSearch.Background = bgBrush;
					this.txtSearch.Foreground = fgBrush;

					this.txtSearch.FontStyle = FontStyles.Italic;
				}
			}
			else {
				BrushConverter convert = new BrushConverter();
				Brush borderBrush = (Brush)convert.ConvertFromString("#FF3f3f3f");

				this.txtSearch.BorderBrush = borderBrush;

				this.txtSearch.Background = Brushes.White;
				this.txtSearch.Foreground = Brushes.Black;

				this.txtSearch.FontStyle = FontStyles.Normal;
			}
		}

		/// <summary>
		/// Handles the Click event of the btnOrganize control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnOrganize_Click(object sender, RoutedEventArgs e) {
			this.OpenContextMenu();
		}

		/// <summary>
		/// Handles the MouseRightButtonUp event of the btnOrganize control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void btnOrganize_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
			this.OpenContextMenu();
		}

		/// <summary>
		/// Handles the Click event of the subExit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void subExit_Click(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		/// <summary>
		/// Handles the Click event of the mnuNewMovie control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void mnuNewMovie_Click(object sender, RoutedEventArgs e) {
			MovieEditor me = new MovieEditor();
			Nullable<bool> result = me.ShowDialog();

			if(result.HasValue && result.Value) {
			}
		}

		#endregion

		#region "Methods"

		/// <summary>
		/// Opens the context menu.
		/// </summary>
		private void OpenContextMenu() {
			if(this.btnOrganize.ContextMenu == null
			|| this.btnOrganize.ContextMenu.HasItems == false) {
				return;
			}

			this.btnOrganize.ContextMenu.PlacementTarget = this.btnOrganize;
			this.btnOrganize.ContextMenu.Placement = PlacementMode.Bottom;
			this.btnOrganize.ContextMenu.StaysOpen = false;
			this.btnOrganize.ContextMenu.IsOpen = !this.btnOrganize.ContextMenu.IsOpen;
		}

		/// <summary>
		/// Loads the data
		/// </summary>
		/// <param name="filter">The filter.</param>
		private void LoadData(FilterType filter) {
			this._searchFilter = filter;
			this._searchValue = "";

			this.LoadData(this._searchFilter, this._searchValue);
		}

		/// <summary>
		/// Loads the data
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="value">The value.</param>
		private void LoadData(FilterType filter, string value) {
			//if(this.dgvMovies.InvokeRequired) {
			//    LoadDataCallback ldCall = new LoadDataCallback(_LoadData);
			//    this.Invoke(ldCall, new object[] { filter, value });
			//}
			//else {
			//this.Cursor = Cursors.WaitCursor;

			//Stopwatch watch = new Stopwatch();

			//watch.Start();

			//if(__useDataBind) {
			//    this.dgvMovies.DataSource = null;
			//}
			//else {
			//    this.dgvMovies.Rows.Clear();
			//}

			this._movies = new List<Movie>();

			// load the list of movies from the database
			//Stopwatch watch2 = new Stopwatch();

			//watch2.Start();

			switch(filter) {
				case FilterType.NoFilter:
				case FilterType.ResetFilter:
					this._movies = this._db.GetMovieList(
						this._sortExpression,
						this._sortDirection
					);
					break;

				case FilterType.AllConferred:
				case FilterType.AllOriginals:
				case FilterType.WithoutGenre:
					this._movies = this._db.GetMovieList(
						filter,
						this._sortExpression,
						this._sortDirection
					);
					break;

				case FilterType.Codec:
					this._movies = this._db.GetMovieList(
						filter,
						"",
						Configuration.Current.GetCodecNumber(value),
						false,
						this._sortExpression,
						this._sortDirection
					);
					break;

				case FilterType.Genre:
				case FilterType.Category:
				case FilterType.Name:
				case FilterType.Actor:
				case FilterType.Director:
				case FilterType.Producer:
				default:
					this._movies = this._db.GetMovieList(
						filter,
						value,
						this._sortExpression,
						this._sortDirection
					);
					break;
			}

			//watch2.Stop();
			//TimeSpan span2 = watch2.Elapsed;

			//Console.WriteLine(
			//    "_LoadData [only GetMovieList] - Required time: {0} - {1}ms ({2} Ticks)",
			//    span2.ToString(),
			//    watch2.ElapsedMilliseconds,
			//    watch2.ElapsedTicks
			//);

			// bind list to the grid
			//Stopwatch watch3 = new Stopwatch();

			//watch3.Start();

			//this._movies.RemoveRange(50, this._movies.Count - 50);

			this._current_amount = this._movies.Count;
			//this.tslblAmount.Text = "Anzahl Filme: " + this._current_amount.ToString();

			//this.dgMovies.ItemsSource = this._movies;

			//Binding b = new Binding();
			//b.BindsDirectlyToSource = true;
			//b.Mode = BindingMode.OneWay;
			//b.Source = this._movies;

			//BindingGroup bg = new BindingGroup(

			//this.dgMovies.BindingGroup.IsSealed = true;
			//new BindingGroup();

			//this.dgMovies.DataContext = this._movies;

			//ICollectionView view = CollectionViewSource.GetDefaultView(DataGrid_Standard.ItemsSource);
			//iecv = (IEditableCollectionView)view;
			//iecv.NewItemPlaceholderPosition = NewItemPlaceholderPosition.None; 

			//this.dgvMovies.DataSource = this._movies;
			this.dgMovies.ItemsSource = this._movies;
			//this.dgMovies.DataContext

			//if(__useDataBind) {
			//    this.dgvMovies.AutoGenerateColumns = false;
			//    this.dgvMovies.DataSource = this._movies;
			//}
			//else {
			//    int count = 0;

			//    foreach(Movie mov in this._movies) {
			//        this.dgvMovies.Rows.Add(1);

			//        //tmp = mov.Quality.ToString();
			//        //qualiText = tmp.Substring(0, mov.Quality.ToString().IndexOf("_"));

			//        //int.TryParse(tmp.Substring(tmp.LastIndexOf("_") + 1), out quali);

			//        //Static stc = this._sb.GetStaticItem("C002", mov.Country);

			//        this.dgvMovies.Rows[count].Cells[0].Value = mov.ID;
			//        this.dgvMovies.Rows[count].Cells[1].Value = mov.SortValue;
			//        this.dgvMovies.Rows[count].Cells[2].Value = mov.Number;
			//        this.dgvMovies.Rows[count].Cells[3].Value = mov.Name;
			//        this.dgvMovies.Rows[count].Cells[4].Value = mov.DiscAmount.ToString();
			//        this.dgvMovies.Rows[count].Cells[5].Value = mov.IsOriginal;
			//        this.dgvMovies.Rows[count].Cells[6].Value = mov.Codec.ToString();
			//        this.dgvMovies.Rows[count].Cells[7].Value = mov.GenresString;
			//        this.dgvMovies.Rows[count].Cells[8].Value = mov.CategoriesString;
			//        this.dgvMovies.Rows[count].Cells[9].Value = mov.QualityString;//"(" + quali.ToString() + ") " + qualiText;
			//        this.dgvMovies.Rows[count].Cells[10].Value = mov.CountryString;
			//        this.dgvMovies.Rows[count].Cells[11].Value = mov.IsConferred;
			//        this.dgvMovies.Rows[count].Cells[12].Value = mov.ConferredTo;

			//        count++;
			//    }

			//    this.dgvMovies.FirstDisplayedScrollingRowIndex = this._current_amount - 1;
			//    this.dgvMovies.Rows[this._current_amount - 1].Selected = true;
			//}

			//watch.Stop();

			//TimeSpan span = watch.Elapsed;

			//watch3.Stop();
			//TimeSpan span3 = watch3.Elapsed;

			//Console.WriteLine(
			//    "_LoadData [only DataBind] - Required time: {0} - {1}ms ({2} Ticks)",
			//    span3.ToString(),
			//    watch3.ElapsedMilliseconds,
			//    watch3.ElapsedTicks
			//);

			//this.tslblAmount.Text += string.Format(
			//    " - Required time: {0} - {1}ms ({2} Ticks)",
			//    span.ToString(),
			//    watch.ElapsedMilliseconds,
			//    watch.ElapsedTicks
			//);

			//this.Cursor = Cursors.Default;
			//}
		}

		#endregion
	}
}
