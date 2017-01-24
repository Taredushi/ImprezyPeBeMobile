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
        void ShowNotification(string title, string text);
        void StartService();
    }
}
