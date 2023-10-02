using Newtonsoft.Json;

namespace Arival.TwoFactorAuth.Entities.DTO {
    public class ResponseError {
        [JsonProperty("type")]
        public string ErrorType { get; set; }

        [JsonProperty("code")]

        public string Code { get; set; }

        [JsonProperty("message")]

        public string Message { get; set; }

        public static ResponseError ToErrorResponse(string errorType, string code, string errorMesssage) {
            return new ResponseError {
                ErrorType = errorType,
                Code = code,
                Message = errorMesssage
            };
        }
    }
}
