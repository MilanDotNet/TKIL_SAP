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
    public class GetPRNumberByDateRangeController : AuthorizedController
    {
        private readonly IGetPRNumberByDateRangeService _GetPRNumberByDateRangeService;
        public GetPRNumberByDateRangeController(IGetPRNumberByDateRangeService GetPRNumberByDateRangeService)
        {
            this._GetPRNumberByDateRangeService = GetPRNumberByDateRangeService;
        }

        [HttpGet("GetPRNumberByDateRange")]
        public async Task<IActionResult> GetPRNumberByDateRange(string FromDate, string ToDate)
        {
            return Ok(await this._GetPRNumberByDateRangeService.GetPRNumberByDateRange(FromDate, ToDate));
        }
    }
}
