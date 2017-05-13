using System;
using System.Collections.Generic;
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

		public void SheduleNotification()
		{
		OneSignal.Current.IdsAvailable((playerID, pushToken) =>
		{
	
			string userId = playerID;
			var notification = new Dictionary<string, object>();
			notification["contents"] = new Dictionary<string, string>() { { "en", "Test Message" } };

			notification["include_player_ids"] = new List<string>() { userId };
			// Example of scheduling a notification in the future.
			notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U");

			OneSignal.Current.PostNotification(notification);



		});



		}
	}

