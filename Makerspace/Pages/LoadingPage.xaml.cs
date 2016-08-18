using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Pages;

namespace Makerspace
{
	public partial class LoadingPage : ContentPage
	{
		public LoadingPage()
		{
			InitializeComponent();

			var makerspacePage = new MakerspacePage();

			Device.BeginInvokeOnMainThread(async () =>
			{
				
			while (makerspacePage.DataSource.IsLoading)
			{

					await Task.Delay(250);

			}
				makerspacePage.DataSource.MaskKey("Biography");
				//get the events in the background and set as oldimage before any updates are written
				Task.Run(async () => EventsWriterHelper.oldImage = await EventsWriterHelper.getEventsList());
				await Navigation.PushAsync(makerspacePage,false);
			} );

				
		}
	}
}

