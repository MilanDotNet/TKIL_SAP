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
    public class GetPRByDateRangeController : AuthorizedController
    {
        private readonly IGetPRByDateRangeService _GetPRByDateRangeService;
        public GetPRByDateRangeController(IGetPRByDateRangeService GetPRByDateRangeService)
        {
            this._GetPRByDateRangeService = GetPRByDateRangeService;
        }

        [HttpGet("GetPRByDateRange")]
        public async Task<IActionResult> GetPRByDateRange(string FromDate, string ToDate)
        {
            return Ok(await this._GetPRByDateRangeService.PRByDateRange(FromDate, ToDate));
        }

    }
}
