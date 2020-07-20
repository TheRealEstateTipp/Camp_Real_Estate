using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace CampRealEstate.Controllers
{
    [Authorize(Roles = "Contractor")]
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
                new SelectListItem {Text = "Home Inspector", Value = "2" },
                new SelectListItem {Text = "HVAC", Value = "3"},
                new SelectListItem {Text = "Plumber", Value = "4"},
                new SelectListItem {Text = "Electrician", Value = "5"},
                new SelectListItem {Text = "Other", Value = "6"}
            };

            return View(contractor);
        }

        [HttpPost]
        public IActionResult Create(Contractor contractor)
        {
            contractor.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Contractors.Add(contractor);
            _context.SaveChanges();
            return RedirectToAction("Upload");
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file, Contractor contractor)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            contractor = _context.Contractors.Where(l => l.IdentityUserId == userId).SingleOrDefault();

            if (file.Length > 0)
            {
                string _fileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Directory.GetCurrentDirectory(), @"UploadedImages", _fileName);
                using (var filestream = new FileStream(_path, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                contractor.ImageUrl = _path;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditScreeningQuestions()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contractor = _context.Contractors.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            return View(contractor);
        }

        [HttpPost]
        public IActionResult EditScreeningQuestions(Contractor contractor)
        {
            _context.Contractors.Update(contractor);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
