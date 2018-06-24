using System.Collections.Generic;

namespace SmartTech.Models.Admin
{
    public class IndexViewModel
    {
        public IEnumerable<IndexListingViewModel> AllEmployees { get; set; }
        
        public IEnumerable<IndexListingViewModel> ActiveEmployees { get; set; }
        
        public IEnumerable<IndexListingViewModel> PendingEmployees { get; set; }
    }
}