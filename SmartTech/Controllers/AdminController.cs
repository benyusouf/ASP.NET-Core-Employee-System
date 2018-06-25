using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Data;
using SmartTech.Models.Admin;

namespace SmartTech.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationUser _applicationUser;

        public AdminController(IApplicationUser applicationUser)
        {
            _applicationUser = applicationUser;
        }
        
        // GET -- Index - Listing All Registered Employee Action
        public IActionResult Index()
        {
            var allEmployees = _applicationUser.GetAllEmployees();
            var allEmployeesResult = allEmployees
                .Select(r => new IndexListingViewModel
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Gender = r.Gender,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber
                });

            var activeEmployees = _applicationUser.GetActiveEmployees();
            var acticeEmployeesResult = activeEmployees
                .Select(r => new IndexListingViewModel
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Gender = r.Gender,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber
                });

            var pendingEmployees = _applicationUser.GetPendingEmployees();
            var pendingEmployeesResult = pendingEmployees
                .Select(r => new IndexListingViewModel
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Gender = r.Gender,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber
                });

            var model = new IndexViewModel
            {
                AllEmployees = allEmployeesResult,
                ActiveEmployees = acticeEmployeesResult,
                PendingEmployees = pendingEmployeesResult
            };
            return
            View(model);
        }

        // GET -- Detail - Employee Detail Action
        public async Task<IActionResult> Detail(string id)
        {
            var detail = await _applicationUser.GetEmployeeById(id);
            var status = _applicationUser.GetEmployeeStatus(id);
            var role = _applicationUser.GetRole(id);

            if (detail == null) return View();
            var model = new DetailViewModel
            {
                Id = detail.Id,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                Email = detail.Email,
                PhoneNumber = detail.PhoneNumber,
                Address = detail.Address,
                BirthDate = detail.BirthDate,
                Gender = detail.Gender,
                NumberOfChildren = detail.NumberOfChildren,
                MaritalStatus = detail.MaritalStatus,
                Role = role,
                ProfilePictureUrl = detail.ProfilePictureUrl,
                
                Active = detail.Active,
                Status = status,
                Department = detail.Department,
                Position = detail.Position,
                AnnualSalary = detail.AnnualSalary,
                DateJoined = detail.DateJoined,
                ContractExpirationDate = detail.ContractExpirationDate,
                LastDateOfPromotion = detail.LastDateOfPromotion,
                YearsOfService = detail.YearsOfService,
                MonthsOfService = detail.MonthsOfService,
                DaysOfService = detail.DaysOfService,
                HighestQualification = detail.HighestQualification,
                LastSchoolAttended = detail.LastSchoolAttended
            };
            return View(model);
        }
        
        // GET -- Edit - Show Employee Edit Page with some data from the database
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await _applicationUser.GetEmployeeById(id);
            if (employee == null) return View();
            var model = new EditViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                NumberOfChildren = employee.NumberOfChildren,
                MaritalStatus = employee.MaritalStatus,
                
                Active = employee.Active,
                Department = employee.Department,
                Position = employee.Position,
                HighestQualification = employee.HighestQualification,
                LastSchoolAttended = employee.LastSchoolAttended,
                DateJoined = employee.DateJoined.Date,
                ContractExpirationDate = employee.ContractExpirationDate.Date,
                YearsOfService = employee.YearsOfService,
                MonthsOfService = employee.MonthsOfService,
                DaysOfService = employee.DaysOfService,
                LastDateOfPromotion = employee.LastDateOfPromotion.Date
            };
            
            return View(model);
        }
        
        // POST -- Edit - Submit Employee changes and Update Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditViewModel vm)
        {
            if (!ModelState.IsValid) return View();

            var employee = await _applicationUser.GetEmployeeById(id);
            if (employee == null) return View();
            employee.FirstName = vm.FirstName;
            employee.LastName = vm.LastName;
            employee.Email = vm.Email;
            employee.PhoneNumber = vm.PhoneNumber;
            employee.Address = vm.Address;
            employee.BirthDate = vm.BirthDate;
            employee.Gender = vm.Gender;
            employee.MaritalStatus = vm.MaritalStatus;
            employee.NumberOfChildren = vm.NumberOfChildren;

            employee.Active = vm.Active;
            employee.Department = vm.Department;
            employee.Position = vm.Position;
            employee.LastSchoolAttended = vm.LastSchoolAttended;
            employee.HighestQualification = vm.HighestQualification;
            employee.DateJoined = vm.DateJoined;
            employee.ContractExpirationDate = vm.ContractExpirationDate;
            employee.YearsOfService = vm.YearsOfService;
            employee.MonthsOfService = vm.MonthsOfService;
            employee.DaysOfService = vm.DaysOfService;
            employee.LastDateOfPromotion = vm.LastDateOfPromotion;
            employee.AnnualSalary = vm.AnnualSalary;

            await _applicationUser.UpdateEmployee(employee);
            return RedirectToAction("Detail", new {id = employee.Id});
        }

        // Delete Employee Action
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _applicationUser.GetEmployeeById(id);
            await _applicationUser.DeleteEmployee(employee);   
            return RedirectToAction("Index");
        }
    }
}