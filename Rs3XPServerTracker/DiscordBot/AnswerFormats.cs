using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTrackerLibrary.MyClasses;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;
namespace DiscordBot
{
    public class AnswerFormats
    {
        public string setTableLine(skillvalues skill)
        {

            string line = "";
            switch (skill.Name)
            {
                case "Overall":
                    line = "|---Overall---|";
                    break;
                case "Attack":
                    line = "|----Attack---|";
                    break;
                case "Defence":
                    line = "|---Defence---|";
                    break;
                case "Strength":
                    line = "|---Strength--|";
                    break;
                case "Constitution":
                    line = "|-Constitution|";
                    break;
                case "Ranged":
                    line = "|----Ranged---|";
                    break;
                case "Prayer":
                    line = "|----Prayer---|";
                    break;
                case "Magic":
                    line = "|----Magic----|";
                    break;
                case "Cooking":
                    line = "|---Cooking---|";
                    break;
                case "Woodcutting":
                    line = "|-Woodcutting-|";
                    break;
                case "Fletching":
                    line = "|--Fletching--|";
                    break;
                case "Fishing":
                    line = "|---Fishing---|";
                    break;
                case "Firemaking":
                    line = "|--Firemaking-|";
                    break;
                case "Crafting":
                    line = "|---Crafting--|";
                    break;
                case "Smithing":
                    line = "|---Smithing--|";
                    break;
                case "Mining":
                    line = "|----Mining---|";
                    break;
                case "Herblore":
                    line = "|---Herblore--|";
                    break;
                case "Agility":
                    line = "|---Agility---|";
                    break;
                case "Thieving":
                    line = "|---Thieving--|";
                    break;
                case "Slayer":
                    line = "|----Slayer---|";
                    break;
                case "Farming":
                    line = "|---Farming---|";
                    break;
                case "Runecrafting":
                    line = "|-Runecrafting|";
                    break;
                case "Hunter":
                    line = "|----Hunter---|";
                    break;
                case "Construction":
                    line = "|-Construction|";
                    break;
                case "Summoning":
                    line = "|--Summoning--|";
                    break;
                case "Dungeoneering":
                    line = "|Dungeoneering|";
                    break;
                case "Divination":
                    line = "|--Divination-|";
                    break;
                case "Invention":
                    line = "|--Invention--|";
                    break;
                case "Archeology":
                    line = "|--Archeology-|";
                    break;
            }
            switch (skill.Level.ToString().Length)
            {
                case 1:
                    line += "--" + skill.Level + "--|";
                    break;
                case 2:
                    line += "--" + skill.Level + "-|";
                    break;
                case 3:
                    line += "-" + skill.Level + "-|";
                    break;
                case 4:
                    line += "-" + skill.Level + "|";
                    break;
            }
            switch (skill.Xp.ToString().Length)
            {
                case 1:
                    line += "----" + skill.Xp + "----|";
                    break;
                case 2:
                    line += "----" + skill.Xp + "---|";
                    break;
                case 3:
                    line += "---" + skill.Xp + "---|";
                    break;
                case 4:
                    line += "---" + skill.Xp + "--|";
                    break;
                case 5:
                    line += "--" + skill.Xp + "--|";
                    break;
                case 6:
                    line += "--" + skill.Xp + "-|";
                    break;
                case 7:
                    line += "-" + skill.Xp + "-|";
                    break;
                case 8:
                    line += "-" + skill.Xp + "|";
                    break;
                case 9:
                    line += "" + skill.Xp + "|";
                    break;
            }
            switch (skill.Rank.ToString().Length)
            {
                case 1:
                    line += "----" + skill.Rank + "----|";
                    break;
                case 2:
                    line += "----" + skill.Rank + "---|";
                    break;
                case 3:
                    line += "---" + skill.Rank + "---|";
                    break;
                case 4:
                    line += "---" + skill.Rank + "--|";
                    break;
                case 5:
                    line += "--" + skill.Rank + "--|";
                    break;
                case 6:
                    line += "--" + skill.Rank + "-|";
                    break;
                case 7:
                    line += "-" + skill.Rank + "-|";
                    break;
            }

            return line;
        }





        public async Task<string> FormatXPAnswerTable(Rs3Player rs3Player)
        {
            string lineSplit = "+-------------+-----+---------+---------+";           
            string lineHeader = "|----Skill----|Level|-XPGainz-|HighScore|";    
            string botAnswer = rs3Player.Name + " ``` \n" + lineSplit + "\n" + lineHeader + "\n" + lineSplit; ;
            foreach (skillvalues skillvalues in rs3Player.Skillvalues)
            {                
                string line = "\n";
                line += setTableLine(skillvalues);
                botAnswer += line ;
            }
            botAnswer += "\n" + lineSplit+"```";
            return botAnswer;
        }   

    }
}
