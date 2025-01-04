using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace dcbot.Events;

public class CommandHandler(DiscordSocketClient client)
{
    private readonly CommandService _commands = new();

    public async Task InitializeAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

        client.MessageReceived += HandleCommandAsync;

        client.ButtonExecuted += Commands.Lottery.FunAsync;
        client.ButtonExecuted += Commands.User.FunAsync;
        client.ButtonExecuted += Commands.Gacha.FunAsync;
        client.ButtonExecuted += Commands.Character.FunAsync;
        
    }


    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage message) return;
        if (message.Author.IsBot) return;

        var argPos = 0;
        if (!message.HasCharPrefix('!', ref argPos)) return;

        var context = new SocketCommandContext(client, message);
        var result = await _commands.ExecuteAsync(context, argPos, null);


        if (!result.IsSuccess)
        {
            Console.WriteLine("Command failed");
            Console.Error.WriteLine(result.Error);
            Console.Error.WriteLine(result);
            await Console.Error.WriteLineAsync(result.ErrorReason);
            await context.Channel.SendMessageAsync("Something went wrong");
        }
    }
}


// Updated Program class to use CommandHandler