using System;
using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Foundation;
using HockeyApp.iOS;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;

namespace Makerspace.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		AmazonSimpleNotificationServiceClient snsClient;
			

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();

			snsClient = new AmazonSimpleNotificationServiceClient(new CognitoAWSCredentials(
				Keys.AWSCognito,
				RegionEndpoint.USEast1), RegionEndpoint.USEast1);
			
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure(Keys.HockeyApp);
			manager.StartManager();

			LoadApplication(new App());

			var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
  				UIUserNotificationType.Alert |
 				 UIUserNotificationType.Badge |
  			UIUserNotificationType.Sound,
  				null
				);

			app.RegisterUserNotificationSettings(pushSettings);
			app.RegisterForRemoteNotifications();


			var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
			x = typeof(Xamarin.Forms.Themes.LightThemeResources);
			x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);
			return base.FinishedLaunching(app, options);


		}

		public override async void RegisteredForRemoteNotifications(UIApplication application, NSData token)
		{
			var deviceToken = token.Description.Replace("<", "").Replace(">", "").Replace(" ", "");
			if (!string.IsNullOrEmpty(deviceToken))
			{
				//register with SNS to create an endpoint ARN
				var response = await snsClient.CreatePlatformEndpointAsync(
				new CreatePlatformEndpointRequest
				{
					Token = deviceToken,
					PlatformApplicationArn = Keys.APNSEndpointARN /* insert your platform application ARN here */
				});

				var endpoint = response.EndpointArn;


				var subscribeResponse = await snsClient.SubscribeAsync(new SubscribeRequest
				{
					TopicArn = Keys.AppTopicARN,
					Endpoint = endpoint,
					Protocol = "application"

				});

			}

		}

		public override void ReceivedRemoteNotification(UIApplication app, NSDictionary userInfo)
		{
			// Process a notification received while the app was already open
			ProcessNotification(userInfo);
		}

		void ProcessNotification(NSDictionary userInfo)
		{
			if (userInfo == null)
				return;

			Console.WriteLine("Received Notification");

			var apsKey = new NSString("aps");

			if (userInfo.ContainsKey(apsKey))
			{

				var alertKey = new NSString("alert");

				var aps = (NSDictionary)userInfo.ObjectForKey(apsKey);

				if (aps.ContainsKey(alertKey))
				{
					var alert = (NSString)aps.ObjectForKey(alertKey);

					try
					{

						var avAlert = new UIAlertView("Makerspace", alert, null, "OK", null);
						avAlert.Show();

					}
					catch (Exception ex)
					{

					}

					Console.WriteLine("Notification: " + alert);
				}
			}
		}

	}
}

