using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSharp {
	//BEGIN INTERFACE
	public interface IElement {
		string ElementName { get;}
		string ToWebOptimizedString (PassonStylesheet PS); //Web Optimized ToString()
	}
	//END INTERFACE BEGIN ATTRIBUTESET
	public class AttributeSet {
		public enum InputType {Text, Password, Radio, Checkbox, Submit, File}
		public enum FormMethod {GET, POST}
		protected Dictionary<string, string> attributes = new Dictionary<string, string>();
		public Styleset Styleset;
		public List<Style> Styles = new List<Style>();
		public AttributeSet() {

		}
		internal AttributeSet Attrib(string key, string value) {
			attributes [key] = value;
			return this;
		}
		public AttributeSet Accept(string value) {return Attrib ("accept", value);}
		public AttributeSet Action(string value) {return Attrib ("action", value);}
		public AttributeSet Alt(string value) {return Attrib ("alt", value);}
		public AttributeSet Enctype(MIME M) {return Attrib ("enctype", M.ToString());}
		public AttributeSet ID(string value) {return Attrib ("id", value);}
		public AttributeSet Method(FormMethod value) {return Attrib ("method", value.ToString().ToLower());}
		public AttributeSet Name(string value) {return Attrib ("name", value);}
		public AttributeSet Reference(string value) {return Attrib ("href", value);}
		public AttributeSet Relationship(string value) {return Attrib ("rel", value);}
		public AttributeSet Source(string value) {return Attrib ("src", value);}
		public AttributeSet Title(string value) {return Attrib ("title", value);}
		public AttributeSet Type(InputType value) {return Attrib ("type", value.ToString().ToLower());}
		public AttributeSet Value(string value) {return Attrib ("value", value);}
		public virtual string GetPassonText(PassonStylesheet PS) {
			return (Styleset != null ? String.Format(" class=\"{0}\"", String.Join("", PS[Styleset])) : String.Empty) + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
			//return (Styleset != null ? String.Format(" class=\"{0}\"", PS[Styleset, elementtype]) : String.Empty) + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", String.Join("", Styles)) : String.Empty) + String.Join ("", attributes.Select((k, v) => String.Format(" {0}=\"{1}\"", k, v)));
		}
		public override string ToString () {
			return (Styles.Count > 0 || Styleset != null ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty) + (Styleset != null ? String.Join("", Styleset.Select((k) => String.Format("{0}:{1};", k.Key, k.Value))) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
		}
	}

	public class AttributeSetA {
		protected Dictionary<string, string> attributes = new Dictionary<string, string>();
		public AttributeStyleset Styleset = new AttributeStyleset(null, null, null, null);
		public Styleset Link {get {return Styleset.Link;} set {Styleset.Link = value;}}
		public Styleset Visited {get {return Styleset.Visited;} set {Styleset.Visited = value;}}
		public Styleset Hover {get {return Styleset.Hover;} set {Styleset.Hover = value;}}
		public Styleset Active {get {return Styleset.Active;} set {Styleset.Active = value;}}
		public List<Style> Styles = new List<Style>();
		public AttributeSetA () {}
		internal AttributeSetA Attrib(string key, string value) {
			attributes [key] = value;
			return this;
		}
		public AttributeSetA ID(string value) {return Attrib ("id", value);}
		public AttributeSetA Reference(string value) {return Attrib ("href", value);}
		public AttributeSetA Title(string value) {return Attrib ("title", value);}
		public virtual string GetPassonText(PassonStylesheet PS) {
			return ((Link != null || Visited != null || Hover != null || Active != null) ? String.Format(" class=\"{0}\"", String.Join("", PS[Link, Visited, Hover, Active])) : String.Empty)
				 + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
			//return (Styleset != null ? String.Format(" class=\"{0}\"", PS[Styleset, elementtype]) : String.Empty) + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", String.Join("", Styles)) : String.Empty) + String.Join ("", attributes.Select((k, v) => String.Format(" {0}=\"{1}\"", k, v)));
		}
		public override string ToString () {
			return (Styles.Count > 0 || Link != null ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty) + (Link != null ? String.Join("", Link.Select((k) => String.Format("{0}:{1};", k.Key, k.Value))) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
		}
	}
	//END ATTRIBUTESET BEGIN ABSTRACTS
	public class ElementEmpty : IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public ElementEmpty Accept(string value) {this.Attributes.Accept (value); return this;}
		public ElementEmpty Action(string value) {this.Attributes.Action (value); return this;}
		public ElementEmpty Alt(string value) {this.Attributes.Alt (value); return this;}
		public ElementEmpty Enctype(MIME value) {this.Attributes.Enctype (value); return this;}
		public ElementEmpty ID(string value) {this.Attributes.ID (value); return this;}
		public ElementEmpty Method(AttributeSet.FormMethod value) {this.Attributes.Method (value); return this;}
		public ElementEmpty Name(string value) {this.Attributes.Name (value); return this;}
		public ElementEmpty Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementEmpty Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementEmpty Source(string value) {this.Attributes.Source (value); return this;}
		public ElementEmpty Title(string value) {this.Attributes.Title (value); return this;}
		public ElementEmpty Type(AttributeSet.InputType value) {this.Attributes.Type (value); return this;}
		public ElementEmpty Value(string value) {this.Attributes.Value (value); return this;}
		public ElementEmpty SetStyleset(Styleset S) {this.Attributes.Styleset = S; return this;}
		public ElementEmpty AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}
		public string ElementName { get{return name;} }
		public ElementEmpty (string name) {
			this.name = name;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>", this.ElementName, this.Attributes.GetPassonText(PS));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>", this.ElementName, this.Attributes.ToString());
		}
	}
	public class ElementText : IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public ElementText Accept(string value) {this.Attributes.Accept (value); return this;}
		public ElementText Action(string value) {this.Attributes.Action (value); return this;}
		public ElementText Alt(string value) {this.Attributes.Alt (value); return this;}
		public ElementText Enctype(MIME value) {this.Attributes.Enctype (value); return this;}
		public ElementText ID(string value) {this.Attributes.ID (value); return this;}
		public ElementText Method(AttributeSet.FormMethod value) {this.Attributes.Method (value); return this;}
		public ElementText Name(string value) {this.Attributes.Name (value); return this;}
		public ElementText Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementText Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementText Source(string value) {this.Attributes.Source (value); return this;}
		public ElementText Title(string value) {this.Attributes.Title (value); return this;}
		public ElementText Type(AttributeSet.InputType value) {this.Attributes.Type (value); return this;}
		public ElementText Value(string value) {this.Attributes.Value (value); return this;}
		public ElementText SetStyleset(Styleset S) {this.Attributes.Styleset = S; return this;}
		public ElementText AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}
		public string Text;
		public string ElementName { get{return name;} }
		public ElementText (string name, string text) {
			this.name = name;
			this.Text = text;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), HttpUtility.HtmlEncode(this.Text));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), HttpUtility.HtmlEncode(this.Text));
		}
	}
	public class ElementContainer : List<IElement>, IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public ElementContainer Accept(string value) {this.Attributes.Accept (value); return this;}
		public ElementContainer Action(string value) {this.Attributes.Action (value); return this;}
		public ElementContainer Alt(string value) {this.Attributes.Alt (value); return this;}
		public ElementContainer Enctype(MIME value) {this.Attributes.Enctype (value); return this;}
		public ElementContainer ID(string value) {this.Attributes.ID (value); return this;}
		public ElementContainer Method(AttributeSet.FormMethod value) {this.Attributes.Method (value); return this;}
		public ElementContainer Name(string value) {this.Attributes.Name (value); return this;}
		public ElementContainer Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementContainer Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementContainer Source(string value) {this.Attributes.Source (value); return this;}
		public ElementContainer Title(string value) {this.Attributes.Title (value); return this;}
		public ElementContainer Type(AttributeSet.InputType value) {this.Attributes.Type (value); return this;}
		public ElementContainer Value(string value) {this.Attributes.Value (value); return this;}
		public ElementContainer SetStyleset(Styleset S) {this.Attributes.Styleset = S; return this;}
		public ElementContainer AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}
		public string ElementName { get{return name;} }
		public ElementContainer (string name, params IElement[] contents) : base (contents) {
			this.name = name;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), String.Join("", this.Select((e) => e.ToWebOptimizedString(PS))));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), String.Join("", this));
		}
	}
	public class NonformatText : IElement {
		public string ElementName {get {return String.Empty;}}
		protected string text;
		public NonformatText(string text) {
			this.text = text;
		}
		public string ToWebOptimizedString(PassonStylesheet PS) {
			return text;
		}
		public override string ToString () {
			return text;
		}
	}
	public class AttributeElement : IElement {
		public AttributeSetA Attributes = new AttributeSetA();
		public AttributeElement ID(string value) {Attributes.ID(value); return this;}
		public AttributeElement Reference(string value) {Attributes.Reference(value);return this;}
		public AttributeElement Title(string value) {Attributes.Title(value); return this;}
		public AttributeElement SetStyleset(AttributeStyleset ASET) {this.Attributes.Styleset = ASET; return this;}
		public AttributeElement AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}
		public string ElementName {get {return "a";}}
		private IElement element;
		public AttributeElement(IElement element) {
			this.element = element;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), this.element.ToWebOptimizedString(PS));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), this.element.ToString());
		}
	}
	public static class HTML {
		//END ABSTRACTS BEGIN SPECIALS
		public static AttributeElement Attribute(IElement IE) {return new AttributeElement (IE);}
		public static AttributeElement Attribute(string IE) {return new AttributeElement (new NonformatText(IE));}
		//END SPECIALS BEGIN EMPTIES
		public static ElementEmpty Breakline () {return new ElementEmpty ("br");}
		public static ElementEmpty Image(string src) {return new ElementEmpty ("img").Source(src);}
		public static ElementEmpty Input (AttributeSet.InputType type, string name) {return new ElementEmpty ("input").Type (type).Name (name);}
		public static ElementEmpty Input (AttributeSet.InputType type, string name, string value) {return new ElementEmpty ("input").Type (type).Name (name).Value (value);}
		public static ElementEmpty FileInput (string name, string mimeaccept) {return new ElementEmpty ("input").Type (AttributeSet.InputType.File).Name (name).Accept (mimeaccept);}
		public static ElementEmpty Submit(string name = "Submit") {return new ElementEmpty ("input").Type (AttributeSet.InputType.Submit).Value (name);}
		//END EMPTIES BEGIN TEXTS
		public static ElementText Span(string text) {return new ElementText ("span", text);}
		//END TEXTS BEGIN CONTAINERS
		public static ElementContainer Divider(params IElement[] contents) {return new ElementContainer ("div", contents);} 
		public static ElementContainer Form(AttributeSet.FormMethod method, params IElement[] contents) { return new  ElementContainer ("form", contents).Method (method).Enctype (MIME.MultipartFormData); }
		//END CONTAINERS BEGIN STRUCTURES
	}

	public class Webpage {
		public string HTitle;
		public ElementContainer Body;
		public Webpage(string title, params IElement[] contents) {
			this.HTitle = title;
			this.Body = new ElementContainer("body", contents);
		}
		public void Add (IElement element) {
			Body.Add (element);
		}
		public void Add (params IElement[] elements) {
			Body.AddRange (elements);
		}
		public override string ToString () {
			PassonStylesheet PS = new PassonStylesheet ();
			return String.Format ("<{3}>{4}{5}</{3}><{0}{1}>{2}</{0}>", Body.ElementName, Body.Attributes.GetPassonText(PS), String.Join("", Body.Select((e) => e.ToWebOptimizedString(PS))), "head", String.Format("<title>{0}</title>", this.HTitle), String.Format("<style type=\"text/css\">{0}</style>", String.Join ("\n", PS.GetFormattedSheet ())));
		}
	}
	//END STRUCTURES
}

