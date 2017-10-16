using System.Threading.Tasks;
using Shucks.Services.Blackjack;
using Discord.Commands;

namespace Shucks.Modules
{
    public class BlackjackModule : ModuleBase<SocketCommandContext>
    {
        private BlackJackService service;

        public BlackjackModule(BlackJackService service)
        {
            this.service = service;
        }

        [Command("blackjack")]
        public async Task StartGame()
        {
            await Context.Channel.SendMessageAsync(service.StartGame());
        }
    }
}
