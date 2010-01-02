using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spica.Net.Prowl
{
	public class ProwlNotification
	{
		public ProwlNotificationPriority Priority { get; set; }
		public String Application { get; set; }
		public String Event { get; set; }
		public String Description { get; set; }
	}
}
