using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class ChooseCoachOrPlayerPage : ContentPage
	{
		public ChooseCoachOrPlayerPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
		private async void OnAthleteClicked(object sender, EventArgs e)
		{
			User newAthlete = new User();
			newAthlete.isCoach = false;
			await Navigation.PushAsync(new ChoseTeamPage(false));
		}
		private async void OnCoachClicked(object sender, EventArgs e)
		{
			User newAthlete = new User();
			newAthlete.isCoach = true;
			await Navigation.PushAsync(new ChoseTeamPage(true));
		}
	}
}
