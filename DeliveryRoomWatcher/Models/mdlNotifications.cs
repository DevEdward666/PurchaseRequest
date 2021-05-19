using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryRoomWatcher.Models
{
    public class mdlNotifications
    {
        public int offset { get; set; }
        public string name { get; set; }
        public class notifbydept
        {
            public string dept { get; set; }
            public int offset { get; set; }

        }
        public class searchNotif
        {
            public string priority { get; set; }
            public string title { get; set; }
            public int offset { get; set; }
        }
        public class createnotifications
        {
            public string title { get; set; }
            public string body { get; set; }
            public string priority { get; set; }
            public string audience { get; set; }
            public string created_by { get; set; }
        }
        public class NotificationPost
        {
            public virtual string Notification { get; set; }
            public virtual string from { get; set; }
            public virtual string to { get; set; }
            public virtual string type { get; set; }
        }
    }
}
