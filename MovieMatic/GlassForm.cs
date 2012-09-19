using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Toenda.MovieMatic {
	public partial class GlassForm : Form {
		/* Struktur, die bestimmt, um wie viele Pixel der Rahmen 
		zu jeder Seite verbreitert wird */
		[StructLayout(LayoutKind.Sequential)]
		public struct MARGINS {
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}

		/* Verbreitert den Vista-Rahmen nach innen */
		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd,
		   ref MARGINS margins);

		/* Überprüft, ob die Desktopgestaltung aktiviert ist */
		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern bool DwmIsCompositionEnabled();

		/* Panel für den Inhalt */
		protected Panel contentPanel;

		/* Die Hintergrundfarbe */
		public override Color BackColor {
			get {
				return this.contentPanel.BackColor;
			}
			set {
				this.contentPanel.BackColor = value;
			}
		}

		/* Überprüft, ob ein Glass-Rahmen möglich ist */
		public bool IsGlassFrameEnabled {
			get {
				try {
					return DwmIsCompositionEnabled();
				}
				catch(DllNotFoundException) {
					// Die DLL dwmapi.dll ist nicht verfügbar. 
					// Wahrscheinlich läuft die Anwendung unter
					// einer älteren Windows-Version
					return false;
				}
			}
		}

		/* Konstruktor */
		public GlassForm() {
			// Panel erzeugen und den Steuerelementen hinzufügen
			this.contentPanel = new Panel();
			this.contentPanel.Left = 0;
			this.contentPanel.Top = 0;
			this.contentPanel.Width = this.ClientRectangle.Width;
			this.contentPanel.Height = this.ClientRectangle.Height;
			this.contentPanel.Anchor = AnchorStyles.Left |
			   AnchorStyles.Top | AnchorStyles.Right |
			   AnchorStyles.Bottom;
			this.contentPanel.BackColor = System.Drawing.Color.Transparent;
			//this.contentPanel.BackColor = base.BackColor;
			this.SuspendLayout();
			this.Controls.Add(this.contentPanel);
			this.ResumeLayout(false);
		}

		/* Platziert das Panel neu, wenn die Padding-Eigenschaft geändert wird */
		protected override void OnPaddingChanged(EventArgs e) {
			base.OnPaddingChanged(e);

			// Die Position des Panels neu berechnen
			this.contentPanel.Left = this.Padding.Left;
			this.contentPanel.Top = this.Padding.Top;
			this.contentPanel.Width = this.ClientRectangle.Width -
			   this.Padding.Left - this.Padding.Right;
			this.contentPanel.Height = this.ClientRectangle.Height -
			   this.Padding.Top - this.Padding.Bottom;

			// Den Rahmen neu definieren
			this.ExtendFrame();
		}

		/* Verbreitert den Rahmen des Fensters entsprechend den Einstellungen 
		 * 
		   in Padding, wenn das Formular geladen wird */
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			this.ExtendFrame();
		}

		/* Zeichnet den Hintergrund schwarz um den Glass-Effekt zu ermöglichen */
		protected override void OnPaintBackground(PaintEventArgs e) {
			base.OnPaintBackground(e);

			if(this.IsGlassFrameEnabled) {
				// Die Hintergrundfarbe auf Schwarz setzen um den
				// Glass-Effekt zu ermöglichen
				//e.Graphics.Clear(Color.Black);
				//e.Graphics.Clear(Color.Black);
			}
		}

		/* Wird überschrieben um auf den Wechsel des Vista-Themas zu reagieren */
		protected override void WndProc(ref Message m) {
			const int DWMCOMPOSITIONCHANGED = 0x031E;
			if(m.Msg == DWMCOMPOSITIONCHANGED) {
				// Den Rahmen neu definieren
				this.ExtendFrame();
				this.Invalidate();
			}
			base.WndProc(ref m);
		}

		/* Verbreitert den Rahmen des Fensters entsprechend 
		   den Einstellungen in Padding */
		private void ExtendFrame() {
			if(this.IsGlassFrameEnabled) {
				if(this.DesignMode == false) {
					// Wenn nicht im Design-Modus: den Rahmen verbreitern
					MARGINS margins = new MARGINS() {
						Left = this.Padding.Left,
						Top = this.Padding.Top,
						Right = this.Padding.Right,
						Bottom = this.Padding.Bottom
					};
					DwmExtendFrameIntoClientArea(this.Handle, ref margins);
				}
			}
		}
	}
}
