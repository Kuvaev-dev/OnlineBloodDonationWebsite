﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ReigstrationMV
    {
        public int UserTypeID { get; set; }
        public string ContactNo { get; set; }
        public int CityID { get; set; }
        public int BloodGroupID { get; set; }
        public int GenderID { get; set; }
        public SeekerMV Seeker { get; set; }
        public HospitalMV Hospital { get; set; }
        public BloodBankMV BloodBank { get; set; }
        public DonorMV Donor { get; set; }
        public UserMV User { get; set; }

        public ReigstrationMV()
        {
            Seeker = new SeekerMV();
            Hospital = new HospitalMV();
            BloodBank = new BloodBankMV();
            Donor = new DonorMV();
            User = new UserMV();
        }
    }
}