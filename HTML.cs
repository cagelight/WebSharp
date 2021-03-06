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
	public enum ButtonT {Button, Reset, Submit}
	public enum InputT {Text, Password, Radio, Checkbox, Submit, File, Hidden}
	public enum FormMethodT {GET, POST}
	public class AttributeSet {
		protected Dictionary<string, string> attributes = new Dictionary<string, string>();
		public List<Styleset> Stylesets = new List<Styleset>();
		public List<Style> Styles = new List<Style>();
		public AttributeSet() {

		}
		public AttributeSet Attrib(string key, string value) {
			if (key != null && value != null) {
				attributes [key] = value;
			}
			return this;
		}
		public AttributeSet Accept(string value) {return Attrib ("accept", value);}
		public AttributeSet Action(string value) {return Attrib ("action", value);}
		public AttributeSet Alt(string value) {return Attrib ("alt", value);}
		public AttributeSet Enctype(MIME M) {return Attrib ("enctype", M.ToString());}
		public AttributeSet Formaction(string value) {return Attrib ("formaction", value);}
		public AttributeSet Height(int value) {return Attrib ("height", value.ToString());}
		public AttributeSet ID(string value) {return Attrib ("id", value);}
		public AttributeSet Method(FormMethodT value) {return Attrib ("method", value.ToString().ToLower());}
		public AttributeSet Name(string value) {return Attrib ("name", value);}
		public AttributeSet Reference(string value) {return Attrib ("href", value);}
		public AttributeSet Relationship(string value) {return Attrib ("rel", value);}
		public AttributeSet Source(string value) {return Attrib ("src", value);}
		public AttributeSet Title(string value) {return Attrib ("title", value);}
		public AttributeSet InputType(InputT value) {return Attrib ("type", value.ToString().ToLower());}
		public AttributeSet Value(string value) {return Attrib ("value", value);}
		public AttributeSet Width(int value) {return Attrib ("width", value.ToString());}
		public virtual string GetPassonText(PassonStylesheet PS) {
			return (Stylesets.Count > 0 ? String.Format(" class=\"{0}\"", String.Join(" ", Stylesets.Select(x => PS[x]))) : String.Empty) + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
			//return (Styleset != null ? String.Format(" class=\"{0}\"", PS[Styleset, elementtype]) : String.Empty) + (Styles.Count > 0 ? String.Format(" style=\"{0}\"", String.Join("", Styles)) : String.Empty) + String.Join ("", attributes.Select((k, v) => String.Format(" {0}=\"{1}\"", k, v)));
		}
		public override string ToString () {
			return (Styles.Count > 0 || Stylesets.Count > 0 ? String.Format(" style=\"{0}\"", (Styles.Count > 0 ? String.Join("", Styles) : String.Empty) + (Stylesets.Count > 0 ? String.Join("", Stylesets.SelectMany((k) => k.Select((s) => String.Format("{0}:{1};", s.Key, s.Value)))) : String.Empty)) : String.Empty) + String.Join ("", attributes.Select((k) => String.Format(" {0}=\"{1}\"", k.Key, k.Value)));
		}
	}

	public class AttributeSetA {
		protected Dictionary<string, string> attributes = new Dictionary<string, string>();
		public List<Styleset> Stylesets = new List<Styleset>();
		public AttributeStyleset Styleset = new AttributeStyleset(null, null, null, null);
		public Styleset Link {get {return Styleset.Link;} set {Styleset.Link = value;}}
		public Styleset Visited {get {return Styleset.Visited;} set {Styleset.Visited = value;}}
		public Styleset Hover {get {return Styleset.Hover;} set {Styleset.Hover = value;}}
		public Styleset Active {get {return Styleset.Active;} set {Styleset.Active = value;}}
		public List<Style> Styles = new List<Style>();
		public AttributeSetA () {}
		public AttributeSetA Attrib(string key, string value) {
			if (key != null && value != null) {
				attributes [key] = value;
			}
			return this;
		}
		public AttributeSetA ID(string value) {return Attrib ("id", value);}
		public AttributeSetA Reference(string value) {return Attrib ("href", value);}
		public AttributeSetA Title(string value) {return Attrib ("title", value);}
		public virtual string GetPassonText(PassonStylesheet PS) {
			return ((Link != null || Visited != null || Hover != null || Active != null || Stylesets.Count > 0) ? String.Format(" class=\"{0}\"", String.Join(" ", Stylesets.Select(x => PS[x]).Concat(new string[] {PS[Link, Visited, Hover, Active]}))) : String.Empty)
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
		public ElementEmpty Formaction(string value) {this.Attributes.Formaction (value); return this;}
		public ElementEmpty Height(int value) {this.Attributes.Height (value); return this;}
		public ElementEmpty ID(string value) {this.Attributes.ID (value); return this;}
		public ElementEmpty Method(FormMethodT value) {this.Attributes.Method (value); return this;}
		public ElementEmpty Name(string value) {this.Attributes.Name (value); return this;}
		public ElementEmpty Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementEmpty Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementEmpty Source(string value) {this.Attributes.Source (value); return this;}
		public ElementEmpty Title(string value) {this.Attributes.Title (value); return this;}
		public ElementEmpty InputType(InputT value) {this.Attributes.InputType (value); return this;}
		public ElementEmpty Value(string value) {this.Attributes.Value (value); return this;}
		public ElementEmpty Width(int value) {this.Attributes.Width (value); return this;}
		public ElementEmpty CustomAttribute(string attr, string value) {this.Attributes.Attrib (attr, value);return this;}
		public ElementEmpty SetStyleset(Styleset S) {this.Attributes.Stylesets.Clear ();this.Attributes.Stylesets.Add(S); return this;}
		public ElementEmpty AddStyleset(Styleset S) {this.Attributes.Stylesets.Add(S); return this;}
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
		public bool HTMLProtection = true;
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public ElementText EnableProtection () {this.HTMLProtection = true; return this;}
		public ElementText DisableProtection () {this.HTMLProtection = false; return this;}
		public ElementText Accept(string value) {this.Attributes.Accept (value); return this;}
		public ElementText Action(string value) {this.Attributes.Action (value); return this;}
		public ElementText Alt(string value) {this.Attributes.Alt (value); return this;}
		public ElementText Enctype(MIME value) {this.Attributes.Enctype (value); return this;}
		public ElementText Formaction(string value) {this.Attributes.Formaction (value); return this;}
		public ElementText ID(string value) {this.Attributes.ID (value); return this;}
		public ElementText Method(FormMethodT value) {this.Attributes.Method (value); return this;}
		public ElementText Name(string value) {this.Attributes.Name (value); return this;}
		public ElementText Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementText Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementText Source(string value) {this.Attributes.Source (value); return this;}
		public ElementText Title(string value) {this.Attributes.Title (value); return this;}
		public ElementText InputType(InputT value) {this.Attributes.InputType (value); return this;}
		public ElementText Value(string value) {this.Attributes.Value (value); return this;}
		public ElementText CustomAttribute(string attr, string value) {this.Attributes.Attrib (attr, value);return this;}
		public ElementText SetStyleset(Styleset S) {this.Attributes.Stylesets.Clear ();this.Attributes.Stylesets.Add(S); return this;}
		public ElementText AddStyleset(Styleset S) {this.Attributes.Stylesets.Add(S); return this;}
		public ElementText AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}
		public string Text;
		public string ElementName { get{return name;} }
		public ElementText (string name, string text) {
			this.name = name;
			this.Text = text;
		}
		public virtual string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), HTMLProtection ? HttpUtility.HtmlEncode(this.Text) : this.Text);
		}
		public override string ToString () {
			return String.Format ("<{0}{1}>{2}</{0}>", this.ElementName, this.Attributes.ToString(), HTMLProtection ? HttpUtility.HtmlEncode(this.Text) : this.Text);
		}
	}
	public class ElementContainer : List<IElement>, IElement {
		protected string name;
		public AttributeSet Attributes = new AttributeSet();
		public ElementContainer Accept(string value) {this.Attributes.Accept (value); return this;}
		public ElementContainer Action(string value) {this.Attributes.Action (value); return this;}
		public ElementContainer Alt(string value) {this.Attributes.Alt (value); return this;}
		public ElementContainer Enctype(MIME value) {this.Attributes.Enctype (value); return this;}
		public ElementContainer Formaction(string value) {this.Attributes.Formaction (value); return this;}
		public ElementContainer ID(string value) {this.Attributes.ID (value); return this;}
		public ElementContainer Method(FormMethodT value) {this.Attributes.Method (value); return this;}
		public ElementContainer Name(string value) {this.Attributes.Name (value); return this;}
		public ElementContainer Reference(string value) {this.Attributes.Reference (value); return this;}
		public ElementContainer Relationship(string value) {this.Attributes.Relationship (value); return this;}
		public ElementContainer Source(string value) {this.Attributes.Source (value); return this;}
		public ElementContainer Title(string value) {this.Attributes.Title (value); return this;}
		public ElementContainer InputType(InputT value) {this.Attributes.InputType (value); return this;}
		public ElementContainer Value(string value) {this.Attributes.Value (value); return this;}
		public ElementContainer CustomAttribute(string attr, string value) {this.Attributes.Attrib (attr, value);return this;}
		public ElementContainer SetStyleset(Styleset S) {this.Attributes.Stylesets.Clear ();this.Attributes.Stylesets.Add(S); return this;}
		public ElementContainer AddStyleset(Styleset S) {this.Attributes.Stylesets.Add(S); return this;}
		public ElementContainer AddStyles(params Style[] S) {this.Attributes.Styles.AddRange (S);return this;}

		public string ElementName { get{return name;} }
		public ElementContainer (string name, params IElement[] contents) : base (contents) {
			this.name = name;
		}
		public void Add(params IElement[] e) {
			this.AddRange (e);
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
		public AttributeElement AddStyleset(Styleset S) {this.Attributes.Stylesets.Add (S); return this;}
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
	public class ButtonElement : ElementText {
		ButtonT BT;
		public ButtonElement(string text, ButtonT BT) : base("button", text) {
			this.BT = BT;
		}
		public override string ToWebOptimizedString(PassonStylesheet PS) {
			return String.Format ("<{0}{1} type=\"{3}\">{2}</{0}>", this.ElementName, this.Attributes.GetPassonText(PS), HttpUtility.HtmlEncode(this.Text), BT.ToString().ToLower());
		}
		public override string ToString () {
			return String.Format ("<{0}{1} type=\"{3}\">{2}</{0}>", this.ElementName, this.Attributes.ToString(), HttpUtility.HtmlEncode(this.Text), BT.ToString().ToLower());
		}
	}
	public static class HTML {
		//END ABSTRACTS BEGIN SPECIALS
		public static AttributeElement Attribute(IElement IE) {return new AttributeElement (IE);}
		public static AttributeElement Attribute(string IE) {return new AttributeElement (new NonformatText(IE));}
		//END SPECIALS BEGIN EMPTIES
		public static ElementEmpty Breakline () {return new ElementEmpty ("br");}
		public static ElementEmpty Image(string src) {return new ElementEmpty ("img").Source(src);}
		public static ElementEmpty Input (InputT type, string name) {return new ElementEmpty ("input").InputType (type).Name (name);}
		public static ElementEmpty Input (InputT type, string name, string value) {return new ElementEmpty ("input").InputType (type).Name (name).Value (value);}
		public static ElementEmpty FileInput (string name, string mimeaccept) {return new ElementEmpty ("input").InputType (InputT.File).Name (name).Accept (mimeaccept);}
		public static ElementEmpty Submit(string name = "Submit") {return new ElementEmpty ("input").InputType (InputT.Submit).Value (name);}
		//END EMPTIES BEGIN TEXTS
		public static ElementText Button(ButtonT B, string text) {return new ButtonElement (text, B);}
		public static ElementText Paragraph(string text) {return new ElementText ("p", text);}
		public static ElementText Span(string text) {return new ElementText ("span", text);}
		//END TEXTS BEGIN CONTAINERS
		public static ElementContainer Divider(params IElement[] contents) {return new ElementContainer ("div", contents);} 
		public static ElementContainer Form(FormMethodT method, params IElement[] contents) { ElementContainer F = new  ElementContainer ("form", contents).Method (method); if (method == FormMethodT.POST) {F.Enctype (MIME.MultipartFormData);} return F; }
		//END CONTAINERS BEGIN STRUCTURES
	}

	public class Webpage {
		public string HTitle;
		public ElementContainer Body;
		public List<string> Scripts = new List<string>();
		public List<string> ScriptReferences = new List<string>();
		protected List<Styleset> preset = new List<Styleset> ();
		protected List<AttributeStyleset> preaset = new List<AttributeStyleset> ();
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
		public void Preload(params Styleset[] S) {
			this.preset.AddRange (S);
		}
		public void Preload(params AttributeStyleset[] S) {
			this.preaset.AddRange (S);
		}
		public override string ToString () {
			PassonStylesheet PS = new PassonStylesheet ();
			preset.ForEach (s => PS.ManualAdd(s));
			preaset.ForEach (s => PS.ManualAdd(s));
			return String.Format ("<{3}>{4}{5}{6}{7}</{3}><{0}{1}>{2}</{0}>", 
			                      Body.ElementName, 
			                      Body.Attributes.GetPassonText(PS), 
			                      String.Join("", Body.Select((e) => e.ToWebOptimizedString(PS))), 
			                      "head", 
			                      String.Format("<title>{0}</title>", this.HTitle), 
			                      String.Format("<style type=\"text/css\">{0}</style>", String.Join ("\n", PS.GetFormattedSheet ())), 
			                      String.Join("\n", ScriptReferences.Select(s => String.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", s))),
			                      String.Join("\n", Scripts.Select(s => String.Format("<script type=\"text/javascript\">{0}</script>", s)))
			                      );
		}
	}
	//END STRUCTURES
}

