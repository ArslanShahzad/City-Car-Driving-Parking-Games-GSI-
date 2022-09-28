using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        var notification = new AndroidNotification();
        notification.Title = "New Car Parking Environment is Ready!";
        notification.Text = "Drive Be Careful While Driving in Real Parking";
        notification.FireTime = System.DateTime.Now.AddDays(1);
        notification.SmallIcon = "icon_1";
        notification.LargeIcon = "icon_0";
        notification.RepeatInterval = new System.TimeSpan(24,0,0);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");


    }


}
