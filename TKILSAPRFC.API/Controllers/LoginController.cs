using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;

namespace TKILSAPRFC.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    public class LoginController : AnonymousBaseController
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }
        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginVM UserLoginVM)
        {
            return Ok(await this.loginService.UserLogin(UserLoginVM));
        }
    }
}
