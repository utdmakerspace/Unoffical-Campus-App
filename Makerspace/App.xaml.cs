using System.Diagnostics;
using Xamarin.Forms;

namespace Makerspace
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//MainPage = new MainController();
			if(Device.OS == TargetPlatform.Android)
			{
				MainPage = new AndroidRootPage();
			}
			else
			{
				MainPage = new MainController();
			}
		}
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		public static bool isFirstTime = true;

		protected override void OnResume()
		{
			MessagingCenter.Send<App>(this, "resume");
			// Handle when your app resumes
			isFirstTime = true;
				
		}






	}
}

