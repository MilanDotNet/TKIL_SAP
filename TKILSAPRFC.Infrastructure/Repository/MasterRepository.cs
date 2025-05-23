﻿using Microsoft.Extensions.Configuration;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NwRfcNet;
using NwRfcNet.TypeMapper;
using static TKILSAPRFC.Model.ViewModels.mdl_PR;
using System.Runtime.InteropServices;


namespace TKILSAPRFC.Infrastructure.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly RfcConnection _Rfcconnection;

        public MasterRepository(ICurrentUser currentUser, IConfiguration configuration, RfcConnection rfcconnection)
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
                DateTime utcNow = DateTime.UtcNow;
                string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)? "India Standard Time": "Asia/Kolkata";
                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);
                Console.WriteLine("Start Time: " + istTime.ToString("hh:mm:ss tt _ dd-MMM-yyyy"));

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
                foreach (var row in output.EX_T_PR_INFO)
                {
                    Console.WriteLine($"| {srNo,-5} |  {row.PR_NUMBER,-12} | {row.PR_TYPE,-10} | {row.PR_TYPE_DESC,-20} |");
                    srNo++;
                }
                DateTime utcNow2 = DateTime.UtcNow;
                string timeZoneId2 = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "India Standard Time" : "Asia/Kolkata";
                TimeZoneInfo istZone2 = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId2);
                DateTime istTime2 = TimeZoneInfo.ConvertTimeFromUtc(utcNow2, istZone2);
                Console.WriteLine("End Time: " + istTime2.ToString("hh:mm:ss tt _ dd-MMM-yyyy"));
                Console.WriteLine("Success");
                msg.msg = "Successful. Data retrieved.";
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
