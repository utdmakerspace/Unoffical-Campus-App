using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class AnnouncementsPage : ContentPage
	{
		public AnnouncementsPage()
		{
			InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "Feed");

			AnnouncementsIsloading.IsRunning = true;
			primaryAnnouncementCell.Tapped += async (sender, e) => { await Navigation.PushAsync(new AnnouncementsListPage(announcements)); };
		}

		List<Announcement> announcements;

		protected async override void OnAppearing()
		{
			announcements = await AnnouncementsHelper.getAnnouncements();
			AnnouncementTitle.Text = announcements[0].publisher;
			AnnouncementText.Text = announcements[0].text;
			AnnouncementsIsloading.IsRunning = false;
			AnnouncementsIsloading.IsVisible = false;
			primaryAnnouncementCell.IsEnabled = true;



			var twitterdata = await TwitterHelper.getTwitterFeed();
			TwitterListView.IsRefreshing = false;
			TwitterListView.ItemsSource = twitterdata.statuses;

			base.OnAppearing();
		}


		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var listview = (ListView)sender;
			var status = listview.SelectedItem as Status;

			Device.OpenUri(new Uri(string.Format("https://twitter.com/{0}/status/{0}",status.id_str,status.user.screen_name)));
		}
	}
}

