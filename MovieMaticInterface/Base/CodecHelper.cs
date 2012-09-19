using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Toenda.Foundation;
using Toenda.MovieMaticInterface.Bean;

namespace Toenda.MovieMaticInterface.Base {
	public class CodecHelper {
		/// <summary>
		/// Get the number of a codec by it's string name
		/// </summary>
		/// <param name="codec"></param>
		/// <returns></returns>
		public static int GetCodecNumber(string codec) {
			return (int)CodecHelper.GetCodecByString(codec);
		}

		/// <summary>
		/// Get a codec ba a string name
		/// </summary>
		/// <param name="codec"></param>
		/// <returns></returns>
		public static Codec GetCodecByString(string codec) {
			return codec.ToEnum<Codec>();
		}
	}
}
