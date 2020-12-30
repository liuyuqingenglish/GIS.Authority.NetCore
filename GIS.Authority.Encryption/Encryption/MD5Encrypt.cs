/*
* ==============================================================================
*
* Filename: MD5Encrypt
* ClrVersion: 4.0.30319.42000
* Description: md5加密对象
*
* Version: 1.0
* Created: 2020/3/25 16:34:18
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
    /// md5加密类管理对象
    /// </summary>
    internal class MD5Encrypt : IEncryption
    {
        /// <summary>
        /// md5对象
        /// </summary>
        private static MD5CryptoServiceProvider m_pTripDes = new MD5CryptoServiceProvider();

        /// <summary>
        /// 加密字符串.
        /// </summary>
        /// <param name="encryptionStr">需要解密的字符串.</param>
        /// <returns>返回加密字符串.</returns>
        public string Encryption(string encryptionStr)
        {
            try
            {
                string pwd = string.Empty;
                MD5 md5 = MD5.Create(); //实例化一个md5对像

                //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptionStr));

                //通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                for (int i = 0; i < s.Length; i++)
                {
                    //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                    pwd = pwd + s[i].ToString("X2");
                }

                return pwd;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptioStr">密文</param>
        /// <returns>明文</returns>
        public string Decryption(string decryptioStr)
        {
            throw new Exception("md5 no decrypt");
        }
    }
}