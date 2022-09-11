﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class RequestMV
    {
        public int RequestID { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestByID { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AcceptedID { get; set; }
        public string AcceptedFullName { get; set; }
        public int AcceptedTypeID { get; set; }
        public string AcceptedType { get; set; }
        public int RequiredBloodGroupID { get; set; }
        public string BloodGroup { get; set; }
        public int RequestTypeID { get; set; }
        public string RequestTypeStatus { get; set; }
        public int RequestStatusID { get; set; }
        public string RequestStatus { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required*")]
        public DateTime ExpectedDate { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Required*")]
        public string RequestDetails { get; set; }
    }
}