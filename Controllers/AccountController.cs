using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementWebAPI.Models;
using ProductManagementWebAPI.Repository;

namespace ProductManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountRepository.SignUpAsync(signUpModel);

            if (result.Succeeded)
                return Ok(new { message = "User created successfully." });

            return BadRequest(result.Errors);
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _accountRepository.SignInAsync(model);
        }
    }
}
