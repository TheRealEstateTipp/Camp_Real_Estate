using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampRealEstate.Data;
using Microsoft.AspNetCore.Mvc;

namespace CampRealEstate.Controllers
{
    public class LoanOfficerController : Controller
    {
        private ApplicationDbContext _context;

        public LoanOfficerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
