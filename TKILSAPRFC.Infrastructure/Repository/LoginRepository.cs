using Dapper;
using Microsoft.Extensions.Configuration;
using TKILSAPRFC.Core.Enums;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Helper;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.DataBaseModels;
using TKILSAPRFC.Model.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace TKILSAPRFC.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserLoginDetailWithDatabase?> ValidateUserMultiDB(UserLoginVM loginVM)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                if (loginVM.Username == AppSettings.Current.Username.ToString() && loginVM.Password == AppSettings.Current.Password.ToString())
                {
                    UserLoginDetail userLoginDetail = new UserLoginDetail();
                    userLoginDetail.PERSON_ID = 1;
                    userLoginDetail.USER_ID = "TKILSAPRFC_SAFAL";
                    userLoginDetail.FULL_NAME = "Safal Softcom";
                    userLoginDetail.EMAIL_ADDRESS = "tech-team@safalsoftcom.com";

                    UserLoginDetailWithDatabase UserLoginDetailWithDatabase = new UserLoginDetailWithDatabase
                    {
                        UserLoginDetail = userLoginDetail,
                        DatabaseName = "TKIL_MVC_SAP"
                    };
                    return UserLoginDetailWithDatabase;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
