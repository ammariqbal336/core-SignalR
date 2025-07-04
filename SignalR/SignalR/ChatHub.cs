using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System.Threading.Tasks;

public class ChatHub : Hub
{

    public async Task SendMessage(string user, double lat,double lng)
    {
        Console.WriteLine($"Received from {user}: {lat}: {lng}");
        var location = new Location();
        location.UserId = user;
        location.Latitude = lat;
        location.Longitude = lng;
        await Clients.All.SendAsync("ReceiveMessage", new {location});
    }
}