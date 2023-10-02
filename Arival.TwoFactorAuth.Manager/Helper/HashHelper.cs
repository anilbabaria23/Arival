using System.Security.Cryptography;

namespace Arival.TwoFactorAuth.Manager.Helper {
    public class HashHelper {

        private const int SaltSize = 16;

        public static string CreateHash(string plainText, int hashSize, int iteration) {
            using(RNGCryptoServiceProvider rngCrypto = new()) {
                byte[] arrSalt;
                rngCrypto.GetBytes(arrSalt = new byte[SaltSize]);
                using(Rfc2898DeriveBytes rfcDeriveBytes = new(plainText, arrSalt, iteration)) {
                    byte[] hash = rfcDeriveBytes.GetBytes(hashSize);
                    byte[] hashBytes = new byte[SaltSize + hashSize];
                    Array.Copy(arrSalt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, hashSize);
                    string base64HashString = Convert.ToBase64String(hashBytes);
                    return $"{iteration}~{base64HashString}";
                }
            }
        }

        public static bool VerifyHash(string plainText, string hashedText, int hashSize = 20) {
            string[] arrHashString = hashedText.Split("~");
            int iteration = Convert.ToInt32(arrHashString[0]);
            string base64Hash = arrHashString[1];

            byte[] hashBytes = Convert.FromBase64String(base64Hash);
            
            byte[] arrSalt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, arrSalt, 0, SaltSize);

            using(Rfc2898DeriveBytes rfcDeriveBytes = new(plainText, arrSalt, iteration)) {
                byte[] arrHash = rfcDeriveBytes.GetBytes(hashSize);
                for(int i = 0; i < hashSize; i++) {
                    if(hashBytes[i+SaltSize] != arrHash[i]) {
                        return false;
                    }
                }

                return true;
            }
        }

    }
}
