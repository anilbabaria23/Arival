namespace Arival.TwoFactorAuth.Entities.CustomException {
    public class ApiValidationsException : Exception {
        public string ErrorCode { get; set; }

        public string ErrorType { get; set; }

        public ApiValidationsException(string errorType, string errorCode, string message) : base(message) {
            this.ErrorCode = errorCode;
            this.ErrorType = errorType;
        }
    }
}
