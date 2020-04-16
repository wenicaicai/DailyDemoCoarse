using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace SocketClientBeta
{
    public class ClientBeta
    {
        private string _ip;
        private int _port;
        private Socket _socket;
        private byte[] _data = new byte[1024 * 1024 * 2];

        public ClientBeta(int port)
        {
            _ip = "127.0.0.1";
            _port = port;
        }

        public void StartClient()
        {
            try
            {
                //1.实例化套接字
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.创建IP对象
                IPAddress address = IPAddress.Parse(_ip);
                //3.创建网络端口，包括IP和端口
                IPEndPoint endPoint = new IPEndPoint(address, _port);
                //4.建立连接
                _socket.Connect(endPoint);
                Console.WriteLine("success connect.");
                //5.接收数据
                int len = _socket.Receive(_data);
                Console.WriteLine("接受服务器{0}，消息：{1}\n", _socket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(_data));
                //6.向服务器发送消息
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1001);
                    string sendMessage = string.Format("Client send message at {0}\n", DateTime.Now.ToString());
                    _socket.Send(Encoding.UTF8.GetBytes(sendMessage));
                    Console.WriteLine("向服务器发送消息{0}\n", sendMessage);
                    int lenI = _socket.Receive(_data);
                    Console.WriteLine("接受服务器{0}，消息：{1}\n", _socket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(_data));

                }

            }
            catch (Exception ex)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("THE END");
            Console.ReadKey();
        }

    }
}
