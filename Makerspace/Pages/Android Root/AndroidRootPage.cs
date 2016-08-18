using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Makerspace
{
	public class AndroidRootPage : MasterDetailPage
	{
		public AndroidRootPage()
		{
			var menuPage = new MenuPage();
			Icon = "icon.png";
			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as AndroidMenuItem);

			Master = menuPage;
			Detail = new NavigationPage(new EventsPage());
		}

		void NavigateTo(AndroidMenuItem menu)
		{
			Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

			Detail = new NavigationPage(displayPage);

			IsPresented = false;
		}

	}
}


