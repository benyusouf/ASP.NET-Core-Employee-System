using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Data;
using SmartTech.Models.Admin;

namespace SmartTech.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUser _applicationUser;
        private readonly IHostingEnvironment _environment;
        private static string _filename;

        public AdminController(IApplicationUser applicationUser, IHostingEnvironment environment)
        {
            _applicationUser = applicationUser;
            _environment = environment;
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
        
        
        // GET -- Admin Profile Action - Display Admin registration Detail
        public async Task<IActionResult> AdminProfile(string id)
        {
            var profile = await _applicationUser.GetAdminById(id);
            var role = _applicationUser.GetRole(id);

            if (profile == null) return View();
            var model = new AdminProfileViewModel
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate,
                Gender = profile.Gender,
                NumberOfChildren = profile.NumberOfChildren,
                MaritalStatus = profile.MaritalStatus,
                Role = role,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                AdminPasscode = profile.AdminPasscode    
            };
            return View(model);
        }
        
        // GET -- Admin Profile Edit Action - Display Admin Edit Page with some data from the database
        public async Task<IActionResult> AdminEdit(string id)
        {
            var admin = await _applicationUser.GetAdminById(id);
            if (admin == null) return View();
            var model = new AdminEditViewModel
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
                Address = admin.Address,
                BirthDate = admin.BirthDate.Date,
                Gender = admin.Gender,
                NumberOfChildren = admin.NumberOfChildren,
                MaritalStatus = admin.MaritalStatus,
            };
            
            return View(model);
        }
        
        // POST -- Admin Profile Edit Action -- Submit and Update Changes to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEdit(string id, AdminEditViewModel vm)
        {
            if (!ModelState.IsValid) return View();
            
            // Creating path to upload picture and uploading picture -- Accessed from AccountController
            await PictureUpload(vm.ProfilePictureUrl, vm.Id);

            var admin = await _applicationUser.GetAdminById(id);
            if (admin == null) return View();
            admin.FirstName = vm.FirstName;
            admin.LastName = vm.LastName;
            admin.Email = vm.Email;
            admin.Address = vm.Address;
            admin.PhoneNumber = vm.PhoneNumber;
            admin.BirthDate = vm.BirthDate;
            admin.Gender = vm.Gender;
            admin.MaritalStatus = vm.MaritalStatus;
            admin.NumberOfChildren = vm.NumberOfChildren;
            admin.ProfilePictureUrl = _filename;

            await _applicationUser.UpdateAdmin(admin);
            return RedirectToAction("AdminProfile", new {id = admin.Id});
        }
        
        // DELETE -- Delete Admin Account
        public async Task<IActionResult> AdminDelete(string id)
        {
            var admin = await _applicationUser.GetAdminById(id);
            await _applicationUser.DeleteAdmin(admin);
            return RedirectToAction("Login", "Account");
        }
        
        
        // Profile Picture Function Definition to be called on Register and Edit post methods 
        public async Task<string> PictureUpload(IFormFile imageName, string id)
        {
            if (imageName == null) return _filename;
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(Path.Combine(uploadPath, id));
            _filename = imageName.FileName;

            // Check to see if user agent is IE
            if (_filename.Contains('\\'))
            {
                _filename = _filename.Split('\\').Last();
            }

            using (var fileStream = new FileStream(Path.Combine(uploadPath, id, _filename), FileMode.Create))
            {
                await imageName.CopyToAsync(fileStream);
            }

            return _filename;
        }
    }
}