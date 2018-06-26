using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartTech.Data;
using SmartTech.Data.Entities;

namespace SmartTech.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationContext _context;

        public ApplicationUserService(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<ApplicationUser> GetAllEmployees()
        {
            return _context.Employees;
        }

        public IEnumerable<ApplicationUser> GetActiveEmployees()
        {
            return _context.Employees.Where(e => e.Active);
        }

        public IEnumerable<ApplicationUser> GetPendingEmployees()
        {
            return _context.Employees.Where(e => e.Active == false);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        public Task<Admin> GetAdminById(string id)
        {
            return _context.Admins.SingleOrDefaultAsync(a => a.Id == id);
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }

        public string GetFirstName(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.FirstName;
        }

        public string GetLastName(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.LastName;
        }

        public string GetRole(string id)
        {
            var role = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.Role;

            return role == "admin" ? "Administrator" : "Employee";
        }

        public string GetEmail(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.Email;
        }

        public string GetPhoneNumber(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.PhoneNumber;
        }

        public DateTime? GetBirthDate(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.BirthDate;
        }

        public string GetGender(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.Gender;
        }

        public string GetMaritalStatus(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.MaritalStatus;
        }

        public string GetProfilePictureUrl(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.ProfilePictureUrl;
        }

        public string GetAddress(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.Address;
        }

        public int GetNumberOfChildren(string id)
        {
            var numberOfChildren = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id)?.NumberOfChildren;
            if (numberOfChildren != null)
                return (int) numberOfChildren;
            return 0;
        }

        public string GetEmployeeStatus(string id)
        {
            var active = _context.Employees.FirstOrDefault(e => e.Id == id)?.Active;

            return active.Equals(true) ? "Active Employee" : "Pending Employee";
        }

        public DateTime GetEmployeeDateJoined(string id)
        {
            var dateJoined = _context.Employees.FirstOrDefault(e => e.Id == id)?.DateJoined;
            if (dateJoined != null)
                return (DateTime) dateJoined;
            
            return DateTime.Today;
        }

        public DateTime GetEmployeeContractExpirationDate(string id)
        {
            var contractExpirationDate = _context.Employees.FirstOrDefault(e => e.Id == id)?.ContractExpirationDate;
            if (contractExpirationDate != null)
                return (DateTime) contractExpirationDate;
            
            return DateTime.Today.AddYears(2);
        }

        public DateTime GetEmployeeLastDateOfPromotion(string id)
        {
            var lastDateOfPromotion = _context.Employees.FirstOrDefault(e => e.Id == id)?.LastDateOfPromotion;
            if (lastDateOfPromotion != null)
                return (DateTime) lastDateOfPromotion;
            
            return GetEmployeeDateJoined(id);
        }

        public int GetEmployeeYearsOfService(string id)
        {
            var yearsOfService = _context.Employees.FirstOrDefault(e => e.Id == id)?.YearsOfService;
            if (yearsOfService != null)
                return (int) yearsOfService;
            
            return 2;
        }

        public int GetEmployeeMonthsOfService(string id)
        {
            var monthsOfService = _context.Employees.FirstOrDefault(e => e.Id == id)?.MonthsOfService;
            if (monthsOfService != null)
                return (int) monthsOfService;
            
            return 6;
        }

        public int GetEmployeeDaysOfService(string id)
        {
            var daysOfService = _context.Employees.FirstOrDefault(e => e.Id == id)?.DaysOfService;
            if (daysOfService != null)
                return (int) daysOfService;
            
            return 0;
        }

        public string GetEmployeeHighestQualification(string id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id)?.HighestQualification;
        }

        public string GetEmployeeLastSchoolAttended(string id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id)?.LastSchoolAttended;
        }

        public string GetEmployeeDepartment(string id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id)?.Department;
        }

        public string GetEmployeePosition(string id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id)?.Position;
        }

        public decimal GetEmployeeAnnualSalary(string id)
        {
            var annualSalary = _context.Employees.FirstOrDefault(e => e.Id == id)?.AnnualSalary;
            if (annualSalary != null)
                return (decimal) annualSalary;

            return 0;
        }

        public Task<Employee> GetEmployeeById(string id)
        {
            return _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdmin(Admin admin)
        {
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }
    }
}