using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace CampRealEstate.Controllers
{
    public class ContractorController : Controller
    {
        private ApplicationDbContext _context;

        public ContractorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contractor = _context.Contractors.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            if(contractor == null)
            {
                return RedirectToAction("Create");
            }
            return View(contractor);
        }

        public IActionResult Create()
        {
            Contractor contractor = new Contractor();
            contractor.ContractorType = new List<SelectListItem>
            {
                new SelectListItem {Text = "General", Value = "1"},
                new SelectListItem {Text = "Home Inspector", Value = "2"},
                new SelectListItem {Text = "HVAC", Value = "3"},
                new SelectListItem {Text = "Plumber", Value = "4"},
                new SelectListItem {Text = "Other", Value = "5"}

            };

            return View(contractor);
        }

        [HttpPost]
        public IActionResult Create(Contractor contractor)
        {
            contractor.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Contractors.Add(contractor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
