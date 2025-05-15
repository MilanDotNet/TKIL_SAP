using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Service.Services.Interface
{
    public interface ILoginService
    {
        Task<ApiResponse<AuthResponseVm>> UserLogin(UserLoginVM entity);
    }
}
