using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Foundation;
using FacebookLogin.iOS.View.Controls;




[assembly: Xamarin.Forms.Dependency(typeof(PushNotifications_iOS))]
public class PushNotifications_iOS : PushNotifications
	{

		public void ClearNotifications()
		{
		UIApplication.SharedApplication.CancelAllLocalNotifications();	
		}
		public void SheduleNotification(double seconds,string title,string body)
		{
				// create the notification
				var notification = new UILocalNotification();

			// set the fire date (the date time in which it will fire)
			//notification.FireDate = DateTimeToNSDate(aTime);
			notification.FireDate =NSDate.FromTimeIntervalSinceNow(seconds);

			    // configure the alert
			    notification.AlertAction = title;
			    notification.AlertBody = body;

			    // modify the badge
			    notification.ApplicationIconBadgeNumber = 1;

			    // set the sound to be the default sound
			    notification.SoundName = UILocalNotification.DefaultSoundName;

			    // schedule it
			    UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

	}
