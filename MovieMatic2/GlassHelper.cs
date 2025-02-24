﻿using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class GlassHelper
	/// </summary>
	public class GlassHelper {
		struct MARGINS {
			/// <summary>
			/// Initializes a new instance of the <see cref="MARGINS"/> struct.
			/// </summary>
			/// <param name="t">The t.</param>
			public MARGINS(Thickness t) {
				Left = (int)t.Left;
				Right = (int)t.Right;
				Top = (int)t.Top;
				Bottom = (int)t.Bottom;
			}

			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern bool DwmIsCompositionEnabled();

		/// <summary>
		/// Extends the glass frame.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <param name="margin">The margin.</param>
		/// <returns></returns>
		public static bool ExtendGlassFrame(Window window, Thickness margin) {
			if(!DwmIsCompositionEnabled()) {
				return false;
			}

			IntPtr hwnd = new WindowInteropHelper(window).Handle;

			if(hwnd == IntPtr.Zero) {
				throw new InvalidOperationException("The Window must be shown before extending glass.");
			}

			// Set the background to transparent from both the WPF and Win32 perspectives
			SolidColorBrush background = new SolidColorBrush(Colors.Red);

			background.Opacity = 0.5;

			window.Background = Brushes.Transparent;

			HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

			MARGINS margins = new MARGINS(margin);

			DwmExtendFrameIntoClientArea(hwnd, ref margins);

			return true;
		}
	}
}
