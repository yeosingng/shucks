using System.Threading.Tasks;

using Discord.Commands;
using Shucks.Services;

namespace Shucks.Modules.EightBall
{
    public class EightBallModule : ModuleBase<SocketCommandContext>
    {
        private EightBallService eightBall;

        public EightBallModule(EightBallService eight)
        {
            this.eightBall = eight;
        }

        [Command("8")]
        public async Task generateResponse([Remainder] string question)
        {
            await Context.Channel.SendMessageAsync(eightBall.GenerateResponse());
        }
    }
}
