using System;
using System.Collections.Generic;
using Amazon.SimpleNotificationService;
using Amazon;

using Xamarin.Forms;
using Newtonsoft.Json;
using Amazon.SimpleNotificationService.Model;

namespace Makerspace
{
	public partial class SendNotificationPage : ContentPage
	{
		AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient(new Authenticator().getCredentials(), RegionEndpoint.USEast1);

		public SendNotificationPage()
		{
			InitializeComponent();
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			var hasChecked = await DisplayAlert("Done?", "Have you made sure everything is safe to publish. Did you verify with Ravi?", "Yes", "No");
			if(hasChecked)
			{
				var content = messageEditor.Text;

				var apns = new APNS() { aps = new Aps() { alert = content, sound = "default" } };
				var gcm = new GCM() { data = new Data { message = content } };

				var apnsJson = JsonConvert.SerializeObject(apns);
				var gcmJson = JsonConvert.SerializeObject(gcm);

				var APNSmessage = new NotificationStructure()
				{ 
					APNS_SANDBOX = apnsJson, 
					APNS = apnsJson, 
					GCM = gcmJson, 
					@default = "Your friend is doing something" 
				};

				string payload = JsonConvert.SerializeObject(APNSmessage);

					var notifyFriend = await client.PublishAsync(
						new PublishRequest
						{
							MessageStructure = "json",
							Message = payload,
							TargetArn = "arn:aws:sns:us-east-1:939884077921:UTDMakerspace"
						});
				await DisplayAlert("Done", "Sent the Notification! Ask your friend to make sure they got it", "Ok!");
				await Navigation.PopAsync();
								
			}

		}
	}
}

