using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampRealEstate.Data;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

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

        public IActionResult Create()
        {
            Client client = new Client();
            client.MilitaryStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "Active", Value = "1"},
                new SelectListItem {Text = "Veteran", Value = "2"}
            };

            client.MilitaryBranch = new List<SelectListItem>
            {
                new SelectListItem {Text = "Army", Value = "1"},
                new SelectListItem {Text = "Air Force", Value = "2"},
                new SelectListItem {Text = "Coast Guard", Value = "3"},
                new SelectListItem {Text = "Navy", Value = "4"},
                new SelectListItem {Text = "Marines", Value = "5"},
            };
            

            return View();
        }
    }


}
