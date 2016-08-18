using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Makerspace
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

		}

		protected override void OnDisappearing()
		{
			passwordForm.Text = "";
			base.OnDisappearing();
		}

		async void Handle_Completed(object sender, System.EventArgs e)
		{
			var enteredPassword = passwordForm.Text;

			passwordForm.IsPassword = false;
			passwordForm.Text = "Hold Up Verifying Password...";

			var isAdmin = await PasswordHelper.verifyAdminPassword(enteredPassword);

			var isSTM = await PasswordHelper.verifySTMPassword(enteredPassword);


			if (isAdmin)
			{	
				await Navigation.PushAsync(new AdminPage());

			} else if(isSTM)
			{
				await Navigation.PushAsync(new STMPanelPage());
			}
			else
			{
				passwordForm.Text = "";
				passwordForm.IsPassword = true;
				await DisplayAlert("Yo ... ", "Are you in the wrong place? Wrong password", "Uh, maybe");
			}

			passwordForm.IsPassword = true;
		
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			if (passwordForm.Text.Equals("officers2016"))
			{
				Navigation.PushAsync(new AdminPage());
			}
			else if (passwordForm.Text.Equals("stms2016"))
			{
				Navigation.PushAsync(new STMPanelPage());
			}
			else
			{
				DisplayAlert("Yo ... ", "Are you in the wrong place?", "Uh, maybe");
			}
		}
	}
}

