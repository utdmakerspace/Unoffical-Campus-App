using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class PlaceEventPage : ContentPage
	{

		bool CanGoBack = false;

		public PlaceEventPage(Session newSession)
		{
			InitializeComponent();
			var eventsSource = JsonConvert.DeserializeObject<ObservableCollection<Session>>(EventsWriterHelper.oldImage);
			EventsListView.ItemsSource = eventsSource;

			_newSession = newSession;
		}

		Session _newSession;

		protected override bool OnBackButtonPressed()
		{
			DisplayAlert("Hold up", "Still adding the event. I will automatically go back once done.", "I'll wait.");
			return CanGoBack;
		}

		async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var eventsSource = (ObservableCollection<Session>)EventsListView.ItemsSource;

			var index = eventsSource.IndexOf((Session)EventsListView.SelectedItem);

			if(index == 0)
			{
				var onTop = await DisplayAlert("Top Element", "Add on top or below?", "Top", "Below");

				if(onTop)
				{
					await EventListHelper.addNewEvent(_newSession);
				}
				else
				{
					await EventListHelper.addNewEvent(_newSession, index + 1);
				}
			}
			else
			{
				await EventListHelper.addNewEvent(_newSession, index + 1);
			}


			await DisplayAlert("Great!", "Event added!", "Ok");
			CanGoBack = true;
			await Navigation.PopAsync();


		}
	}
}

