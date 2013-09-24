using System;
using System.Collections.Generic;

namespace WebSharp {
	public class HTMLAttributes : Dictionary<string,string> {
		public HTMLAttributes() : base() {}
		internal HTMLAttributes Attrib(string key, string value) {
			this [key] = value;
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
		public HTMLAttributes Style(string value) {return Attrib ("style", value);}
		public HTMLAttributes Title(string value) {return Attrib ("title", value);}
		public HTMLAttributes Type(string value) {return Attrib ("type", value);}
		public HTMLAttributes Value(string value) {return Attrib ("value", value);}

		public override string ToString () {
			string rstr = String.Empty;
			foreach (KeyValuePair<string,string> KVP in this) {
				rstr += String.Format (" {0}=\"{1}\"", KVP.Key, KVP.Value);
			}
			return rstr;
		}
	}

	public interface IElement {
		string Name { get; }
		HTMLAttributes Attributes { get; }
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
		public override string ToString () {
			string contents = String.Empty;
			foreach (IElement E in this) {
				contents += E;
			}
			return String.Format ("<{0}{1}>{2}</{0}>", Name, Attributes, contents);
		}
	}

	public static class HTML {
		public static readonly ElementEmpty Breakline = new ElementEmpty ("br");
	}
}

