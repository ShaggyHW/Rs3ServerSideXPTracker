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
            string BotAnswer = "";

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!SHW".ToLower()))
                {
                    if (e.Message.Content.ToLower().Contains("commands".ToLower()))
                    {
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n https://github.com/ShaggyHW/Rs3ServerSideXPTracker/blob/master/README.md");
                    }
                    if (e.Message.Content.ToLower().Contains("stats".ToLower()))
                    {
                        string message = e.Message.Content.ToLower();
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 2; i < mArray.Length; i++)
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
                                    var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (e.Message.Content.ToLower().Contains("gains".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        string message = e.Message.Content.ToLower();
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 2; i < mArray.Length; i++)
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
                                    var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (e.Message.Content.ToLower().Contains("gainz".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        string message = e.Message.Content.ToLower();
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 2; i < mArray.Length; i++)
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
                                    var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                                }
                            }
                            else
                            {
                                BotAnswer = JsonConvert.SerializeObject(rs3Player);
                                AnswerFormats answerFormats = new AnswerFormats();
                                BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Gainz");
                                int xy = BotAnswer.Length;
                                Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                                var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                    }
                    if (e.Message.Content.ToLower().Contains("listgainz".ToLower()))
                    {
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n Command Is Currently Disabled! Check Commands \"!rs3tracker command\"");
                        //Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        //string message = e.Message.Content.ToLower();
                        //message = message.Remove(0, 15);
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
                    if (e.Message.Content.ToLower().Contains("new".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        string message = e.Message.Content.ToLower();
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 2; i < mArray.Length; i++)
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
                                var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                            }
                        }
                        else
                        {
                            BotAnswer = JsonConvert.SerializeObject(rs3Player);
                            AnswerFormats answerFormats = new AnswerFormats();
                            BotAnswer = await answerFormats.FormatXPAnswerTable(rs3Player, "Current");
                            int xy = BotAnswer.Length;
                            Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer);
                            var x = await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + BotAnswer + "");
                        }

                    }

                    if (e.Message.Content.ToLower().Contains("link".ToLower()))
                    {
                        Console.WriteLine(DateTime.Now + ": " + e.Message.Content);
                        string message = e.Message.Content.ToLower();
                        string[] mArray = message.Split(' ');
                        string username = "";
                        for (int i = 2; i < mArray.Length; i++)
                        {
                            username += mArray[i] + " ";
                        }
                        username = username.Trim();
                        SqlFunctions.CreateLink_DiscordAcc_Rs3Acc(username, e.Message.Author.Id.ToString());
                        await functionsRS.RegisterPlayer(username);

                        Console.WriteLine("<@!" + e.Message.Author.Id + ">" + "\n" + username + " Linked with discord account");
                        await e.Message.RespondAsync("<@!" + e.Message.Author.Id + ">" + "\n" + username + " Linked with discord account");
                    }
                }
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
