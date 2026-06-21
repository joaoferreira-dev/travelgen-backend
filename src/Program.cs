using TravelGenApi.Models;
using TravelGenApi.Services;
using TravelGenApi.Services.External;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddScoped<IGeocodingClient, MockGeocodingClient>();
builder.Services.AddScoped<IAttractionClient, MockAttractionClient>();
builder.Services.AddScoped<IWeatherClient, MockWeatherClient>();
builder.Services.AddScoped<ITripGeneratorService, TripGeneratorService>();

var app = builder.Build();
var apiGroup = app.MapGroup("/travelgen-api/v1");

apiGroup.MapGet("/health-check", () => "Healthy!");

// Endpoint do Gerador de Roteiros
apiGroup.MapPost("/itinerary/generate", async (TripRequest request, ITripGeneratorService service) =>
{
    var itinerary = await service.GenerateItineraryAsync(request);
    return Results.Ok(itinerary);
});

app.Run();
