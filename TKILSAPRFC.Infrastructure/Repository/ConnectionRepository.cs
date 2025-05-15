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
using SapNwRfc;
using NwRfcNet;


namespace TKILSAPRFC.Infrastructure.Repository
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly SapConnection _connection;
        public ConnectionRepository(ICurrentUser currentUser, IConfiguration configuration, SapConnection connection)
        {
            this.currentUser = currentUser;
            _configuration = configuration;
            _connection = connection;
        }

        public async Task<ResponseMsg> ConnectionTest()
        {
            var msg = new ResponseMsg();
            try
            {
                bool isConnected = TestConnection();
                msg.msg = isConnected ? "Success" : "Not connected";
                msg.code = 200;
                return msg;
            }
            catch (Exception ex)
            {
                msg.msg = "Error: " + ex.Message;
                msg.code = 500;
                return msg;
            }
        }

        public bool TestConnection()
        {
            try
            {
                if (_connection == null)
                    return false;

                return _connection.IsValid;
            }
            catch
            {
                return false;
            }
        }

        public static SapConnectionParameters GetParameters()
        {
            return new SapConnectionParameters
            {
                Name = "QA",
                AppServerHost = "10.66.38.115",
                SystemNumber = "00",
                User = "SAFAL_COMUSR",
                Password = @"GSj2C%w4Ykz{ERTNt]P]/3N<WlS\FD6#kT@b#4#R",
                Client = "100",
                Language = "EN",
                PoolSize = "10"
            };
        }

        public static RfcConnection CreateConnection()
        {
            return new RfcConnection(
                sncMyname: "QA",
                hostname: "10.66.38.115",
                systemNumber: "00",
                userName: "SAFAL_COMUSR",
                password: @"GSj2C%w4Ykz{ERTNt]P]/3N<WlS\FD6#kT@b#4#R",
                client: "100",
                language: "EN"
            );
        }

        public static string GetConnectionString()
        {
            var p = GetParameters();

            return $"NAME={p.Name};ASHOST={p.AppServerHost};SYSNR={p.SystemNumber};" +
                   $"USER={p.User};PASSWD={p.Password};CLIENT={p.Client};LANG={p.Language};" +
                   $"POOL_SIZE={p.PoolSize}";
        }
    }
}
