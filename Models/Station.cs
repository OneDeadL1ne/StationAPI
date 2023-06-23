using System.ComponentModel.DataAnnotations;

namespace StationAPI.Models;

public class Station
{
    [Key]
    public int IdStation { get; set; }

    public string StationName { get; set; }
    
    public string? Railway { get; set; }

    public string? Lan { get; set; }
    
    public string? Lot { get; set; }
    
    
}

    