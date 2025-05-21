using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using Serilog;
using System.Text.Json;
using System.Net.Http;
using System.Diagnostics;
using TKILSAPRFC.Core.Helpers;

namespace TKILSAPRFC.API.Controllers
{
    public class ConnectionController : AnonymousBaseController
    {
        private readonly IConnectionService _connectionService1;
        public ConnectionController(IConnectionService connectionService)
        {
            this._connectionService1 = connectionService;
        }

        [HttpGet("ConnectionPing")]
        public async Task<IActionResult> ConnectionTest()
        {
            return Ok(await this._connectionService1.ConnectionTest());
        }
    }
}
