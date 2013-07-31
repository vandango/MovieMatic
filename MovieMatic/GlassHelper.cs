using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Struct MARGINS
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct MARGINS {
		public int cxLeftWidth;
		public int cxRightWidth;
		public int cyTopHeight;
		public int cyBottomHeight;
	}

	/// <summary>
	/// Class GlassHelper
	/// </summary>
	public class GlassHelper {
		[DllImport("dwmapi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(
		   IntPtr hWnd,
		   ref MARGINS pMarInset
		);
	}
}
