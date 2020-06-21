using System;
using System.Collections.Generic;
using System.Text;

namespace BeastAndBody.Data.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Rules { get; set; }
        public ApplicationUser Coach { get; set; }
        public ICollection<ApplicationUser> Clients { get; set; }
        public string Location { get; set; }
    }
}
