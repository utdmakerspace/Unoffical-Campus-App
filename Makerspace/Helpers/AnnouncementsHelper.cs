using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;

namespace Makerspace
{
	public class AnnouncementsHelper
	{
		public static async Task<List<Announcement>> getAnnouncements()
		{
			return await "https://s3.amazonaws.com/utdmakerspace/announcements.json".GetJsonAsync<List<Announcement>>();
		}
	}
}

