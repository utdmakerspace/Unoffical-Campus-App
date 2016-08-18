using System;

namespace Makerspace
{
	public class NotificationStructure
	{
		public string @default {get;set;}
		public string APNS_SANDBOX { get; set; }
		public string APNS { get; set; }
		public string GCM { get; set; }

	}
		
	public class APNS
	{
		public Aps aps { get; set; }
	}

	public class GCM
	{
		public Data data { get; set; }
	}

	public class Data
	{
		public string message { get; set; }

	}

	public class Aps
	{
		public string alert { get; set; }
		public string sound { get; set; }
		public int badge { get; set;}
	}


}

