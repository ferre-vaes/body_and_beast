using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeastAndBody.Data.Models
{
    public class Role: IdentityRole<int>
    {
        public class Constants
        {
            public const string Coach = "coach";
            public const string Client = "client";
        }
    }
}
