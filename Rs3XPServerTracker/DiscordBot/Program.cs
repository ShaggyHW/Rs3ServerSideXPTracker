using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTrackerLibrary;
using XPTrackerLibrary.MyClasses;
using DSharpPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft;
using System.IO;
using System.Reflection;

namespace DiscordBot
{
    class Program
    {
        static FunctionsRS functionsRS = new FunctionsRS();
        static Rs3Player rs3Player = new Rs3Player();
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            DiscordConfiguration discordConfiguration = new DiscordConfiguration();
            discordConfiguration.Token = "";
            discordConfiguration.TokenType = TokenType.Bot; 
            var discord = new DiscordClient(discordConfiguration);
            string BotAnswer = "";
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!SHWStats".ToLower()))
                {
                    Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                    string message = e.Message.Content.ToLower();
                    string[] mArray = message.Split(' ');
                    string username = "";
                    for (int i = 1; i < mArray.Length; i++)
                    {
                        username += mArray[i] + " ";
                    }
                    username.Trim();
                    rs3Player = await functionsRS.Calculate(username);
                    BotAnswer = JsonConvert.SerializeObject(rs3Player);
                    AnswerFormats answerFormats = new AnswerFormats();
                    BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player);
                    int xy = BotAnswer.Length;
                    Console.WriteLine("<@!"+e.Message.Author.Id+ ">" + "\n" + BotAnswer);
                    var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">"+"\n" + BotAnswer + "");
                }

                if (e.Message.Content.ToLower().StartsWith("!Dev".ToLower()))
                {
                    Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                    string message = e.Message.Content.ToLower();
                    message = message.Remove(0,5);
                    string username = "";
                    string[] Multiple = message.Split(';');
                    foreach(string user in Multiple)
                    {
                        username = user;
                        rs3Player = await functionsRS.Calculate(username);
                        BotAnswer = JsonConvert.SerializeObject(rs3Player);
                        AnswerFormats answerFormats = new AnswerFormats();
                        BotAnswer = await answerFormats.FormatXPAnswer(rs3Player);
                        string MergedAnswer;
                        if (BotAnswer.Length > 2000)
                        {
                            string[] answerArray = BotAnswer.Split('\n');

                        }

                        Console.WriteLine(BotAnswer);
                        var x = await e.Message.RespondAsync(DateTime.Now + ":\n " + BotAnswer + "");
                    }
                    string[] mArray = message.Split(' ');
                    
                    //for (int i = 1; i < mArray.Length; i++)
                    //{
                    //    username += mArray[i] + " ";
                    //}
                    //username.Trim();
                   
                }







                //await e.Message.RespondAsync("pong!");
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
