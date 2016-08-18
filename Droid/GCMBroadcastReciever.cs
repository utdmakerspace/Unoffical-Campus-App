using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Makerspace.Droid
{
	[BroadcastReceiver(Permission = "com.google.android.c2dm.permission.SEND")]
	[IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "com.raviteja.makerspace" })]
	[IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] { "com.raviteja.makerspace" })]
	[IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "com.raviteja.makerspace" })]
	public class GCMBroadcastReceiver : BroadcastReceiver
	{
		const string TAG = "PushHandlerBroadcastReceiver";
		public override void OnReceive(Context context, Intent intent)
		{
			GCMIntentService.RunIntentInService(context, intent);
			SetResult(Result.Ok, null, null);
		}
	}

	[BroadcastReceiver]
	[IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted })]
	public class GCMBootReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			GCMIntentService.RunIntentInService(context, intent);
			SetResult(Result.Ok, null, null);
		}
	}
}