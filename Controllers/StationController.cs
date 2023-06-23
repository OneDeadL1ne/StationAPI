using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationAPI.Context;
using StationAPI.Models;
using StationAPI.Services.StationServices;

namespace StationAPI.Controllers;

[ApiController]
[Route("api/Stations")]
public class StationController : Controller
{
    private readonly IStationServices _stationServices;

    public StationController(IStationServices stationServices)
    {
        _stationServices = stationServices;
    }
    
    [HttpGet("GetAllStations")]
    public async Task<ActionResult<ServiceResponse<List<Station>>>> Get()
    {
        return Ok(await _stationServices.GetAllStation());

    }
    [HttpGet("GetStation")]
    public async Task<ActionResult<ServiceResponse<Station>>> GetStation(int id)
    {
        return Ok(await _stationServices.GetDataStation(id));
    }
    [HttpGet("GetPointStation")]
    public async Task<ActionResult<ServiceResponse<ResponceMaps.Response>>> GetPointStation(int id)
    {
        return Ok(await _stationServices.GetPointStation(id));
    }
    
    [HttpGet("ForGetStationPoint")]
    public async Task<ActionResult<ServiceResponse<string>>> ForGetStationPoint(int idStart, int idEnd)
    {
        return Ok(await _stationServices.ForGetStationPoint(idStart, idEnd));
    }
    
    [HttpPut("UpdatePointStation")]
    public async Task<ActionResult<ServiceResponse<Station>>> UpdatePointStation(int id, int NumGeoObject)
    {
        return Ok(await _stationServices.UpdatePointStation(id, NumGeoObject));
    }
    
    
    [HttpPut("PutStationLanAndLot")]
    public async Task<ActionResult<ServiceResponse<Station>>> PutStationLanAndLot(int id, string lan, string lot)
    {
        return Ok(await _stationServices.PutStationLanAndLot(id,lan,lot));
    }
}