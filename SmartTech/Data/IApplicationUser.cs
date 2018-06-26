using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartTech.Data.Entities;

namespace SmartTech.Data
{
    public interface IApplicationUser
    {
        IEnumerable<ApplicationUser> GetAllEmployees();
        IEnumerable<ApplicationUser> GetActiveEmployees();
        IEnumerable<ApplicationUser> GetPendingEmployees();

        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByEmail(string email);
        Task<Admin> GetAdminById(string id);
        Employee GetEmployeeByEmail(string email);

        string GetFirstName(string id);
        string GetLastName(string id);
        string GetRole(string id);
        string GetEmail(string id);
        string GetPhoneNumber(string id);
        DateTime? GetBirthDate(string id);
        string GetGender(string id);
        string GetMaritalStatus(string id);
        string GetProfilePictureUrl(string id);
        string GetAddress(string id);
        int GetNumberOfChildren(string id);

        string GetEmployeeStatus(string id);
        DateTime GetEmployeeDateJoined(string id);
        DateTime GetEmployeeContractExpirationDate(string id);
        DateTime GetEmployeeLastDateOfPromotion(string id);
        int GetEmployeeYearsOfService(string id);
        int GetEmployeeMonthsOfService(string id);
        int GetEmployeeDaysOfService(string id);
        string GetEmployeeHighestQualification(string id);
        string GetEmployeeLastSchoolAttended(string id);
        string GetEmployeeDepartment(string id);
        string GetEmployeePosition(string id);
        decimal GetEmployeeAnnualSalary(string id);

        Task<Employee> GetEmployeeById(string id);
        
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task UpdateAdmin(Admin admin);
        Task DeleteAdmin(Admin admin);
    }
}