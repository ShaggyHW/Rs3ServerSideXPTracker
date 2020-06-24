using MySql.Data.MySqlClient;
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
        #region NewVersion
        public string DelBotHosts(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotHostsTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                mySqlConnection.Close();
                query = "DELETE From " + settings.BotHostsTable + " Where DiscordID='" + discordID + "'";
                mySqlConnection.Open();
                cmd = new MySqlCommand(query, mySqlConnection);
                cmd.ExecuteReader();

                return " Was Removed from Hosts";
            }
            mySqlConnection.Close();

            return " Was Not an Host";
        }
        public bool GetBotHosts(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotHostsTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            mySqlConnection.Close();
            return false;
        }
        public string AddBotHosts(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotHostsTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return " is already Host";
            }
            mySqlConnection.Close();
            mySqlConnection.Open();
            query = "Insert Into " + settings.BotHostsTable + " (DiscordID) " +
                 "VALUES ('" + discordID + "')";
            cmd = new MySqlCommand(query, mySqlConnection);
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            return " added to Host Group";
        }
        public string DelBotAdmins(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotAdminTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                mySqlConnection.Close();
                query = "DELETE From " + settings.BotAdminTable + " Where DiscordID='" + discordID + "'";
                mySqlConnection.Open();
                cmd = new MySqlCommand(query, mySqlConnection);
                cmd.ExecuteReader();

                return " Was Removed from Admins";
            }
            mySqlConnection.Close();

            return " Was Not an Admin";
        }
        public bool GetBotAdmins(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotAdminTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            mySqlConnection.Close();
            return false;
        }
        public string AddBotAdmins(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.BotAdminTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return " is already Admin";
            }
            mySqlConnection.Close();
            mySqlConnection.Open();
            query = "Insert Into " + settings.BotAdminTable + " (DiscordID) " +
                 "VALUES ('" + discordID + "')";
            cmd = new MySqlCommand(query, mySqlConnection);
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            return " added to Admin Group";
        }
        public void CreateLink_DiscordAcc_Rs3Acc(string rs3name, string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "Delete From " + settings.Rs3Player_DiscordAccTable + " Where DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            mySqlConnection.Open();
            query = "Insert Into " + settings.Rs3Player_DiscordAccTable + " (Username,DiscordID) " +
                "VALUES ('" + rs3name + "','" + discordID + "')";
            cmd = new MySqlCommand(query, mySqlConnection);
            reader = cmd.ExecuteNonQuery();
            mySqlConnection.Close();

        }
        public string GetLinkedAccount(string discordID)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Close();
            mySqlConnection.Open();
            string query = "SELECT username FROM " + settings.Rs3Player_DiscordAccTable + " WHERE DiscordID='" + discordID + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return reader.GetString(0);
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public void InsertIntoDBPlayers(MyClasses.Rs3Player rs3Player)
        {
            //New User
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);


            mySqlConnection.Open();
            string query = "Insert Into " + settings.Rs3PlayerTable + " (Name) VALUES ('" + rs3Player.Name.ToLower() + "')";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteNonQuery();
            mySqlConnection.Close();

            foreach (MyClasses.skillvalues skillvalues in rs3Player.Skillvalues)
            {
                mySqlConnection.Open();
                query = "Insert Into " + settings.Rs3PlayerSkillsTable + " (Username,Name,Level,XP,Rank,ID,SyncTime) " +
                    "VALUES ('" + rs3Player.Name.ToLower() + "','" + skillvalues.Name + "','" + skillvalues.Level + "','" + skillvalues.Xp + "','" + skillvalues.Rank + "','" + skillvalues.ID + "','" + rs3Player.SyncTime + "')";
                cmd = new MySqlCommand(query, mySqlConnection);
                reader = cmd.ExecuteNonQuery();
                mySqlConnection.Close();
            }
        }
        public void InsertIntoDBPlayerGainz(MyClasses.Rs3Player rs3Player)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            mySqlConnection.Open();
            string query = "Delete From " + settings.Rs3PlayerSkillGainzTable + " Where Username='" + rs3Player.Name + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteNonQuery();
            mySqlConnection.Close();

            foreach (MyClasses.skillvalues skillvalues in rs3Player.Skillvalues)
            {
                mySqlConnection.Open();
                query = "Insert Into " + settings.Rs3PlayerSkillGainzTable + " (Username,name,Level,XP,Rank,ID,SyncTime) " +
                    "VALUES ('" + rs3Player.Name.ToLower() + "','" + skillvalues.Name + "','" + skillvalues.Level + "','" + skillvalues.Xp + "','" + skillvalues.Rank + "','" + skillvalues.ID + "','" + rs3Player.SyncTime + "')";
                cmd = new MySqlCommand(query, mySqlConnection);
                reader = cmd.ExecuteNonQuery();
                mySqlConnection.Close();
            }
        }
        public MyClasses.Rs3Player GetRs3PlayerGainz(string name)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            MyClasses.Rs3Player rs3Player = new MyClasses.Rs3Player();
            rs3Player.Skillvalues = new List<MyClasses.skillvalues>();
            MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString);
            mySqlConnection2.Open();
            string query = "SELECT ID,Level,Name,Rank,Xp,Username,SyncTime FROM " + settings.Rs3PlayerSkillGainzTable + " WHERE username = '" + name + "'";
            var cmd = new MySqlCommand(query, mySqlConnection2);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MyClasses.skillvalues skillvalues = new MyClasses.skillvalues();
                    skillvalues.ID = Convert.ToInt32(reader.GetString(0));
                    skillvalues.Level = Convert.ToInt16(reader.GetString(1));
                    skillvalues.Name = reader.GetString(2);
                    skillvalues.Rank = Convert.ToInt64(reader.GetString(3));
                    skillvalues.Xp = Convert.ToInt32(reader.GetString(4));
                    rs3Player.Name = reader.GetString(5);
                    rs3Player.SyncTime = reader.GetString(6);
                    rs3Player.Skillvalues.Add(skillvalues);
                }
                mySqlConnection2.Close();
                return rs3Player;
            }
            mySqlConnection.Close();
            return null;
        }
        #endregion





        #region WIP
        public MyClasses.Rs3Player GetRs3PlayerDB(string name)
        {

            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Close();
            mySqlConnection.Open();
            string query = "SELECT ID,Level,name,Rank,Xp,SyncTime FROM " + settings.Rs3PlayerSkillsTable + " WHERE Username='" + name + "' order by SyncTime Desc";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                MyClasses.Rs3Player rs3Player = new MyClasses.Rs3Player();
                rs3Player.Name = name;
                rs3Player.Skillvalues = new List<MyClasses.skillvalues>();

                while (reader.Read())
                {
                    MyClasses.skillvalues skillvalues = new MyClasses.skillvalues();
                    skillvalues.ID = Convert.ToInt32(reader.GetString(0));
                    skillvalues.Level = Convert.ToInt16(reader.GetString(1));
                    skillvalues.Name = reader.GetString(2);
                    skillvalues.Rank = Convert.ToInt64(reader.GetString(3));
                    skillvalues.Xp = Convert.ToInt64(reader.GetString(4));
                    rs3Player.Skillvalues.Add(skillvalues);
                    rs3Player.SyncTime = reader.GetString(5);

                }
                mySqlConnection.Close();
                return rs3Player;
            }
            mySqlConnection.Close();
            return null;
        }

        public MyClasses.Rs3Player GetRs3PlayerDBSince(string name, string since)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Close();
            mySqlConnection.Open();
            string query = "SELECT ID,Level,name,Rank,Xp,SyncTime FROM " + settings.Rs3PlayerSkillsTable + " WHERE Username='" + name + "' And SyncTime Like '"+since+"' order by SyncTime Desc";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                MyClasses.Rs3Player rs3Player = new MyClasses.Rs3Player();
                rs3Player.Name = name;
                rs3Player.Skillvalues = new List<MyClasses.skillvalues>();

                while (reader.Read())
                {
                    MyClasses.skillvalues skillvalues = new MyClasses.skillvalues();
                    skillvalues.ID = Convert.ToInt32(reader.GetString(0));
                    skillvalues.Level = Convert.ToInt16(reader.GetString(1));
                    skillvalues.Name = reader.GetString(2);
                    skillvalues.Rank = Convert.ToInt64(reader.GetString(3));
                    skillvalues.Xp = Convert.ToInt64(reader.GetString(4));
                    rs3Player.Skillvalues.Add(skillvalues);
                    rs3Player.SyncTime = reader.GetString(5);

                }
                mySqlConnection.Close();
                return rs3Player;
            }
            mySqlConnection.Close();
            return null;
        }
        #endregion

        #region OldVersion
        public string CreateSkillingComp(MyClasses.CompSettings compSettings)
        {


            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * From " + settings.SkillingCompTable + " Where CompName='" + compSettings.Name + "'";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return "Already Exists";
            }
            mySqlConnection.Close();
            mySqlConnection.Open();
            query = "Insert Into " + settings.SkillingCompTable + " (CompName,Status,EndDate,StartDate) " +
                 "VALUES ('" + compSettings.Name + "','" + compSettings.status + "','" + compSettings.end + "','" + compSettings.start + "')";
            cmd = new MySqlCommand(query, mySqlConnection);
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            return "Was Created";
        }




        #endregion
    }
}
