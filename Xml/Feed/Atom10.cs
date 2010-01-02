using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Spica.Xml.Feed
{
	[XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
	public class Atom10Document : IFeedDocument
	{
		#region XmlElements
		[XmlElement("title")]
		public String _title;

		[XmlElement("subtitle")]
		public String _subtitle;

		[XmlElement("link")]
		public List<Atom10Link> _link;

		[XmlElement("entry")]
		public List<Atom10Item> _items;
		#endregion

		#region Interface
		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public String Description { get { return _subtitle; } }

		[XmlIgnore()]
		public Uri Link { get { return _link.Count > 0 ? _link[0].HRef : null; } }

		[XmlIgnore()]
		public List<IFeedItem> Items { get { return new List<IFeedItem>(_items.ToArray()); } }
		#endregion

		#region XmlSerializer
		private static Object _sync = new object();
		private static XmlSerializer _serializer = null;
		static Atom10Document()
		{
			lock (_sync)
			{
				if (_serializer == null)
					_serializer = new XmlSerializer(typeof(Atom10Document));
			}
		}
		public static XmlSerializer Serializer { get { return _serializer; } }
		#endregion
	}

	public class Atom10Item : IFeedItem
	{
		#region XmlElements
		[XmlElement("author")]
		public Atom10Person _author;

		[XmlElement("link")]
		public List<Atom10Link> _link;

		[XmlElement("title")]
		public String _title;

		[XmlElement("content")]
		public String _content;

		[XmlElement("published")]
		public String _published;
		#endregion

		#region Interface
		[XmlIgnore()]
		public String Author { get { return _author.Name; } }

		[XmlIgnore()]
		public Uri Link { get { return _link.Count > 0 ? _link[0].HRef : null; } }

		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public String Description { get { return _content; } }

		[XmlIgnore()]
		public DateTime PublishDate { get { return DateTime.Parse(_published); } }
		#endregion
	}

	public class Atom10Person
	{
		#region XmlElements
		[XmlElement("name")]
		public String _name;

		[XmlElement("url")]
		public String _uri;

		[XmlElement("email")]
		public String _email;
		#endregion

		[XmlIgnore()]
		public String Name { get { return _name; } }

		[XmlIgnore()]
		public Uri Uri { get { return Utility.ParseUri(_uri); } }

		[XmlIgnore()]
		public String Email { get { return _email; } }
	}

	public class Atom10Link
	{
		#region XmlElements
		[XmlAttribute("href")]
		public String _href;

		[XmlAttribute("rel")]
		public String _rel;

		[XmlAttribute("type")]
		public String _type;

		[XmlAttribute("hreflang")]
		public String _hreflang;

		[XmlAttribute("title")]
		public String _title;

		[XmlAttribute("length")]
		public String _length;
		#endregion

		[XmlIgnore()]
		public Uri HRef { get { return Utility.ParseUri(_href); } }

		[XmlIgnore()]
		public String Rel { get { return _rel; } }

		[XmlIgnore()]
		public String Type { get { return _type; } }

		[XmlIgnore()]
		public String HrefLang { get { return _hreflang; } }

		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public String Length { get { return _length; } }
	}
}
