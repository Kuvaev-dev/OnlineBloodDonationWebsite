using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BloodGroupsMV
    {
        public int BloodGroupID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
    }
}