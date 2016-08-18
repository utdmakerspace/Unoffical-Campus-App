using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Util;
using Android.Text;
using Amazon.SimpleNotificationService;
using Amazon.CognitoIdentity;
using Amazon;
using Amazon.SimpleNotificationService.Model;

namespace Makerspace.Droid
{

	[Service]
	public class GCMIntentService : IntentService
	{
		AmazonSimpleNotificationServiceClient snsClient;

		static PowerManager.WakeLock sWakeLock;
		static object LOCK = new object();

		public static void RunIntentInService(Context context, Intent intent)
		{
			lock (LOCK)
			{
				if (sWakeLock == null)
				{
					// This is called from BroadcastReceiver, there is no init.
					var pm = PowerManager.FromContext(context);
					sWakeLock = pm.NewWakeLock(
						WakeLockFlags.Partial, "My WakeLock Tag");
				}
			}

			sWakeLock.Acquire();
			intent.SetClass(context, typeof(GCMIntentService));
			context.StartService(intent);
		}

		protected override void OnHandleIntent(Intent intent)
		{
			try
			{
				Context context = this.ApplicationContext;
				string action = intent.Action;

				if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
				{
					HandleRegistration(intent);
				}
				else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
				{
					HandleMessage(intent);
				}
			}
			finally
			{
				lock (LOCK)
				{
					//Sanity check for null as this is a public method
					if (sWakeLock != null)
						sWakeLock.Release();
				}
			}
		}

		private async void HandleRegistration(Intent intent)
		{
			string registrationId = intent.GetStringExtra("registration_id");
			string error = intent.GetStringExtra("error");
			string unregistration = intent.GetStringExtra("unregistered");


			CognitoAWSCredentials credentials = new Authenticator().getCredentials();
			snsClient = new AmazonSimpleNotificationServiceClient(credentials, RegionEndpoint.USEast1);


			if (string.IsNullOrEmpty(error))
			{
				var response = await snsClient.CreatePlatformEndpointAsync(new CreatePlatformEndpointRequest
				{
					Token = registrationId,
					PlatformApplicationArn = Keys.GCMEndpointARN /* insert your platform application ARN here */
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

		private void HandleMessage(Intent intent)
		{
			string message = string.Empty;
			Bundle extras = intent.Extras;
			if (!string.IsNullOrEmpty(extras.GetString("message")))
			{
				message = extras.GetString("message");
			}
			else
			{
				message = extras.GetString("default");
			}

			Log.Info("Messages", "message received = " + message);

			AndroidUtils.ShowNotification(this, "Makerspace", message);

			//show the message

		}
	}
}