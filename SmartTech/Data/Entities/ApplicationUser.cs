using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartTech.Data.Entities
{
    public abstract class ApplicationUser: IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        
        [Required, DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        
        [Required] 
        public string Gender { get; set; }
        
        [Required] 
        public string Role { get; set; }
        
        [Required] 
        public string Address { get; set; }
        
        [Required] 
        public string MaritalStatus { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string ProfilePictureUrl { get; set; }

        public int NumberOfChildren { get; set; }
        
        
        
    }
}