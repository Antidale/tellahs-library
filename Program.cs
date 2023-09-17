using DSharpPlus;
using DSharpPlus.SlashCommands;
using tellahs_library.Commands;

var token = Environment.GetEnvironmentVariable("DiscordBotToken");

var discord = new DiscordClient(new DiscordConfiguration
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged
});

var slash = discord.UseSlashCommands();
slash.RegisterCommands<Recall>();
slash.RegisterCommands<BossRecall>();

await discord.ConnectAsync();
//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);