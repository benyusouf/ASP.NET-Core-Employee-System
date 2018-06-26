using System;

namespace SmartTech.Models.Admin
{
    public class AdminProfileViewModel
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

        public string AdminPasscode { get; set; }
    }
}