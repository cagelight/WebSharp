using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebSharp {

	public class MIME {
		public readonly string Type;
		public readonly string Format;
		public readonly string[] Extensions;
		public MIME(string type, string format, params string[] extensions) {
			Type = type;
			Format = format;
			Extensions = extensions;
		}
		public override string ToString() {
			return String.Format ("{0}/{1}", Type, Format);
		}
		public static MIME FromExtension(string extension) {
			return Manager.FromExtension (extension.Substring(1));
		}
		public static MIME FromText(string text) {
			return Manager.FromText (text);
		}
		public static MIME OctetStream = new MIME ("application", "octet-stream", "exe", "bin");
		public static MIME FormData = new MIME ("application", "x-www-form-urlencoded");
		public static MIME JPEG = new MIME ("image", "jpeg", "jpg", "jpeg");
		public static MIME PNG = new MIME ("image", "png", "png");
		public static MIME Icon = new MIME ("image", "x-icon", "ico");
		public static MIME MultipartFormData = new MIME ("multipart", "form-data");
		public static MIME HTML = new MIME ("text", "html", "htm", "html");
		public static MIME Plaintext = new MIME ("text", "plain", "txt");
		private static readonly MIMEManager Manager = new MIMEManager();
	}

	internal class MIMEManager {
		private Dictionary<string, MIME> TextDict = new Dictionary<string, MIME>();
		private Dictionary<string, MIME> ExtDict = new Dictionary<string, MIME>();
		public MIMEManager() {
			foreach(FieldInfo FI in typeof(MIME).GetFields().Where((f) => (f.FieldType == typeof(MIME) && f.IsStatic))) {
				MIME cur = FI.GetValue (null) as MIME;
				TextDict [cur.ToString ()] = cur;
				foreach(string E in cur.Extensions) {
					ExtDict [E] = cur;
				}
			}
		}
		internal MIME FromText (string text) {
			try {
				return TextDict[text];
			} catch {
				return MIME.OctetStream;
			}
		}
		internal MIME FromExtension (string ext) {
			try {
				return ExtDict[ext];
			} catch {
				return MIME.OctetStream;
			}
		}
	}
}

