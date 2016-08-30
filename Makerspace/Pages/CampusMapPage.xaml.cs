using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class CampusMapPage : ContentPage
	{
		public CampusMapPage()
		{
			InitializeComponent();

			browser.Source = new UrlWebViewSource() { Url = "https://www.utdallas.edu/maps"};
		}


		void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
		{
			
		}

		void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
		{
			browser.IsVisible = true;
			load.IsRunning = false;
			load.IsVisible = false;

		}
	}
}

