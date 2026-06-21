using TravelGenApi.Models;

namespace TravelGenApi.Services.External;

public record Coordinates(double Latitude, double Longitude);

public interface IGeocodingClient
{
    Task<Coordinates?> GetCoordinatesAsync(string city);
}

public interface IAttractionClient
{
    Task<List<Attraction>> GetAttractionsAsync(Coordinates coords, List<string> interests);
}

public interface IWeatherClient
{
    Task<string> GetWeatherForecastAsync(Coordinates coords);
}

// ------ MOCKS PARA O MVP INICIAL (Para testar o fluxo sem chamar as APIs reais) ------

public class MockGeocodingClient : IGeocodingClient
{
    public Task<Coordinates?> GetCoordinatesAsync(string city)
    {
        // Mock Roma
        return Task.FromResult<Coordinates?>(new Coordinates(41.9028, 12.4964));
    }
}

public class MockAttractionClient : IAttractionClient
{
    public Task<List<Attraction>> GetAttractionsAsync(Coordinates coords, List<string> interests)
    {
        var mockAttractions = new List<Attraction>
        {
            new("Coliseu", "Anfiteatro romano", 41.8902, 12.4922),
            new("Fontana di Trevi", "Fonte de água histórica", 41.9009, 12.4833),
            new("Panteão", "Templo de todos os deuses", 41.8986, 12.4769),
            new("Museus Vaticanos", "Museus de arte cristã e antiguidades", 41.9065, 12.4536)
        };
        return Task.FromResult(mockAttractions);
    }
}

public class MockWeatherClient : IWeatherClient
{
    public Task<string> GetWeatherForecastAsync(Coordinates coords)
    {
        return Task.FromResult("Ensolarado, 25°C");
    }
}
