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
using XPTrackerLibrary;
using XPTrackerLibrary.SettingsFolder;
using Newtonsoft;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Rs3TrackerManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings settings = new Settings();

        FunctionsRS FunctionsRS = new FunctionsRS();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlSettings mySqlSettings = new MySqlSettings();
            mySqlSettings.database = MYSQLDB.Text;
            mySqlSettings.ip = MYSQLIP.Text;
            mySqlSettings.username = MYSQLUSER.Text;
            mySqlSettings.password = MYSQLPW.Text;

            settings.SaveMySQLSettings(mySqlSettings);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            var BotPath = new FileInfo(location.AbsolutePath).Directory.FullName+ "\\DiscordBot.exe";
            Process.Start(BotPath);
        }
    }
}
