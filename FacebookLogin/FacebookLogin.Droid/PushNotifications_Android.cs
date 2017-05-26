using System;
using Android;
using Android.App;
using Android.Content;
using Plugin.LocalNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotifications_Android))]
public class PushNotifications_Android : PushNotifications
	{
		public void ClearNotifications()
		{

		}
		public void SheduleNotification(double seconds, string title, string body)
		{
			CrossLocalNotifications.Current.Show(title, body, 101, DateTime.Now.AddSeconds(15));

			
		}

	
	}
