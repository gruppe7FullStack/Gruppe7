using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Gruppe7.Data;
using Gruppe7.Models;

namespace Gruppe7.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _context;
        private readonly string _clientId = "10e02932-6bce-463a-b1e0-e997285d2071"; // Test-client id
        private readonly string _clientSecret = "67be8d57-2d8f-4e9e-80e8-ed1447d54d20"; // Test-client secret

        public WeatherService(HttpClient httpClient, DatabaseContext context)
        {
            _httpClient = httpClient;
            _context = context;
            var byteArray = new System.Text.UTF8Encoding().GetBytes($"{_clientId}:{_clientSecret}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task FetchAndStoreWeatherDataAsync()
        {
            string baseUrl = "https://frost.met.no/observations/v0.jsonld"; // yr.no sitt api
            string source = "SN27160"; // Skoppum, Horten værmålingstasjon 
            string element = "air_temperature";

            DateTime now = DateTime.UtcNow;
            DateTime startDate = now.AddDays(-7);

            for (DateTime date = startDate; date <= now; date = date.AddDays(1))
            {
                string referencetime = date.ToString("yyyy-MM-dd");

                var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}?sources={source}&referencetime={referencetime}&elements={element}");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(responseData);

                    foreach (var data in jsonResponse["data"])
                    {
                        var observationDateTime = DateTime.Parse(data["referenceTime"].ToString());
                        var observationDate = observationDateTime.Date; // Sett tiden til 00:00:00
                        var airTemperature = data["observations"].FirstOrDefault(o => o["elementId"].ToString() == "air_temperature");

                        if (airTemperature != null)
                        {
                            // Sjekk om observasjonen for denne dage allerede eksisterer. 
                            if (!_context.Observation.Any(o => o.Date == observationDate))
                            {
                                var newObservation = new Observation
                                {
                                    Elementid = Guid.NewGuid(),
                                    Value = (double)airTemperature["value"],
                                    TimeOffset = airTemperature["timeOffset"].ToString(),
                                    TimeResolution = airTemperature["timeResolution"].ToString(),
                                    TimeSeriesId = (int)airTemperature["timeSeriesId"],
                                    Date = observationDate
                                };

                                _context.Add(newObservation);
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync(); // Lagre
        }
    }
}

