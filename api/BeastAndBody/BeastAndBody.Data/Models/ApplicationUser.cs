using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeastAndBody.Data.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string Regio { get; set; }
        public Activity Activity { get; set; }
    }
}
