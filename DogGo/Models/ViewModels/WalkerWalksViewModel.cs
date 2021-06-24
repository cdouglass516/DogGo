using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerWalksViewModel
    {
        public Walker Walker { get; set; }
        public Neighborhood NH { get; set; }
        public List<WalksViewModel> Walks { get; set;}

    }
}
