using System;

namespace PCL.Shared.Domain
{
    public class Romance : BaseDomainModel
    {
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public int MyActivityId { get; set; }
        public virtual MyActivity MyActivity { get; set; }
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}