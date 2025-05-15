using Newtonsoft.Json;
using System.Net;

namespace TKILSAPRFC.Core.Helpers
{
    public class ApiResponse
    {
        public bool Success { get { return Status == HttpStatusCode.OK; } }
        public HttpStatusCode Status { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Result { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
