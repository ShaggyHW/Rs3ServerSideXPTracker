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

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!SHWStats".ToLower()))
                {
                    string message = e.Message.Content.ToLower();
                    string[] mArray = message.Split(' ');
                    string username = "";
                    for (int i = 1; i < mArray.Length; i++)
                    {
                        username += mArray[i] + " ";
                    }
                    username.Trim();
                    rs3Player = await functionsRS.Calculate(username);
                }
               
                string BotAnswer = JsonConvert.SerializeObject(rs3Player);               
                await e.Message.RespondAsync(BotAnswer);




                //await e.Message.RespondAsync("pong!");
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
