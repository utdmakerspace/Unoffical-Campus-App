using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Flurl.Http;
using Xamarin.Forms;

namespace Makerspace
{
	public partial class EventsPage : ContentPage
	{

		public EventsPage()
		{
			InitializeComponent();

			if(App.isFirstTime)
			{
				EventsListView.IsRefreshing = true;
				App.isFirstTime = false;
			}

		}

		protected async override void OnAppearing()
		{
			var eventsSource = await fetchJsonData();
			Debug.WriteLine("Fetching Data");
			EventsListView.ItemsSource = eventsSource;
			Task.Run(async () => EventsWriterHelper.oldImage = await EventsWriterHelper.getEventsList());
			base.OnAppearing();
			EventsListView.IsRefreshing = false;
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var listview = (ListView)sender;
			var sessionItem = listview.SelectedItem as Session;
			Navigation.PushAsync(new EventDetailsPage(sessionItem));
		}


		public async Task<ObservableCollection<Session>> fetchJsonData()
		{
			return await "https://s3.amazonaws.com/utdmakerspace/events.json".GetJsonAsync<ObservableCollection<Session>>();
		}

	}
}

