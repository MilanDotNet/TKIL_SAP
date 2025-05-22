using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.Net;

namespace TKILSAPRFC.Service.Services
{
    public class GetPRByDateRangeService : IGetPRByDateRangeService
    {
        private readonly HttpClient _client;
        private readonly IGetPRByDateRangeRepository _GetPRByDateRangeRepository;
        public GetPRByDateRangeService(IGetPRByDateRangeRepository GetPRByDateRangeRepository, HttpClient client)
        {
            this._GetPRByDateRangeRepository = GetPRByDateRangeRepository;
            this._client = client;
        }
        
        public async Task<ApiResponse<ResponseMsg>> PRByDateRange(string FromDate, string ToDate)
        {
            ResponseMsg msg = await _GetPRByDateRangeRepository.PRByDateRange(FromDate, ToDate);
            if (msg.code == 200)
            {
                return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.OK);
            }
            return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.BadRequest);
        }
    }
}
