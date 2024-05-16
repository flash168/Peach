using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;


namespace PeachPlayer
{
    public class NotificationManager
    {

        private WindowNotificationManager _manager;
        public NotificationManager(TopLevel level)
        {
            _manager = new WindowNotificationManager(level) { MaxItems = 3 };
        }

        public void Show(string content, string title = "提示", NotificationType type = NotificationType.Information)
        {
            _manager?.Show(new Notification(title, content, type));
        }
    }
}

