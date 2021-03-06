using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using donniebot.services;
using System.Threading.Tasks;
using donniebot.classes;
using Interactivity;

namespace donniebot.commands
{
    [Name("Music")]
    public class SkipPlayCommand : ModuleBase<ShardedCommandContext>
    {
        private readonly AudioService _audio;
        private readonly MiscService _misc;
        private readonly CommandService _cmds;
        private readonly GuildPrefix _defPre;

        public SkipPlayCommand(AudioService audio, MiscService misc, CommandService cmds, GuildPrefix defPre)
        {
            _audio = audio;
            _misc = misc;
            _cmds = cmds;
            _defPre = defPre;
        }

        [Command("skipplay")]
        [Alias("sp", "skp", "skpl")]
        [RequireDjRole]
        [Summary("Skips the current song to play another.")]
        public async Task SkipPlayAsync([Summary("The URL or YouTube search query."), Remainder] string queryOrUrl = null)
        {
            if (!_audio.HasSongs(Context.Guild.Id))
            {
                await ReplyAsync($"There are no songs to skip! Use {_defPre.Prefix}add instead.");
                return;
            }

            await _audio.SkipAsync(Context.User as SocketGuildUser);
            await _audio.AddAsync(Context.User as SocketGuildUser, Context.Channel as SocketTextChannel, queryOrUrl, false, 0);
        }
    }
}