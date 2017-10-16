using Discord.Commands;
using System.Threading.Tasks;
using Shucks.Services.Hangman;

namespace Shucks.Modules.Hangman
{
    public class HangmanModule : ModuleBase<SocketCommandContext>
    {
        private HangmanService _hangman;

        public HangmanModule(HangmanService hangman)
        {
            _hangman = hangman;
        }

        [Command("hm")]
        public async Task HangmanGuess(string guess = "")
        {
            await Context.Channel.SendMessageAsync(_hangman.processCommands(guess));
        }

        [Command("guesses")]
        public async Task HangmanGuesses()
        {
            await Context.Channel.SendMessageAsync(_hangman.guesses());
        }
    }
}
