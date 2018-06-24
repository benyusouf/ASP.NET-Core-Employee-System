using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartTech.Models.Account
{
    public class RegisterViewModel
    {
        
        
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

        [Required, MaxLength(20), Display(Name = "Choose Role")]
        public string Role { get; set; }
        
        [MaxLength(8), MinLength(8), Display(Name = "Admin Passcode")] 
        public string AdminPasscode { get; set; }
        
        [DataType(DataType.ImageUrl), Display(Name = "Choose Photo")]
        public IFormFile ProfilePictureUrl { get; set; }

        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match!")]
        public string ConfirmPassword { get; set; }
    }
}