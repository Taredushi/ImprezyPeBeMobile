using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;

namespace EventsPbMobile.Models
{
    public class Place : RealmObject
    {
        [PrimaryKey]
        public int PlaceId { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }

        public string ShortName
        {
            get
            {
                var splitted = Name.Split(' ');
                if (splitted.Length == 1 && splitted[0].Length<=3)
                    return Name;
                var shorted = new StringBuilder();
                foreach (var s in splitted)
                {
                    shorted.Append(s[0]);
                }
                return shorted.ToString();  
            }
        }

        public IList<Activity> Activities { get; }
    }
}