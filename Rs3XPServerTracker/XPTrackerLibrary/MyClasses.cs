using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTrackerLibrary.MyClasses
{
    public class Rs3Player
    {
        public string Name { get; set; }
        public List<skillvalues> Skillvalues { get; set; }
    }

    public class skillvalues
    {
        public short Level { get; set; }
        public long Xp { get; set; }
        public long Rank { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
