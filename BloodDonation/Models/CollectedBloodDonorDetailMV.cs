﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class CollectedBloodDonorDetailMV
    {
        public int DonorID { get; set; }
        public string FullName { get; set; }
        public int GenderID { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastDonationDate { get; set; }
        public string ContactNo { get; set; }
        public string CNIC { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int AccountStatus { get; set; }
        public int UserTypeID { get; set; }
        public string Description { get; set; }
    }
}