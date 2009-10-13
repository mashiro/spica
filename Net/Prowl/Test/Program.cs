using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spica.Net.Prowl;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				String apiKey = Console.ReadLine();

				ProwlClient client = new ProwlClient(apiKey);
				if (client.Verify().StatusCode == System.Net.HttpStatusCode.OK)
				{
					ProwlNotification notification = new ProwlNotification();
					notification.Priority = ProwlNotificationPriority.Normal;
					notification.Application = "Spica.Net.Prowl";
					notification.Event = "Add Notification";
					notification.Description = "Test";

					client.Add(notification);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}
