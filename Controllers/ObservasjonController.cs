/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gruppe7.Data;
using Gruppe7.Models;
using Gruppe7.Services;

namespace Gruppe7.Controllers
{
    public class ObservasjonController : Controller
    {
        private readonly DatabaseContext _context; // Database kontekst
        private readonly WeatherService _weatherService; // Tjenste for å få data fra yr sitt api

        public ObservasjonController(DatabaseContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Observasjon
        public async Task<IActionResult> Index()
        {
            // Fetch og lagre weather data fra yr sitt api 
            await _weatherService.FetchAndStoreWeatherDataAsync();

            var observations = await _context.Observation
                .OrderByDescending(o => o.Date)
                .Take(7) // Kun de 7 siste dagene
                .ToListAsync(); ; // Dette var nødvendig fordi i oppgave teksten står det at kontroller og katalogen i views skal ha forskjellige navn

            return View("~/Views/Observation/Index.cshtml", observations); // localhost:<port>/Observasjon
        }
    }
}*/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gruppe7.Data;
using Gruppe7.Services;

namespace Gruppe7.Controllers
{
    public class ObservasjonController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly WeatherService _weatherService;

        public ObservasjonController(DatabaseContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch and store weather data
            await _weatherService.FetchAndStoreWeatherDataAsync();

            // Retrieve the latest 7 days of observations
            var observasjoner = await _context.Observation
                .OrderByDescending(o => o.Date)
                .Take(7)
                .ToListAsync();

            return View("~/Views/Observation/Index.cshtml", observasjoner);
        }
    }
}


