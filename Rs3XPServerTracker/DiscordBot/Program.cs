using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPTrackerLibrary;
using XPTrackerLibrary.MyClasses;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using DSharpPlus.Net;
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
        static MySqlFunctions SqlFunctions = new MySqlFunctions();
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
            string[] role;
            string BotAnswer = "";

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!SHW".ToLower()))
                {
                    string message = e.Message.Content.ToLower();
                    message = message.Replace("!SHW".ToLower(), string.Empty);
                    message = message.Trim();
                    bool isAdmin = SqlFunctions.GetBotAdmins(e.Author.Id.ToString());
                    #region AdminCommands
                    if (message.ToLower().StartsWith("add_admin".ToLower()))
                    {
                        if (isAdmin)
                        {
                            var mentionedUser = e.Message.MentionedUsers;
                            foreach (DiscordUser discordUser in mentionedUser)
                            {
                                string response = SqlFunctions.AddBotAdmins(discordUser.Id.ToString());
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n " + discordUser.Username + response);
                            }
                        }
                        else
                        {
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n You do Not Have Permissions to Use this Command");
                        }
                    }
                    if (message.ToLower().StartsWith("del_admin".ToLower()))
                    {
                        if (isAdmin)
                        {
                            var mentionedUser = e.Message.MentionedUsers;
                            foreach (DiscordUser discordUser in mentionedUser)
                            {
                                string response = SqlFunctions.DelBotAdmins(discordUser.Id.ToString());
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n " + discordUser.Username + response);
                            }
                        }
                        else
                        {
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n You do Not Have Permissions to Use this Command");
                        }
                    }
                    #endregion


                    #region UserCommands
                    if (message.ToLower().StartsWith("commands".ToLower()))
                    {                        
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n https://github.com/ShaggyHW/Rs3ServerSideXPTracker/blob/master/README.md");
                    }
                    if (message.ToLower().StartsWith("stats".ToLower()))
                    {
                        
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 1; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        if (string.IsNullOrEmpty(username))
                        {
                            username = SqlFunctions.GetLinkedAccount(e.Message.Author.Id.ToString());

                        }
                        if (string.IsNullOrEmpty(username))
                        {
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n No Linked Account Please use \"!SHW link username\" to link your discord and RS3 account");
                        }
                        else
                        {
                            rs3Player = await functionsRS.GetCurrentStats(username);
                            if (rs3Player.Error != null)
                            {
                                if (!string.IsNullOrEmpty(rs3Player.Error))
                                {
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + rs3Player.Error + "");
                                }
                                else
                                {
                                    BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                    AnswerFormats answerFormats = new AnswerFormats();
                                    BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                                    int xy = BotAnswer.Length;
                                    Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (message.ToLower().StartsWith("gains".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 1; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        if (string.IsNullOrEmpty(username))
                        {
                            username = SqlFunctions.GetLinkedAccount(e.Message.Author.Id.ToString());

                        }
                        if (string.IsNullOrEmpty(username))
                        {
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n No Linked Account Please use \"!SHW link username\" to link your discord and RS3 account");
                        }
                        else
                        {
                            rs3Player = await functionsRS.Calculate(username);
                            if (rs3Player.Error != null)
                            {
                                if (!string.IsNullOrEmpty(rs3Player.Error))
                                {
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + rs3Player.Error + "");
                                }
                                else
                                {
                                    BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                    AnswerFormats answerFormats = new AnswerFormats();
                                    BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                    int xy = BotAnswer.Length;
                                    Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (message.ToLower().StartsWith("gainz".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                      
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 1; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        if (string.IsNullOrEmpty(username))
                        {
                            username = SqlFunctions.GetLinkedAccount(e.Message.Author.Id.ToString());

                        }
                        if (string.IsNullOrEmpty(username))
                        {
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n No Linked Account Please use \"!SHW link username\" to link your discord and RS3 account");
                        }
                        else
                        {
                            rs3Player = await functionsRS.Calculate(username);
                            if (rs3Player.Error != null)
                            {
                                if (!string.IsNullOrEmpty(rs3Player.Error))
                                {
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + rs3Player.Error + "");
                                }
                                else
                                {
                                    BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                    AnswerFormats answerFormats = new AnswerFormats();
                                    BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                    int xy = BotAnswer.Length;
                                    Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                    await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (message.ToLower().StartsWith("listgainz".ToLower()))
                    {
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n Command Is Currently Disabled! Check Commands \"!rs3tracker command\"");
                        //Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        //message = message.Remove(0, 9).Trim();
                        //string[] users = message.Split(';');
                        //foreach (string user in users)
                        //{
                        //    rs3Player = await functionsRS.Calculate(user);
                        //    if (rs3Player != null)
                        //    {
                        //        BotAnswer = JsonConvert.SerializeObject(rs3Player);
                        //        AnswerFormats answerFormats = new AnswerFormats();
                        //        BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                        //        int xy = BotAnswer.Length;
                        //        Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                        //        var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                        //    }
                        //}
                    }
                    if (message.ToLower().StartsWith("new".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 1; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        var rs3Player = await functionsRS.RegisterPlayer(username);

                        if (rs3Player.Error != null)
                        {
                            if (!string.IsNullOrEmpty(rs3Player.Error))
                            {
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + rs3Player.Error + "");
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                        else
                        {
                            BotAnswer = JsonConvert.SerializeObject(rs3Player);
                            AnswerFormats answerFormats = new AnswerFormats();
                            BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                            int xy = BotAnswer.Length;
                            Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                            await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                        }

                    }
                    if (message.ToLower().StartsWith("link".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);                        
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 1; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        SqlFunctions.CreateLink_DiscordAcc_Rs3Acc(username, e.Message.Author.Id.ToString());
                        await functionsRS.RegisterPlayer(username);

                        Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + username + " Linked with discord account");
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + username + " Linked with discord account");
                    }
                    #endregion
                }
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
