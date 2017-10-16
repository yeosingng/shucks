using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;

using YoutubeExplode;
using System.Linq;
using YoutubeExplode.Models;

namespace Shucks.Services
{
    public class AudioPlayerService
    {
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();

        public async Task Join(IGuild guild, IVoiceChannel channel)
        {
            IAudioClient client;
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                return;
            }
            if (channel.Guild.Id != guild.Id)
            {
                return;
            }

            var audioClient = await channel.ConnectAsync();

            if (ConnectedChannels.TryAdd(guild.Id, audioClient))
            {
                //await Log(LogSeverity.Info, $"Connected to voice on {guild.Name}.");
            }
        }

        public async Task LeaveAudio(IGuild guild)
        {
            IAudioClient client;
            if (ConnectedChannels.TryRemove(guild.Id, out client))
            {
                await client.StopAsync();
                //await Log(LogSeverity.Info, $"Disconnected from voice on {guild.Name}.");
            }
        }

        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
        {
            // Your task: Get a full path to the file if the value of 'path' is only a filename.
            if (!File.Exists(path))
            {
                await channel.SendMessageAsync("File does not exist.");
                return;
            }
            IAudioClient client;
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");
                var output = CreateStream(path).StandardOutput.BaseStream;

                // You can change the bitrate of the outgoing stream with an additional argument to CreatePCMStream().
                // If not specified, the default bitrate is 96*1024.
                var stream = client.CreatePCMStream(AudioApplication.Music, 1920);
                await output.CopyToAsync(stream);
                await stream.FlushAsync().ConfigureAwait(false);
            }
        }

        public async Task SendYTAudio(IGuild guild, IMessageChannel channel, string url)
        {
            var client = new YoutubeClient();

            string id = NormalizeId(url);

            var videoInfo = await client.GetVideoInfoAsync(id);
            var streamInfo = videoInfo.AudioStreams.OrderBy(s => s.Bitrate).Last();
            string path = "C:\\Users\\Yeo\\documents\\visual studio 2017\\Projects\\Shucks\\Shucks\\Audio\\" + $"{videoInfo.Title}.{streamInfo.Container.GetFileExtension()}";

            try
            {
                using (var input = await client.GetMediaStreamAsync(streamInfo))
                using (var file = File.Create(path))
                {
                    await input.CopyToAsync(file);
                }
                await SendAudioAsync(guild, channel, path);
            }
            finally
            {
                File.Delete(path);
            }
        }

        private static string NormalizeId(string input)
        {
            if (!YoutubeClient.TryParseVideoId(input, out string id))
                id = input;
            return id;
        }

        private Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -codec:a libmp3lame -qscale:a 2 output.mp3",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }
    }
}
