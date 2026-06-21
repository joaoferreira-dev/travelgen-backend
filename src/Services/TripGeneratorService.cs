using TravelGenApi.Models;
using TravelGenApi.Services.External;

namespace TravelGenApi.Services;

public interface ITripGeneratorService
{
    Task<TripItinerary> GenerateItineraryAsync(TripRequest request);
}

public class TripGeneratorService : ITripGeneratorService
{
    private readonly IGeocodingClient _geocodingClient;
    private readonly IAttractionClient _attractionClient;
    private readonly IWeatherClient _weatherClient;

    public TripGeneratorService(
        IGeocodingClient geocodingClient, 
        IAttractionClient attractionClient, 
        IWeatherClient weatherClient)
    {
        _geocodingClient = geocodingClient;
        _attractionClient = attractionClient;
        _weatherClient = weatherClient;
    }

    public async Task<TripItinerary> GenerateItineraryAsync(TripRequest request)
    {
        // 1. Converter cidade em coordenadas
        var coords = await _geocodingClient.GetCoordinatesAsync(request.Destination);
        if (coords == null) return new TripItinerary(request.Destination, new List<DayPlan>());

        // 2 & 3. Buscar pontos turísticos e clima
        var attractions = await _attractionClient.GetAttractionsAsync(coords, request.Interests);
        var weather = await _weatherClient.GetWeatherForecastAsync(coords);

        // 4 & 5. Ranqueamento simples e Distribuição em dias
        var dayPlans = new List<DayPlan>();
        int attractionsPerDay = attractions.Count > 0 ? (int)Math.Ceiling((double)attractions.Count / Math.Max(1, request.Days)) : 0;

        for (int i = 1; i <= request.Days; i++)
        {
            var dailyAttractions = attractions
                .Skip((i - 1) * attractionsPerDay)
                .Take(attractionsPerDay)
                .ToList();
                
            dayPlans.Add(new DayPlan(i, weather, dailyAttractions));
        }

        // 6. Retorna estrutura JSON
        return new TripItinerary(request.Destination, dayPlans);
    }
}
