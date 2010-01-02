using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spica.Xml.Feed
{
	/// <summary>
	/// Utility
	/// </summary>
	public class Utility
	{
		public static DateTime ParseDateTime(String dateTimeString)
		{
			DateTime dateTime;
			if (!DateTime.TryParse(dateTimeString, out dateTime))
				dateTime = DateTime.Now;

			return dateTime;
		}

		public static Uri ParseUri(String uriString)
		{
			if (String.IsNullOrEmpty(uriString))
				return null;

			return new Uri(uriString);
		}

		public static TResult ValueOrDefault<T, TResult>(T arg, Func<T, TResult> func)
			where T : class
		{
			if (arg == null)
				return default(TResult);

			return func(arg);
		}
	}
}
