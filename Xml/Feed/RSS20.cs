using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Spica.Xml.Feed
{
	[XmlRoot("rss")]
	public class RSS20Document : IFeedDocument
	{
		#region XmlElements
		[XmlElement("channel")]
		public RSS20Channel _channel;
		#endregion

		#region Interface
		[XmlIgnore()]
		public String Title { get { return _channel._title; } }

		[XmlIgnore()]
		public String Description { get { return _channel._description; } }

		[XmlIgnore()]
		public Uri Link { get { return _channel.Link; } }

		[XmlIgnore()]
		public List<IFeedItem> Items { get { return new List<IFeedItem>(_channel._items.ToArray()); } }
		#endregion

		#region XmlSerializer
		private static Object _sync = new object();
		private static XmlSerializer _serializer = null;
		static RSS20Document()
		{
			lock (_sync)
			{
				if (_serializer == null)
					_serializer = new XmlSerializer(typeof(RSS20Document));
			}
		}
		public static XmlSerializer Serializer { get { return _serializer; } }
		#endregion
	}

	public class RSS20Channel
	{
		#region XmlElements
		[XmlElement("title")]
		public String _title;

		[XmlElement("description")]
		public String _description;

		[XmlElement("link")]
		public String _link;

		[XmlElement("pubDate")]
		public String _pubDate;

		[XmlElement("item")]
		public List<RSS20Item> _items;
		#endregion

		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public String Description { get { return _description; } }

		[XmlIgnore()]
		public Uri Link { get { return Utility.ParseUri(_link); } }

		[XmlIgnore()]
		public DateTime PublishDate { get { return Utility.ParseDateTime(_pubDate); } }
	}

	public class RSS20Item : IFeedItem
	{
		#region XmlElements
		[XmlElement("author")]
		public String _author;

		[XmlElement("link")]
		public String _link;

		[XmlElement("pubDate")]
		public String _pubDate;

		[XmlElement("title")]
		public String _title;

		[XmlElement("description")]
		public String _description;
		#endregion

		#region Interface
		[XmlIgnore()]
		public String Author { get { return _author; } }

		[XmlIgnore()]
		public Uri Link { get { return Utility.ParseUri(_link); } }

		[XmlIgnore()]
		public String Title { get { return _title; } }

		[XmlIgnore()]
		public String Description { get { return _description; } }

		[XmlIgnore()]
		public DateTime PublishDate { get { return Utility.ParseDateTime(_pubDate); } }
		#endregion
	}
}
