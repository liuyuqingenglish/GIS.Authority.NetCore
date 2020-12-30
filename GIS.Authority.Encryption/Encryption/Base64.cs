/*
* ==============================================================================
*
* Filename: Base64
* ClrVersion: 4.0.30319.42000
* Description:base64加解密
*
* Version: 1.0
* Created: 2020/3/25 16:33:40
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.Text;

namespace GIS.Authority.Encryption
{
    /// <summary>
    /// base64加解密
    /// </summary>
    internal class Base64 : IEncryption
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptioStr">密文</param>
        /// <returns>明文</returns>
        public string Decryption(string decryptioStr)
        {
            return DecodeBase64(Encoding.UTF8, decryptioStr);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptionStr">明文</param>
        /// <returns>密文</returns>
        public string Encryption(string encryptionStr)
        {
            return EncodeBase64(Encoding.UTF8, encryptionStr);
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="codeType">编码方式</param>
        /// <param name="code">明文</param>
        /// <returns>密文</returns>
        public string EncodeBase64(Encoding codeType, string code)
        {
            string encode = string.Empty;
            byte[] bytes = codeType.GetBytes(code);

            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }

            return encode;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="codeType">编码方式</param>
        /// <param name="code">密文</param>
        /// <returns>明文</returns>
        public string DecodeBase64(Encoding codeType, string code)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(code);

            try
            {
                decode = codeType.GetString(bytes);
            }
            catch
            {
                decode = code;
            }

            return decode;
        }
    }
}