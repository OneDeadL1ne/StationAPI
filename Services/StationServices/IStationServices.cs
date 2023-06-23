using StationAPI.Models;

namespace StationAPI.Services.StationServices;

public interface IStationServices
{
    Task<ServiceResponse<List<Station>>> GetAllStation();
    Task<ServiceResponse<Station>> UpdatePointStation(int id, int NumGeoObject);
    Task<ServiceResponse<ResponceMaps.Response>> GetPointStation(int id);
    Task<ServiceResponse<Station>> GetDataStation(int id);

    Task<ServiceResponse<Station>> PutStationLanAndLot(int id,string lan, string lot);

    Task<ServiceResponse<string>> ForGetStationPoint(int idStart, int idEnd);

}