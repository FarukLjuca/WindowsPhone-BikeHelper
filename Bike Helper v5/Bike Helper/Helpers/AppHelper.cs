using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Bike_Helper.Helpers
{
    public class AppHelper
    {
        private static ToastNotification toast;
        private static Timer timer;

        public static void showToast(String message)
        {
            var toastTemplate = ToastTemplateType.ToastImageAndText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));
            toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(2);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

            timer = new Timer(_ => HideToast(), null, new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 2));
        }

        private static void HideToast()
        {
            ToastNotificationManager.CreateToastNotifier().Hide(toast);
            timer.Dispose();
        }
    }
}
