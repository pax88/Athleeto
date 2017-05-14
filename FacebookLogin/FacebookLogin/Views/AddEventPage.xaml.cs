using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FacebookLogin
{
	public partial class AddEventPage : ContentPage
	{
		public NavigationPage nav;
		public INavigation NavigationUsed;
		public AddEventPage()
		{
			InitializeComponent();

			EventDate.Date = HomePage.SelectedDate;
			EventTime.Time = DateTime.Now.TimeOfDay;
			EventTimeEnding.Time = DateTime.Now.TimeOfDay;
			//this.Title = "Add Event";

			//NavigationPage.SetHasBackButton(this, false);
			//NavigationPage.SetHasNavigationBar(this, false);



			//((NavigationPage)(this)).BarBackgroundColor = Color.FromHex("#efeeed");
			//((TimePicker)(EventTime))
		}

		private async void ToggledSwitch(object sender, EventArgs e)
		{
			if (Database.CurrentAthlete.isCoach == false)
			{
				EventTeamEvent.IsToggled = false;
			}

		}


		private async void OnBackEventClicked(object sender, EventArgs e)
		{ 
			await Navigation.PopModalAsync();
		}
		private async void OnAddEventClicked(object sender, EventArgs e)
		{
			if (EventTime.Time.Hours == EventTimeEnding.Time.Hours)
				if (EventTime.Time.Minutes == EventTimeEnding.Time.Minutes)
					return;

			if (EventTime.Time.Hours > EventTimeEnding.Time.Hours)
				return;
			
			DateTime date = new DateTime(EventDate.Date.Year,EventDate.Date.Month,EventDate.Date.Day, ((TimePicker)(EventTime)).Time.Hours,((TimePicker)(EventTime)).Time.Minutes,0);

			DateTime dateEnd = new DateTime(EventDate.Date.Year, EventDate.Date.Month, EventDate.Date.Day, ((TimePicker)(EventTimeEnding)).Time.Hours, ((TimePicker)(EventTimeEnding)).Time.Minutes, 0);

			TimeSpan diff = dateEnd.Subtract(date);
			bool isTeamEvent = ((Switch)(EventTeamEvent)).IsToggled;

			await Database.AddTraining(Database.FacebookProfile.Id, Database.CurrentAthlete.TeamID.ToString() ,EventName.Text , date.ToString("yyyyMMddHHmmss"),  dateEnd.ToString("yyyyMMddHHmmss"),diff.TotalMinutes.ToString(), isTeamEvent);

			//await Navigation.PopAsync(true);
			//Navigation.RemovePage(this);
			//await Navigation.pop();
			await Navigation.PopModalAsync();
			//NavigationUsed.RemovePage(nav.CurrentPage);

			PushNotificationsHandler.instance.UpdatePushNotifcaions();


		}

	}
}
