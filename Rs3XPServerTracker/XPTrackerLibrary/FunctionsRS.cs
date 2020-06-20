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
        }

        public async void Calculate(string Username)
        {
            MyClasses.Rs3Player rs3PlayerAPI = await Rs3API.GetRs3Player(Username);
            MyClasses.Rs3Player rs3PlayerDB = MySqlFunctions.GetRs3PlayerDB(Username);
            if (rs3PlayerAPI == null)
            {
                return;
            }
            if (rs3PlayerDB == null)
            {
                MySqlFunctions.InsertIntoDB(rs3PlayerAPI, true);
            }
            else
            {
                for





                MySqlFunctions.InsertIntoDB(rs3PlayerAPI, false);
            }



        }



    }
}
