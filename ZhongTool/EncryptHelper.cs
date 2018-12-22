using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ZhongTool
{
    /// <summary>
    /// 加密解密工具类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string MD5Encrypt(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            return CreateHash(data, "MD5").ToLower();
        }

        public virtual string CreateHash(byte[] data, string hashAlgorithm = "SHA1")
        {
            if (String.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
        /// <summary>
        /// 得到Base64加密后字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string EncryptToBase64String(string input)
        {
            if (input == null)
            {
                throw new ArgumentException(nameof(input));
            }

            byte[] dataBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(dataBytes);
        }
        /// <summary>
        /// 解密Base64字符串，得到原始字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DecryptFromBase64String(string input)
        {
            if (input == null)
            {
                throw new ArgumentException(nameof(input));
            }

            byte[] dataBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(dataBytes);
        }
        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string UrlEncode(string input)
        {
            return HttpUtility.UrlEncode(input, Encoding.UTF8);
        }
        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string UrlDecode(string input)
        {
            return HttpUtility.UrlDecode(input, Encoding.UTF8);
        }
        /// <summary>
        /// HTML编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }
        /// <summary>
        /// HTML解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string HtmlDecode(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        //public string AesEncrypt(string input)
        //{
        //   AES
        //}

    }
}