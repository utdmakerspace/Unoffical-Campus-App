using System;

using Xamarin.Forms;

namespace Makerspace
{
	public class MapPage : ContentPage
	{
		ActivityIndicator load;

		public MapPage()
		{
			

			WebView browser = new WebView
			{
				Source = new UrlWebViewSource
				{
					Url = "https://www.utdallas.edu/maps"
				},

				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand

			};

			if(Device.OS == TargetPlatform.iOS)
			{
				Device.OpenUri(new Uri("http://www.utdallas.edu/mobile/maps.php"));
			}


			load = new ActivityIndicator
			{
				IsVisible = false,
				Color = Color.Gray

			};

		

			browser.Navigating += webOnNavigating;
			browser.Navigated += webOnEndNavigating;

			RelativeLayout relativeLayout = new RelativeLayout()
			{
				HeightRequest = 100,
			};

			relativeLayout.Children.Add(
				browser,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Width;
				}),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Height;
				})
			);

			relativeLayout.Children.Add(
				load,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Width;
				}),
				Constraint.Constant(30)
			);





			Content = relativeLayout;

		}

		void webOnNavigating(object sender, WebNavigatingEventArgs e)
		{
			
			load.IsRunning = true;
			load.IsVisible = true;

		}

		void webOnEndNavigating(object sender, WebNavigatedEventArgs e)
		{

			load.IsRunning = false;
			load.IsVisible = false;
		}
	}
}


