using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TKILSAPRFC.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
    [AllowAnonymous]
    public class AnonymousBaseController : BaseController
    {
    }

    [Authorize]
    public class AuthorizedController : BaseController
    {
    }
}
