using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Security.Interfaces;
using VehicleTracking.Security.Models;

namespace VehicleTracking.Security
{
    public class UserService : IUserService
    {
        private readonly SignInManager<UsersSystem> _signInManager;
        private readonly UserManager<UsersSystem> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<UsersSystem> userManager,
            SignInManager<UsersSystem> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task CreateRolesAsync()
        {
            string[] roleNames = { "Admin", "User", "HR" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
            UsersSystem user = await _userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new UsersSystem()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };
                await _userManager.CreateAsync(user, "123456789");
            }
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        public async Task<string> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public async Task<string> Register(RegisterModel model)
        {
            var user = new UsersSystem
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }


        private async Task<string> GenerateJwtToken(string email, UsersSystem user)
        {
            var roleTask = _userManager.GetRolesAsync(user);
           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Configurations:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Configurations:JwtExpireDays"]));


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            //Assign Roles
            var roles = await roleTask;
            foreach (var r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var token = new JwtSecurityToken(
                _configuration["Configurations:JwtIssuer"],
                _configuration["Configurations:JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
