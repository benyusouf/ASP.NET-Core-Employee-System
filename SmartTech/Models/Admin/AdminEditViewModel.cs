using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartTech.Models.Admin
{
    public class AdminEditViewModel
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
        
        [DataType(DataType.ImageUrl), Display(Name = "Choose Photo")]
        public IFormFile ProfilePictureUrl { get; set; }
    }
}