using SMLogControlLibrary;
using SmoreControlLibrary;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmoreVision.CommClass
{
    public class TCPServerControl
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;
        public string LastErrInfo = "";

        public bool CycledListenClient = false;
        public bool CycledListenClientSend = false;

        private Socket SocketWitch = null;
        private Socket SocketSend = null;

        private Task m_RecieveAcceptProcess = null;
        private Task m_RecieveAcceptSenderProcess = null;

        public ManualResetEvent m_event = new ManualResetEvent(false);

        public ManualResetEvent m_event2 = new ManualResetEvent(false);

        //服务端获取到的字符串信息
        public string RecieveMessage = "";
        //服务端获取到信息标志位
        public bool RecieveFlag = false;

        public static TCPServerControl Instance = new TCPServerControl();


        public TCPServerControl()
        {

        }

        public TCPServerControl(string _ip, int  _port)
        {
            ListenSocket(_ip, _port);
        }

        public void Init(string _ip, int _port)
        {
            ListenSocket(_ip, _port);
        }

        public int StartListenClientThread()
        {
            if (m_RecieveAcceptProcess == null || m_RecieveAcceptProcess.IsCompleted == true)
            {
                CycledListenClient = true;
                m_RecieveAcceptProcess = Task.Factory.StartNew(() => RecieveAccept());
                SMLogWindow.OutLog($"TCP服务器监听线程启动.", Color.Green);
            }
            return ERROR_OK;
        }

        public int StartListenClientSendThread(object obj)
        {
            if (m_RecieveAcceptSenderProcess == null || m_RecieveAcceptSenderProcess.IsCompleted == true)
            {
                CycledListenClientSend = true;
                m_RecieveAcceptSenderProcess = Task.Factory.StartNew(() => ReceiveClientSend(obj));
                SMLogWindow.OutLog($"TCP服务器监听客户端发送信息线程启动.", Color.Green);
            }
            return ERROR_OK;
        }

        public int ListenSocket(string _ip,int  _port)
        {
            try
            {
                //创建一个socket用于监听
                SocketWitch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //提供一个IP地址，用于监控网络接口上的所有活动
                IPAddress ip = IPAddress.Any;
                IPEndPoint piont = new IPEndPoint(IPAddress.Parse(_ip),_port);
                //绑定监听端口
                SocketWitch.Bind(piont);
                SocketWitch.Listen(10);
                StartListenClientThread();
                return ERROR_OK;
            }
            catch (Exception ex)
               {
                LastErrInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }

        /// <summary>
        /// 监听客户端线程
        /// </summary>
        /// <returns></returns>
        public int RecieveAccept()
        {
            try
            {
                while (CycledListenClient)
                {
                    // Socket SocketSend = SocketWitch.Accept();

                    SocketSend = SocketWitch.Accept();

                    //获取远程主机的IP地址和端口号
                    SMLogWindow.OutLog(SocketSend.RemoteEndPoint.ToString() + "服务器端连接成功.", Color.Green);
                    //创建一个线程用于接收客户端发送过来的信息
                    StartListenClientSendThread(SocketSend);
                }
                SMLogWindow.OutLog($"TCP服务器监听客户端线程结束.", Color.Green);
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastErrInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }

       /// <summary>
       /// 监听客户端发送信息线程
       /// </summary>
       /// <returns></returns>
        public int ReceiveClientSend(object obj)
        {
            try
            {
                SocketSend = obj as Socket;
                while (CycledListenClientSend)
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = SocketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    RecieveMessage = Encoding.UTF8.GetString(buffer, 0, r);
                    SMLogWindow.OutLog(SocketSend.RemoteEndPoint.ToString() + ":" + RecieveMessage, Color.Green);
                    switch (RecieveMessage) //握手信号
                    {
                        case "2222":
                            {
                                RecieveFlag = true;
                                ServerSendToClient($"Message Received");
                            }
                            break;
                        case "1":
                            m_event.Set();
                            break;
                        case "2":
                            m_event2.Set();
                            break;
                        default:
                            {
                                ServerSendToClient("null");
                            }
                            break;
                    }

                }
                SMLogWindow.OutLog($"TCP服务器监听客户端发送信息线程结束.", Color.Green);
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastErrInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }

        /// <summary>
        /// 服务器向客户端发送信息
        /// </summary>
        /// <param name="sendMsg"></param>
        /// <returns></returns>
        public int ServerSendToClient(string sendMsg)
        {
            try
            {
                string str = Convert.ToString(sendMsg);
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                //byte[] buffer = System.Te    xt.Encoding.UTF8.GetBytes(str);
                SocketSend.Send(buffer);
                return ERROR_OK;
            }
            catch (Exception ex)
            {
                LastErrInfo = ex.ToString();
                return ERROR_FAILED;
            }
        }
      
    }
}
