using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTech.Data;
using SmartTech.Data.Entities;
using SmartTech.Models.Account;

namespace SmartTech.Controllers
{
    [Authorize(Roles = "Employee, Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _environment;
        private readonly IApplicationUser _applicationUser;
        
        private static string _filename;
        private static string _userRole;
       // private IHttpContextAccessor _httpContextAccessor;


        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IHostingEnvironment environment,
            IApplicationUser applicationUser,
            IHttpContextAccessor httpContextAccessor 
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _applicationUser = applicationUser;
          //  _httpContextAccessor = httpContextAccessor;
        }
        
        
        // GET Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        // POST Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            
                
            switch (vm.Role)
            {
                case "admin":
                    var admin = new Admin
                    {
                        UserName = vm.Email, Email = vm.Email, FirstName = vm.FirstName, LastName = vm.LastName, 
                        BirthDate = vm.BirthDate, PhoneNumber = vm.PhoneNumber, Address = vm.Address, Gender = vm.Gender,
                        MaritalStatus = vm.MaritalStatus, NumberOfChildren = vm.NumberOfChildren, 
                        ProfilePictureUrl = _filename, Role = vm.Role, AdminPasscode = vm.AdminPasscode
                    };
                            
                    var adminResult = await _userManager.CreateAsync(admin, vm.Password);

                    if (adminResult.Succeeded)
                    {
                        // Creating path to upload picture and uploading picture
                        await PictureUpload(vm.ProfilePictureUrl, admin.Id);
                        admin.ProfilePictureUrl = _filename;
                        await _applicationUser.UpdateAdmin(admin);

                        await _userManager.AddToRoleAsync(admin, "Admin");
                        await _signInManager.SignInAsync(admin, false);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        foreach (var error in adminResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                    break;
                        
                case "employee":
                    var employee = new Employee
                    {
                        UserName = vm.Email, Email = vm.Email, FirstName = vm.FirstName, LastName = vm.LastName, 
                        BirthDate = vm.BirthDate, PhoneNumber = vm.PhoneNumber, Address = vm.Address, Gender = vm.Gender,
                        MaritalStatus = vm.MaritalStatus, NumberOfChildren = vm.NumberOfChildren, 
                        ProfilePictureUrl = _filename, Role = vm.Role
                    };
                            
                    var employeeResult = await _userManager.CreateAsync(employee, vm.Password);

                    if (employeeResult.Succeeded)
                    {
                        // Creating path to upload picture and uploading picture
                        await PictureUpload(vm.ProfilePictureUrl, employee.Id);
                        employee.ProfilePictureUrl = _filename;
                        await _applicationUser.UpdateEmployee(employee);
                        await _userManager.AddToRoleAsync(employee, "Employee");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in employeeResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                    break;
                            
            }
            return View(vm);
        }
        
       
        
        // GET Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        // POST Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = _applicationUser.GetUserByEmail(vm.Email);

            if (user == null){
                ModelState.AddModelError("", "Invalid Login Attemp!");
                return View(vm);
            }
                
            _userRole = user.Role.ToLower();
                
            switch (_userRole)
            {
                case "admin":
                    var adminResult = 
                        await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
                    if (adminResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Password!");
                        return View();
                    }
                    break;
                    
                case "employee":
                    var employee = _applicationUser.GetEmployeeByEmail(vm.Email);
                    if (employee.Active)
                    {
                        var employeeResult =
                            await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
                        if (employeeResult.Succeeded)
                        {
                            //This doesn't work -- var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                           //This seems redundant -- var currentUser = _applicationUser.GetEmployeeByEmail(vm.Email);
                            
                            return RedirectToAction("Profile", new {id = employee.Id});
                        }
                    }
                    else
                    {
                        return RedirectToAction("Contact", "Home");
                    }
                    break;
                    
                default:
                    ModelState.AddModelError("", "Invalid Login Attempt!");
                    return View(vm);
            }

            return View(vm);
        }
        
        // LOGOUT -- Logout Action
        public async Task<IActionResult> Logout() { 
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Login", "Account"); 
        }   
    
        // GET Profile
        public async Task<IActionResult> Profile(string id)
        {
            var profile = await _applicationUser.GetEmployeeById(id);
            var status = _applicationUser.GetEmployeeStatus(id);
            var role = _applicationUser.GetRole(id);

            if (profile == null) return View();
            var model = new ProfileViewModel
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate.Date,
                Gender = profile.Gender,
                NumberOfChildren = profile.NumberOfChildren,
                MaritalStatus = profile.MaritalStatus,
                Role = role,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                
                Active = profile.Active,
                Status = status,
                Department = profile.Department,
                Position = profile.Position,
                AnnualSalary = profile.AnnualSalary,
                DateJoined = profile.DateJoined.Date,
                ContractExpirationDate = profile.ContractExpirationDate.Date,
                LastDateOfPromotion = profile.LastDateOfPromotion.Date,
                YearsOfService = profile.YearsOfService,
                MonthsOfService = profile.MonthsOfService,
                DaysOfService = profile.DaysOfService,
                HighestQualification = profile.HighestQualification,
                LastSchoolAttended = profile.LastSchoolAttended
            };
            return View(model);

        }
        
        
     
        // GET Edit
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
                BirthDate = employee.BirthDate.Date,
                Gender = employee.Gender,
                NumberOfChildren = employee.NumberOfChildren,
                MaritalStatus = employee.MaritalStatus,
            };
            
            return View(model);
        }
        
        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditViewModel vm)
        {
            if (!ModelState.IsValid) return View();
            // Creating path to upload picture and uploading picture
            await PictureUpload(vm.ProfilePictureUrl, vm.Id);

            var employee = await _applicationUser.GetEmployeeById(id);
            if (employee == null) return View();
            employee.FirstName = vm.FirstName;
            employee.LastName = vm.LastName;
            employee.Email = vm.Email;
            employee.Address = vm.Address;
            employee.PhoneNumber = vm.PhoneNumber;
            employee.BirthDate = vm.BirthDate;
            employee.Gender = vm.Gender;
            employee.MaritalStatus = vm.MaritalStatus;
            employee.NumberOfChildren = vm.NumberOfChildren;
            employee.ProfilePictureUrl = _filename;

            await _applicationUser.UpdateEmployee(employee);
           return RedirectToAction("Profile", new {id = employee.Id});
        }
        
        // Delete Employee Account Action
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _applicationUser.GetEmployeeById(id);
            await _applicationUser.DeleteEmployee(employee);   
            return RedirectToAction("Login");
        }
        
        
        
        
        
        
        // Profile Picture Function Definition to be called on Register and Edit post methods 
        [AllowAnonymous]
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