using System.Security.Cryptography;
using System.Text;

namespace Arival.TwoFactorAuth.Manager.Helper {
    public class AuthCodeGenerator {
        public static string GenerateRandomAuthCode(string allowedChars, int authCodeSize) {
            
            char[] authCodeAllowedChars = allowedChars.ToCharArray();
            byte[] data = new byte[4 * authCodeSize];
            using(RNGCryptoServiceProvider rngCryptoServiceProvider = new()) {
                rngCryptoServiceProvider.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(authCodeSize);
            for(int i = 0; i < authCodeSize; i++) {
                uint rnd = BitConverter.ToUInt32(data, i * 4);
                long idx = rnd % authCodeAllowedChars.Length;
                result.Append(authCodeAllowedChars[idx]);
            }

            return result.ToString();
        }
    }
}
