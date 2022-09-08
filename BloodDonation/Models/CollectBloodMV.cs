using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class CollectBloodMV
    {
        public int BloodBankStockDetailID { get; set; }
        public int BloodBankStockID { get; set; }
        public int BloodGroupID { get; set; }
        public int CampaignID { get; set; }
        public double Quantity { get; set; }
        public int DonorID { get; set; }
        public DateTime DonateDateTime { get; set; }
        public CollectedBloodDonorDetailMV DonorDetails { get; set; }

        public CollectBloodMV()
        {
            DonorDetails = new CollectedBloodDonorDetailMV();
        }
    }
}