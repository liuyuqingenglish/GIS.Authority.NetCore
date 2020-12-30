/*
* ==============================================================================
*
* Filename: EncryptUtils
* ClrVersion: 4.0.30319.42000
* Description:加解密对象
*
* Version: 1.0
* Created: 2020/3/25 16:34:05
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;

namespace GIS.Authority.Encryption
{
    /// <summary>
    /// 加解密
    /// </summary>
    public class EncryptUtils
    {
        /// <summary>
        /// 3des加密方式
        /// </summary>
        public const string DES3 = "3DES";

        /// <summary>
        /// des加密方式
        /// </summary>
        public const string DES = "DES";

        /// <summary>
        /// md5加密方式
        /// </summary>
        public const string MD5 = "MD5";

        /// <summary>
        /// Base64
        /// </summary>
        public const string Base64 = "BASE64";

        /// <summary>
        /// 加解密接口
        /// </summary>
        private IEncryption m_IEncryption = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="encryptType">加密类型</param>
        /// <param name="keys">秘钥</param>
        public EncryptUtils(string encryptType, string keys)
        {
            string typeStr = encryptType.ToUpper();
            switch (typeStr)
            {
                case DES3:
                    m_IEncryption = new DES3(keys);
                    break;

                case MD5:
                    m_IEncryption = new MD5Encrypt();
                    break;

                case DES:
                    m_IEncryption = new DESEncryption(keys);
                    break;

                case Base64:
                    m_IEncryption = new Base64();
                    break;

                default:
                    return;
                    throw new Exception("not support the type:" + encryptType);
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptionStr"></param>
        /// <returns></returns>
        public string Encryption(string encryptionStr)
        {
            return m_IEncryption.Encryption(encryptionStr);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptionStr"></param>
        /// <returns></returns>
        public string Decryption(string decryptionStr)
        {
            return m_IEncryption.Decryption(decryptionStr);
        }
    }
}