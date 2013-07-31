using System;
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
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Interaction logic for MovieEditor.xaml
	/// </summary>
	public partial class MovieEditor : NavigationWindow {
		//private bool _neverRendered = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="MovieEditor"/> class.
		/// </summary>
		public MovieEditor() {
			InitializeComponent();

			this.Navigate(new ME_Page());
			//this.AddChild(new ME_Page());

			//this.CanGoBack = true;
			//this.CanGoForward = true;
		}

		/// <summary>
		/// Handles the SourceInitialized event of the NavigationWindow control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void NavigationWindow_SourceInitialized(object sender, EventArgs e) {
			//GlassHelper.ExtendGlassFrame(this, new Thickness(0, 35, 0, 0));
		}

		///// <summary>
		///// Raises the <see cref="E:System.Windows.Window.ContentRendered"/> event.
		///// </summary>
		///// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		//protected override void OnContentRendered(EventArgs e) {
		//    if(this._neverRendered) {
		//        // The window takes the size of its content because SizeToContent
		//        // is set to WidthAndHeight in the markup. We then allow
		//        // it to be set by the user, and have the content take the size
		//        // of the window.

		//        this.SizeToContent = SizeToContent.Manual;

		//        FrameworkElement root = this.Content as FrameworkElement;

		//        if(root != null) {
		//            root.Width = double.NaN;
		//            root.Height = double.NaN;
		//        }

		//        this._neverRendered = false;
		//    }

		//    base.OnContentRendered(e);
		//}
	}
}
