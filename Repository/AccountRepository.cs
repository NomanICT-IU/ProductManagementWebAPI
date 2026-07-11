using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagementWebAPI.Data;
using ProductManagementWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagementWebAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(signUpModel.Email!);

            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Email already exists."
                });
            }

            var user = new ApplicationUser
            {
                FullName = signUpModel.FullName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
                IsActive = true
            };

            // Password is hashed automatically
            return await _userManager.CreateAsync(user, signUpModel.Password!);
        }

        public async Task<IActionResult> SignInAsync(SignInModel signInModel)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(signInModel.Email!);

            if (user == null)
            {
                return new UnauthorizedObjectResult(new
                {
                    Message = "Invalid Email or Password."
                });
            }

            // Check if account is active
            if (!user.IsActive)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "Your account is inactive."
                });
            }

            // Verify password
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                signInModel.Password!,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new UnauthorizedObjectResult(new
                {
                    Message = "Invalid Email or Password."
                });
            }

            // JWT Claims
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // Secret Key
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            // Create Token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                FullName = user.FullName,
                Email = user.Email
            });
        }
    }
}