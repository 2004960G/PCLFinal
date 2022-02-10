using System;
using System.Collections.Generic;

namespace PCL.Shared.Domain
{
    public class Applicant : BaseDomainModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public virtual List<Romance> Romances { get; set; }
    }
}