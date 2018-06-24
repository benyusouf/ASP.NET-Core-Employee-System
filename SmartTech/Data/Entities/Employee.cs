using System;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Data.Entities
{
    public class Employee: ApplicationUser
    {
        public bool Active { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime DateJoined { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime ContractExpirationDate { get; set; }
        
        public int YearsOfService { get; set; }
        
        public int MonthsOfService { get; set; }
        
        public int DaysOfService { get; set; }
        
        public string HighestQualification { get; set; }
        
        public string LastSchoolAttended { get; set; }
        
        public string Department { get; set; }
        
        public string Position { get; set; }

        public decimal AnnualSalary { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime LastDateOfPromotion { get; set; }
        
        // public IEnumerable<Bonuses> Bonuses { get; set; }
        
        
        
    }
}