using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CampRealEstate
{
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public GlobalRouting (ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            if (controller.Equals("Home"))
            {
                if (_claimsPrincipal.IsInRole("Admin"))
                {
                    context.Result = new RedirectToActionResult("Index", "Admin", null);
                }
                else if (_claimsPrincipal.IsInRole("Client"))
                {
                    context.Result = new RedirectToActionResult("Index", "Client", null);
                }
                else if (_claimsPrincipal.IsInRole("RealEstateAgent"))
                {
                    context.Result = new RedirectToActionResult("Index", "RealEstateAgent", null);
                }
                else if (_claimsPrincipal.IsInRole("LoanOfficer"))
                {
                    context.Result = new RedirectToActionResult("Index", "LoanOfficer", null);
                }
                else if (_claimsPrincipal.IsInRole("Contractor"))
                {
                    context.Result = new RedirectToActionResult("Index", "Contractor", null);
                }
            }
           
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
