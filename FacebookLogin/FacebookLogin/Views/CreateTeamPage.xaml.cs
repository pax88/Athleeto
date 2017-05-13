using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class CreateTeamPage : ContentPage
	{
		string TeamNameEntry = "";
		public CreateTeamPage()
		{
			InitializeComponent();
			this.Title = "Create Team";
		}
		private async void OnCreateClicked(object sender, EventArgs e)
		{
			await Database.AddTeam(TeamNameEntry,Database.FacebookProfile.Id,"");
			await Database.LoadTeamData();
			Navigation.PopAsync();
		}

		void OnTextCompleted(object sender, EventArgs args)
		{
			TeamNameEntry = ((Entry)sender).Text;
		}
		void OnTextChanged(object sender, TextChangedEventArgs args)
		{
			TeamNameEntry = args.NewTextValue;
		}


	}
}
