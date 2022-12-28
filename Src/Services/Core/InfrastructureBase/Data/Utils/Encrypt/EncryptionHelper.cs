using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InfrastructureBase.Data
{
    public class EncryptionHelper
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string EncryptToSHA1(string str)
        {
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = hashAlgorithm.ComputeHash(utf8Bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte hashByte in hashBytes)
                    builder.AppendFormat("{0:x2}", hashByte);
                return builder.ToString();
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string EncryptToMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            md5.Dispose();
            return Convert.ToBase64String(str2);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string EncryptToMD5_x2(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            md5.Dispose();
            var builder = new StringBuilder();
            foreach (var t in str2) { builder.Append(t.ToString("x2")); }
            return builder.ToString();
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SHA256EncryptString(string data)
        {
            byte[] str1 = Encoding.UTF8.GetBytes(data);
            SHA256 shaM = new SHA256Managed();
            byte[] hashBytes = shaM.ComputeHash(str1);
            shaM.Clear();
            shaM.Dispose();
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SHA256EncryptString_x2(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var hash = SHA256.Create().ComputeHash(bytes);
            var builder = new StringBuilder();
            foreach (var t in hash) { builder.Append(t.ToString("x2")); }
            return builder.ToString();
        }

        /// <summary>
        /// RSA公钥加密
        /// </summary>
        /// <param name="publickey">XML公钥</param>
        /// <param name="content"></param>
        /// <returns></returns
        public static string RSAEncrypt(string publickey, string content)
        {
            try
            {
                string encryptedContent = string.Empty;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(RSAPublicKeyJavaToDotNet(publickey));
                    byte[] encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(content), false);
                    encryptedContent = Convert.ToBase64String(encryptedData);
                }
                return encryptedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA私钥解密
        /// </summary>
        /// <param name="privatekey">XML私钥</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecrypt(string privatekey, string content)
        {
            try
            {
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(RSAPrivateKeyDotNetToJava(privatekey));
                var PlainTextBArray = Encoding.UTF8.GetBytes(content);
                PlainTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(PlainTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// RSA私钥格式转换，java->.net
        /// </summary>
        /// <param name="privateKey">java生成的RSA私钥</param>
        /// <returns></returns>
        public static string RSAPrivateKeyJavaToDotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>
        /// RSA公钥格式转换，java->.net
        /// </summary>
        /// <param name="publicKey">java生成的公钥</param>
        /// <returns></returns>
        public static string RSAPublicKeyJavaToDotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>
        /// RSA公钥格式转换，.net->java
        /// </summary>
        /// <param name="publicKey">.net生成的公钥</param>
        /// <returns></returns>
        public static string RSAPublicKeyDotNetToJava(string publicKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(publicKey);
            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            RsaKeyParameters pub = new RsaKeyParameters(false, m, p);
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(serializedPublicBytes);
        }

        /// <summary>
        /// RSA私钥格式转换，.net->java
        /// </summary>
        /// <param name="privateKey">.net生成的私钥</param>
        /// <returns></returns>
        public static string RSAPrivateKeyDotNetToJava(string privateKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(privateKey);
            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
            BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
            BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
            BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
            BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));
            RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
            return Convert.ToBase64String(serializedPrivateBytes);
        }
    }
}
