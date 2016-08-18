using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using ImageCircle.Forms.Plugin.Droid;
using Android.Gms.Common;
using Android.Util;

namespace Makerspace.Droid
{
	[Activity(Label = "Makerspace", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);
			// ... your own OnCreate implementation
			CrashManager.Register(this, Keys.HockeyApp);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			ImageCircleRenderer.Init();

			LoadApplication(new App());
			RegisterForGCM();
		}

		private void RegisterForGCM()
		{
			string senders = Keys.GCMId;
			Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
			intent.SetPackage("com.google.android.gsf");
			intent.PutExtra("app", PendingIntent.GetBroadcast(this, 0, new Intent(), 0));
			intent.PutExtra("sender", senders);
			StartService(intent);
		}




	}
}

