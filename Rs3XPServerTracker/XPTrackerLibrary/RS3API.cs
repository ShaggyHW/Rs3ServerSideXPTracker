using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace XPTrackerLibrary.Rs3API
{
    public class Rs3API
    {
        public async Task<MyClasses.Rs3Player> GetRs3Player(string Username)
        {
            var restClient = new RestClient("https://secure.runescape.com/m=hiscore/index_lite.ws");
            var request = new RestRequest("?player=" + Username, DataFormat.Json);

            var response = restClient.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            MyClasses.Rs3Player rs3Player = new MyClasses.Rs3Player();
            rs3Player.Name = Username;
            rs3Player.Skillvalues = new List<MyClasses.skillvalues>();
            StringReader stringReader = new StringReader(response.Content);
            int i = 0;
            while (true)
            {
                string line = stringReader.ReadLine();
                if (line != null)
                {
                    MyClasses.skillvalues skillvalues = new MyClasses.skillvalues();
                    skillvalues.ID = i;
                    skillvalues.Rank = Convert.ToInt64(line.Split(',')[0]);
                    skillvalues.Level = Convert.ToInt16(line.Split(',')[1]);
                    skillvalues.Xp = Convert.ToInt64(line.Split(',')[2]);
                    rs3Player.Skillvalues.Add(skillvalues);
                }
                else
                {
                    break;
                }
                i++;
                if (i > 28)
                    break;
            }





            foreach (MyClasses.skillvalues skillvalues in rs3Player.Skillvalues)
            {
                switch (skillvalues.ID)
                {
                    case 0:
                        skillvalues.Name = "Overall";
                        break;
                    case 1:
                        skillvalues.Name = "Attack";
                        break;
                    case 2:
                        skillvalues.Name = "Defence";
                        break;
                    case 3:
                        skillvalues.Name = "Strength";
                        break;
                    case 4:
                        skillvalues.Name = "Constitution";
                        break;
                    case 5:
                        skillvalues.Name = "Ranged";
                        break;
                    case 6:
                        skillvalues.Name = "Prayer";
                        break;
                    case 7:
                        skillvalues.Name = "Magic";
                        break;
                    case 8:
                        skillvalues.Name = "Cooking";
                        break;
                    case 9:
                        skillvalues.Name = "Woodcutting";
                        break;
                    case 10:
                        skillvalues.Name = "Fletching";
                        break;
                    case 11:
                        skillvalues.Name = "Fishing";
                        break;
                    case 12:
                        skillvalues.Name = "Firemaking";
                        break;
                    case 13:
                        skillvalues.Name = "Crafting";
                        break;
                    case 14:
                        skillvalues.Name = "Smithing";
                        break;
                    case 15:
                        skillvalues.Name = "Mining";
                        break;
                    case 16:
                        skillvalues.Name = "Herblore";
                        break;
                    case 17:
                        skillvalues.Name = "Agility";
                        break;
                    case 18:
                        skillvalues.Name = "Thieving";
                        break;
                    case 19:
                        skillvalues.Name = "Slayer";
                        break;
                    case 20:
                        skillvalues.Name = "Farming";
                        break;
                    case 21:
                        skillvalues.Name = "Runecrafting";
                        break;
                    case 22:
                        skillvalues.Name = "Hunter";
                        break;
                    case 23:
                        skillvalues.Name = "Construction";
                        break;
                    case 24:
                        skillvalues.Name = "Summoning";
                        break;
                    case 25:
                        skillvalues.Name = "Dungeoneering";
                        break;
                    case 26:
                        skillvalues.Name = "Divination";
                        break;
                    case 27:
                        skillvalues.Name = "Invention";
                        break;
                    case 28:
                        skillvalues.Name = "Archeology";
                        break;

                }
            }
            rs3Player.Skillvalues.Sort((x, y) => { return x.ID - y.ID; });


            return rs3Player;
        }

    }
}
