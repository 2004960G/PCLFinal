using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Shared.Domain
{
    public class MyActivity : BaseDomainModel
    {
        public int Year { get; set; }
        //public int Month { get; set; }
        //public int Day { get; set; }
        public int CafeId { get; set; }
        public virtual Cafe Cafe { get; set; }
        public int DinnerId { get; set; }
        public virtual Dinner Dinner { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int FestivalId { get; set; }
        public virtual Festival Festival { get; set; }
        public virtual List<Romance> Romances { get; set; }
    }
}
