using DSharpPlus;
using DSharpPlus.SlashCommands;
using tellahs_library.Commands;
using Serilog;
using Microsoft.Extensions.Logging;

var token = Environment.GetEnvironmentVariable("DiscordBotToken");

Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                    .CreateLogger();

var logFactory = new LoggerFactory().AddSerilog();

var discord = new DiscordClient(new DiscordConfiguration
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged,
    LoggerFactory = logFactory
});

var slash = discord.UseSlashCommands();

//Register test commands for the bot's server
slash.RegisterCommands<Tournament>(1153453420649402438);

//Register global commands
slash.RegisterCommands<Recall>();
slash.RegisterCommands<BossRecall>();

await discord.ConnectAsync();
//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
