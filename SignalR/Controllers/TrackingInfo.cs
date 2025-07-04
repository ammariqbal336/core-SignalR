using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackingInfo : ControllerBase
    {

        private readonly TrackingUserContext _context;
        private readonly IHubContext<ChatHub> _hubContext;


        public TrackingInfo(IHubContext<ChatHub> chatHub,TrackingUserContext context)
        {
            _hubContext = chatHub;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Location location)
        {
            if (location == null)
                return BadRequest("Invalid data.");

            // Save to DB or process here
            Console.WriteLine($"Received: {location.UserId}, {location.Latitude}, {location.Longitude}");

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage",new { location });
            return Ok(new { message = "Location received", location });
        }

        [HttpPost]
        public async Task<IActionResult> TrackPostAsync([FromBody] Location location)
        {
            if (location == null)
                return BadRequest("Invalid data.");

            // Save to DB or process here
            Console.WriteLine($"Received: {location.UserId}, {location.Latitude}, {location.Longitude}");

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("NewReceiveMessage", new { location });
            return Ok(new { message = "Location received", location });
        }
    }
}

