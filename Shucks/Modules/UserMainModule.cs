using Discord.Commands;
using System.Threading.Tasks;
using Shucks.Services.Hangman;
using Shucks.Services.UserSystem;

namespace Shucks.Modules
{
    public class UserMainModule : ModuleBase<SocketCommandContext>
    {
        UserMainService _service;

        public UserMainModule(UserMainService service)
        {
            _service = service;
        }

        [Command("add")]
        public async Task AddUser()
        {
            await Context.Channel.SendMessageAsync(_service.addUserUsingDefaults(Context.User.Username));
        }

        [Command("balance")]
        public async Task Balance()
        {
            await Context.Channel.SendMessageAsync(_service.checkUserBalance(Context.User.Username));
        }
    }
}
