# TravelGen AI — Project Planning (MVP → V2)

## Objective
Build a travel itinerary generator using external APIs, rule-based logic in MVP, and pluggable AI in V2.

## Stack
- Frontend: React + TypeScript
- Backend: ASP.NET Core Web API
- Database: PostgreSQL
- Deploy: Vercel (frontend), Render (backend), Neon/Supabase (DB)

## Architecture
React UI → ASP.NET Core API → Application Layer → Domain Layer → Infrastructure Layer (APIs + DB + AI provider)

## MVP Scope (No AI)

### Input
- Destination
- Days
- Budget
- Interests

### Output
- Day-by-day itinerary
- Attractions per day
- Basic map view
- Weather forecast

## External APIs
- OpenStreetMap (geocoding)
- OpenTripMap (tourist attractions)
- Open-Meteo (weather)
- Unsplash (images optional)

## MVP Logic
1. Convert city to coordinates
2. Fetch attractions
3. Fetch weather
4. Rank attractions
5. Distribute across days
6. Return structured itinerary JSON

## Database
Trips, Attractions, ItineraryDays

## AI Design (Future Ready)

### Interface
public interface ITravelAIProvider {
    Task<TravelItinerary> GenerateItineraryAsync(TravelAIRequest request);
}

## V2 AI Integration
- Ollama (local models like Llama 3 / Mistral)
- OpenAI (cloud models)

AI responsibilities:
- Organize itinerary
- Personalize based on interests
- Explain decisions

## Important Principle
AI must NOT generate raw data (attractions, weather, etc). It only structures and enhances.

## Frontend Pages
- Home (form)
- Trip results (itinerary + map)
- History (future)

## Maps
Leaflet.js for visualization

## Deployment
- Frontend: Vercel
- Backend: Render
- DB: Neon/Supabase

## Roadmap
MVP → Rule-based system
V2 → AI enhancement
V3 → SaaS (auth, sharing, multi-language)

