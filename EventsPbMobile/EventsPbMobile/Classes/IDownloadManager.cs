using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPbMobile.Classes
{
    public interface IDownloadManager
    {
        void Download(string uri, string filename);
    }
}
