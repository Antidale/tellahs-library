using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System.Net.Http.Json;
using tellahs_library.Dtos;

namespace tellahs_library.Commands
{
    public class TournamentRegistration : ApplicationCommandModule
    {
        private HttpClient? HttpClient { get; init; }

        [SlashCommand("Register", "Register for an active tournament")]
        public async Task RegisterAsync(InteractionContext ctx)
        {
            if (HttpClient == null) 
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote.");
                return; 
            }
            var user = ctx.User;

            var registration = new Registration()
            {
                UserId = user.Id,
                UserName = user.Username,
                GuildId = ctx.Guild.Id
            };

            var response = await HttpClient.PostAsJsonAsync("", registration);

            if (response.IsSuccessStatusCode)
            {
                //respond to the user
                await ctx.CreateResponseAsync("you registered!", ephemeral: true);

                //update "x registrants" message for this tournament
                var channel = await ctx.Client.GetChannelAsync(1);
                var message = await channel.GetMessageAsync(1);
                await message.ModifyAsync("updated message");

                //handle role updates?
            }
            else
            {
                await ctx.CreateResponseAsync("registration failed");
            }
            
            return;
        }

        [SlashCommand("Register", "Register for an active tournament")]
        public async Task UnregisterAsync(InteractionContext ctx)
        {
            if (HttpClient == null)
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote.");
                return;
            }
            var user = ctx.User;

            var registration = new Registration()
            {
                UserId = user.Id,
                UserName = user.Username,
                GuildId = ctx.Guild.Id
            };

            var response = await HttpClient.PostAsJsonAsync("", registration);
            var responseDto = response.Content.ReadFromJsonAsync<RegistrationResponse>();
            if (response.IsSuccessStatusCode)
            {
                //respond to the user
                await ctx.CreateResponseAsync("you're no longer registered", ephemeral: true);

                //update "x registrants" message for this tournament
                var channel = await ctx.Client.GetChannelAsync(1);
                var message = await channel.GetMessageAsync(1);
                await message.ModifyAsync("updated message");

                //handle role updates?
            }
            else
            {
                await ctx.CreateResponseAsync("unregistration failed");
            }

            return;
        }

        [SlashCommand("CreateTournament", "Create A Tournament")]
        [SlashRequireUserPermissions(DSharpPlus.Permissions.Administrator)]
        public async Task CreateTournamentAsync(InteractionContext ctx)
        {
            if (HttpClient == null)
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote.");
                return;
            }
            await ctx.CreateResponseAsync("Tournament Created");
        }

        [SlashCommand("CreateTournamentOverride", "Create A Tournament")]
        [SlashRequireOwner]
        public async Task CreateTournamentOverrideAsync(InteractionContext ctx)
        {
            if (HttpClient == null)
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote.");
                return;
            }

            await ctx.CreateResponseAsync("Tournament Created");
        }

    }
}
