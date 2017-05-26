//using System;
//using Android;
//using Android.App;
//using Android.Content;

//public class NotifyServAndroid : INotifyServ
//{
//	public void MyLocalNotification(string title, string text, DateTime time)
//	{
//		// Instantiate the builder and set notification elements:
//		Notification.Builder builder = new Notification.Builder(Application.Context)
//.SetContentTitle(title).SetContentText(text);

//		// Build the notification:
//		Notification notification = builder.Build();

//		// Get the notification manager:
//		NotificationManager notificationManager =
//		Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

//		// Publish the notification:
//		const int notificationId = 0;
//		notificationManager.Notify(notificationId, notification);

//		builder.SetWhen(time.Millisecond);
//	}
//}

//interface INotifyServ
//{
//	void MyLocalNotification(string title, string text, DateTime time);
//}