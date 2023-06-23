using Newtonsoft.Json;

namespace StationAPI.Models;

public class ResponceMaps
{
    public Root _responce { get; set; }

    public ResponceMaps(string response)
    { 
        _responce= JsonConvert.DeserializeObject<Root>(response);    
    }
    
    public class Address
    {
        public string country_code { get; set; }
        public string formatted { get; set; }
        public List<Component> Components { get; set; }
    }

    public class AddressDetails
    {
        public Country Country { get; set; }
    }

    public class BoundedBy
    {
        public Envelope Envelope { get; set; }
    }

    public class Component
    {
        public string kind { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string addressLine { get; set; }
        public string countryNameCode { get; set; }
        public string countryName { get; set; }
        public Country country { get; set; }
        public Locality locality { get; set; }
    }

    public class Envelope
    {
        public string lowerCorner { get; set; }
        public string upperCorner { get; set; }
    }

    public class FeatureMember
    {
        public GeoObject GeoObject { get; set; }
    }

    public class GeocoderMetaData
    {
        public string precision { get; set; }
        public string text { get; set; }
        public string kind { get; set; }
        public Address Address { get; set; }
        public AddressDetails AddressDetails { get; set; }
    }

    public class GeocoderResponseMetaData
    {
        public string request { get; set; }
        public string results { get; set; }
        public string found { get; set; }
    }

    public class GeoObject
    {
        public MetaDataProperty metaDataProperty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public BoundedBy boundedBy { get; set; }
        public Point Point { get; set; }
    }

    public class GeoObjectCollection
    {
        public MetaDataProperty metaDataProperty { get; set; }
        public List<FeatureMember> featureMember { get; set; }
    }

    public class Locality
    {
    }

    public class MetaDataProperty
    {
        public GeocoderResponseMetaData GeocoderResponseMetaData { get; set; }
        public GeocoderMetaData GeocoderMetaData { get; set; }
    }

    public class Point
    {
        public string pos { get; set; }
    }

    public class Response
    {
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }

    public class Root
    {
        public Response response { get; set; }
    }


}