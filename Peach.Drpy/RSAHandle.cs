using System.Security.Cryptography;
using System.Text;

namespace Peach.Drpy
{
    public class RSAHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="pub">是公钥或私钥</param>
        /// <param name="encrypt">是加密或解密</param>
        /// <param name="input">输入的字符串</param>
        /// <param name="inBase64">是输入字符串是否编码</param>
        /// <param name="key">rsa密钥</param>
        /// <param name="outBase64">输出是编码</param>
        /// <returns></returns>
        public string RSAX(string mode, bool pub, bool encrypt, string input, bool inBase64, string key, bool outBase64)
        {
            if (string.IsNullOrEmpty(input)) return input;
            byte[] PlainArray;
            byte[] ResultArray;
            string Result;

            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);
                PlainArray = (new UnicodeEncoding()).GetBytes(input);
                if (encrypt)
                    ResultArray = rsa.Encrypt(PlainArray, false);
                else
                    ResultArray = rsa.Decrypt(PlainArray, false);

                Result = Convert.ToBase64String(ResultArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
