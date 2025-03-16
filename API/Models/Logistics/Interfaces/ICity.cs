namespace API.Models.Logistics.Interfaces
{
    public interface ICity
    {
        int CityId { get; set; }
        string Name { get; set; }
        string State { get; set; }
        string Country { get; set; }
        string ZipCode { get; set; }

        Result<ICity> AddCity(ICity city);
        Result<ICity> UpdateCity(ICity city);
        Result<ICity> GetCityById(int cityId);
        Result<List<ICity>> GetAllCities();
        Result<bool> RemoveCity(int cityId);
    }
}
