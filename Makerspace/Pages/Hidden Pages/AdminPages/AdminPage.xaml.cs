using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class AdminPage : ContentPage
	{
		public AdminPage()
		{
			InitializeComponent();
			AddTab.Tapped += ((sender,e) => { Navigation.PushAsync(new AddEventPage()); });
			DeleteTab.Tapped += ((sender, e) => { Navigation.PushAsync(new RemoveEventPage()); });
			NotifyTab.Tapped += ((sender, e) => { Navigation.PushAsync(new SendNotificationPage()); });

	
		}


	}
}

