using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BeastAndBody.Data.Models.Enums;

namespace BeastAndBody.Data.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string Regio { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType Type { get; set; }
    }
}
