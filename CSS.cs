using System;
using System.Collections.Generic;

namespace WebSharp {
	public struct Style{
		public string Key;
		public string Value;
		public Style(string Key, string Value) {
			this.Key = Key;
			this.Value = Value;
		}
		public static explicit operator KeyValuePair<string,string>(Style S) {
			return new KeyValuePair<string, string> (S.Key, S.Value);
		}
		public static implicit operator Style(KeyValuePair<string,string> S) {
			return new Style (S.Key, S.Value);
		}
		public override string ToString () {
			return String.Format ("{0}:{1};", Key, Value);
		}
	}

	public class Styleset : Dictionary<string, string> {
		public string Name;
		public Styleset(string name, params Style[] styles) : base() {
			Name = name;
			foreach (Style S in styles) {
				this [S.Key] = S.Value;
			}
		}
		public override string ToString () {
			string rstr = String.Empty;
			foreach(Style S in this) {
				rstr += S;
			}
			return rstr;
		}
	}

	public struct Styleclass {
		public string Tag, Class, State;
		public Styleclass (string Tag, string Class = "", string State = "") {
			this.Tag = Tag;
			this.Class = Class;
			this.State = State;
		}
		public override string ToString () {
			return String.Format ("{0}{1}{2}", Tag, Class==""?"":"."+Class, State==""?"":":"+State);
		}
	}

	public class Stylesheet : Dictionary<Styleclass,Styleset> {
		public Stylesheet() : base() {

		}

		public override string ToString () {
			string rstr = String.Empty;
			foreach(KeyValuePair<Styleclass, Styleset> KVP in this) {
			}
			return rstr;
		}
	}
}

