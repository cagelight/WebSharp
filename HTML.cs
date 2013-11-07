using System;
using System.Collections.Generic;
using System.Linq;

namespace WebSharp {
	//BEGIN INTERFACE
	public interface IElement {
		string Type { get;}
		string WOToString (PassonStylesheet PS); //Web Optimized ToString()
	}
	//END INTERFACE BEGIN ATTRIBUTESET
	public class AttributeSet {
		protected Dictionary<string, string> attributes = new Dictionary<string, string>();
		protected Styleset styleset;
		protected List<Style> additionalStyles = new List<Style>();
		public AttributeSet() {

		}
		public string GetPassonText(PassonStylesheet PS, string elementtype) {
			return (styleset != null ? String.Format(" class=\"{0}\"", PS[styleset, elementtype]) : String.Empty) + (additionalStyles.Count > 0 ? String.Format(" style=\"{0}\"", String.Join("", additionalStyles)) : String.Empty) + String.Join ("", attributes.Select((k, v) => String.Format(" {0}=\"{1}\"", k, v)));
		}
		public override string ToString () {
			return (additionalStyles.Count > 0 ? String.Format(" style=\"{0}\"", String.Join("", additionalStyles) + String.Join("", styleset.Select((k, v) => String.Format("{0}:{1};", k, v)))) : String.Empty) + String.Join ("", attributes.Select((k, v) => String.Format(" {0}=\"{1}\"", k, v)));
		}
	}
	//END ATTRIBUTESET BEGIN ABSTRACTS
	public abstract class ElementEmpty : IElement {
		protected string name;
		public AttributeSet attributes = new AttributeSet();
		public string Type { get{return name;} }
		public ElementEmpty (string name) {
			this.name = name;
		}
		public virtual string WOToString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>", this.Type, this.attributes.GetPassonText(PS, name));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>", this.Type, this.attributes.ToString());
		}
	}
	public abstract class ElementText : IElement {
		protected string name;
		public AttributeSet attributes = new AttributeSet();
		public string Text;
		public string Type { get{return name;} }
		public ElementText (string name, string text) {
			this.name = name;
			this.Text = text;
		}
		public virtual string WOToString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}<{0}>", this.Type, this.attributes.GetPassonText(PS, name), this.Text);
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}<{0}>", this.Type, this.attributes.ToString(), this.Text);
		}
	}
	public abstract class ElementContainer : List<IElement>, IElement {
		protected string name;
		public AttributeSet attributes = new AttributeSet();
		public string Type { get{return name;} }
		public ElementContainer (string name, params IElement[] contents) : base (contents) {
			this.name = name;
		}
		public virtual string WOToString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}<{0}>", this.Type, this.attributes.GetPassonText(PS, name), String.Join("", this.Select((e) => e.WOToString(PS))));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}<{0}>", this.Type, this.attributes.ToString(), String.Join("", this));
		}
	}
	//END ABSTRACTS BEGIN EMPTIES
	public class Breakline : ElementEmpty {
		public Breakline () : base ("br") {

		}
	}
	//END EMPTIES BEGIN TEXTS
	public class Span : ElementText {
		public Span (string text) : base ("span", text) {

		}
	}
	//END TEXTS BEGIN CONTAINERS
	public class Divider : ElementContainer {
		public Divider (params IElement[] contents) : base ("div", contents) {

		}
	}
	//END CONTAINERS BEGIN STRUCTURES
	//END STRUCTURES
	/*public class HTMLAttributes : Dictionary<string,string> {
		internal Styleset styleSet;
		internal List<Style> stylesAdditional = new List<Style>();
		public HTMLAttributes() : base() {}
		internal HTMLAttributes Attrib(string key, string value) {
			this [key] = value;
			return this;
		}
		public HTMLAttributes AddStyle(Style S) {
			stylesAdditional.Add (S);
			return this;
		}
		public HTMLAttributes AddStyles(params Style[] SS) {
			foreach(Style S in SS) {
				stylesAdditional.Add (S);
			}
			return this;
		}
		public HTMLAttributes SetStyleset(Styleset S) {
			this.styleSet = S;
			return this;
		}
		public HTMLAttributes Action(string value) {return Attrib ("action", value);}
		public HTMLAttributes Class(string value) {return Attrib ("class", value);}
		public HTMLAttributes ID(string value) {return Attrib ("id", value);}
		public HTMLAttributes Method(string value) {return Attrib ("method", value);}
		public HTMLAttributes Name(string value) {return Attrib ("name", value);}
		public HTMLAttributes Reference(string value) {return Attrib ("href", value);}
		public HTMLAttributes Relationship(string value) {return Attrib ("rel", value);}
		public HTMLAttributes Source(string value) {return Attrib ("src", value);}
		//public HTMLAttributes Style(string value) {return Attrib ("style", value);}
		public HTMLAttributes Title(string value) {return Attrib ("title", value);}
		public HTMLAttributes Type(string value) {return Attrib ("type", value);}
		public HTMLAttributes Value(string value) {return Attrib ("value", value);}

		internal string ToStringWebOp (string classname) {
			string rstr = String.Format(" class=\"{0}\"", classname);
			string sstr = String.Empty;
			if (this.stylesAdditional.Count > 0) {
				foreach (Style S in this.stylesAdditional) {
					sstr += S;
				}
			}
			if (sstr != String.Empty)
				sstr = String.Format (" style=\"{0}\"", sstr);
			rstr += sstr;
			foreach (KeyValuePair<string,string> KVP in this) {
				rstr += String.Format (" {0}=\"{1}\"", KVP.Key, KVP.Value);
			}
			return rstr;
		}

		public override string ToString () {
			string rstr = String.Empty;
			string sstr = String.Empty;
			if (this.styleSet != null) {
				sstr += styleSet;
			}
			if (this.stylesAdditional.Count > 0) {
				foreach (Style S in this.stylesAdditional) {
					sstr += S;
				}
			}
			if (sstr != String.Empty)
				sstr = String.Format (" style=\"{0}\"", sstr);
			rstr += sstr;
			foreach (KeyValuePair<string,string> KVP in this) {
				rstr += String.Format (" {0}=\"{1}\"", KVP.Key, KVP.Value);
			}
			return rstr;
		}
	}

	public interface IElement {
		string Name { get; }
		HTMLAttributes Attributes { get; }
		string ToStringWebOp (string classname);
	}
	public class ElementEmpty : IElement {
		private string name;
		public string Name {get{return name;}}
		private HTMLAttributes attrib;
		public HTMLAttributes Attributes {get{return attrib;}}
		public ElementEmpty(string name) {
			this.name = name;
			attrib = new HTMLAttributes ();
		}
		public string ToStringWebOp(string classname) {
			return String.Format ("<{0}{1}>", Name, Attributes.ToStringWebOp(classname));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>", Name, Attributes);
		}
	}
	public class ElementText : IElement {
		private string name;
		public string Name {get{return name;}}
		private HTMLAttributes attrib;
		public HTMLAttributes Attributes {get{return attrib;}}
		public string Content;
		public ElementText(string name, string content) {
			this.name = name;
			this.Content = content;
			attrib = new HTMLAttributes ();
		}
		public string ToStringWebOp(string classname) {
			string fcont = Content.Replace("\r\n", "<br>");
			fcont = fcont.Replace("\n", "<br>");
			return String.Format ("<{0}{1}>{2}</{0}>", Name, Attributes.ToStringWebOp(classname), fcont);
		}
		public override string ToString () {
			string fcont = Content.Replace("\r\n", "<br>");
			fcont = fcont.Replace("\n", "<br>");
			return String.Format ("<{0}{1}>{2}</{0}>", Name, Attributes, fcont);
		}
	}
	public class ElementContainer : List<IElement>, IElement {
		private string name;
		public string Name {get{return name;}}
		private HTMLAttributes attrib;
		public HTMLAttributes Attributes {get {return attrib;}}
		public ElementContainer(string name, params IElement[] elements) : base(elements) {
			this.name = name;
			attrib = new HTMLAttributes ();
		}
		public string ToStringWebOp(string classname) {
			string contents = String.Empty;
			foreach (IElement E in this) {
				contents += E;
			}
			return String.Format ("<{0}{1}>{2}</{0}>", Name, Attributes.ToStringWebOp(classname), contents);
		}
		public override string ToString () {
			string contents = String.Empty;
			foreach (IElement E in this) {
				contents += E;
			}
			return String.Format ("<{0}{1}>{2}</{0}>", Name, Attributes, contents);
		}
	}

	public class Webpage {
		public Webpage () {

		}
	}

	public static class HTML {
		public static readonly ElementEmpty Breakline = new ElementEmpty ("br");
	} */
}

