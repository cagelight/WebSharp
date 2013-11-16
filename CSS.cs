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
		//BACKGROUND
		public static Style BackgroundColor(Hexcolor color) {return new Style ("background-color", color.ToString());}
		public static Style BackgroundColor(RGBA color) {return new Style ("background-color", color.ToString());}
		public static Style BackgroundColor(string hex) {return new Style ("background-color", hex.StartsWith("#") ? hex : "#" + hex);}
		public static Style BackgroundImage(string urlpath) {return new Style ("background-image", String.Format("url({0})", urlpath));}
		public static Style BackgroundPosition(string value) {return new Style ("background-position", value);}
		public static Style BackgroundRepeat(Repeat value) {return new Style ("background-position", RepeatDict[value]);}
		public static Style BackgroundRepeat() {return new Style ("background-position", "inherit");}
		//BORDER AND OUTLINE
		public static Style BoxShadow(int xpx, int ypx, Hexcolor color, int blur = 0, int spread = 0, bool inset = false) {return new Style ("box-shadow", String.Format("{0} {1}{3}{4} {2}{5}", xpx, ypx, color.ToString(), blur!=0?" "+blur:String.Empty, spread!=0?" "+spread:String.Empty, inset?" inset":String.Empty));}
		public static Style BoxShadow(int xpx, int ypx, RGBA color, int blur = 0, int spread = 0, bool inset = false) {return new Style ("box-shadow", String.Format("{0} {1}{3}{4} {2}{5}", xpx, ypx, color.ToString(), blur!=0?" "+blur:String.Empty, spread!=0?" "+spread:String.Empty, inset?" inset":String.Empty));}
		//DIMENSION
		public static Style Width(string value) {return new Style ("width", value);}
		public static Style Height(string value) {return new Style ("height", value);}
		public static Style MinWidth(string value) {return new Style ("min-width", value);}
		public static Style MinHeight(string value) {return new Style ("min-height", value);}
		public static Style MaxWidth(string value) {return new Style ("max-width", value);}
		public static Style MaxHeight(string value) {return new Style ("max-height", value);}
		public static Style Width() {return new Style ("width", "inherit");}
		public static Style Height() {return new Style ("height", "inherit");}
		public static Style MinWidth() {return new Style ("min-width", "inherit");}
		public static Style MinHeight() {return new Style ("min-height", "inherit");}
		public static Style MaxWidth() {return new Style ("max-width", "inherit");}
		public static Style MaxHeight() {return new Style ("max-height", "inherit");}
		//TEXT
		public static Style Color(Hexcolor color) {return new Style ("color", color.ToString());}
		public static Style Color(RGBA color) {return new Style ("color", color.ToString());}
		public static Style Color(string hex) {return new Style ("color", hex.StartsWith("#") ? hex : "#" + hex);}
		//BEGIN STATIC STYLE HELPERS
		public enum Repeat { XY, X, Y, None }
		internal static Dictionary<Repeat, string> RepeatDict = new Dictionary<Repeat, string>() {{Repeat.XY, "repeat"}, {Repeat.X, "repeat-x"}, {Repeat.Y, "repeat-y"}, {Repeat.None, "no-repeat"}};
		public struct Hexcolor {
			public byte R;
			public byte G;
			public byte B;
			public Hexcolor(byte r, byte g, byte b) {
				R = r; G = g; B = b;
			}
			public override int GetHashCode () {
				return R + G + B;
			}
			public override bool Equals (object obj) {
				if (obj == null || obj.GetType() != typeof(Hexcolor))
					return false;
				Hexcolor H = (Hexcolor)obj;
				return (this.R == H.R && this.G == H.G && this.B == H.B);
			}
			public override string ToString () {
				return String.Format ("#{0:X2}{1:X2}{2:X2}", R, G, B);
			}
			public static Hexcolor Black = new Hexcolor(0, 0, 0);
		}
		public struct RGBA {
			public byte R;
			public byte G;
			public byte B;
			public float A;
			public RGBA(byte r, byte g, byte b, float a) {
				R = r; G = g; B = b; A = a;
			}
			public override int GetHashCode () {
				return (R + G + B) + A.GetHashCode ();
			}
			public override bool Equals (object obj) {
				if (obj == null || obj.GetType() != typeof(RGBA))
					return false;
				RGBA H = (RGBA)obj;
				return (this.R == H.R && this.G == H.G && this.B == H.B && this.A == H.A);
			}
			public override string ToString () {
				return String.Format ("rbga({0},{1},{2},{3})", R, G, B, A);
			}
			public static Hexcolor Black = new Hexcolor(0, 0, 0);
		}
	}
}

