using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Flurl.Http;
using Xamarin.Forms;

namespace Makerspace
{
	public partial class DiningPage : ContentPage
	{
		public DiningPage()
		{
			InitializeComponent();

			if (App.isFirstTime)
			{
				DiningListView.IsRefreshing = true;
				App.isFirstTime = false;
			}

		}

		protected async override void OnAppearing()
		{
			if (App.isFirstTime)
			{
				DiningListView.IsRefreshing = true;
				App.isFirstTime = false;
			}

			var eventsSource = await fetchJsonData();
			DiningListView.ItemsSource = eventsSource;
			base.OnAppearing();
			DiningListView.IsRefreshing = false;
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var listview = (ListView)sender;
			var diningItem = listview.SelectedItem as DiningItem;
			Navigation.PushAsync(new DiningDetailsPage(diningItem));
		}


		public async Task<ObservableCollection<DiningItem>> fetchJsonData()
		{
			return await "https://s3.amazonaws.com/utdmakerspace/dining.json".GetJsonAsync<ObservableCollection<DiningItem>>();
		}


	}
}

