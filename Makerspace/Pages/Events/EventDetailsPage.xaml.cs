using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Forms;

namespace Makerspace
{
	public partial class EventDetailsPage : ContentPage
	{

		Session thisEvent = new Session();
		public EventDetailsPage(Session details)
		{
			InitializeComponent();
			thisEvent = details;
			TitleText.Text = details.title;
			ProcessedTimeText.Text = processTimes(details);
			AbstractText.Text = details.@abstract;
			//presenterListView.ItemsSource = convertAttendeeToList(details);

			PresenterName.Text = details.presenter;
			PresenterDetail.Text = details.biography;
			PresenterAccent.BackgroundColor = details.color;

			//set colors
			AddTab.TextColor = details.color;
			BackgroundBox.Color = details.color;
			TextBackground.BackgroundColor = details.color;


		}


		List<Session> convertAttendeeToList(Session details)
		{
			return new List<Session>() { 
				new Session() 
				{ 
					presenter = details.presenter, 
					biography = details.biography, 
					theme=details.theme
				}

			};
		}

		string processTimes(Session details)
		{
			var startTime = details.start;
			var endTime = details.end;

			return string.Format("{0}-{1}", startTime.ToString("f"), endTime.ToString("t"));
		}


		void Handle_Tapped(object sender, System.EventArgs e)
		{
			//add event to calendar
			Device.BeginInvokeOnMainThread(async () =>
			{
				var answer = await DisplayAlert("Add Event to Calendar?", "Save this event for later", "Yes", "No");
				if (answer)
				{
					var eventDetails = new CalendarEvent()
					{
						Start = thisEvent.start,
						End = thisEvent.end,
						Description = thisEvent.@abstract,
						Location = thisEvent.room,
						Name = thisEvent.title,
						ExternalID = "makerspace"
					};

					await EventListHelper.incrementAttendeeCount();

					try
					{
						var localCalendar = await GetOrCreateMakerCalendarAsync();

						await CrossCalendars.Current.AddOrUpdateEventAsync(localCalendar, eventDetails);

						await DisplayAlert("Done!", "Added to Calendar", "Yay!");

					}
					catch (UnauthorizedAccessException)
					{
						await DisplayAlert("Uh-Oh", "Go to Settings and Enable Calendar Access for this App!", "Ok");


					}
				}
			});
		}


		string calendarId = "MKSPC";

		public async Task<Calendar> GetOrCreateMakerCalendarAsync()
		{

			var id = calendarId;
			if (!string.IsNullOrWhiteSpace(id))
			{
				try
				{
					var calendar = await CrossCalendars.Current.GetCalendarByIdAsync(id);
					if (calendar != null)
						return calendar;
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Unable to get calendar.. odd as we created it already: " + ex);

				}

			}

			//if for some reason the calendar does not exist then simply create a enw one.
			if (Device.OS == TargetPlatform.Android)
			{
				//On android it is really hard to delete a calendar made by an app, so just add to default calendar.
				try
				{
					var calendars = await CrossCalendars.Current.GetCalendarsAsync();
					foreach (var calendar in calendars)
					{
						//find first calendar we can add stuff to
						if (!calendar.CanEditEvents)
							continue;

						calendarId = calendar.ExternalID;
						return calendar;
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Unable to get calendars.. " + ex);
				}
			}
			else
			{
				//try to find evolve app if already uninstalled for some reason
				try
				{
					var calendars = await CrossCalendars.Current.GetCalendarsAsync();
					foreach (var calendar in calendars)
					{
						//find first calendar we can add stuff to
						if (calendar.CanEditEvents && calendar.Name == "Makerspace Calendar")
						{
							calendarId = calendar.ExternalID;
							return calendar;
						}
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Unable to get calendars.. " + ex);
				}
			}

			var makerCalendar = new Calendar();
			makerCalendar.Color = "#7635EB";
			makerCalendar.Name = "Makerspace Calendar";
			makerCalendar.ExternalID = id;

			try
			{
				await CrossCalendars.Current.AddOrUpdateCalendarAsync(makerCalendar);
				calendarId = makerCalendar.ExternalID;
				return makerCalendar;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to create calendar.. " + ex);
			}

			return null;
		}
	}
}

