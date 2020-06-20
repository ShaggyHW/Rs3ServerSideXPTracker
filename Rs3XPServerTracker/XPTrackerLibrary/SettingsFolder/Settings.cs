using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using IniParser.Parser;

namespace XPTrackerLibrary.SettingsFolder
{
    public class Settings
    {
        public string Rs3PlayerTable = "tbl_RS3Player";
        public string Rs3PlayerSkillsTable = "tbl_Rs3PlayerSkillsTable";
        public SettingsClasses settingsClasses = new SettingsClasses();

        public class Values
        {
            public string option { get; set; }
            public string value { get; set; }
        }



        public Values getValues()
        {
            Values values = new Values();


            return values;
        }

        public void SaveMySQLSettings(MySqlSettings mysqlSettings)
        {
            var executingPath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string iniFileDirectory = new FileInfo(executingPath.AbsolutePath).Directory.ToString() + "\\Config";
            if (!Directory.Exists(iniFileDirectory))
            {
                Directory.CreateDirectory(iniFileDirectory);
            }
            string iniFile = iniFileDirectory + "\\config.ini";
            if (!File.Exists(iniFile))
            {
                var x = File.Create(iniFile);
                x.Close();
            }
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();
            iniData["SQLSettings"]["ip"] = mysqlSettings.ip;
            iniData["SQLSettings"]["database"] = mysqlSettings.database;
            iniData["SQLSettings"]["password"] = mysqlSettings.password;
            iniData["SQLSettings"]["username"] = mysqlSettings.username;
            parser.WriteFile(iniFile, iniData);
        }

        public MySqlSettings GetMySqlSettings()
        {
            var executingPath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string iniFileDirectory = new FileInfo(executingPath.AbsolutePath).Directory.ToString() + "\\Config";
            string iniFile = iniFileDirectory + "\\config.ini";
            MySqlSettings mySqlSettings = new MySqlSettings();
            if (File.Exists(iniFile))
            {
                
                var parser = new FileIniDataParser();
                IniData iniData = parser.ReadFile(iniFile);
                foreach (SectionData section in iniData.Sections)
                {
                    foreach (KeyData key in section.Keys)
                    {
                        if (key.KeyName == "ip")
                        {
                            mySqlSettings.ip = key.Value;
                        }

                        if (key.KeyName == "database")
                        {
                            mySqlSettings.database = key.Value;
                        }
                        if (key.KeyName == "password")
                        {
                            mySqlSettings.password = key.Value;
                        }
                        if (key.KeyName == "username")
                        {
                            mySqlSettings.username = key.Value;
                        }
                    }
                }

                return mySqlSettings;
            }
            else
            {
                return null;
            }
        }
    }
}
