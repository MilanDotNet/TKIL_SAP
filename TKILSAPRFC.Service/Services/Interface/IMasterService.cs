using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Service.Services.Interface
{
    public interface IMasterService
    {
        public Task<ApiResponse<ResponseMsg>> PRByDateRange(string FromDate, string ToDate);
    }
}
