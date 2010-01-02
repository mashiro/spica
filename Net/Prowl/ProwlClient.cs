using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace Spica.Net.Prowl
{
	public class ProwlClient
	{
		public const String BaseUri = "https://prowl.weks.net/publicapi/";
		public String APIKey { get; set; }
		public String ProviderKey { get; set; }

		#region Constructor
		public ProwlClient()
		{
			APIKey = String.Empty;
			ProviderKey = String.Empty;
		}

		public ProwlClient(String apiKey)
			: base()
		{
			APIKey = apiKey;
		}

		public ProwlClient(String apiKey, String providerKey)
			: base()
		{
			APIKey = apiKey;
			ProviderKey = providerKey;
		}
		#endregion

		#region Public
		public HttpWebResponse Verify()
		{
			StringBuilder builder = new StringBuilder("verify");

			var appender = CreateParameterAppender(builder);
			appender("apikey", APIKey);
			appender("providerkey", ProviderKey);

			return Post(builder.ToString());
		}

		public HttpWebResponse Add(ProwlNotification notification)
		{
			StringBuilder builder = new StringBuilder("add");

			var appender = CreateParameterAppender(builder);
			appender("apikey", APIKey);
			appender("providerkey", ProviderKey);
			appender("priority", (Int32)notification.Priority);
			appender("application", HttpUtility.UrlEncode(notification.Application));
			appender("event", HttpUtility.UrlEncode(notification.Event));
			appender("description", HttpUtility.UrlEncode(notification.Description));

			return Post(builder.ToString());
		}
		#endregion

		#region Private
		private HttpWebResponse Post(String uri)
		{
			HttpWebRequest request = HttpWebRequest.Create(BaseUri + uri) as HttpWebRequest;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = 0;
			return request.GetResponse() as HttpWebResponse;
		}

		private Action<String, Object> CreateParameterAppender(StringBuilder builder)
		{
			Boolean isFirst = true;
			return (name, value) =>
			{
				String strValue = value == null ? String.Empty : value.ToString();
				if (!String.IsNullOrEmpty(strValue))
				{
					builder.Append(isFirst ? "?" : "&");
					builder.AppendFormat("{0}={1}", name, strValue);
					isFirst = false;
				}
			};
		}
		#endregion
	}
}
