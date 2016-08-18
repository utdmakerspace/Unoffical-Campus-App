using System;
using System.Collections.Generic;

namespace Makerspace
{
	public class TwitterModel
	{

		public List<Status> statuses { get; set; }

	}

	public class Status
	{
		public string created_at { get; set; }
		public string id_str { get; set; }
		public string text { get; set; }
		public bool truncated { get; set; }
		public User user { get; set; }
		public int retweet_count { get; set; }
		public int favorite_count { get; set; }
		public bool favorited { get; set; }
		public bool retweeted { get; set; }
		public bool possibly_sensitive { get; set; }
		public string lang { get; set; }
		public RetweetedStatus retweeted_status { get; set;}

	}

	public class RetweetedStatus
	{
		public string created_at { get; set; }
		public object id { get; set; }
		public string id_str { get; set; }
		public string text { get; set; }
		public User user { get; set;}
	}

	public class User
	{
		public object id { get; set; }
		public string id_str { get; set; }
		public string name { get; set; }
		public string screen_name { get; set; }
		public string location { get; set; }
		public string description { get; set; }
		public string url { get; set; }
	}

}

