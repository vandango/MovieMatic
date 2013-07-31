using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;

namespace Toenda.MovieMatic2 {
	/// <summary>
	/// Desktop Windows Manager APIs
	/// </summary>
	internal class DwmApi {
		/// <summary>
		/// DWMs the enable blur behind window.
		/// </summary>
		/// <param name="hWnd">The h WND.</param>
		/// <param name="pBlurBehind">The p blur behind.</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

		/// <summary>
		/// DWMs the extend frame into client area.
		/// </summary>
		/// <param name="hWnd">The h WND.</param>
		/// <param name="pMargins">The p margins.</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

		/// <summary>
		/// DWMs the is composition enabled.
		/// </summary>
		/// <returns></returns>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		/// <summary>
		/// DWMs the color of the get colorization.
		/// </summary>
		/// <param name="pcrColorization">The PCR colorization.</param>
		/// <param name="pfOpaqueBlend">if set to <c>true</c> [pf opaque blend].</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmGetColorizationColor(
			out int pcrColorization,
			[MarshalAs(UnmanagedType.Bool)]out bool pfOpaqueBlend);

		/// <summary>
		/// DWMs the enable composition.
		/// </summary>
		/// <param name="bEnable">if set to <c>true</c> [b enable].</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmEnableComposition(bool bEnable);

		/// <summary>
		/// DWMs the register thumbnail.
		/// </summary>
		/// <param name="dest">The dest.</param>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern IntPtr DwmRegisterThumbnail(IntPtr dest, IntPtr source);

		/// <summary>
		/// DWMs the unregister thumbnail.
		/// </summary>
		/// <param name="hThumbnail">The h thumbnail.</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

		/// <summary>
		/// DWMs the update thumbnail properties.
		/// </summary>
		/// <param name="hThumbnail">The h thumbnail.</param>
		/// <param name="props">The props.</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmUpdateThumbnailProperties(IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

		/// <summary>
		/// DWMs the size of the query thumbnail source.
		/// </summary>
		/// <param name="hThumbnail">The h thumbnail.</param>
		/// <param name="size">The size.</param>
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

		/// <summary>
		/// 
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public class DWM_THUMBNAIL_PROPERTIES {
			public uint dwFlags;
			public RECT rcDestination;
			public RECT rcSource;
			public byte opacity;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fVisible;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fSourceClientAreaOnly;

			public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
			public const uint DWM_TNP_RECTSOURCE = 0x00000002;
			public const uint DWM_TNP_OPACITY = 0x00000004;
			public const uint DWM_TNP_VISIBLE = 0x00000008;
			public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
		}

		/// <summary>
		/// 
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public class MARGINS {
			public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;

			public MARGINS(int left, int top, int right, int bottom) {
				cxLeftWidth = left;
				cyTopHeight = top;
				cxRightWidth = right;
				cyBottomHeight = bottom;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public class DWM_BLURBEHIND {
			public uint dwFlags;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fEnable;
			public IntPtr hRegionBlur;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fTransitionOnMaximized;

			public const uint DWM_BB_ENABLE = 0x00000001;
			public const uint DWM_BB_BLURREGION = 0x00000002;
			public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT {
			public int left, top, right, bottom;

			public RECT(int left, int top, int right, int bottom) {
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}
	}
}
