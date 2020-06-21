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

    public class FunctionsRS
    {
        Rs3API.Rs3API Rs3API = new Rs3API.Rs3API();
        MySqlFunctions MySqlFunctions = new MySqlFunctions();
        static SettingsFolder.Settings settings = new SettingsFolder.Settings();
        static FunctionsRS()
        {

            SettingsFolder.TableCreatorClass tableCreatorClass = new SettingsFolder.TableCreatorClass();
            tableCreatorClass.createTables(settings.Rs3PlayerTable);
            tableCreatorClass.createTables(settings.Rs3PlayerSkillsTable);
            tableCreatorClass.createTables(settings.Rs3PlayerSkillGainzTable);
            tableCreatorClass.createTables(settings.Rs3Player_DiscordAccTable);
        }

        public async Task<MyClasses.Rs3Player> RegisterPlayer(string Username)
        {
            MyClasses.Rs3Player rs3PlayerAPI = await Rs3API.GetRs3Player(Username);
            MyClasses.Rs3Player rs3PlayerDB = MySqlFunctions.GetRs3PlayerDB(Username);
            if (rs3PlayerAPI == null)
            {
                rs3PlayerAPI = new MyClasses.Rs3Player();
                rs3PlayerAPI.Error = "Player \"" + Username + "\" Doesn's Exist in the RS Database!";
                return rs3PlayerAPI;
            }
            else
            {
                if (rs3PlayerDB != null)
                {
                    rs3PlayerDB = new MyClasses.Rs3Player();
                    rs3PlayerDB.Error = "Player \"" + Username + "\" is Already Being Tracked!";
                    return rs3PlayerDB;
                }
                else
                {
                    MySqlFunctions.InsertIntoDBPlayers(rs3PlayerAPI, true);
                    return rs3PlayerAPI;
                }
            }
        }

        public async Task<MyClasses.Rs3Player> GetCurrentStats(string Username)
        {
            MyClasses.Rs3Player rs3PlayerAPI = await Rs3API.GetRs3Player(Username);
            if (rs3PlayerAPI != null)
            {
                return rs3PlayerAPI;
            }
            else
            {
                rs3PlayerAPI = new MyClasses.Rs3Player();
                rs3PlayerAPI.Error = "Player \"" + Username + "\" Doesn's Exist in the RS Database!";
                return rs3PlayerAPI;
            }
        }



            public async Task<MyClasses.Rs3Player> Calculate(string Username)
        {
            MyClasses.Rs3Player rs3PlayerAPI = await Rs3API.GetRs3Player(Username);
            MyClasses.Rs3Player rs3PlayerDB = MySqlFunctions.GetRs3PlayerDB(Username);
            MyClasses.Rs3Player rs3PlayerGainz = new MyClasses.Rs3Player();
            if (rs3PlayerAPI == null)
            {
                rs3PlayerAPI = new MyClasses.Rs3Player();
                rs3PlayerAPI.Error = "Player \"" + Username + "\" Doesn's Exist in the RS Database!";
                return rs3PlayerAPI;
            }
            if (rs3PlayerDB == null)
            {
                //New User Add him to DB
                //MySqlFunctions.InsertIntoDBPlayers(rs3PlayerAPI, true);
                rs3PlayerDB = new MyClasses.Rs3Player();
                rs3PlayerDB.Error = "Player \"" + Username + "\" Doesn's Exist in the Bot Database!";
                return rs3PlayerDB;
            }
            else
            {
                MyClasses.Rs3Player rs3PlayerGainzInsert = new MyClasses.Rs3Player();
                rs3PlayerGainzInsert.Name = Username;
                rs3PlayerGainzInsert.Skillvalues = new List<MyClasses.skillvalues>();
                //old user Calculate and Update Info
                foreach (MyClasses.skillvalues skillvaluesAPI in rs3PlayerAPI.Skillvalues)
                {
                    foreach (MyClasses.skillvalues skillvaluesDB in rs3PlayerDB.Skillvalues)
                    {
                        if (skillvaluesAPI.ID == skillvaluesDB.ID)
                        {
                            MyClasses.skillvalues skillvaluesGainz = new MyClasses.skillvalues();
                            skillvaluesGainz = skillvaluesDB;
                            skillvaluesGainz.Xp = skillvaluesAPI.Xp - skillvaluesDB.Xp;
                            rs3PlayerGainzInsert.Skillvalues.Add(skillvaluesGainz);
                        }
                    }
                }
                MySqlFunctions.InsertIntoDBPlayerGainz(rs3PlayerGainzInsert);
                //MySqlFunctions.InsertIntoDBPlayers(rs3PlayerAPI, false);

                rs3PlayerGainz = MySqlFunctions.GetRs3PlayerGainz(Username);

            }
            return rs3PlayerGainz;
        }
    }
}
