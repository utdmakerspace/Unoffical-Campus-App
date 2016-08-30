using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class InfoPage : ContentPage
	{
		public InfoPage()
		{
			InitializeComponent();

			DiningTab.Tapped += (sender, e) => { Navigation.PushAsync(new DiningPage()); };
			DiningTab.TextColor = Color.Black;



			MapTab.Tapped += (sender, e) => { Navigation.PushAsync(new MapPage());};
			MapTab.TextColor = Color.Black;

			AdminTab.Tapped += (sender, e) => { Navigation.PushAsync(new LoginPage()); };
			STMLoginTab.Tapped += (sender, e) => { Navigation.PushAsync(new LoginPage()); };

			OfficersTab.Tapped += (sender, e) => { Navigation.PushAsync(new GenericListDataPage("https://s3.amazonaws.com/utdmakerspace/officers.json"){Title="Officers"}); };
			STMTab.Tapped += (sender, e) => { Navigation.PushAsync(new GenericListDataPage("https://s3.amazonaws.com/utdmakerspace/stms.json"){ Title = "STMS" }); };
			SponsorsTab.Tapped += (sender, e) => { Navigation.PushAsync(new GenericListDataPage("https://s3.amazonaws.com/utdmakerspace/sponsors.json"){ Title = "Sponsors" }); };



			ConductTab.Tapped += (sender, e) => { Navigation.PushAsync(new CodeOfConductPage()); };
			ContactTab.Tapped += (sender, e) => { Navigation.PushAsync(new ContactPage()); };
		}

	}
}

