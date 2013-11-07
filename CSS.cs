using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSharp {
	public class PassonStylesheet {
		protected Dictionary<Styleset, Tuple<string, HashSet<string>>> setDict;
		public PassonStylesheet () {
			setDict = new Dictionary<Styleset, Tuple<string, HashSet<string>>> ();
		}
		public string this[Styleset sset, string elementtype] {
			get {
				if (!setDict.ContainsKey(sset)) {
					setDict [sset] = new Tuple<string, HashSet<string>> (setDict.Count.ToString("X") + sset.GetHashCode().ToString("X4"), new HashSet<string>());
				}
				setDict [sset].Item2.Add (elementtype);
				return setDict [sset].Item1;
			}
		}
		public List<string> GetFormattedSheet () {
			List<string> sheet = new List<string> ();
			foreach (KeyValuePair<Styleset, Tuple<string, HashSet<string>>> KVP in setDict) {
				foreach (string type in KVP.Value.Item2) {
					sheet.Add (String.Format("{0}.{1} {{{2}}}", type, KVP.Value.Item1, KVP.Key.ToString()));
				}
			}
			return sheet;
		}
	}
	public class Styleset : Dictionary<string, string> {
		public Styleset(params Style[] styles) : base() {
			foreach(Style S in styles) {
				this [S.property] = S.value;
			}
		}
		public override int GetHashCode () {
			string t = String.Empty;
			foreach(Style S in this) {
				t += S.property;
			}
			return t.GetHashCode ();
		}
		public override bool Equals (object obj) {
			if (obj == null)
				return false;
			Styleset S = obj as Styleset;
			if (S == null)
				return false;
			if (S.Count != this.Count)
				return false;
			foreach(bool r in S.Zip(this, (a, b) => (a.Key == b.Key && a.Value == b.Value))) {
				if (!r)
					return false;
			}
			return true;
		}
		public override string ToString () {
			return String.Join ("",this.Select((k, v) => String.Format("{0}:{1};", k, v)));
		}
	}
	public class Style : IComparable {
		public string property;
		public string value;
		public Style (string property, string value) {
			this.property = property;
			this.value = value;
		}
		public override int GetHashCode () {
			return (property).GetHashCode ();
		}
		public override bool Equals (object obj) {
			if (obj == null)
				return false;
			Style S = obj as Style;
			if (S == null)
				return false;
			if (this.property == S.property && this.value == S.value)
				return true;
			return false;
		}
		public int CompareTo(object obj) {
			if (obj == null)
				return 1;
			Style S = obj as Style;
			if (S == null)
				return 1;
			int ps = this.property.CompareTo (S.property);
			if (ps == 0){
				return this.value.CompareTo (S.value);
			} else {
				return ps;
			}
		}
		public override string ToString () {
			return String.Format ("{0}:{1};", this.property, this.value);
		}
		public static implicit operator Style(KeyValuePair<string, string> KVP) {
			return new Style (KVP.Key, KVP.Value);
		}
	}
}

