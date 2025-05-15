using Microsoft.AspNetCore.Mvc;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Service.Services.Interface
{
    public interface IConnectionService
    {
        public Task<ApiResponse<ResponseMsg>> ConnectionTest();
    }
}
