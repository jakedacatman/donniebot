using System;
using Discord.Commands;
using donniebot.services;
using System.Threading.Tasks;
using Discord.Addons.Interactive;

namespace donniebot.commands
{
    [Name("Audio")]
    public class DisconnectCommand : InteractiveBase<ShardedCommandContext>
    {
        private readonly AudioService _audio;
        private readonly MiscService _misc;

        public DisconnectCommand(AudioService audio, MiscService misc)
        {
            _audio = audio;
            _misc = misc;
        }

        [Command("disconnect")]
        [Alias("di", "dis", "leave")]
        [Summary("Leaves the current voice channel.")]
        public async Task LeaveCmd()
        {
            try
            {
                var vc = Context.Guild.CurrentUser.VoiceChannel;
                if (vc == null)
                {
                    await ReplyAsync("I am not connected to a voice channel.");
                    return;
                }

                await _audio.DisconnectAsync(vc);
            }
            catch (Exception e)
            {
                await ReplyAsync(embed: (await _misc.GenerateErrorMessage(e)).Build());
            }
        }
    }
}