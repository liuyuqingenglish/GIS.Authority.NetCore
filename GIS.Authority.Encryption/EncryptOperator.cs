/*
* ==============================================================================
*
* Filename: Encrypt
* ClrVersion: 4.0.30319.42000
* Description: 加解密辅助对象
*
* Version: 1.0
* Created: 2020/3/25 16:33:58
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
    /// 加解密辅助对象
    /// </summary>
    public class EncryptOperator
    {
        /// <summary>
        /// 静态对象
        /// </summary>
        private static EncryptOperator Instance;

        /// <summary>
        /// 获取静态对象
        /// </summary>
        /// <returns>加解密对象</returns>
        public static EncryptOperator GetInstance()
        {
            if (Instance == null)
            {
                Instance = new EncryptOperator();
            }

            return Instance;
        }

        /// <summary>
        /// 获取3des加密对象
        /// </summary>
        /// <returns>3des对象</returns>
        public EncryptUtils Get3EdsInstance()
        {
            //密钥可用配置文件的方式，这里暂时先用上;
            return new EncryptUtils(EncryptUtils.DES3, "P29fp2Ob439YeoHKbtrtQ50V");
        }

        /// <summary>
        /// 获取md5加密对象
        /// </summary>
        /// <returns>md5对象</returns>
        public EncryptUtils GetMD5Instance()
        {
            return new EncryptUtils(EncryptUtils.MD5, string.Empty);
        }

        /// <summary>
        /// 获取base64加密对象
        /// </summary>
        /// <returns>base64对象</returns>
        public EncryptUtils GetBase64Instance()
        {
            return new EncryptUtils(EncryptUtils.Base64, string.Empty);
        }
    }
}