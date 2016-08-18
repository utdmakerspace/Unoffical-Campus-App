
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Makerspace.Droid
{
	[Activity(Theme = "@style/MyTheme", //Indicates the theme to use for this activity
			 MainLauncher = true, //Set it as boot activity
			 NoHistory = true)] //Doesn't place it in back stack
	public class SplashScreen : Activity
	{
		#region Protected Methods

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//System.Threading.Thread.Sleep(3000); //Let's wait awhile...
			this.StartActivity(typeof(MainActivity));
		}

		#endregion Protected Methods
	}
}

