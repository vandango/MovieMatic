using System;
using System.Collections.Generic;
using System.Text;

namespace Toenda.MovieMaticInterface.Bean {
	/// <summary>
	/// Class Static
	/// </summary>
	public class Static {
		private string m_strTag;
		private string m_strType;
		private string m_strContent;
		private string m_strValue;
		private int m_iLength;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Static() {
		}

		/// <summary>
		/// Get or set the tag
		/// </summary>
		public string Tag {
			get { return m_strTag; }
			set { m_strTag = value; }
		}

		/// <summary>
		/// Get or set the type
		/// </summary>
		public string Type {
			get { return m_strType; }
			set { m_strType = value; }
		}

		/// <summary>
		/// Get or set the Content
		/// </summary>
		public string Content {
			get { return m_strContent; }
			set { m_strContent = value; }
		}

		/// <summary>
		/// Get or set the Value
		/// </summary>
		public string Value {
			get { return m_strValue; }
			set { m_strValue = value; }
		}

		/// <summary>
		/// Get or set the Length
		/// </summary>
		public int Length {
			get { return m_iLength; }
			set { m_iLength = value; }
		}
	}
}
