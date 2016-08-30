using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class AddEventPage : ContentPage
	{
		

		public AddEventPage()
		{
			InitializeComponent();

			startDatePicker.MinimumDate = DateTime.Now;
			startDatePicker.MaximumDate = DateTime.Now.AddMonths(3);


		}



		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			
			var hasChecked  = await DisplayAlert("Done?", "Have you made sure everything is safe to publish. Did you verify with Ravi?", "Yes", "No");

			if(hasChecked)
			{
				var startDate = startDatePicker.Date;
				var startTime = startTimePicker.Time;
				startDate = startDate.Add(startTimePicker.Time);

				var endDate = endDatePicker.Date;
				endDate = endDate.Add(endTimePicker.Time);



				var session = new Session()
				{
					start = startDate,
					end = endDate,
					Start = endDate.ToString("f"),
					End = endDate.ToString("f"),
					presenter = presenterEntry.Text,
					biography = presenterEditor.Text,
					@abstract = descEditor.Text,
					title = titleEntry.Text,
					room = roomEntry.Text,
					image = imageEntry.Text,
					theme = themeEntry.Text
				};

				await Navigation.PushAsync(new PlaceEventPage(session));

				/* await EventListHelper.addNewEvent(session);

				await DisplayAlert("Great!", "Event added!", "Ok");
				CanGoBack = true;
				await Navigation.PopAsync();*/


			}

		}
	}
}

