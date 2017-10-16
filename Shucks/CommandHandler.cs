using System;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

using Shucks.Services;
using Shucks.Services.UserSystem;
using Shucks.Services.Hangman;
using Shucks.Services.Blackjack;

namespace Shucks
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _commands = new CommandService();
            var services = new ServiceCollection()
                .AddSingleton(new HangmanService())
                .AddSingleton(new AudioPlayerService())
                .AddSingleton(new EightBallService())
                .AddSingleton(new UserMainService())
                .AddSingleton(new BlackJackService());

            _services = services.BuildServiceProvider();

            _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (msg.HasCharPrefix('.', ref argPos))
            {
                var context = new SocketCommandContext(_client, msg);
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
