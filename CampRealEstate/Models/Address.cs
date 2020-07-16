using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampRealEstate.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "Street Address:")]
        public string StreetAddress { get; set; }

        [Display(Name = "City:")]
        public string City { get; set; }

        [Display(Name = "State:")]
        public string State { get; set; }

        [Display(Name = "Zip Code:")]
        public double ZipCode { get; set; }

        [Display(Name = "Latitude:")]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude:")]
        public double? Longitude { get; set; }
    }
}
