using System.Security.Cryptography;
using System.Web;
using static API_Structure.Constants.Constants;

namespace API_Structure.X_BAL.Utilities
{
    public class CommonFunction
    {
        public static string DecryptInput(string Data)
        {
            Data = HttpUtility.UrlDecode(Data);
            using (AesManaged aesManaged = new())
            {
                ICryptoTransform decryptor = aesManaged.CreateDecryptor(Convert.FromBase64String(AESKeys.AES256EncryptString), Convert.FromBase64String(AESKeys.AES256IVString));
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(Data)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                            Data = reader.ReadToEnd();
                    }
                }
            }
            return Data;
        }

        public static string DecryptOutput(string Data)
        {
            using (AesManaged aesManaged = new())
            {
                aesManaged.Padding = PaddingMode.PKCS7;
                aesManaged.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = aesManaged.CreateDecryptor(Convert.FromBase64String(AESKeys.AES256EncryptString), Convert.FromBase64String(AESKeys.AES256IVString));
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(Data)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                            Data = reader.ReadToEnd();
                    }
                }
            }
            return Data;
        }
    }
}
