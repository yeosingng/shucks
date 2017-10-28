using System.Threading.Tasks;
using Shucks.Services.Blackjack;
using Discord.Commands;
using Shucks.Playing_Cards;
using System.Collections.Generic;

namespace Shucks.Modules
{
    public class BlackjackModule : ModuleBase<SocketCommandContext>
    {
        private BlackJackService service;

        public BlackjackModule(BlackJackService service)
        {
            this.service = service;
        }

        [Command("startgame")]
        public async Task StartGame()
        {
            await Context.Channel.SendMessageAsync(service.StartGame());
            foreach (ulong cardHolderId in service.GetCardHolders().Keys)
            {
                service.GetCardHolders().TryGetValue(cardHolderId, out CardHolder holder);

                await Context.Channel
                    .GetUserAsync(cardHolderId).Result
                    .GetOrCreateDMChannelAsync().Result
                    .SendMessageAsync(holder.HandToString());
            }
        }

        [Command("joinbj")]
        public async Task JoinGame()
        {
            await Context.Channel.SendMessageAsync(service.JoinGame(Context.User));
        }
    }
}
