namespace Arival.TwoFactorAuth.Entities {

    public static class ErrorCode {
        public const string Invalid2FACodeRequest = "Invalid_2FA_Code_Request";
        public const string Invalid2FACodeVerificationRequest = "Invalid_2FA_Code_Verification_Request";
        public const string MaxConcurrentCodeLimit = "Max_Concurrent_Code_Limit";
        public const string InternalError = "Internal_Error";
        public const string PhoneNumberMissing = "missing_phone_number";
        public const string AuthCodeMissing = "missing_auth_code";
    }
    public static class ErrorType {
        public const string InvalidInput = "Invalid_Input";
        public const string ServerError = "Server_Error";
    }

    public static class ErrorMessage {
        public const string Invalid2FACodeRequest = "Invalid payload to generate 2FA Code";
        public const string Invalid2FACodeVerificationRequest = "Invalid payload to verify 2FA Code";
        public const string MaxConcurrentCodeLimit = "Maximum concurrent active code limit reached for phone number {0}";
        public const string PhoneNumberMissing = "Please provide phone number field with valid data";
        public const string AuthCodeMissing = "Please provide auth code field with valid data";
    }
}
