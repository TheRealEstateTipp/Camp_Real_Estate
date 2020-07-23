using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampRealEstate.Controllers
{   
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();

            if (admin == null)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("GetProfessionals");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Admin admin)
        {
            admin.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetProfessionals()
        {
            var proList = new ProfessionalViewModel()
            {
                Contractors = _context.Contractors.ToList(),
                LoanOfficers = _context.LoanOfficers.ToList(),
                RealEstateAgents = _context.RealEstateAgents.ToList()
            };

            return View(proList);
        }

        public async Task<IActionResult> ApproveContractor(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            return View(contractor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveContractor(int? id, Contractor contractor)
        {
            if (id != contractor.ContractorId)
            {
                return NotFound();
            }

            _context.Update(contractor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
