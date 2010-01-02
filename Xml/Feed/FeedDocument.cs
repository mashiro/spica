using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Spica.Xml.Feed
{
	[Serializable]
	public class InvalidFeedException : Exception
	{
		public InvalidFeedException() { }
		public InvalidFeedException(string message) : base(message) { }
		public InvalidFeedException(string message, Exception inner) : base(message, inner) { }
		protected InvalidFeedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}

	public static class FeedDocument
	{
		public static IFeedDocument Load(String url)
		{
			using (XmlReader reader = XmlReader.Create(url))
			{
				return Load(reader);
			}
		}

		public static IFeedDocument Load(XmlReader reader)
		{
			if (RSS20Document.Serializer.CanDeserialize(reader))
			{
				return RSS20Document.Serializer.Deserialize(reader) as RSS20Document;
			}
			else if (RSS10Document.Serializer.CanDeserialize(reader))
			{
				return RSS10Document.Serializer.Deserialize(reader) as RSS10Document;
			}
			else if (Atom10Document.Serializer.CanDeserialize(reader))
			{
				return Atom10Document.Serializer.Deserialize(reader) as Atom10Document;
			}

			throw new InvalidFeedException("無効なフィードが指定されました。");
		}
	}
}
