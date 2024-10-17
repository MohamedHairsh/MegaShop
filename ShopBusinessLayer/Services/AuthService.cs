using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ShopBusinessLayer.ApplicationConstants;
using ShopBusinessLayer.Common;
using ShopBusinessLayer.InputModels;
using ShopBusinessLayer.Services.Interfaces;
using ShopBusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ShopBusinessLayer.Services
{
    public class AuthService : IAuthService

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private ApplicationUser ApplicationUser;
        //  protected APIResponse _response;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            ApplicationUser = new();
            // _response = new APIResponse();

        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            ApplicationUser.FirstName = register.FirstName;
            ApplicationUser.LastName = register.LastName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(ApplicationUser, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "Admin");
            }
            return result.Errors;
        }
        public async Task<object> Login(Login login)
        {
            ApplicationUser = await _userManager.FindByEmailAsync(login.Email);
            if (ApplicationUser == null)
            {
                return "Invalid Email Address";
            }
            var result = await _signInManager.PasswordSignInAsync(ApplicationUser, login.Password, isPersistent: true, lockoutOnFailure: true);

            var isValidCredential = await _signInManager.CheckPasswordSignInAsync(ApplicationUser, login.Password, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var token = await GenerateToken();
                LoginResponse loginResponse = new LoginResponse
                {
                    UserId = ApplicationUser.Id,
                    Token = token,

                };
                return loginResponse; ;
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return "Your Account is Locked,Contact System Admin";
                }
                if (result.IsNotAllowed)
                {
                    return "Please Verify Email Address";
                }

                if (isValidCredential == null)
                {
                    return "Invalid Password";
                }
                else
                {
                    return "Login Failed";
                }

            }

        }

        public async Task<dynamic> GenerateToken()
        {
            var seacurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var signingCredentials = new SigningCredentials(seacurityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(ApplicationUser);
            var rolesClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,ApplicationUser.Email)
            }.Union(rolesClaims).ToList();

            var token = new JwtSecurityToken
                (
                  issuer: _config["JwtSettings: Issuer"],
                  audience: _config["JwtSettings: Audience"],
                  claims: claims,
                  signingCredentials: signingCredentials,
                  expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<APIResponse> ResetPasswordAsync(ResetPassWord model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
                return new APIResponse
                {
                    IsSuccess = false,
                    DisplayMessage = "No User found"
                };


            if (model.NewPassword != model.ConfirmPassword)
                return new APIResponse
                {
                    IsSuccess = false,
                    DisplayMessage = "Password doesn't match its confirmation",
                };
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
                return new APIResponse
                {
                    DisplayMessage = "Password has been reset successfully!",
                    IsSuccess = true,
                };
            return new APIResponse
            {
                DisplayMessage = "Password reset failed",
                IsSuccess = false,

            };
        }

    }
}
