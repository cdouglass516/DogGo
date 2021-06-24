using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalksViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DisplayDate { get; set; }
        public int Duration { get; set; }
        public string DisplayDuration { get; set; }
        public int WalkerId { get; set; }
        public string WalkerName { get; set; }
        public int DogId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string DogName { get; set; }
    }
}
