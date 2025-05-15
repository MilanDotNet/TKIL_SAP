using TKILSAPRFC.Model.DataBaseModels;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Infrastructure.Repository.Interface
{
    public interface ILoginRepository
    {
        //Task<UserLoginDetail?> ValidateUser(UserLoginVM loginVM);
        Task<UserLoginDetailWithDatabase?> ValidateUserMultiDB(UserLoginVM loginVM);
    }
}
