using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Shucks
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            string token = "MzM1Njg1NDgyNDc2MjA4MTI5.DEyFHQ.vEpefufL1R1U7j7ucKQOaiMrooQ";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}
