using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Toenda.MovieMatic {
	/// <summary>
	/// Class AppLoader
	/// </summary>
	public class AppLoader {
		private AppProcessLoader _loader;

		/// <summary>
		/// Default Ctor
		/// </summary>
		public AppLoader() {
		}

		/// <summary>
		/// Load a application in a new thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public void LoadAppInThread(string name) {
			this._loader = new AppProcessLoader(name);
			Thread lcwThread = new Thread(new ThreadStart(this._loader.Start));
			lcwThread.Start();
		}

		/// <summary>
		/// Load a application in a new thread.
		/// </summary>
		/// <param name="param">The parameters.</param>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public void LoadAppInThread(string name, string param) {
			this._loader = new AppProcessLoader(name, param);
			Thread lcwThread = new Thread(new ThreadStart(this._loader.Start));
			lcwThread.Start();
		}

		/// <summary>
		/// Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public void LoadApp(string name) {
			this._loader = new AppProcessLoader(name);
			this._loader.Start();
		}

		/// <summary>
		/// Load a application in the same thread.
		/// </summary>
		/// <param name="param">The parameters.</param>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public void LoadApp(string name, string param) {
			this._loader = new AppProcessLoader(name, param);
			this._loader.Start();
		}

		/// <summary>
		/// Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
		public void LoadApp(string name, bool waitForExit) {
			this._loader = new AppProcessLoader(name);
			this._loader.Start(waitForExit);
		}

		/// <summary>
		/// Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
		public void LoadApp(string name, bool waitForExit, int timeInMilliseconds) {
			this._loader = new AppProcessLoader(name);
			this._loader.Start(waitForExit, timeInMilliseconds);
		}

		/// <summary>
		/// Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="param">The parameters.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
		public void LoadApp(string name, string param, bool waitForExit, int timeInMilliseconds) {
			this._loader = new AppProcessLoader(name);
			this._loader.Start(param, waitForExit, timeInMilliseconds);
		}

		/// <summary>
		/// Private class AppProcessLoader
		/// </summary>
		private class AppProcessLoader {
			private Process _app;

			/// <summary>
			/// Default Ctor
			/// </summary>
			/// <param name="name">The name of the application</param>
			public AppProcessLoader(string name) {
				this._app = new Process();
				this._app.StartInfo.FileName = name;
			}

			/// <summary>
			/// Default Ctor
			/// </summary>
			/// <param name="name">The name of the application</param>
			public AppProcessLoader(string name, string param) {
				this._app = new Process();
				this._app.StartInfo.FileName = name;
				this._app.StartInfo.Arguments = param;
			}

			/// <summary>
			/// Start now
			/// </summary>
			public void Start() {
				bool running = this._app.Start();
			}

			/// <summary>
			/// Start now
			/// </summary>
			/// <param name="param">The parameters.</param>
			public void Start(string param) {
				this._app.StartInfo.Arguments = param;
				bool running = this._app.Start();
			}

			/// <summary>
			/// Start now
			/// </summary>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			public void Start(bool waitForExit) {
				bool running = this._app.Start();

				if(waitForExit) {
					this._app.WaitForExit();
				}
			}

			/// <summary>
			/// Start now
			/// </summary>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
			public void Start(bool waitForExit, int timeInMilliseconds) {
				bool running = this._app.Start();

				if(waitForExit) {
					if(timeInMilliseconds > 0) {
						this._app.WaitForExit();
					}
					else {
						this._app.WaitForExit(timeInMilliseconds);
					}
				}
			}

			/// <summary>
			/// Start now
			/// </summary>
			/// <param name="param">The parameters.</param>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
			public void Start(string param, bool waitForExit, int timeInMilliseconds) {
				this._app.StartInfo.Arguments = param;
				bool running = this._app.Start();

				if(waitForExit) {
					if(timeInMilliseconds > 0) {
						this._app.WaitForExit();
					}
					else {
						this._app.WaitForExit(timeInMilliseconds);
					}
				}
			}
		}
	}
}
