using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Pages;

namespace Makerspace
{
	public partial class GenericListDataPage : ListDataPage
	{
		public GenericListDataPage(string dataSource)
		{
			DataSource = new JsonDataSource() { Source = JsonSource.FromUri(new Uri(dataSource))};
			InitializeComponent();

		}
	}
}

