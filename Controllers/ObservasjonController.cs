using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gruppe7.Data;
using Gruppe7.Models;

namespace Gruppe7.Controllers
{
    public class ObservasjonController : Controller
    {
        private readonly DatabaseContext _context;

        public ObservasjonController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Observasjon
        public async Task<IActionResult> Index()
        {
            var observations = await _context.Observation
                .OrderByDescending(o => o.Date)
                .Take(7) // Kun de 7 siste dagene
                .ToListAsync(); ; // Dette var nødvendig fordi i oppgave teksten står det at kontroller og katalogen i views skal ha forskjellige navn
            return View("~/Views/Observation/Index.cshtml", observations); // localhost:<port>/Observasjon
        }
    }
}

