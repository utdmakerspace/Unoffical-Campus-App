using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Makerspace
{
	public class MenuListData : List<AndroidMenuItem>
	{
		public MenuListData()
		{
			this.Add(new AndroidMenuItem()
			{
				Title = "Events",
				IconSource = "tab_sessions.png",
				TargetType = typeof(EventsPage)
			});

			this.Add(new AndroidMenuItem()
			{
				Title = "Announcements Page",
				IconSource = "tab_sessions.png",
				TargetType = typeof(AnnouncementsListPage)
			});


			this.Add(new AndroidMenuItem()
			{
				Title = "Info",
				IconSource = "tab_about.png",
				TargetType = typeof(InfoPage)
			});


		}
	}

}


