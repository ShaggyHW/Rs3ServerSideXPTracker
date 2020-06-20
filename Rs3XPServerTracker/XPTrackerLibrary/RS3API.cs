using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace XPTrackerLibrary.Rs3API
{
    public class Rs3API
    {


        public async Task<MyClasses.Rs3Player> GetRs3Player(string Username)
        {


            var restClient = new RestClient("https://apps.runescape.com/runemetrics/profile/profile");
            var request = new RestRequest("?user=" + Username + "&activities=0", DataFormat.Json);
            restClient.UseNewtonsoftJson();

            var response = restClient.Get(request);

            JsonNetSerializer jsonNetSerializer = new JsonNetSerializer();
            MyClasses.Rs3Player rs3Player = jsonNetSerializer.Deserialize<MyClasses.Rs3Player>(response);
            foreach(MyClasses.skillvalues skillvalues in rs3Player.Skillvalues)
            {
                switch (skillvalues.ID)
                {
                    case 0:
                        skillvalues.Name = "Attack";
                        break;
                    case 1:
                        skillvalues.Name = "Defence";
                        break;
                    case 2:
                        skillvalues.Name = "Strenght";
                        break;
                    case 3:
                        skillvalues.Name = "Constitution";
                        break;
                    case 4:
                        skillvalues.Name = "Ranged";
                        break;
                    case 5:
                        skillvalues.Name = "Prayer";
                        break;
                    case 6:
                        skillvalues.Name = "Magic";
                        break;
                    case 7:
                        skillvalues.Name = "Cooking";
                        break;
                    case 8:
                        skillvalues.Name = "Woodcutting";
                        break;
                    case 9:
                        skillvalues.Name = "Fletching";
                        break;
                    case 10:
                        skillvalues.Name = "Fishing";
                        break;
                    case 11:
                        skillvalues.Name = "Firemaking";
                        break;
                    case 12:
                        skillvalues.Name = "Crafting";
                        break;
                    case 13:
                        skillvalues.Name = "Smithing";
                        break;
                    case 14:
                        skillvalues.Name = "Mining";
                        break;
                    case 15:
                        skillvalues.Name = "Herblore";
                        break;
                    case 16:
                        skillvalues.Name = "Agility";
                        break;
                    case 17:
                        skillvalues.Name = "Thieving";
                        break;
                    case 18:
                        skillvalues.Name = "Slayer";
                        break;
                    case 19:
                        skillvalues.Name = "Farming";
                        break;
                    case 20:
                        skillvalues.Name = "Runecrafting";
                        break;
                    case 21:
                        skillvalues.Name = "Hunter";
                        break;
                    case 22:
                        skillvalues.Name = "Construction";
                        break;
                    case 23:
                        skillvalues.Name = "Summoning";
                        break;
                    case 24:
                        skillvalues.Name = "Dungeoneering";
                        break;
                    case 25:
                        skillvalues.Name = "Divination";
                        break;
                    case 26:
                        skillvalues.Name = "Invention";
                        break;
                    case 27:
                        skillvalues.Name = "Archeology";
                        break;

                }
            }
            rs3Player.Skillvalues.Sort((x,y)=> { return x.ID - y.ID; });


            return rs3Player;
        }

    }
}
