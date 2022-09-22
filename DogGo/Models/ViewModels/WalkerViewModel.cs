using System.Collections.Generic;
using System.Linq;

namespace DogGo.Models.ViewModels
{
    public class WalkerViewModel
    {
        public Walker Walker { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public List<Walk> Walks { get; set; }

        public int TotalWalkTime
        {
            get
            {
                return Walks.Sum(w => w.Duration);
            }
        }
    }
}
