using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Service.Services.Interface
{
    public interface IGetPRNumberByDateRangeService
    {
        public Task<ApiResponse<ResponseMsg>> GetPRNumberByDateRange(string FromDate, string ToDate);
    }
}
