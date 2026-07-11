using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagementWebAPI.Models;

namespace ProductManagementWebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);

        Task<IActionResult> SignInAsync(SignInModel signInModel);
    }
}