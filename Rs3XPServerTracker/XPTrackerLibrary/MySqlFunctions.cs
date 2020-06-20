﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTrackerLibrary
{
    public class MySqlFunctions
    {
        SettingsFolder.Settings settings = new SettingsFolder.Settings();

        public void InsertIntoDB(MyClasses.Rs3Player rs3Player, bool isNew)
        {
            if (isNew)
            {
                //New User
                var mysqlSettings = settings.GetMySqlSettings();
                string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                string query = "Insert Into " + settings.Rs3PlayerTable + " (Name,Rank,LoggedIN) VALUES ('" + rs3Player.Name.ToLower() + "','" + rs3Player.Rank + "','" + rs3Player.LoggedIn + "')";
                var cmd = new MySqlCommand(query, mySqlConnection);
                var reader = cmd.ExecuteNonQuery();
                mySqlConnection.Close();

                foreach (MyClasses.skillvalues skillvalues in rs3Player.Skillvalues)
                {
                    mySqlConnection.Open();
                    query = "Insert Into " + settings.Rs3PlayerSkillsTable + " (Username,Name,Level,XP,Rank,ID) " +
                        "VALUES ('"+rs3Player.Name.ToLower()+"','" + skillvalues.Name + "','" + skillvalues.Level + "','" + skillvalues.Xp + "','" + skillvalues.Rank + "','"+ skillvalues .ID+ "')";
                    cmd = new MySqlCommand(query, mySqlConnection);
                    reader = cmd.ExecuteNonQuery();
                    mySqlConnection.Close();
                }
            }
            else
            {
                //Update User
            }
        }

        public MyClasses.Rs3Player GetRs3PlayerDB(string name)
        {
            
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT Name,Rank,LoggedIn FROM " + settings.Rs3PlayerTable + " WHERE Name = '" + name + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                MyClasses.Rs3Player rs3Player = new MyClasses.Rs3Player();
                rs3Player.Name = reader.GetString(0);
                rs3Player.Rank = reader.GetString(1);
                rs3Player.LoggedIn = reader.GetString(2);
                rs3Player.Skillvalues = new List<MyClasses.skillvalues>();

                MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString);
                mySqlConnection2.Open();
                string query2 = "SELECT ID,Level,Name,Rank,Xp FROM " + settings.Rs3PlayerSkillsTable + " WHERE Name = '" + name + "'";
                var cmd2 = new MySqlCommand(query2, mySqlConnection2);
                var reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        MyClasses.skillvalues skillvalues = new MyClasses.skillvalues();
                        skillvalues.ID = Convert.ToInt32(reader2.GetString(0));
                        skillvalues.Level = Convert.ToInt16(reader2.GetString(1));
                        skillvalues.Name = reader2.GetString(2);
                        skillvalues.Rank = Convert.ToInt64(reader2.GetString(3));
                        skillvalues.Xp = Convert.ToInt32(reader2.GetString(4));
                        rs3Player.Skillvalues.Add(skillvalues);
                    }
                }
                mySqlConnection2.Close();
                return rs3Player;
            }
            mySqlConnection.Close();
            return null;
        }

    }
}
