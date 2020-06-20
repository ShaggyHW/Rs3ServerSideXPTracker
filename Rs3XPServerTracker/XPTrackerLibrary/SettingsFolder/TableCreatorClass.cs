using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace XPTrackerLibrary.SettingsFolder
{
    public class TableCreatorClass
    {
        Settings settings = new Settings();
        public void createTables(string table)
        {
            var mysqlSettings = settings.GetMySqlSettings();
            string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", mysqlSettings.ip, mysqlSettings.database, mysqlSettings.username, mysqlSettings.password);
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string query = "SELECT * FROM information_schema.tables WHERE table_schema = '"+mySqlConnection.Database+"' AND table_name = '"+table+"' LIMIT 1";
            var cmd = new MySqlCommand(query, mySqlConnection);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                if(table == "tbl_RS3Player")
                {
                    mySqlConnection.Close();
                    mySqlConnection.Open();                    
                    query = "CREATE TABLE "+table+" (Name varchar(255), Rank varchar(255), LoggedIn varchar(255))";
                    cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.ExecuteNonQuery();
                }
                if(table == "tbl_Rs3PlayerSkillsTable")
                {
                    mySqlConnection.Close();
                    mySqlConnection.Open();
                    query = "CREATE TABLE " + table + " (Name varchar(255), Level varchar(255), Xp varchar(255), Rank varchar(255), ID int)";
                    cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
