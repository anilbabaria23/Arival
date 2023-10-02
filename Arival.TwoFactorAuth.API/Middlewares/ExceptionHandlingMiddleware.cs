using Arival.TwoFactorAuth.Entities.CustomException;
using Arival.TwoFactorAuth.Entities;
using Arival.TwoFactorAuth.Entities.DTO;
using Newtonsoft.Json;

namespace Arival.TwoFactorAuth.API.Middlewares {
    public class ExceptionHandlingMiddleware {
        
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            } catch(Exception ex) {

                var response = context.Response;
                response.ContentType = "application/json";
                ResponseError errorResponse = null;
                if(ex is ApiValidationsException) {
                    response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    ApiValidationsException validationsException = (ApiValidationsException)ex;
                    errorResponse = ResponseError.ToErrorResponse(validationsException.ErrorType, validationsException.ErrorCode, validationsException.Message);
                } else {
                    response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    errorResponse = ResponseError.ToErrorResponse(ErrorType.ServerError, ErrorCode.InternalError, ex.Message);
                    ILogger<ExceptionHandlingMiddleware> logger = (ILogger<ExceptionHandlingMiddleware>)context.RequestServices.GetService(typeof(ILogger<ExceptionHandlingMiddleware>));
                    logger.LogError(ex, ex.Message);
                }
                await response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
