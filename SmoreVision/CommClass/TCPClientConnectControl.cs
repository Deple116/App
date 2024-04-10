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
    public partial class TCPClientConnectControl
    {
        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;
        public string LastErrInfo = "";
        private static bool IsConnect = false;

        public bool CycledConnectClient = false;
        private static Socket Client = null;
        private Task m_ConnectProcess = null;

        private string _IP = null;
        private int _Port = 0;

        private Task m_TestConnect = null;
        public bool CycleTestConnect = false;
        //服务端返回的字符串信息
        public static string RecieveMessage = "";

        private int ConnectTime = 0;

        public static TCPClientConnectControl Instance = new TCPClientConnectControl("127.0.0.1",7788);


        public TCPClientConnectControl(string _ip, int _port)
        {
            try
            {
                //创建一个socket用于发送
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _IP = _ip;
                _Port = _port;
                StartConnectClientThread();
                StartTestConnectThread();

            }
            catch (Exception ex)
            {
                LastErrInfo = ex.ToString();
            }
        }

        public int StartConnectClientThread()
        {
            if (m_ConnectProcess == null || m_ConnectProcess.IsCompleted == true)
            {
                CycledConnectClient = true;
                m_ConnectProcess = Task.Factory.StartNew(() => ConnectAttemp());
                SMLogWindow.OutLog($"TCP客户端监控线程启动.", Color.Green);
            }
            return ERROR_OK;
        }

        public int StartTestConnectThread()
        {
            if (m_TestConnect == null || m_TestConnect.IsCompleted == true)
            {
                CycleTestConnect = true;
                m_TestConnect = Task.Factory.StartNew(() => TestConnect());
                SMLogWindow.OutLog($"TCP客户端连接测试启动.", Color.Green);
            }
            return ERROR_OK;
        }

        public void TestConnect()
        {
            while (CycleTestConnect)
            {
                try
                {
                    Client.Send(Encoding.Default.GetBytes("test"));
                 //   SMLogWindow.OutLog($"TCP客户端连接测试成功.", Color.Green);
                }
                catch (Exception)
                {
                    IsConnect = false;
                    if (ConnectTime < 5)
                    {
                        SMLogWindow.OutLog($"TCP客户端连接测试失败.", Color.Red);
                    }
                }

                Thread.Sleep(15000);
            }
            SMLogWindow.OutLog($"TCP客户端连接测试线程结束.", Color.Green);
        }

        public void ConnectAttemp()
        {
            while (CycledConnectClient)
            {

                if (IsConnect == false)
                {
                    try
                    {
                        Client.Close();//必须关闭后重新初始化。
                        Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Client.Connect(_IP, _Port);//连接到服务器指定IP及Port

                        IsConnect = true;
                        ConnectTime = 0;
                        SMLogWindow.OutLog($"TCP客户端连接成功.", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        IsConnect = false;
                        ConnectTime++;
                        if (ConnectTime < 5)
                        {
                            SMLogWindow.OutLog($"TCP客户端未成功连接服务器，将立即重试.", Color.Red);
                        }
                    }
                }
                Thread.Sleep(3000);
            }
            SMLogWindow.OutLog($"TCP客户端连接监控线程结束.", Color.Green);
        }


        public static int ClientSendMsg(string Meg)
        {
            if (IsConnect == true)
            {
                try
                {
                    SMLogWindow.OutLog(Client.RemoteEndPoint.ToString() + "发送开始", Color.Green);
                    Client.Send(Encoding.Default.GetBytes(Meg));//将当前的用户名发送给服务器端
                    SMLogWindow.OutLog(Client.RemoteEndPoint.ToString() + "发送:" + Meg, Color.Green);

                    byte[] buffer = new byte[10240 * 10240 * 2];
                    int r = Client.Receive(buffer);
                    if (r != 0)
                    {
                        RecieveMessage = Encoding.UTF8.GetString(buffer, 0, r);
                        SMLogWindow.OutLog(Client.RemoteEndPoint.ToString() + "收到:" + RecieveMessage, Color.Green);
                        switch (RecieveMessage) //握手信号
                        {
                            case "Message Received":
                                {
                                    ;
                                }
                                break;
                            default:
                                {
                                    ;
                                }
                                break;
                        }
                    }
                    return ERROR_OK;
                }
                catch
                {
                    IsConnect = false;
                    SMLogWindow.OutLog($"TCP客户端向服务器发送信息失败，尝试重连.", Color.Red);
                    return ERROR_FAILED;
                }
            }
            else
            {
                SMLogWindow.OutLog("向服务器发送:" + Meg + "失败", Color.Red);
                return ERROR_FAILED;
            }
        }

    }
}
