using System;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Models.Admin
{
    public class EditViewModel
    {
        public string Id { get; set; }
        
        [Required, EmailAddress, MaxLength(100), Display(Name = "Email Address")]
        public string Email { get; set; }
        
        [Required, MaxLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required, MaxLength(25), DataType(DataType.PhoneNumber), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Required, DataType(DataType.DateTime), Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
        
        [Required, DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Required, MaxLength(10)]
        public string Gender { get; set; }

        [Required, MaxLength(20), Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Number of Children (if Any)")]
        public int NumberOfChildren { get; set; }

        
        
        
        
        [Required, Display(Name = "Activate Employee")]
        public bool Active { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }

        [Display(Name = "Highest Qualification Obtained")]
        public string HighestQualification { get; set; }

        [Display(Name = "Last School Attended")]
        public string LastSchoolAttended { get; set; }
        
        [Required, DataType(DataType.DateTime), Display(Name = "Date Hired")]
        public DateTime DateJoined { get; set; }
        
        [DataType(DataType.DateTime), Display(Name = "Contract Expire")]
        public DateTime ContractExpirationDate { get; set; }
        
        [Required, Display(Name = "Years")]
        public int YearsOfService { get; set; }

        [Required, Display(Name = "Months")]
        public int MonthsOfService { get; set; }

        [Required, Display(Name = "Days")]
        public int DaysOfService { get; set; }

        [Required, Display(Name = "Annual Salary")]
        public decimal AnnualSalary { get; set; }

        [Required, DataType(DataType.DateTime), Display(Name = "Last Date of Promotion")]
        public DateTime LastDateOfPromotion { get; set; }
    }
}