using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampRealEstate.Models
{
    public class ProfessionalViewModel
    {
        public IEnumerable<RealEstateAgent> RealEstateAgents { get; set; }

        public IEnumerable<LoanOfficer> LoanOfficers { get; set; }

        public IEnumerable<Contractor> Contractors { get; set; }
    }
}
