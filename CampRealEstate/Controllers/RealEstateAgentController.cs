using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampRealEstate.Controllers
{
    public class RealEstateAgentController : Controller
    {
        private ApplicationDbContext _context;

        public RealEstateAgentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var realEstateAgent = _context.RealEstateAgents.Where(r => r.IdentityUserId == userId).FirstOrDefault();

            if (realEstateAgent == null)
            {
                return RedirectToAction("Create");
            }

            return View(realEstateAgent);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RealEstateAgent realEstateAgent)
        {
            realEstateAgent.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.RealEstateAgents.Add(realEstateAgent);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
