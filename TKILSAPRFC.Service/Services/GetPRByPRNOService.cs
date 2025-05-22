using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.Net;

namespace TKILSAPRFC.Service.Services
{
    public class GetPRByPRNOService : IGetPRByPRNOService
    {
        private readonly HttpClient _client;
        private readonly IGetPRByPRNORepository _GetPRByPRNORepository;
        public GetPRByPRNOService(IGetPRByPRNORepository GetPRByPRNORepository, HttpClient client)
        {
            this._GetPRByPRNORepository = GetPRByPRNORepository;
            this._client = client;
        }
        
        public async Task<ApiResponse<ResponseMsg>> GetPRByPRNO(string PRNO)
        {
            ResponseMsg msg = await _GetPRByPRNORepository.GetPRByPRNO(PRNO);
            if (msg.code == 200)
            {
                return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.OK);
            }
            return ApiResponseHelper.CreateApiResponse(msg, HttpStatusCode.BadRequest);
        }
    }
}
