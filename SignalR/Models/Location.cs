using System;
using System.Collections.Generic;

namespace SignalR.Models;

public partial class Location
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime? Timestamp { get; set; }
}
