using System;

namespace WebSharp {
	public class Style {
		public string Name;
		public string Value;
		public override string ToString () {
			return String.Format ("{0}:{1};", Name, Value);
		}
	}
}

