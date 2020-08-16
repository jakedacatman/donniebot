using System;
using System.Collections.Generic;   
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using donniebot.services;
using Discord.Addons.Interactive;
using System.IO;
using SixLabors.ImageSharp;

namespace donniebot.commands
{
    [Name("Image")]
    public class ImageInfoCommand : InteractiveBase<ShardedCommandContext>
    {
        private readonly DiscordShardedClient _client;
        private readonly ImageService _img;
        private readonly MiscService _misc;

        public ImageInfoCommand(DiscordShardedClient client, ImageService img, MiscService misc)
        {
            _client = client;
            _img = img;
            _misc = misc;
        }

        [Command("info")]
        [Alias("i", "inf")]
        [Summary("Gets some information about an image.")]
        public async Task ImageInfoCmd([Summary("The image to get the information for.")] string url = null)
        {
            try
            {
                url = await _img.ParseUrlAsync(url, Context);
                var info = await _img.GetInfo(url);
                var em = new EmbedBuilder()
                    .WithColor(_misc.RandomColor())
                    .WithCurrentTimestamp()
                    .WithThumbnailUrl(url)
                    .WithFields(new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder().WithName("Width").WithValue(info["width"]).WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Height").WithValue(info["height"]).WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Resolution").WithValue($"{long.Parse(info["width"]) * long.Parse(info["height"])} pixels").WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Frames").WithValue(info["frames"]).WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Color depth").WithValue(info["bpp"] + "bpp").WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Frames/second").WithValue((info["fps"] == "Infinity" ? "unknown " : info["fps"]) + "fps").WithIsInline(true),
                    });
                await Context.Channel.SendMessageAsync(embed: em.Build());
            }
            catch (Exception e)
            {
                await ReplyAsync(embed: (await _misc.GenerateErrorMessage(e)).Build());
            }
        }
    }
}