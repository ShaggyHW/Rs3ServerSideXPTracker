using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace XPTrackerLibrary
{

    public class FunctionsRS : Control
    {
        Rs3API.Rs3API Rs3API = new Rs3API.Rs3API();
        MySqlFunctions MySqlFunctions = new MySqlFunctions();
        static SettingsFolder.Settings settings = new SettingsFolder.Settings();
        static FunctionsRS()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FunctionsRS), new FrameworkPropertyMetadata(typeof(FunctionsRS)));
            SettingsFolder.TableCreatorClass tableCreatorClass = new SettingsFolder.TableCreatorClass();
            tableCreatorClass.createTables(settings.Rs3PlayerTable);
            tableCreatorClass.createTables(settings.Rs3PlayerSkillsTable);
            tableCreatorClass.createTables(settings.Rs3PlayerSkillGainzTable);
        }

        public async Task<MyClasses.Rs3Player> Calculate(string Username)
        {
            MyClasses.Rs3Player rs3PlayerAPI = await Rs3API.GetRs3Player(Username);
            MyClasses.Rs3Player rs3PlayerDB = MySqlFunctions.GetRs3PlayerDB(Username);
            if (rs3PlayerAPI == null)
            {
                return null;
            }
            if (rs3PlayerDB == null)
            {
                //New User Add him to DB
                MySqlFunctions.InsertIntoDBPlayers(rs3PlayerAPI, true);
                return null;
            }
            else
            {
                MyClasses.Rs3Player rs3PlayerGainz = new MyClasses.Rs3Player();
                rs3PlayerGainz.Name = Username;
                rs3PlayerGainz.Skillvalues = new List<MyClasses.skillvalues>();
                //old user Calculate and Update Info
                foreach (MyClasses.skillvalues skillvaluesAPI in rs3PlayerAPI.Skillvalues)
                {
                    foreach(MyClasses.skillvalues skillvaluesDB in rs3PlayerDB.Skillvalues)
                    {
                        if (skillvaluesAPI.ID == skillvaluesDB.ID)
                        {
                            MyClasses.skillvalues skillvaluesGainz = new MyClasses.skillvalues();
                            skillvaluesGainz = skillvaluesDB;
                            skillvaluesGainz.Xp = skillvaluesAPI.Xp - skillvaluesDB.Xp;
                            rs3PlayerGainz.Skillvalues.Add(skillvaluesGainz);
                        }
                    }
                }
                MySqlFunctions.InsertIntoDBPlayerGainz(rs3PlayerGainz);
                //MySqlFunctions.InsertIntoDBPlayers(rs3PlayerAPI, false);

                var gainz = MySqlFunctions.GetRs3PlayerGainz(Username);
                return gainz;
            }
                       
        }
    }
}
