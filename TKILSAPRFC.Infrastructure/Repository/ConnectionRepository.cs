using Microsoft.Extensions.Configuration;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using Serilog;
using RestSharp;
using TKILSAPRFC.Core.Helpers;
using Newtonsoft.Json;
using static TKILSAPRFC.Model.ViewModels.mdl_UOM;
using TKILSAPRFC.Infrastructure.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
//using SapNwRfc;
using NwRfcNet;
using TKILSAPRFC.Infrastructure.Common;


namespace TKILSAPRFC.Infrastructure.Repository
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly RfcConnection _Rfcconnection;
        public ConnectionRepository(ICurrentUser currentUser, IConfiguration configuration, RfcConnection rfcconnection)
        {
            this.currentUser = currentUser;
            _configuration = configuration;
            _Rfcconnection = rfcconnection;
        }

        public async Task<ResponseMsg> ConnectionTest()
        {
            var msg = new ResponseMsg();
            try
            {
                _Rfcconnection.Open();
                _Rfcconnection.Ping();

                Console.WriteLine("SAP connection is active.");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Connection Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());

                msg.msg = "Success";
                msg.code = 200;
                return msg;
            }
            catch (Exception ex)
            {
                msg.msg = "Connection Fail : " + ex.Message;
                msg.code = 500;
                return msg;
            }
        }
    }
}
