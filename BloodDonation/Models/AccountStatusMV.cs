using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class AccountStatusMV
    {
        public int AccountStatusID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Status")]
        public string AccountStatus { get; set; }
    }
}