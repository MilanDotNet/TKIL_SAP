using Microsoft.Extensions.Configuration;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NwRfcNet;
using NwRfcNet.TypeMapper;
using static TKILSAPRFC.Model.ViewModels.mdl_PR;
using System.Runtime.InteropServices;
using TKILSAPRFC.Infrastructure.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace TKILSAPRFC.Infrastructure.Repository
{
    public class GetPRNumberByDateRangeRepository : IGetPRNumberByDateRangeRepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly RfcConnection _Rfcconnection;

        public GetPRNumberByDateRangeRepository(ICurrentUser currentUser, IConfiguration configuration, RfcConnection rfcconnection)
        {
            this.currentUser = currentUser;
            this._configuration = configuration;
            _Rfcconnection = rfcconnection;
        }

        public async Task<ResponseMsg> GetPRNumberByDateRange(string FromDate, string ToDate)
        {
            ResponseMsg msg = new ResponseMsg();
            try
            {
                _Rfcconnection.Open();
                _Rfcconnection.Ping();

                Console.WriteLine("SAP connection is active.");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Start Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());

                var mapper = _Rfcconnection.Mapper;
                Configure(mapper);

                var func = _Rfcconnection.CallRfcFunction("ZPRCSNS_GET_PR_NO_BY_DATE");
                var inputParameters = new PurchaseRequestInput
                {
                    IM_V_PR_CHNG_FRM_DATE = FromDate,
                    IM_V_PR_CHNG_TO_DATE = ToDate
                };
                func.Invoke(inputParameters);

                var output = func.GetOutputParameters<PurchaseRequestOutput>();
                if (output.T_PR_NUMBER == null || !output.T_PR_NUMBER.Any())
                {
                    Console.WriteLine("No data retrieved.");
                    msg.msg = "No data retrieved.";
                    msg.code = 204;
                    return msg;
                }

                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine($"| {"SrNo",-5} | {"PR_NUMBER",-20} |");
                Console.WriteLine("---------------------------------------------------------------");
                int srNo = 1;
                foreach (var row in output.T_PR_NUMBER)
                {
                    Console.WriteLine($"| {srNo,-5} |  {row.PR_NUMBER,-12} |");
                    srNo++;
                }
                srNo = srNo - 1;
                Console.WriteLine("End Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());
                Console.WriteLine("Success");
                msg.msg = "Successful. Data retrieved. : PR Count - " + srNo;
                msg.code = 200;
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SAP connection is NOT active. : {ex.Message}, StackTrace: {ex.StackTrace}");
                msg.msg = "Connection failed. Error: " + ex.Message;
                msg.code = 500;
                return msg;
            }
        }
    }
}
