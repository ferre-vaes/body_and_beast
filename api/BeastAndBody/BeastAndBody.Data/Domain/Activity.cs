using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeastAndBody.Data.Models
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public string Rules { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser Coach { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
