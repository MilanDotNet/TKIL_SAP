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
    public class GetPRByPRNOController : AuthorizedController
    {
        private readonly IGetPRByPRNOService _GetPRByPRNOService;
        public GetPRByPRNOController(IGetPRByPRNOService GetPRByPRNOService)
        {
            this._GetPRByPRNOService = GetPRByPRNOService;
        }

        [HttpGet("GetPRByPRNO")]
        public async Task<IActionResult> GetPRByPRNO(string PRNO)
        {
            return Ok(await this._GetPRByPRNOService.GetPRByPRNO(PRNO));
        }
    }
}
