using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPbMobile.Classes
{
    public interface INotification
    {
        void SetAlarm(string title, string text, int eventId, DateTimeOffset eventStartDate);
        void CancelAlarm(int eventId);
        void StartService();
    }
}
