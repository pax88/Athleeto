using System;
public interface PushNotifications
{
	void SheduleNotification(double seconds,string title,string body); //note that interface members are public by default
	void ClearNotifications();
}