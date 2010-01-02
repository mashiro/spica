using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Spica.Xml.Feed
{
	[XmlRoot("RDF", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#")]
	public class RSS10Document : IFeedDocument
	{
		#region XmlSerializer
		private static Object _sync = new object();
		private static XmlSerializer _serializer = null;
		static RSS10Document()
		{
			lock (_sync)
			{
				if (_serializer == null)
					_serializer = new XmlSerializer(typeof(RSS10Document));
			}
		}
		public static XmlSerializer Serializer { get { return _serializer; } }
		#endregion

		[XmlElement("channel", Namespace = "http://purl.org/rss/1.0/")]
		public RSS10Channel _channel;

		[XmlElement("item", Namespace = "http://purl.org/rss/1.0/")]
		public List<RSS10Item> _items;

		#region IFeedDocument メンバ
		[XmlIgnore()]
		public Uri Link { get { return _channel.Link; } }

		[XmlIgnore()]
		public string Title { get { return _channel.Title; } }

		[XmlIgnore()]
		public string Description { get { return _channel.Description; } }

		[XmlIgnore()]
		public List<IFeedItem> Items { get { return new List<IFeedItem>(_items.ToArray()); } }
		#endregion
	}

	public class RSS10Channel
	{
		#region XmlElements
		[XmlElement("title")]
		public String _title;

		[XmlElement("link")]
		public String _link;

		[XmlElement("description")]
		public String _description;

		[XmlElement("date", Namespace = "http://purl.org/dc/elements/1.1/")]
		public String _date;

		[XmlElement("language", Namespace = "http://purl.org/dc/elements/1.1/")]
		public String _language;
		#endregion

		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public Uri Link { get { return Utility.ParseUri(_link); } }

		[XmlIgnore()]
		public String Description { get { return _description; } }

		[XmlIgnore()]
		public DateTime PublishDate { get { return Utility.ParseDateTime(_date); } }
	}

	public class RSS10Item : IFeedItem
	{
		[XmlElement("title")]
		public String _title;

		[XmlElement("link")]
		public String _link;

		[XmlElement("description")]
		public String _description;

		[XmlElement("creator", Namespace = "http://purl.org/dc/elements/1.1/")]
		public String _creator;

		[XmlElement("date", Namespace = "http://purl.org/dc/elements/1.1/")]
		public String _date;

		#region IFeedItem メンバ
		[XmlIgnore()]
		public string Author { get { return _creator; } }

		[XmlIgnore()]
		public Uri Link { get { return Utility.ParseUri(_link); } }

		[XmlIgnore()]
		public string Title { get { return _title; } }

		[XmlIgnore()]
		public string Description { get { return _description; } }

		[XmlIgnore()]
		public DateTime PublishDate { get { return Utility.ParseDateTime(_date); } }
		#endregion
	}
}
