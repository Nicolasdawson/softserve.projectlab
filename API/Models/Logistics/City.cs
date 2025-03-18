using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class City : ICity
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public City(int cityId, string name, string state, string country, string zipCode)
        {
            CityId = cityId;
            Name = name;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public Result<ICity> AddCity(ICity city)
        {
            // Logic for adding a new city (e.g., saving to a database or collection)
            return Result<ICity>.Success(city);
        }

        public Result<ICity> UpdateCity(ICity city)
        {
            // Logic for updating an existing city
            return Result<ICity>.Success(city);
        }

        public Result<ICity> GetCityById(int cityId)
        {
            // Logic for retrieving a city by its ID
            var city = new City(cityId, "New York", "NY", "USA", "10001");
            return Result<ICity>.Success(city);
        }

        public Result<List<ICity>> GetAllCities()
        {
            // Logic for retrieving all cities
            var cities = new List<ICity>
            {
                new City(1, "New York", "NY", "USA", "10001"),
                new City(2, "Los Angeles", "CA", "USA", "90001")
            };
            return Result<List<ICity>>.Success(cities);
        }

        public Result<bool> RemoveCity(int cityId)
        {
            // Logic for removing a city
            return Result<bool>.Success(true); // Assume the city was removed successfully
        }
    }
}
