using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Pages;

namespace Makerspace
{
	public partial class RemoveEventPage : ListDataPage
	{
		bool CanGoBack = false;

		public RemoveEventPage()
		{
			InitializeComponent();

		}

		protected override bool OnBackButtonPressed()
		{
			DisplayAlert("Hold up", "Still deleting the event. I will automatically go back once done.", "I'll wait.");
			return CanGoBack;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			var confirm = await DisplayAlert("Sure?", "Delete this event?", "Yes", "No");

			if(confirm)
			{
				var button = (Button)sender;
				var selection = (DataItem)button.CommandParameter;
				var eventID = Int32.Parse(selection.Name);
				await EventListHelper.removeEvent(eventID);

				await DisplayAlert("Deleted!", "Cool, bye bye event", "Done");
				CanGoBack = true;
				await Navigation.PopAsync();
			}

		}
	}
}

