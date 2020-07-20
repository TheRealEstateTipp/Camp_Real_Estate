using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CampRealEstate.Controllers
{
    [Authorize(Roles ="RealEstateAgent")]
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

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file, RealEstateAgent realEstateAgent)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            realEstateAgent = _context.RealEstateAgents.Where(l => l.IdentityUserId == userId).SingleOrDefault();

            if (file.Length > 0)
            {
                string _fileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Directory.GetCurrentDirectory(), @"UploadedImages", _fileName);
                using (var filestream = new FileStream(_path, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                realEstateAgent.ImageUrl = _path;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
