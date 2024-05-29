using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Test_app.Commands;
using Newtonsoft.Json;
using System.IO;
using Discord.Interactions;

namespace Test_app
{
    public class Config
    {
        public string DiscordToken { get; set; }
    }
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private InteractionService _interactionService;
        private IServiceProvider services;

        static void Main(string[] args)
            => new Program().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync() 
        {
            var configuracoes = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            var token = config.DiscordToken;

            _client = new DiscordSocketClient(configuracoes);
            _commands = new CommandService();
            _client.Log += _client_Log;
            _interactionService = new InteractionService(_client);
            // Set up dependency injection
            services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton<CommandsHandler>()
                .BuildServiceProvider();

            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected and ready!");
                return Task.CompletedTask;
            };
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetGameAsync("As your command", null, ActivityType.Playing);
            await services.GetRequiredService<CommandsHandler>().InstallCommandsAsync(services);
            // Prevent the program from exiting
            await Task.Delay(-1);
        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

    }
}

