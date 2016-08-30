using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
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
			var eventsSource = await EventsWriterHelper.getEventsList();
			var parsedEvents = JsonConvert.DeserializeObject<ObservableCollection<Session>>(eventsSource);
			Debug.WriteLine("Fetching Data");
			EventsListView.ItemsSource = parsedEvents;
			base.OnAppearing();
			EventsListView.IsRefreshing = false;
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var listview = (ListView)sender;
			var sessionItem = listview.SelectedItem as Session;
			Navigation.PushAsync(new EventDetailsPage(sessionItem));
		}




	}
}

