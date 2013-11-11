using System;
using System.Collections.Generic;
using System.Linq;

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
	public abstract class ElementEmpty : IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
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
	public abstract class ElementText : IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public string Text;
		public string ElementName { get{return name;} }
		public ElementText (string name, string text) {
			this.name = name;
			this.Text = text;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), this.Text);
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), this.Text);
		}
	}
	public abstract class ElementContainer : List<IElement>, IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
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
	//END ABSTRACTS BEGIN SPECIALS
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
		public string ElementName {get {return "a";}}
		private IElement element;
		public AttributeElement(IElement element) {
			this.element = element;
		}
		public AttributeElement(string text) {
			this.element = new NonformatText (text);
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), this.element.ToWebOptimizedString(PS));
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), this.element.ToString());
		}
	}
	//END SPECIALS BEGIN EMPTIES
	public class Breakline : ElementEmpty {
		public Breakline () : base ("br") {

		}
	}
	public class Input : ElementEmpty {
		public Input (AttributeSet.InputType type, string name) : base ("input") {
			this.Attributes.Type (type);
			this.Attributes.Name (name);
		}
		public Input (AttributeSet.InputType type, string name, string value) : this(type, name) {
			this.Attributes.Value (value);
		}
	}
	public class FileInput : ElementEmpty {
		public FileInput (string name, string mimeaccept) : base ("input") {
			this.Attributes.Type (AttributeSet.InputType.File);
			this.Attributes.Name (name);
			this.Attributes.Accept (mimeaccept);
		}
	}
	public class Submit : ElementEmpty {
		public Submit (string name = "Submit") : base ("input") {
			this.Attributes.Type (AttributeSet.InputType.Submit);
			this.Attributes.Value (name);
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
	public class Form : ElementContainer {
		public Form (AttributeSet.FormMethod method, params IElement[] contents) : base ("form", contents) {
			this.Attributes.Method (method);
			this.Attributes.Enctype (MIME.MultipartFormData);
		}
	}
	//END CONTAINERS BEGIN STRUCTURES
	public class Webpage : ElementContainer {
		public string Title;
		public Webpage(string title, params IElement[] contents) : base("body", contents) {
			this.Title = title;
		}
		public string ToWebOptimizedString() {
			PassonStylesheet PS = new PassonStylesheet ();
			return String.Format ("<{3}>{4}{5}</{3}><{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), String.Join("", this.Select((e) => e.ToWebOptimizedString(PS))), "head", String.Format("<title>{0}</title>", this.Title), String.Format("<style type=\"text/css\">{0}</style>", String.Join ("\n", PS.GetFormattedSheet ())));
		}
		public override string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{3}>{4}</{3}><{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), String.Join("", this.Select((e) => e.ToWebOptimizedString(PS))), "head", String.Format("<title>{0}</title>", this.Title));
		}
		public override string ToString () {
			return String.Format ("<{3}>{4}</{3}><{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), String.Join("", this), "head", String.Format("<title>{0}</title>", this.Title));
		}
	}
	//END STRUCTURES
}

