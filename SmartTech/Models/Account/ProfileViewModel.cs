using System;
using Microsoft.AspNetCore.Http;

namespace SmartTech.Models.Account
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string MaritalStatus { get; set; }

        public int NumberOfChildren { get; set; }

        public string Role { get; set; }

        public string ProfilePictureUrl { get; set; }


        public bool Active { get; set; }
        
        public string Status { get; set; }
       
        public DateTime DateJoined { get; set; }
       
        public DateTime ContractExpirationDate { get; set; }
        
        public int YearsOfService { get; set; }
        
        public int MonthsOfService { get; set; }
        
        public int DaysOfService { get; set; }
        
        public string HighestQualification { get; set; }
        
        public string LastSchoolAttended { get; set; }
        
        public string Department { get; set; }
        
        public string Position { get; set; }

        public decimal AnnualSalary { get; set; }
        
        public DateTime LastDateOfPromotion { get; set; }
    }
}