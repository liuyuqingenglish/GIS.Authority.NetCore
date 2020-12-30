/*
* ==============================================================================
*
* Filename: IEncryption
* ClrVersion: 4.0.30319.42000
* Description: 加解密接口
*
* Version: 1.0
* Created: 2020/3/25 16:34:12
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Encryption
{
    /// <summary>
    /// 加解密接口
    /// </summary>
    internal interface IEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptionStr">明文</param>
        /// <returns>密文</returns>
        string Encryption(string encryptionStr);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptioStr">密文</param>
        /// <returns>明文</returns>
        string Decryption(string decryptioStr);
    }
}