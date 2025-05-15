using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.Net;

namespace TKILSAPRFC.Service.Services
{
    public class MasterService : IMasterService
    {
        private readonly HttpClient _client;
        private readonly IMasterRepository _MasterRepository;
        public MasterService(IMasterRepository MasterRepository, HttpClient client)
        {
            this._MasterRepository = MasterRepository;
            this._client = client;
        }
        
        public async Task<ApiResponse<ResponseMsg>> PRByDateRange(string FromDate, string ToDate)
        {
            ResponseMsg msg = await _MasterRepository.PRByDateRange(FromDate, ToDate);
            if (msg.code == 200)
            {
                return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.OK);
            }
            return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.BadRequest);
        }
    }
}
