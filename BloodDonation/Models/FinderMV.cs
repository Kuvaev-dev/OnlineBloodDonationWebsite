using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class FinderMV
    {
        public int BloodGroupID { get; set; }
        public int CityID { get; set; }
        public List<FinderSearchResultMV> SearchResult { get; set; }

        public FinderMV()
        {
            SearchResult = new List<FinderSearchResultMV>();
        }
    }
}