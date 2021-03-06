﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CampRealEstate.Models
{
    public class LoanOfficer
    {
        [Key]
        public int LoanOfficerId { get; set; }

        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Display(Name = "Telephone Number:")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "License Number:")]
        public string LicenseNumber { get; set; }

        [Display(Name = "Date Registered In Network:")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DateRegistered { get; set; }

        [Display(Name = "Agree to Response Req:")]
        public bool IsResponsive { get; set; }

        [Display(Name = "VA Loan Knowledge:")]
        public bool HasVALoanKnowledge { get; set; }

        [Display(Name = "Approved by Admin:")]
        public bool IsApproved { get; set; }

        [Display(Name = "Profile Image:")]
        public string ImageUrl { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
