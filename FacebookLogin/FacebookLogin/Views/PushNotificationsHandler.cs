using System;
using System.Collections.Generic;
using System.Globalization;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Plugin.LocalNotifications;
using Xamarin.Forms;

public class PushNotificationsHandler
	{
		public static PushNotificationsHandler _instance=null;
		public static PushNotificationsHandler instance
		{
			get
			{
				if(_instance == null)
					_instance = new PushNotificationsHandler();
				return _instance;
			}
		}

		public PushNotificationsHandler()
		{

		}
		static int id = 0;
		public void UpdatePushNotifcaions()
		{
			
		if (Device.OS == TargetPlatform.iOS)
		{ 
		
		}
		else
		{ 
			id = 0;
			for (int i = 0; i < 100; i++)
			{
				CrossLocalNotifications.Current.Cancel(i);
			}
		}
			
			Xamarin.Forms.DependencyService.Get<PushNotifications>().ClearNotifications();

			List<FacebookLogin.Training> allTrainings = FacebookLogin.Database.GetAllTrainingsUser();
			for (int i = 0; i < allTrainings.Count; i++)
			{
			DateTime date = DateTime.ParseExact(allTrainings[i].EndDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

				TimeSpan diff = date.Subtract(DateTime.Now);
				if (allTrainings[i].HasBeenEvaluated == false)
				{
					if (diff.Days < 7 && DateTime.Compare(date, DateTime.Now) >= 0)
					{
						AddNotification(date.AddMinutes(15));
						AddNotification(date.AddMinutes(120));
					}
				}
			}

		}


		void AddNotification(DateTime aDate)
		{ 
			TimeSpan diff = aDate.Subtract(DateTime.Now);
			Random rand1 = new Random();
		if (Device.OS == TargetPlatform.iOS)
		{
			Xamarin.Forms.DependencyService.Get<PushNotifications>().SheduleNotification(diff.TotalSeconds, "Reminder", "You have to evaluate your pratice!");
		}
		else
		{
	
			CrossLocalNotifications.Current.Show("Reminder", "You have to evaluate your pratice!", id, aDate.ToUniversalTime());
			id++;
		}



			//// This is if you want to do global notification via OneSignal backend
			//OneSignal.Current.IdsAvailable((playerID, pushToken) =>
			//{
			//	string userId = playerID;
			//	var notification = new Dictionary<string, object>();
			//	notification["contents"] = new Dictionary<string, string>() { { "en", "Test Message" } };

			//				notification["include_player_ids"] = new List<string>() { userId };
			//				// Example of scheduling a notification in the future.
			//				notification["send_after"] = aTime.ToUniversalTime().ToString("U");

			//	OneSignal.Current.PostNotification(notification);
			//});
		}
	}

