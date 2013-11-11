using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSharp {
	public class PassonStylesheet {
		protected Dictionary<Styleset, string> setDict;
		protected Dictionary<AttributeStyleset, string> aDict;
		public PassonStylesheet () {
			setDict = new Dictionary<Styleset, string> ();
			aDict = new Dictionary<AttributeStyleset, string> ();
		}
		public string this[Styleset sset] {
			get {
				if (!setDict.ContainsKey(sset)) {
					setDict [sset] = "C" + setDict.Count.ToString("X");
				}
				return setDict [sset];
			}
		}
		public string this[Styleset link, Styleset visited, Styleset hover, Styleset active] {
			get {
				return this[new AttributeStyleset(link, visited, hover, active)];
			}
		}
		public string this[AttributeStyleset aset] {
			get {
				if (!aDict.ContainsKey(aset)) {
					aDict [aset] = "A" + aDict.Count.ToString("X");
				}
				return aDict [aset];
			}
		}
		public List<string> GetFormattedSheet () {
			List<string> sheet = new List<string> ();
			foreach(KeyValuePair<AttributeStyleset, string> KVP in aDict) {
				if (KVP.Key.Link != null)
					sheet.Add (String.Format("a.{0}:link {{{1}}}", KVP.Value, KVP.Key.Link.ToString()));
				if (KVP.Key.Visited != null)
					sheet.Add (String.Format("a.{0}:visited {{{1}}}", KVP.Value, KVP.Key.Visited.ToString()));
				if (KVP.Key.Hover != null)
					sheet.Add (String.Format("a.{0}:hover {{{1}}}", KVP.Value, KVP.Key.Hover.ToString()));
				if (KVP.Key.Active != null)
					sheet.Add (String.Format("a.{0}:active {{{1}}}", KVP.Value, KVP.Key.Active.ToString()));
			}
			foreach (KeyValuePair<Styleset, string> KVP in setDict) {
				sheet.Add (String.Format(".{0} {{{1}}}", KVP.Value, KVP.Key.ToString()));
			}
			return sheet;
		}
	}
	public class AttributeStyleset {
		public Styleset Link;
		public Styleset Visited;
		public Styleset Hover;
		public Styleset Active;
		public AttributeStyleset(Styleset link, Styleset visited, Styleset hover, Styleset active) {
			this.Link = link;
			this.Visited = visited;
			this.Hover = hover;
			this.Active = active;
		}
		public override int GetHashCode () {
			int hc = 0;
			if (Link != null)
				hc += Link.GetHashCode ();
			if (Visited != null)
				hc += Visited.GetHashCode ();
			if (Hover != null)
				hc += Hover.GetHashCode ();
			if (Active != null)
				hc += Active.GetHashCode ();
			return hc;
		}
		public override bool Equals (object obj) {
			if (obj == null)
				return false;
			AttributeStyleset aset = obj as AttributeStyleset;
			if (aset == null)
				return false;
			return (this.Link == aset.Link && this.Visited == aset.Visited && this.Hover == aset.Hover && this.Active == aset.Active);
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
			return String.Join ("",this.Select((k) => String.Format("{0}:{1};", k.Key, k.Value)));
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
		//BEGIN STANDARD STYLES
		public static Style BackgroundColor(byte r, byte g, byte b) {return new Style ("background-color", String.Format("#{0:X2}{1:X2}{2:X2}", r,g,b));}
		public static Style BackgroundColor(string hex) {return new Style ("background-color", hex.StartsWith("#") ? hex : "#" + hex);}
		public static Style Color(byte r, byte g, byte b) {return new Style ("color", String.Format("#{0:X2}{1:X2}{2:X2}", r,g,b));}
		public static Style Color(string hex) {return new Style ("color", hex.StartsWith("#") ? hex : "#" + hex);}
	}
}

