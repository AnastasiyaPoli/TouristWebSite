using DAL.Models;
using System.Collections.Generic;

namespace TouristWebSite.Models
{
    public class ActiveNewsViewModel
    {
        public List<New> ActiveNews { get; set; }
        public List<Discount> ActiveDiscounts { get; set; }
    }
}
