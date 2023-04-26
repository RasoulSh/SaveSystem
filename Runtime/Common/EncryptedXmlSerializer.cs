using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

using UnityEngine;

namespace SaveSystem.Common
{
    /// <summary> 
    /// Class provides static method to serialize and encrypt data to XML 
    ///  
    /// Works on Android, iOS, Standalone. 
    ///  
    /// Does not encrypt data on WP8 
    /// </summary> 
    public static class EncryptedXmlSerializer
    {
        private static readonly string DeviceUniquePrivateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);
        private static readonly string PrivateKey = "R@$0UL73456%$&%^%$h@hh0S31n17543";

        #region API 

 
        public static T Load<T>(string path, bool uniqueDevice) where T : class
        {
            T result;

            if (!File.Exists(path))
            {
                return null;
            }

            string data;
            using (var reader = new StreamReader(path))
            {
                var text = reader.ReadToEnd();
                if (string.IsNullOrEmpty(text))
                    return null;
                try
                {
                    data = DecryptData(text, uniqueDevice);
                }
                catch
                {
                    return null;
                }
            }

            var stream = new MemoryStream();
            using (var sw = new StreamWriter(stream) { AutoFlush = true })
            {
                sw.WriteLine(data);
                stream.Position = 0;
                result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
            }

            return result;
        }
        
        public static void Save<T>(string path, object value, bool uniqueDevice) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                stream.Flush();
                stream.Position = 0;
                string sr = new StreamReader(stream).ReadToEnd();
                var fileStream = new FileStream(path, FileMode.Create);
                var streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(EncryptData(sr, uniqueDevice));
                streamWriter.Close();
                fileStream.Close();
            }
        }

        #endregion

        public static string SerializeData<T>(T data, bool encrypt, bool uniqueDevice) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stream = new MemoryStream();
            serializer.Serialize(stream, data);
            stream.Flush();
            stream.Position = 0;
            string sr = new StreamReader(stream).ReadToEnd();
            if (encrypt)
            {
                return EncryptData(sr, uniqueDevice);   
            }
            return sr;
        }
        public static T DeserializeData<T>(string text, bool encrypt, bool uniqueDevice) where T : class
        {
            string data;
            if (string.IsNullOrEmpty(text))
                return null;
            if (encrypt)
            {
                try
                {
                    data = DecryptData(text, uniqueDevice);
                }
                catch
                {
                    return null;
                }   
            }
            else
            {
                data = text;
            }

            var stream = new MemoryStream();
            using var sw = new StreamWriter(stream) { AutoFlush = true };
            sw.WriteLine(data);
            sw.WriteLine(data);
            stream.Position = 0;
            T result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
            return result;
        }

        #region encrypt_decrypt 
        private static string EncryptData(string toEncrypt, bool uniqueDevice)
        {
#if UNITY_WP8
             return toEncrypt; 
#else
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = CreateRijndaelManaged(uniqueDevice);
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
#endif
        }

        private static string DecryptData(string toDecrypt, bool uniqueDevice)
        {
#if UNITY_WP8
             return toDecrypt; 
#else
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = CreateRijndaelManaged(uniqueDevice);
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
#endif
        }

#if !UNITY_WP8
        private static RijndaelManaged CreateRijndaelManaged(bool uniqueDevice)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(uniqueDevice ? DeviceUniquePrivateKey : PrivateKey);
            var result = new RijndaelManaged();

            var newKeysArray = new byte[16];
            Array.Copy(keyArray, 0, newKeysArray, 0, 16);

            result.Key = newKeysArray;
            result.Mode = CipherMode.ECB;
            result.Padding = PaddingMode.PKCS7;
            return result;
        }
#endif
        #endregion
    }
}
