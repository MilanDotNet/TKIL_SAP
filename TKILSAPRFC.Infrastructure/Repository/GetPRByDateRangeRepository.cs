using Microsoft.Extensions.Configuration;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NwRfcNet;
using NwRfcNet.TypeMapper;
using static TKILSAPRFC.Model.ViewModels.mdl_PRNew;
using System.Runtime.InteropServices;
using TKILSAPRFC.Infrastructure.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace TKILSAPRFC.Infrastructure.Repository
{
    public class GetPRByDateRangeRepository : IGetPRByDateRangeRepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly RfcConnection _Rfcconnection;

        public GetPRByDateRangeRepository(ICurrentUser currentUser, IConfiguration configuration, RfcConnection rfcconnection)
        {
            this.currentUser = currentUser;
            this._configuration = configuration;
            _Rfcconnection = rfcconnection;
        }

        public async Task<ResponseMsg> PRByDateRange(string FromDate, string ToDate)
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
                
                var func = _Rfcconnection.CallRfcFunction("ZPRCSNS_GET_PRCHS_RQSTN");
                var inputParameters = new PurchaseRequestInput
                {
                    IM_V_PR_CHNG_FRM_DATE = FromDate,
                    IM_V_PR_CHNG_TO_DATE = ToDate
                };
                func.Invoke(inputParameters);

                var output = func.GetOutputParameters<PurchaseRequestOutput>();
                if (output.EX_T_PR_INFO == null || !output.EX_T_PR_INFO.Any())
                {
                    Console.WriteLine("No data retrieved.");
                    msg.msg = "No data retrieved.";
                    msg.code = 204;
                    return msg;
                }
                
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine($"| {"SrNo",-5} | {"PR_NUMBER",-20} | {"PR_TYPE",-15} | {"PR_TYPE_DESC",-20} |");
                Console.WriteLine("---------------------------------------------------------------");
                int srNo = 1;
                int srNo_itm = 1;
                foreach (var row in output.EX_T_PR_INFO)
                {
                    Console.WriteLine($"| {srNo,-5} |  {row.PR_NUMBER,-12} | {row.PR_TYPE,-10} | {row.PR_TYPE_DESC,-20} |");
                    if (row.PR_ITM_INFO != null && row.PR_ITM_INFO.Any())
                    {
                        foreach (var item in row.PR_ITM_INFO)
                        {
                            Console.WriteLine($" SrNo: {srNo_itm,-5},  PR_ITM_NO: {item.PR_ITM_NO}, PR_ITM_STATUS: {item.PR_ITM_STATUS}, PR_ITM_RL_DATE: {item.PR_ITM_RL_DATE}, PR_ITM_MATERIAL: {item.PR_ITM_MATERIAL}, PR_ITM_MATERIAL_DSC: {item.PR_ITM_MATERIAL_DSC}, ");
                            srNo_itm++;
                        }
                    }
                    srNo++;
                }
                srNo = srNo - 1;
                srNo_itm = srNo_itm - 1;
                Console.WriteLine("End Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());
                Console.WriteLine("Success");
                msg.msg = "Successful. Data retrieved. : PR Count - " + srNo + " : Total Pr vise Item Count - " + srNo_itm;
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
