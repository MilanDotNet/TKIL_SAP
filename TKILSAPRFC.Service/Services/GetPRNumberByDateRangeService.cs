using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.Net;

namespace TKILSAPRFC.Service.Services
{
    public class GetPRNumberByDateRangeService : IGetPRNumberByDateRangeService
    {
        private readonly HttpClient _client;
        private readonly IGetPRNumberByDateRangeRepository _GetPRNumberByDateRangeRepository;
        public GetPRNumberByDateRangeService(IGetPRNumberByDateRangeRepository GetPRNumberByDateRangeRepository, HttpClient client)
        {
            this._GetPRNumberByDateRangeRepository = GetPRNumberByDateRangeRepository;
            this._client = client;
        }
        
        public async Task<ApiResponse<ResponseMsg>> GetPRNumberByDateRange(string FromDate, string ToDate)
        {
            ResponseMsg msg = await _GetPRNumberByDateRangeRepository.GetPRNumberByDateRange(FromDate, ToDate);
            if (msg.code == 200)
            {
                return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.OK);
            }
            return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.BadRequest);
        }
    }
}
