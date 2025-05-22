using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Service.Services.Interface
{
    public interface IGetPRByPRNOService
    {
        public Task<ApiResponse<ResponseMsg>> GetPRByPRNO(string PRNO);
    }
}
