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
		public static Style BackgroundColor(byte r, byte g, byte b) {return new Style ("background-color", new Hexcolor(r, g, b).ToString());}
		public static Style BackgroundColor(Hexcolor color) {return new Style ("background-color", color.ToString());}
		public static Style BackgroundColor(string hex) {return new Style ("background-color", hex.StartsWith("#") ? hex : "#" + hex);}
		public static Style BackgroundImage(string urlpath) {return new Style ("background-image", String.Format("url({0})", urlpath));}
		public static Style BackgroundPosition(string value) {return new Style ("background-position", value);}
		public static Style BackgroundRepeat(Repeat value) {return new Style ("background-position", RepeatDict[value]);}
		public static Style BackgroundRepeat() {return new Style ("background-position", "inherit");}
		//BORDER AND OUTLINE
		public static Style Border(int widthpx, Border s, Hexcolor c) {return new Style ("border", String.Format("{0}px {1} {2}", widthpx, s.ToString().ToLower(), c.ToString()));}
		public static Style BoxShadow(int xpx, int ypx, Hexcolor color, int blur = 0, int spread = 0, bool inset = false) {return new Style ("box-shadow", String.Format("{0}px {1}px{3}{4} {2}{5}", xpx, ypx, color.ToString(), blur!=0?" "+blur:String.Empty, spread!=0?" "+spread:String.Empty, inset?" inset":String.Empty));}
		//DIMENSION
		public static Style Width(int px) {return new Style ("width", px+"px");}
		public static Style Height(int px) {return new Style ("height", px+"px");}
		public static Style MinWidth(int px) {return new Style ("min-width", px+"px");}
		public static Style MinHeight(int px) {return new Style ("min-height", px+"px");}
		public static Style MaxWidth(int px) {return new Style ("max-width", px+"px");}
		public static Style MaxHeight(int px) {return new Style ("max-height", px+"px");}
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
		//FONT
		public static Style FontFamily(params string[] names) {return new Style ("font-family", String.Join(",", names));}
		public static Style FontSize(int size) {return new Style ("font-size", size+"px");}
		public static Style FontSize(string size) {return new Style ("font-size", size);}
		public static Style FontWeight(Weight weight) {return new Style ("font-weight", weight.ToString().ToLower());}
		//MARGIN
		public static Style Margin(int size) {return new Style ("margin", String.Format("{0}px", size));}
		public static Style Margin(int topbottom, int rightleft) {return new Style ("margin", String.Format("{0}px {1}px", topbottom, rightleft));}
		public static Style Margin(int top, int rightleft, int bottom) {return new Style ("margin", String.Format("{0}px {1}px {2}px", top, rightleft, bottom));}
		public static Style Margin(int top, int right, int bottom, int left) {return new Style ("margin", String.Format("{0}px {1}px {2}px {3}px", top, right, bottom, left));}
		public static Style Margin(string value) {return new Style ("margin", value);}
		public static Style MarginBottom(int px) {return new Style ("margin-bottom", px+"px");}
		public static Style MarginLeft(int px) {return new Style ("margin-left", px+"px");}
		public static Style MarginRight(int px) {return new Style ("margin-right", px+"px");}
		public static Style MarginTop(int px) {return new Style ("margin-top", px+"px");}
		public static Style MarginBottom(string value) {return new Style ("margin-bottom", value);}
		public static Style MarginLeft(string value) {return new Style ("margin-left", value);}
		public static Style MarginRight(string value) {return new Style ("margin-right", value);}
		public static Style MarginTop(string value) {return new Style ("margin-top", value);}
		//PADDING
		public static Style Padding(int size) {return new Style ("padding", String.Format("{0}px", size));}
		public static Style Padding(int topbottom, int rightleft) {return new Style ("padding", String.Format("{0}px {1}px", topbottom, rightleft));}
		public static Style Padding(int top, int rightleft, int bottom) {return new Style ("padding", String.Format("{0}px {1}px {2}px", top, rightleft, bottom));}
		public static Style Padding(int top, int right, int bottom, int left) {return new Style ("padding", String.Format("{0}px {1}px {2}px {3}px", top, right, bottom, left));}
		public static Style Padding(string value) {return new Style ("padding", value);}
		public static Style PaddingBottom(int px) {return new Style ("padding-bottom", px+"px");}
		public static Style PaddingLeft(int px) {return new Style ("padding-left", px+"px");}
		public static Style PaddingRight(int px) {return new Style ("padding-right", px+"px");}
		public static Style PaddingTop(int px) {return new Style ("padding-top", px+"px");}
		public static Style PaddingBottom(string value) {return new Style ("padding-bottom", value);}
		public static Style PaddingLeft(string value) {return new Style ("padding-left", value);}
		public static Style PaddingRight(string value) {return new Style ("padding-right", value);}
		public static Style PaddingTop(string value) {return new Style ("padding-top", value);}
		//POSITIONING
		public static Style Bottom(int px) {return new Style ("bottom", px+"px");}
		public static Style Left(int px) {return new Style ("left", px+"px");}
		public static Style Right(int px) {return new Style ("right", px+"px");}
		public static Style Top(int px) {return new Style ("top", px+"px");}
		public static Style Overflow(Overflow OS) {return new Style ("overflow", OS.ToString().ToLower());}
		public static Style Position(Position PS) {return new Style ("position", PS.ToString().ToLower());}
		//TEXT
		public static Style Color(byte r, byte g, byte b) {return new Style ("color", new Hexcolor(r, g, b).ToString());}
		public static Style Color(Hexcolor color) {return new Style ("color", color.ToString());}
		public static Style Color(string hex) {return new Style ("color", hex.StartsWith("#") ? hex : "#" + hex);}
		public static Style TextAlign(TextAlignment TA) {return new Style ("text-align", TA.ToString().ToLower());}
		public static Style TextDecoration(string value) {return new Style ("text-decoration", value);}
		//BEGIN STATIC STYLE HELPERS
		internal static Dictionary<Repeat, string> RepeatDict = new Dictionary<Repeat, string>() {{Repeat.XY, "repeat"}, {Repeat.X, "repeat-x"}, {Repeat.Y, "repeat-y"}, {Repeat.None, "no-repeat"}};
	}
	public enum Repeat { XY, X, Y, None }
	public enum Border {None, Hidden, Dotted, Dashed, Solid, Double, Groove, Ridge, Inset, Outset, Inherit}
	public enum Overflow {Visible, Hidden, Scroll, Auto, Inherit}
	public enum Position {Static, Absolute, Fixed, Relative, Inherit}
	public enum Weight {Lighter, Normal, Bold, Bolder, Inherit}
	public enum TextAlignment {Left, Center, Right, Justify, Inherit}
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
}

