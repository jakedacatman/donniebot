using System;
using Discord.Commands;
using Discord.WebSocket;
using donniebot.services;
using System.Threading.Tasks;
using Interactivity;

namespace donniebot.commands
{
    [Name("Music")]
    public class SkipCommand : ModuleBase<ShardedCommandContext>
    {
        private readonly AudioService _audio;
        private readonly MiscService _misc;

        public SkipCommand(AudioService audio, MiscService misc)
        {
            _audio = audio;
            _misc = misc;
        }

        [Command("skip")]
        [Alias("sk")]
        [Summary("Votes to skip the current song.")]
        public async Task SkipAsync() => await _audio.SkipAsync(Context.User as SocketGuildUser);
    }
}