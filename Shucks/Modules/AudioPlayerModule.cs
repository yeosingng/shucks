using Discord;
using Discord.Commands;
using Shucks.Services;
using System.Threading.Tasks;

namespace Shucks.Modules.AudioPlayer
{
   public class AudioPlayerModule : ModuleBase<SocketCommandContext> {

        private AudioPlayerService _service;

        public AudioPlayerModule(AudioPlayerService service)
        {
            _service = service;
        }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel()
        {
            await _service.Join(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
        }

        // Remember to add preconditions to your commands,
        // this is merely the minimal amount necessary.
        // Adding more commands of your own is also encouraged.
        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveCmd()
        {
            await _service.LeaveAudio(Context.Guild);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlayCmd([Remainder] string url)
        {
            await _service.SendYTAudio(Context.Guild, Context.Channel, url);
        }
    }
    
}
