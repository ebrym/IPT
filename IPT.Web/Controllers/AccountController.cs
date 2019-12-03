﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using IPT.Repository.AccountRepo;
using IPT.Data.Entity;
using IPT.Web.Models.AccountModel;
using static IPT.Web.Enums.Enums;

namespace IPT.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        private readonly  IAccountManager _accountManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        //[Route("Identity/Account/Login")]
        //public IActionResult LoginRedirect(string ReturnUrl)
        //{
        //    return Redirect("/Account/Login?ReturnUrl=" + ReturnUrl);
        //}
        public AccountController(SignInManager<User> signInManager,
                                RoleManager<Role> roleManager,
                                UserManager<User> userManager, 
                                IAccountManager accountManager, 
                                IMapper mapper,
                                IHttpContextAccessor contextAccessor)
        {
            _accountManager = accountManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
          

            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginViewModel)
        {
      
            if (ModelState.IsValid)
            {
                var user =  _userManager.Users.FirstOrDefault(m => m.Email.Trim() == loginViewModel.Email);
                
                //var user = await _accountManager.GetUserByEmailAsync(loginViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email/password not found");
                    //Alert("Invalid Email/password.", NotificationType.error);
                    return View(loginViewModel);
                }


                //CookieValidatePrincipalContext cn = new CookieValidatePrincipalContext;
                //cn.ReplacePrincipal(newPrincipal);
                //cn.ShouldRenew = true;


                //var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                //if (passwordIsCorrect)
                //{
                //    //var roles = await _userManager.GetRolesAsync(user);

                //    //List<int> rolesId = new List<int>();
                //    var claims = new List<Claim>();

                //    //foreach (var r in roles)
                //    //{
                //    //    var roleId = _roleManager.Roles.Where(x => x.Name == r).First();
                //    //    // claims.Add(new Claim("Roles", r));
                //    //    rolesId.Add(roleId.Id);
                //    //}
                //    //var permissionsForUser = await _permissionRepository.GetPermissionByRoleIdAsync(rolesId);


                //    //foreach (var claim in permissionsForUser)
                //    //{
                //    //    claims.Add(new Claim("Permissions", claim.PermissionName));
                //    //}
                //    claims.Add(new Claim("Email", user.Email));

                //    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                //    if (claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
                //    {
                //        claimsIdentity.AddClaims(claims);
                //    }


                //    //await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                //    //    claimsPrincipal,
                //    //    new AuthenticationProperties { IsPersistent = true });

                //    var props = new AuthenticationProperties();
                //    props.IsPersistent = true;

                //    await _signInManager.Context.SignInAsync(
                //    CookieAuthenticationDefaults.
                //    AuthenticationScheme,
                //    claimsPrincipal, props);

                //    return RedirectToAction("Index", "Home");
                //}

                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email/password not found");
                    View(loginViewModel);
                }



            }
            ModelState.AddModelError("", "Email/password not found");
            return  View(loginViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.Context.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #region"USER SECTION"

        public async Task<IActionResult> CreateUser()
        {
            var roles = await _accountManager.GetRoles();
            var roleList = roles.Select(a => new SelectListItem()
            {
                Value = a.Name,
                Text = a.Name
            }).ToList();
            ViewBag.roles = roleList;
            return View();
        }
        [HttpPost]
  
        public async Task<IActionResult> CreateUser(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var roles = await _accountManager.GetRoles();
                var roleList = roles.Select(a => new SelectListItem()
                {
                    Value = a.Name,
                    Text = a.Name
                }).ToList();
                ViewBag.roles = roleList;

                if (await _accountManager.IsEmailExistAsync(user.Email))
                {
                    Alert("User already exist with the supplied email.", NotificationType.error);
                    return View(user);
                }

                var mappedUser = _mapper.Map<User>(user);
                mappedUser.UserName = user.Email;
                    var result = await _accountManager.CreateUserAsync(mappedUser, user.Password, user.Role);

                if (result)
                {
                    Alert("Account created sucsessfully.", NotificationType.success);
                    return RedirectToAction("Users");
                }
                else
                {
                    Alert("User account could not be created. Please try again later.", NotificationType.error);
                    
                    return View(user);
                }
            }
            Alert("Entries could not be validate. Please try again.", NotificationType.error);
            return View(user);
        }

        public async Task<IActionResult> Users()
        {
           
                var users = await _accountManager.GetUsers();

                List<UserViewModel> userList = _mapper.Map<List<UserViewModel>>(users);
                return View(userList);
       
        }
  
        public async Task<IActionResult> EditUser(int id)
        {
            try
            {
                var users = await _accountManager.GetUserByIdAsync(id);

                RegisterViewModel user = _mapper.Map<RegisterViewModel>(users);
                return View(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(RegisterViewModel user)
        {
            
            if (ModelState.IsValid)
            {
                var getUser = await _accountManager.GetUserByIdAsync(user.Id);
                if (getUser == null)
                    return View(user);
                getUser.FirstName = user.FirstName;
                getUser.LastName = user.LastName;
                getUser.Department = user.Department;
                getUser.Unit = user.Unit;

               // var mappedUser = _mapper.Map<User>(user);
                getUser.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _accountManager.UpdateUserAsync(getUser);
                
                if (result.Succeeded)
                {
                    Alert("Account updated sucsessfully.", NotificationType.success);
                    return RedirectToAction("Users");
                }
                else
                {
                    Alert("User account could not be updated. Please try again later.", NotificationType.error);
                    return View(user);
                }
            }
            Alert("Entries could not be validate. Please try again.", NotificationType.error);
            return View(user);
        }

        //[PermissionValidation("can_create_user", "can_assign_permission")]
        public async Task<IActionResult> AssignUserRole()
        {
            try
            {
                var roles = await _accountManager.GetRoles();
                var users = await _accountManager.GetUsers();
                
                var userList = users.Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                }).ToList();

                List<UserRoleViewModel> userRoleVM = new List<UserRoleViewModel>();
                //foreach (var item in roles)
                //{
                //    userRoleVM.Add(new UserRoleViewModel { Role = item.Name });
                //}

                ViewBag.roles = userRoleVM;
                ViewBag.users = userList;
                
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        //[PermissionValidation("can_create_user", "can_assign_permission")]
        public async Task<IActionResult> AssignUserRole(string Id, List<UserRoleViewModel> UserRole)
        {
            try { 

                if (!string.IsNullOrEmpty(Id))
                {
                    var roles = await _accountManager.GetRoles();
                    var users = await _accountManager.GetUsers();
                    //var currentRoles = await _accountManager.GetUserAndRolesAsync(Convert.ToInt16(Id));

                    var user = await _userManager.FindByIdAsync(Id.ToString());
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    var userList = users.Select(a => new SelectListItem()
                    {
                        Value = a.Id.ToString(),
                        Text = a.FullName
                    }).ToList();

                    List<UserRoleViewModel> userRoleVM = new List<UserRoleViewModel>();
                    foreach (var item in roles)
                    {
                        var isRoleAssigned = currentRoles.Any(x => x.Contains(item.Name));
                        if (isRoleAssigned)
                        {
                            userRoleVM.Add(new UserRoleViewModel { SelectedRole = true, Role = item.Name });
                        }
                        else
                        {
                            userRoleVM.Add(new UserRoleViewModel { SelectedRole = false, Role = item.Name });
                        }
                        //userRoleVM.Add(new UserRoleViewModel { Role = item.Name });
                    }

                    ViewBag.roles = userRoleVM;
                    ViewBag.users = userList;

                    return View();



                }
        }
            catch (Exception ex)
            {
                throw ex;
            }
            Alert("Please select user.", NotificationType.error);
            return View();
        }

        [HttpPost]
     
        public async Task<IActionResult> CreateUserRole(UserViewModel assignUser, List<UserRoleViewModel> UserRole)
        {

            if (ModelState.IsValid)
            {
                List<UserRoleViewModel> roles = new List<UserRoleViewModel>();
                if (UserRole != null)
                {
                    roles = UserRole.Where(x => x.SelectedRole == true).ToList();
                }
                //else
                //{
                //    roles.Add(new UserRoleViewModel { SelectedRole = true, Role = "Enroller" });
                //}

                List<string> selectecRoles = new List<string>();

                foreach (var role in roles)
                {
                    //var isUserinRole = await _userManager.IsInRoleAsync(user, role.Role);
                    //if (isUserinRole && !role.SelectedRole)
                    //{
                    //    await _userManager.RemoveFromRoleAsync(user, role.Role);
                    //}
                    //else
                    //{
                    //    selectecRoles.Add(role.Role);
                    //}
                    selectecRoles.Add(role.Role);
                }


                var result = await _accountManager.AssignUserRoleAsync(assignUser.Id, selectecRoles);

                if (result)
                {
                    //await _userManager.AddToRoleAsync(user, registerViewModel.UserRoles);
                    Alert("Role(s) assigned to user succesfully.", NotificationType.success);
                    return RedirectToAction("Users");
                }
                else
                {
                    Alert("Error assigning role(s). Please try again later.", NotificationType.error);
                    return View("AssignUserRole");
                }
            }
            Alert("Invalid entries. Please, try again.", NotificationType.info);
            return View("AssignUserRole");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind]ChangePasswordModel changePassword)
        {
            if (!ModelState.IsValid)
            {
                Alert("User not found", NotificationType.error);
                return RedirectToAction("Index", "Home");

            }
            if (ModelState.IsValid)
            {

                //var currentUser = _contextAccessor.HttpContext.User.Identity;
                 var currentUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByIdAsync(currentUser.ToString());
                if (user == null)
                {
                    Alert("Could not retrive user details. Please try again.", NotificationType.error);
                    return RedirectToAction("Index", "Home");
                }
                var changpawd = await _userManager.RemovePasswordAsync(user);
                if (!changpawd.Succeeded)
                {
                    Alert("Can not change password.Please try again later.", NotificationType.error);
                    return RedirectToAction("Index", "Home");
                }
                var result = await _userManager.AddPasswordAsync(user, changePassword.Password);
                if (result.Succeeded)
                {
                    Alert("Password updated sucsessfully.", NotificationType.success);
                }
                else
                {
                    Alert("Can not change password. Please try again later.", NotificationType.error);                  
                }
            }
            return RedirectToAction("Index", "Home");
        }



        #endregion


        #region"ROLE SECTION"
        [AllowAnonymous]
  
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        //[PermissionValidation("can_create_role")]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<Role>(role);

                var result = await _accountManager.CreateRoleAsync(mappedRole);

                if (result)
                {
                    Alert("Role created successfully.", NotificationType.success);
                    return RedirectToAction("Roles");
                }
                else
                {
                    Alert("Error updating role. Please try again later.", NotificationType.error);
                    return View("AssignUserRole");
                }

            }
            Alert("Invalid entries. Please, try again.", NotificationType.info);
            return View(role);
        }

        public async Task<IActionResult> Roles()
        {

            var roles = await _accountManager.GetRoles();

            List<RoleViewModel> roleList = _mapper.Map<List<RoleViewModel>>(roles);
            return View(roleList);

        }

      
        public async Task<IActionResult> EditRole(int id)
        {
            try
            {
                var role = await _accountManager.GetRoleByIdAsync(id);

                RoleViewModel appRole = _mapper.Map<RoleViewModel>(role);
                return View(appRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]

        public async Task<IActionResult> EditRole(RoleViewModel role)
        {

            if (ModelState.IsValid)
            {
                var getRole = await _accountManager.GetRoleByIdAsync(role.Id);
                if (getRole == null)
                    return View(role);
                getRole.Name = role.Name;
                
                var result = await _accountManager.UpdateRoleAsync(getRole);

                if (result)
                {
                    Alert("Role updated successfully.", NotificationType.success);
                    return RedirectToAction("Roles");
                }
                else
                {
                    Alert("Role could not be updated. Please try again later.", NotificationType.error);
                    return View();
                }
            }
            Alert("Invalid entries. Please, try again.", NotificationType.info);
            return View(role);
        }
        #endregion


    }
}