using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Pages;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;

namespace Makerspace
{
	public partial class MakerspacePage : ListDataPage
	{

		public static DateTime eventTime = new DateTime();

		string calendarId;

		public MakerspacePage()
		{
			NavigationPage.SetHasBackButton(this, false);
			                   
			MessagingCenter.Subscribe<App>(this, "resume", (sender) =>
			{
				Debug.WriteLine("Refreshing");
				UpdateData();
			});

		
			DataSource = new JsonDataSource() { Source = JsonSource.FromUri(new Uri("https://s3.amazonaws.com/utdmakerspace/events.json")) };


			InitializeComponent();


			//fire if anyone taps or does anything
			PropertyChanged += Property_Changed;

		}

	
		void UpdateData()
		{
			DataSource = new JsonDataSource() { Source = JsonSource.FromUri(new Uri("https://s3.amazonaws.com/utdmakerspace/events.json")) };
		}

		void Property_Changed(object sender, PropertyChangedEventArgs e)
		{
			

			if(DataSource.Data.Count != 0)
			{


				foreach (IDataItem item in Data)
				{

					var itemVal = (IDataSource)item.Value;

					itemVal.MaskKey("biography");

				}

			}

			try
			{
				if (e.PropertyName == "SelectedItem")
				{

					var selectionIndex = (IDataItem)this.SelectedItem;

					var payload = (BaseDataSource)selectionIndex.Value;

					var DataFilter = (IDataSource)payload;


					//cover up some JSON values to not be shown
					DataFilter.MaskKey("biography");
					DataFilter.MaskKey("AttendeeCount");
					DataFilter.MaskKey("Theme");
					DataFilter.MaskKey("end");


					Debug.WriteLine(eventTime.ToString("D"));

				}

			}
			catch (NullReferenceException)
			{
				//catch because it may be a null item
			}
		}

		//handle clicking of the image
		void Handle_Clicked(object sender, EventArgs e)
		{
			var image = (Image)sender;
			var tapgesture = (TapGestureRecognizer)image.GestureRecognizers[0];
			var selection = (DataItem)tapgesture.CommandParameter;
			var payload = (BaseDataSource)selection.Value; //get the object from the image
			addtoCalendar(payload);

		}

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

		public void addtoCalendar(BaseDataSource payload)
		{

			//build our object
			var startTime = (DateTime)payload["start"];
			var endTime = (DateTime)payload["end"];
			var desc = (string)payload["abstract"];
			var location = (string)payload["room"];
			var title = (string)payload["title"];
			Debug.WriteLine(startTime);

			//add event to calendar
			Device.BeginInvokeOnMainThread(async () =>
			{
				var answer = await DisplayAlert("Add Event to Calendar?", "Save this event for later", "Yes", "No");
				if (answer)
				{
					var eventDetails = new CalendarEvent()
					{
						Start = startTime,
						End = endTime,
						Description = desc,
						Location = location,
						Name = title,
						ExternalID = "makerspace"
					};

					if(Device.OS != TargetPlatform.Android)
					{
						await EventListHelper.incrementAttendeeCount();
					}

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
	}


}


