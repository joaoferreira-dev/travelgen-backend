namespace TravelGenApi.Models;

public record TripRequest(string Destination, int Days, decimal Budget, List<string> Interests);

public record TripItinerary(string Destination, List<DayPlan> Days);

public record DayPlan(int DayNumber, string WeatherForecast, List<Attraction> Attractions);

public record Attraction(string Name, string Description, double Latitude, double Longitude);
