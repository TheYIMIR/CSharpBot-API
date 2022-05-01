﻿using DSharpPlus;
using System;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace CSharpBot_API
{
    public class DBot
    {
        static DiscordClient client = new DiscordClient(new DiscordConfiguration()
        {
            Token = "Your-Token",
            TokenType = TokenType.Bot,
        });



        public static async void DCClientStart()
        {
            string prefix = "!";
            client.MessageCreated += async (c, e) =>
            {
                if (e.Message.Content.StartsWith(prefix))
                {
                    string[] ags = e.Message.Content.Split(' ');
                    string command = ags[0].Replace(prefix, "");

                    switch (command)
                    {
                        case "help":
                            await e.Channel.SendMessageAsync("No help!");
                            break;
                        default:
                            break;
                    }
                }
            };
            client.Ready += async (c, e) => {

                await c.UpdateStatusAsync();
            };

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        public static async void DCClientStop()
        {
            await client.DisconnectAsync();
        }

        public static async void SendMessage(string channel, string text)
        {
            await client.SendMessageAsync(await client.GetChannelAsync(ulong.Parse(channel)), text);
        }

        public static async void Update()
        {
            await client.UpdateCurrentUserAsync();
        }

        public static async void ChangeRPC(int rpcMode, string text, string url)
        {
            DiscordActivity activity = new DiscordActivity();

            ActivityType activityType = ActivityType.Playing;
            switch (rpcMode)
            {
                case 0:
                    activityType = ActivityType.Playing;
                    break;
                case 1:
                    activityType = ActivityType.Streaming;
                    break;
                case 2:
                    activityType = ActivityType.ListeningTo;
                    break;
                case 3:
                    activityType = ActivityType.Watching;
                    break;
                case 4:
                    activityType = ActivityType.Custom;
                    break;
                case 5:
                    activityType = ActivityType.Competing;
                    break;
            }

            activity.Name = text;
            activity.ActivityType = activityType;
            activity.StreamUrl = url;
            await client.UpdateStatusAsync(activity);
        }
    }
}