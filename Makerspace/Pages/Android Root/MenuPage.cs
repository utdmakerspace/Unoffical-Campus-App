using System;

using Xamarin.Forms;

namespace Makerspace
{
public class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }

		public MenuPage()
		{
			Title = "Menu"; // The Title property must be set.


			Menu = new MenuListView();

			var image = new Image(){HeightRequest=50,WidthRequest=100};
			image.Source = "PlaceHolder.png";
			var menuLabel = new ContentView
			{
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Label
				{
					TextColor = Color.FromHex("AAAAAA"),
					Text = "Main",
				}
			};

			var layout = new StackLayout
			{
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add(image);
			//layout.Children.Add(menuLabel);
			layout.Children.Add(Menu);

			Content = layout;
		}
	}
}


