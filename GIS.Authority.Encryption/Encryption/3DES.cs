/*
* ==============================================================================
*
* Filename: _3DES
* ClrVersion: 4.0.30319.42000
* Description:3des加密
*
* Version: 1.0
* Created: 2020/3/25 16:33:30
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace GIS.Authority.Encryption
{
    /// <summary>
    /// 3des加密
    /// </summary>
    internal class DES3 : IEncryption
    {
        /// <summary>
        /// 加密对象
        /// </summary>
        private TripleDESCryptoServiceProvider _pTripDes = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">秘钥</param>
        public DES3(string key)
        {
            _pTripDes.Key = Encoding.UTF8.GetBytes(key);
            _pTripDes.Mode = CipherMode.ECB;
            _pTripDes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptionStr">需要加密的字符串-明文</param>
        /// <returns>密文</returns>
        public string Encryption(string encryptionStr)
        {
            try
            {
                ICryptoTransform ery = _pTripDes.CreateEncryptor();
                byte[] buffer = Encoding.UTF8.GetBytes(encryptionStr);
                return Convert.ToBase64String(ery.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptioStr">需要解密的字符串-密文</param>
        /// <returns>明文</returns>
        public string Decryption(string decryptioStr)
        {
            try
            {
                ICryptoTransform cry = _pTripDes.CreateDecryptor();
                byte[] buffer = Convert.FromBase64String(decryptioStr);
                byte[] tempBuffer = cry.TransformFinalBlock(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(tempBuffer, 0, tempBuffer.Length);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}