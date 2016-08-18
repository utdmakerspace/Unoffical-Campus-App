using System;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Makerspace;
using System.Diagnostics;

namespace Makerspace
{
	public class TwitterHelper
	{

		public static async Task<TwitterModel> getTwitterFeed()
		{
			return filterResults(await "https://api.twitter.com/1.1/search/tweets.json?q=%23utdallas".WithOAuthBearerToken(Keys.TwitterToken).GetJsonAsync<TwitterModel>());
		}

		static TwitterModel filterResults(TwitterModel feed)
		{
			var filteredList = new List<Status>();

			foreach(Status status in feed.statuses)
			{
				status.user.screen_name = "@" + status.user.screen_name;
				
				if(status.retweet_count > 0)
				{
					try
					{
						status.user.screen_name = "@" + status.retweeted_status.user.screen_name;
						status.user.name = status.retweeted_status.user.name;
						status.text = status.retweeted_status.text;
						status.id_str = status.retweeted_status.id_str;
						filteredList.Add(status);
					}catch(Exception e)
					{
						Debug.WriteLine("Item Skipped " + e);
					}

				}



			}

			bool hasData = true;
			int counter = 9;


			List<Status> topResults = new List<Status>();

			while(hasData)
			{
				try
				{
					topResults = filteredList.GetRange(0, counter);
					hasData = false;
				}
				catch (Exception e)
				{
					counter--;
					hasData = true;
				}
			}

			feed.statuses = topResults.GroupBy(p => p.id_str).Select(g => g.First()).ToList();

			return feed;

		}

	

	}
}

