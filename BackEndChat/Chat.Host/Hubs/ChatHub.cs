using Chat.Host.Hubs.Abstractions;
using Chat.Host.Models;
using Chat.Host.Services.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Host.Hubs
{    
    public class ChatHub : Hub<IChatHub>
    {        
        private readonly IChatService _chatService;
        private readonly ICacheService _cacheService;
        private const string _chatCacheKey = "ChatHub";

        public ChatHub(
            IChatService chatService,
            ICacheService cacheService
            )
        {            
            _chatService = chatService;
            _cacheService = cacheService;
        }

        public async Task ConnectionRoom(ConnectionOption connection)
        {
            try 
            {
                await Groups
                .AddToGroupAsync(Context.ConnectionId, connection.RoomName);

                var key = GenerateKey(_chatCacheKey, Context.ConnectionId);

                await _cacheService
                    .AddOrUpdateAsync(key, connection);

                var message = $"{DateTime.UtcNow}: connected to room...";

                await Clients
                    .Group(connection.RoomName)
                    .ReceiveMessage($"{connection.UserName}", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // add logger
            }
            
        }

        public async Task SendMessage(string message)
        {     
            try
            {
                var color = await _chatService.ProcessedMessageAsync(message);

                var key = GenerateKey(_chatCacheKey, Context.ConnectionId);

                var connection = await _cacheService
                    .GetAsync<ConnectionOption>(key);

                var updateMessage = $"{DateTime.UtcNow} : {message}";

                await Clients
                    .Group(connection.RoomName)
                    .ReceiveMessage($"{connection.UserName}", updateMessage, color);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // logger
            }
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try 
            {
                var key = GenerateKey(_chatCacheKey, Context.ConnectionId);

                var connection = await _cacheService
                    .GetAsync<ConnectionOption>(key);

                if(connection is not null)
                {                   
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.RoomName);

                    await _cacheService.DeleteKeyAsync(key);

                    var message = $"{DateTime.UtcNow}: disconnected to room...";

                    await Clients
                            .Group(connection.RoomName)
                            .ReceiveMessage($"{connection.UserName}", message);
                }                

                await base.OnDisconnectedAsync(exception);
            } 
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message); //logger
            }            
        }

        private string GenerateKey(params string[] keys)
        {
            return string.Join("", keys)!;
        }
    }
}
