using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace EventsPbMobile.Models
{
    class Settings :RealmObject
    {
        [PrimaryKey]
        public int SettingsId { get; set; }
        public bool NotificationsEnabled { get; set; }
        public bool Notify1HBefore { get; set; }
        public bool Notify1DBefore { get; set; }
        public bool Notify2DBefore { get; set; }

        public DateTimeOffset LastRefreshDate { get; set; }

        public Settings()
        {
            
        }

        public Settings(Settings s)
        {
            SettingsId = s.SettingsId;
            NotificationsEnabled = s.NotificationsEnabled;
            Notify1HBefore = s.Notify1HBefore;
            Notify1DBefore = s.Notify1DBefore;
            Notify2DBefore = s.Notify2DBefore;
            LastRefreshDate = s.LastRefreshDate;
        }
    }
}
