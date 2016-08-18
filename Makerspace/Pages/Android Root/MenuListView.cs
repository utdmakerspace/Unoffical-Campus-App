using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Makerspace
{

public class MenuListView : ListView
	{
		public MenuListView()
		{
			List<AndroidMenuItem> data = new MenuListData();

			ItemsSource = data;
			VerticalOptions = LayoutOptions.FillAndExpand;
			BackgroundColor = Color.Transparent;

			this.SeparatorVisibility = SeparatorVisibility.None; 

			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding(TextCell.TextProperty, "Title");
			cell.SetBinding(TextCell.TextColorProperty,"theme");
	
			//cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");


			ItemTemplate = cell;
		}
	}
}


