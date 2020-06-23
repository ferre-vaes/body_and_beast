using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BeastAndBody.Data.Models.Enums;

namespace BeastAndBody.Data.Models
{
    public class Follow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        public FollowType Type { get; set; }
    }
}
