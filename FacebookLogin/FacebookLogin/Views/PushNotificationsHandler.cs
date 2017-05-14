using System;
using System.Collections.Generic;
using System.Globalization;
using Com.OneSignal;
using Com.OneSignal.Abstractions;

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

		public void UpdatePushNotifcaions()
		{

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

			Xamarin.Forms.DependencyService.Get<PushNotifications>().SheduleNotification( diff.TotalSeconds, "Reminder", "You have to evaluate your pratice!");

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

