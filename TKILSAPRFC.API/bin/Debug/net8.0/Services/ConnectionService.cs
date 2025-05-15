using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.Net;

namespace TKILSAPRFC.Service.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly HttpClient _client;
        private readonly IConnectionRepository _ConnectionRepository;
        public ConnectionService(IConnectionRepository connectionRepository, HttpClient client)
        {
            this._ConnectionRepository = connectionRepository;
            this._client = client;
        }
        
        public async Task<ApiResponse<ResponseMsg>> ConnectionTest()
        {
            ResponseMsg msg = await _ConnectionRepository.ConnectionTest();
            if (msg.code == 200)
            {
                return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.OK);
            }
            return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.BadRequest);
        }
    }
}
