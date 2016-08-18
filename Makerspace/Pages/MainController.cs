using System;

using Xamarin.Forms;

namespace Makerspace
{
	public class MainController : TabbedPage
	{
		public MainController()
		{
			
			Children.Add(new NavigationPage(new AnnouncementsPage()) { Title = "Announcements", Icon="tab_feed"});
			Children.Add(new NavigationPage(new EventsPage()) { Title = "Events", Icon = "tab_sessions" });
			Children.Add(new NavigationPage(new InfoPage()){ Title = "Info", Icon="tab_about" });


		}
	}
}


