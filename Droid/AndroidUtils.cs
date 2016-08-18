using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using System;

namespace Makerspace.Droid
{
	public class AndroidUtils
	{

		private static int REQUEST_CODE = 1001;

		public static void ShowNotification(Context context, string contentTitle,
				string contentText)
		{

			var intent =
				context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
			intent.AddFlags(ActivityFlags.ClearTop);

			var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

			Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
			textStyle.BigText(contentText);
			textStyle.SetSummaryText("UTDesign Makerspace");

			// Intent
			Notification.Builder builder = new Notification.Builder(context)
				.SetContentTitle(contentTitle)
				.SetContentText(contentText)
				.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
				.SetSmallIcon(Resource.Drawable.icon)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				.SetContentIntent(pendingIntent)
				.SetStyle(textStyle)
				.SetPriority(1)
				.SetAutoCancel(true);
			
			// Get the notification manager:
			NotificationManager notificationManager =
				context.GetSystemService(Context.NotificationService) as NotificationManager;

			notificationManager.Notify(1001, builder.Build());
		}

		public static void ShowNotification(Context context, String contentText)
		{
			ShowNotification(context, "", contentText);
		}
	}
}