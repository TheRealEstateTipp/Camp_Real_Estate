using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampRealEstate.Data;
using Microsoft.AspNetCore.Mvc;

namespace CampRealEstate.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
