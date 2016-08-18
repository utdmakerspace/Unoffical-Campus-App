using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class DiningDetailsPage : ContentPage
	{
		public DiningDetailsPage(DiningItem details)
		{
			InitializeComponent();

			TitleText.Text = details.Name;
			ProcessedTimeText.Text = details.currentTimings;
			MondayTime.Text = details.Timings.Monday;
			TuesdayTime.Text = details.Timings.Tuesday;
			WednesdayTime.Text = details.Timings.Wednesday;
			ThursdayTime.Text = details.Timings.Thursday;
			FridayTime.Text = details.Timings.Friday;
			SaturdayTime.Text = details.Timings.Saturday;
			SundayTime.Text = details.Timings.Sunday;


		}
	}
}

