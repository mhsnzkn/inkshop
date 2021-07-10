using AutoMapper;
using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
            UserEditDto model = null;
            if (string.IsNullOrEmpty(id))
            {
                model = new UserEditDto();
            }
            else
            {
                var user = await userManager.FindByIdAsync(id);
                model = mapper.Map<UserEditDto>(user);
                var roles = await userManager.GetRolesAsync(user);
                model.Role = roles.FirstOrDefault();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditDto userDto)
        {
            Core.Utility.Result result = new();
            try
            {
                if (string.IsNullOrEmpty(userDto.Id))
                {
                    var user = new ApplicationUser { UserName = userDto.Username, Email = userDto.Email, PersonnelId = userDto.PersonnelId };
                    var addResult = await userManager.CreateAsync(user, userDto.Password);
                    if (addResult.Succeeded)
                    {
                        await CheckRole(userDto.Role);

                        await userManager.AddToRoleAsync(user, userDto.Role);

                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        await userManager.ConfirmEmailAsync(user, token);

                    }
                    foreach (var error in addResult.Errors)
                    {
                        result.Message += error.Description + ". ";
                    }
                }
                else
                {
                    var currentUser = await userManager.FindByIdAsync(userDto.Id);
                    if(userDto.Username != currentUser.UserName)
                        await userManager.SetUserNameAsync(currentUser, userDto.Username);
                    if(userDto.Email != currentUser.Email)
                    {
                        await userManager.SetEmailAsync(currentUser, userDto.Email);
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(currentUser);
                        await userManager.ConfirmEmailAsync(currentUser, token);
                    }
                    currentUser.PersonnelId = userDto.PersonnelId;
                    await userManager.RemovePasswordAsync(currentUser);
                    await userManager.AddPasswordAsync(currentUser, userDto.Password);
                    var currentRoles = await userManager.GetRolesAsync(currentUser);
                    if (!currentRoles.Any(a => a.Contains(userDto.Role)))
                    {
                        await CheckRole(userDto.Role);

                        await userManager.RemoveFromRoleAsync(currentUser, userDto.Role);
                        await userManager.AddToRoleAsync(currentUser, userDto.Role);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return Ok(result);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = new Core.Utility.Result();
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            var query = mapper.ProjectTo<UserTableDto>(userManager.Users);

            var users = await query.ToListAsync();
            var result = new DataTableResult();
            result.Data = users;
            result.Draw = param.draw;
            result.RecordsTotal = result.RecordsFiltered = users.Count;
            return Ok(result);
        }

        private async Task CheckRole(string role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                var roleToAdd = new IdentityRole(role);
                await roleManager.CreateAsync(roleToAdd);
            }
        }
    }
}
