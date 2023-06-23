using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationAPI.Context;
using StationAPI.Models;

namespace StationAPI.Services.StationServices;

public class StationServices : IStationServices
{
    private readonly ConnectionClass _context;
    public StationServices(ConnectionClass context)
    {
        _context = context;
    }
    
    public async Task<ServiceResponse<List<Station>>> GetAllStation()
    {
        var serviceResponce = new ServiceResponse<List<Station>>();
        var dbStation = await _context.Station.ToListAsync();

        serviceResponce.Data = dbStation;
        return serviceResponce;
    }

    public async Task<ServiceResponse<ResponceMaps.Response>> GetPointStation(int id)
    {
        var serviceResponce = new ServiceResponse<ResponceMaps.Response>();
        var dbStation =  await _context.Station.FirstOrDefaultAsync(x=> x.IdStation==id);
        
        
        try
        {
            string props=string.Empty;
            if (dbStation.Railway!=string.Empty)
            {
                props = $"станция {dbStation.StationName},{dbStation.Railway}" +
                        $" железная дорога";
            }
            else
            {
                props = $"станция {dbStation.StationName}";
            }
            var url = new Uri($"https://geocode-maps.yandex.ru/1.x/?apikey=f0911308-1a2e-4cb2-8733-d352e7ca923d&format=json&geocode={props}");

            string response=String.Empty;
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                response = await result.Content.ReadAsStringAsync();
                
            }

            if (response ==String.Empty)
            {
                throw new Exception("responce пустой");
            }

            ResponceMaps Station = new ResponceMaps(response);

            

            serviceResponce.Data = Station._responce.response;


        }
        catch (Exception e)
        {
            serviceResponce.Message = e.Message;
            serviceResponce.Succes = false;

        }
        
        return serviceResponce;
    }


    public async Task<ServiceResponse<Station>> UpdatePointStation(int id, int NumGeoObject)
    {
        var serviceResponce = new ServiceResponse<Station>();
        var dbStation =  await _context.Station.FirstOrDefaultAsync(x=> x.IdStation==id);
        
        
        try
        {
            string props=string.Empty;
            if (dbStation.Railway!=string.Empty)
            {
                props = $"{dbStation.Railway} железная дорога, станция {dbStation.StationName}";
            }
            else
            {
                props = $"станция {dbStation.StationName}";
            }
            var url = new Uri($"https://geocode-maps.yandex.ru/1.x/?apikey=f0911308-1a2e-4cb2-8733-d352e7ca923d&format=json&geocode={props}");

            string response=String.Empty;
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                response = await result.Content.ReadAsStringAsync();
                
            }

            if (response ==String.Empty)
            {
               throw new Exception("responce пустой");
            }

            ResponceMaps Station = new ResponceMaps(response);

            string[] temp = Station._responce.response.GeoObjectCollection.featureMember[NumGeoObject-1].GeoObject.Point
                .pos.Split(' ');

            Console.WriteLine(temp[1]);
            Console.WriteLine(temp[0]);
            dbStation.Lan = temp[1];
            dbStation.Lot = temp[0];

            
            _context.SaveChanges();
            
            serviceResponce.Data = dbStation;
            
            

        }
        catch (Exception e)
        {
            serviceResponce.Message = e.Message;
            serviceResponce.Succes = false;

        }
        
        return serviceResponce; 
    }

    public async Task<ServiceResponse<Station>> GetDataStation(int id)
    {
        var serviceResponce = new ServiceResponse<Station>();
        var dbStation =  await _context.Station.FirstOrDefaultAsync(x=> x.IdStation==id);

        serviceResponce.Data = dbStation;
        return serviceResponce;
    }

    public async Task<ServiceResponse<Station>> PutStationLanAndLot(int id, string lan, string lot)
    {
        var serviceResponse = new ServiceResponse<Station>();
        var dbStation =  await _context.Station.FirstOrDefaultAsync(x=> x.IdStation==id);

        dbStation.Lan = lan;
        dbStation.Lot = lot;
        _context.SaveChanges();

        dbStation = await _context.Station.FirstOrDefaultAsync(x => x.IdStation == id);
        serviceResponse.Data = dbStation;
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> ForGetStationPoint(int idStart, int idEnd)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            for (int i = idStart; i <= idEnd; i++)
            {
                var dbStation = await _context.Station.FirstOrDefaultAsync(x => x.IdStation == i);
                try
                {
                    string props = string.Empty;
                    if (dbStation.Railway != string.Empty)
                    {
                        props = $"{dbStation.Railway} железная дорога, станция {dbStation.StationName}";
                    }
                    else
                    {
                        props = $"станция {dbStation.StationName}";
                    }

                    var url = new Uri(
                        $"https://geocode-maps.yandex.ru/1.x/?apikey=f0911308-1a2e-4cb2-8733-d352e7ca923d&format=json&geocode={props}");

                    string response = String.Empty;
                    using (var client = new HttpClient())
                    {
                        var result = await client.GetAsync(url);
                        response = await result.Content.ReadAsStringAsync();

                    }

                    if (response == String.Empty)
                    {
                        throw new Exception("responce пустой");
                    }

                    ResponceMaps Station = new ResponceMaps(response);

                    string[] temp = Station._responce.response.GeoObjectCollection.featureMember[0].GeoObject.Point
                        .pos.Split(' ');

                    Console.WriteLine(temp[1]);
                    Console.WriteLine(temp[0]);
                    dbStation.Lan = temp[1];
                    dbStation.Lot = temp[0];


                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    serviceResponse.Message = e.Message;
                    serviceResponse.Succes = false;

                }

            }
        }
        catch (Exception e)
        {
            serviceResponse.Message = e.Message;
            serviceResponse.Succes = false;

        }

        return serviceResponse;
    }
}