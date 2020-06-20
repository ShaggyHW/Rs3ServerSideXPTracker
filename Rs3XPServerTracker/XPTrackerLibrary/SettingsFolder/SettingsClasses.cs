using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTrackerLibrary.SettingsFolder
{
    public class MySqlSettings
    {
        public string ip { get; set; }
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class SettingsClasses
    {
        public MySqlSettings mysqlSettings = new MySqlSettings();
    }
}
