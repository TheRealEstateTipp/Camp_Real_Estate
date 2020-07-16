using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CampRealEstate.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Company Name:")]
        public string CompanyName { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
