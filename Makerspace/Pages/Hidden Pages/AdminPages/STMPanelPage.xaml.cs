using System;
using System.Collections.Generic;
using Amazon.SimpleNotificationService;
using Amazon;

using Xamarin.Forms;
using Amazon.SimpleNotificationService.Model;

namespace Makerspace
{
	
	public partial class STMPanelPage : ContentPage
	{
		AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient(new Authenticator().getCredentials(), RegionEndpoint.USEast1);
		public STMPanelPage()
		{
			InitializeComponent();
			CheckInTab.Tapped += checkIn;
			NotifyTab.Tapped += cantMakeIt;




		}


		async void cantMakeIt(object sender, EventArgs e)
		{
			var checkName = await DisplayAlert("Are you sure?", "Confirm your name:\n" + NameEntry.Text, "Yes", "Whoops");

			if (checkName && !string.IsNullOrEmpty(messageEditor.Text))
			{
				await client.PublishAsync(
					   new PublishRequest
					   {
						   Subject = String.Format("[STM Out {1}] {0} can't make it", NameEntry.Text, DateTime.Now.ToString("d")),
					Message = String.Format("Hey, \n \n I can't make it today. \n \n Here's why: \n \n {1} \n \n Thanks, \n {0}", NameEntry.Text, messageEditor.Text),
						   TargetArn = "arn:aws:sns:us-east-1:939884077921:MakerspaceSTMService"
					   });
				await DisplayAlert("Sent", "We just an e-mail notifying that you won't be there!", "Ok");
				await Navigation.PopToRootAsync();
			}
			else if(string.IsNullOrEmpty(messageEditor.Text))
			{
				await DisplayAlert("Sorry but ... ", "You need to write a reason", "Ok");
			}
		}



		async void checkIn(object sender, EventArgs e)
		{
			var checkName = await DisplayAlert("Are you sure?", "Confirm your name:\n" + NameEntry.Text, "Yes", "Whoops");

			if(checkName )
			{
				 await client.PublishAsync(
						new PublishRequest
						{
					Subject = String.Format("[STM Check-In {1}] {0} just made it", NameEntry.Text, DateTime.Now.ToString("d")),
							Message = String.Format("Hey, \n \n I just wanted to let you all know I made it in today. \n \n Thanks, \n {0}", NameEntry.Text),
							TargetArn = "arn:aws:sns:us-east-1:939884077921:MakerspaceSTMService"
						});
				await DisplayAlert("Sent", "We just an e-mail notifying that you're in!", "Ok");
				await Navigation.PopToRootAsync();
			}

		}



	}
}

