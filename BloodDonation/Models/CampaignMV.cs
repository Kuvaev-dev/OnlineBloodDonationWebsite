using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class CampaignMV
    {
        public int CampaignID { get; set; }

        [Display(Name = "Campaign Title")]
        [Required(ErrorMessage = "Required*")]
        public string CampaignTitle { get; set; }

        [Display(Name = "Campaign Photo")]
        public string CampaignPhoto { get; set; }

        [Display(Name = "Blood Bank")]
        [Required(ErrorMessage = "Required*")]
        public int BloodBankID { get; set; }

        [Display(Name = "Campaign Date")]
        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Date)]
        public DateTime CampaignDate { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Required*")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "Required*")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Required*")]
        public string Location { get; set; }

        [Display(Name = "Campaign Details")]
        [Required(ErrorMessage = "Required*")]
        public string CampaignDetails { get; set; }

        [NotMapped]
        public HttpPostedFileBase CampaignPhotoFile { get; set; }
    }
}