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
    public class MasterController : AnonymousBaseController
    {
        private readonly IMasterService _masterService;
        public MasterController(IMasterService masterService)
        {
            this._masterService = masterService;
        }

        [HttpGet("GetPRByDateRange")]
        public async Task<IActionResult> GetPRByDateRange(string FromDate, string ToDate)
        {
            return Ok(await this._masterService.PRByDateRange(FromDate, ToDate));
        }
    }
}
