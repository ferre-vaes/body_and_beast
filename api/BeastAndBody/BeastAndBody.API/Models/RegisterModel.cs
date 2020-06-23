using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BeastAndBody.Data.Models.Enums;

namespace BeastAndBody.Data.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string Regio { get; set; }

        [Required]
        
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public UserType Type { get; set; }
    }
}
