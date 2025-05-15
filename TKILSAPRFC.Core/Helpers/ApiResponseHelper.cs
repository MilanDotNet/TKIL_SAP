using System.Net;

namespace TKILSAPRFC.Core.Helpers
{
    public class ApiResponseHelper
    {
        /// <summary>
        /// Return API response along with result data
        /// </summary>
        /// <typeparam name="T">Type of Result Object</typeparam>
        /// <param name="result">Result Object</param>
        /// <param name="responseStatus">Response Status</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateApiResponse<T>(T result, HttpStatusCode httpStatusCode) => new()
        {
            Status = httpStatusCode,
            Result = result
        };

        /// <summary>
        /// Return API response with error information
        /// </summary>
        /// <typeparam name="T">Type of Result Object</typeparam>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="errors">List of Errors</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateApiResponse<T>(HttpStatusCode httpStatusCode, List<string>? errors = null) => new()
        {
            Status = httpStatusCode,
            Errors = errors
        };

        /// <summary>
        /// Return API response with error information
        /// </summary>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="errors">List of Errors</param>
        /// <returns></returns>
        public static ApiResponse CreateApiResponse(HttpStatusCode httpStatusCode, List<string>? errors = null) => new()
        {
            Status = httpStatusCode,
            Errors = errors
        };

    }
}
