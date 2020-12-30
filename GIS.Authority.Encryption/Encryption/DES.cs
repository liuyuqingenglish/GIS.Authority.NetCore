/*
* ==============================================================================
*
* Filename: DES
* ClrVersion: 4.0.30319.42000
* Description:des加解密
*
* Version: 1.0
* Created: 2020/3/25 16:33:49
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GIS.Authority.Encryption
{
    /// <summary>
    /// des加解密
    /// </summary>
    internal class DESEncryption : IEncryption
    {
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 加密秘钥
        /// </summary>
        private string _encKey = "hdgpsgis";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="encKey">加密密钥</param>
        public DESEncryption(string encKey)
        {
            _encKey = encKey;
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public string Encryption(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(_encKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream();
                CryptoStream cstream = new CryptoStream(stream, dcsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cstream.Write(inputByteArray, 0, inputByteArray.Length);
                cstream.FlushFinalBlock();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string Decryption(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(_encKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream();
                CryptoStream cstream = new CryptoStream(stream, dcsp.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cstream.Write(inputByteArray, 0, inputByteArray.Length);
                cstream.FlushFinalBlock();
                byte[] bytData = stream.ToArray();
                return Encoding.UTF8.GetString(bytData, 0, bytData.Length);
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="inName">源文件名</param>
        /// <param name="outName">目标文件名</param>
        /// <param name="desKey">秘钥</param>
        /// <param name="desIV">向量</param>
        public void EncryptData(string inName, string outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="inName">源文件名</param>
        /// <param name="outName">目标文件名</param>
        /// <param name="desKey">秘钥</param>
        /// <param name="desIV">向量</param>
        public void DecryptData(string inName, string outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }
    }
}