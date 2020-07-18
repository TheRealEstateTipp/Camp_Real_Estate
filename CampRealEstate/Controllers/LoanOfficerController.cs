﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CampRealEstate.Controllers
{   [Authorize(Roles = "LoanOfficer")]
    public class LoanOfficerController : Controller
    {
        private ApplicationDbContext _context;

        public LoanOfficerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loanOfficer = _context.LoanOfficers.Where(l => l.IdentityUserId == userId).FirstOrDefault();

            if(loanOfficer == null)
            {
                return RedirectToAction("Create");
            }

            return View(loanOfficer);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(LoanOfficer loanOfficer)
        {
                loanOfficer.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.LoanOfficers.Add(loanOfficer);
                _context.SaveChanges();
                return RedirectToAction("Upload");
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file, LoanOfficer loanOfficer)
        {
            if (file.Length > 0)
            {
                loanOfficer.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string _fileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Directory.GetCurrentDirectory(), @"UploadedImages", _fileName);
                using (var filestream = new FileStream(_path, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                loanOfficer.ImageUrl = _path;
                _context.LoanOfficers.Update(loanOfficer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
