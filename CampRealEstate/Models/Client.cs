using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CampRealEstate.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Display(Name = "Client's First Name:")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Client's Last Name:")]
        public string ClientLastName { get; set; }

        [Display(Name = "Telephone Number:")]
        [DataType(DataType.PhoneNumber)]
        public string ClientPhone { get; set; }

        [Display(Name = "Email:")]
        public string ClientEmail { get; set; }

        [Display(Name = "Client ZipCode:")]
        public double ZipCode { get; set; }

        [Display(Name = "Branch of Service:")]
        public string MilitaryBranch { get; set; }

        [Display(Name = "Military Status:")]
        public string MilitaryStatus { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
