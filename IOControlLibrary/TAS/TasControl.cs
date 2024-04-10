using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOdemo;
using SMLogControlLibrary;
using System.Drawing;

namespace IOControlLibrary.TAS
{
    public class TasControl
    {

        public Socket ClientSocket;  //声明负责通信的socket
        //public int SFlag = 0;    //连接服务器成功标志
        private int ReceiveLen = 0; //总计接收到的数据长度
        private byte[] response = new byte[5024];
        private int dev_addr = 0x01;
        private int[] do_status = new int[2];// 8个输出引脚的电平状态

        public string ServerIP { get; set; }

        public string ServerPort { get; set; }

        private bool bRecive = false;

        Thread th1;     //声明一个线程
        #region 寄存器地址
        public const byte MB_READ_COILS = 0x01;             //读线圈寄存器
        public const byte MB_READ_DISCRETE = 0x02;          //读离散输入寄存器
        public const byte MB_READ_HOLD_REG = 0x03;          //读保持寄存器
        public const byte MB_READ_INPUT_REG = 0x04;         //读输入寄存器
        public const byte MB_WRITE_SINGLE_COIL = 0x05;      //写单个线圈
        public const byte MB_WRITE_SINGLE_REG = 0x06;       //写单寄存器
        public const byte MB_WRITE_MULTIPLE_COILS = 0x0f;   //写多线圈
        public const byte MB_WRITE_MULTIPLE_REGS = 0x10;    //写多寄存器

        private const int DIGITAL_OUTPUT = 0x00;              //DO寄存器地址
        private const int DIGITAL_INPUT = 0x20;             //DI寄存器地址
        private const int VOLTAGE = 0x80;             //电压寄存器地址
        private const int TEMPERATURE = 0xA0;             //温度寄存器地址
        private const int CURRENT = 0xC0;             //电流寄存器地址


        #endregion
        public TasControl()
        {
            bRecive = true;
        }

        ~TasControl() {

            bRecive = false;
        }
        public bool Init(string _ServerIP, string _port)
        {
            ServerIP = _ServerIP;

            ServerPort = _port;

            
            th1 = new Thread(Receive);
            th1.IsBackground = true;
            th1.Start();

            return SocketConnect();
        }

        public void SendCmd(int index, bool bopen)
        {
            button_do_Set_index(index, bopen);
        }


        private bool SocketConnect()
        {
            #region 连接服务器端
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);     //声明负责通信的套接字

            SMLogWindow.OutLog($"ServerIP:{ServerIP}:ServerPort:{ServerPort}", Color.Green, loglevel: LogLevel.Info);
            IPAddress IP = IPAddress.Parse(ServerIP);      //获取设置的IP地址
            int Port = int.Parse(ServerPort);       //获取设置的端口号
            IPEndPoint iPEndPoint = new IPEndPoint(IP, Port);    //指定的端口号和服务器的ip建立一个IPEndPoint对象
            EndPoint Remote = (EndPoint)(iPEndPoint);

            try
            {
                ClientSocket.Connect(iPEndPoint);       //用socket对象的Connect()方法以上面建立的IPEndPoint对象做为参数，向服务器发出连接请求
                //SFlag = 1;  //若连接成功将标志设置为1

                //Printf(DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + textBox_Addr.Text + "连接成功！" + "\r\n");
                //button_Connect.BeginInvoke(new Action(() =>
                //{
                //    button_Connect.Enabled = false;     //禁止操作连接按钮
                //}));
                //开启一个线程接收数据
                return true;
            }
            catch(Exception ex)
            {
                SMLogWindow.OutLog($"SocketConnect:{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);
                return false;
                //button_Connect.BeginInvoke(new Action(() =>
                //{
                //    button_Connect.Enabled = true;     //禁止操作连接按钮
                //}));
                //MessageBox.Show("服务器未打开");
                //Printf("连接失败！\r\n");
            }
            #endregion
        }

        private void Receive()
        {
            //Socket socketRec = sk as Socket;
            while (bRecive)
            {
                try
                {
                 
                    if (ClientSocket == null||!ClientSocket.Connected)
                    {
                       // SMLogWindow.OutLog($"ClientSocket.Connected:{ClientSocket.Connected}", Color.Green, loglevel: LogLevel.Error);
                        continue;
                    }
                      
                    ReceiveLen = ClientSocket.Receive(response);
                   
                        
                    if (ReceiveLen > 0)
                    {
                        string Res = HexValueConvertHexString(response, ReceiveLen).TrimEnd('\0');
                        //Printf(DateTime.Now.ToString("hh:mm:ss  ") + "->Rx：");   //打印接收时间
                        //Printf(Res + "\r\n");     //使用UTF8编码接收中文不会乱码
                    }
                }
                catch (SocketException ex)
                {

                    // 发生异常，关闭连接
                    ClientSocket.Close();
                    //Printf("连接已断开");
                    //button_Connect.BeginInvoke(new Action(() =>
                    //{
                    //    button_Connect.Enabled = true;     //禁止操作连接按钮
                    //}));
                    SMLogWindow.OutLog($"连接已断开:{ClientSocket.Connected}:{ex.ToString()}", Color.Green, loglevel: LogLevel.Error);

                    bool bconnect = SocketConnect();
                    SMLogWindow.OutLog($"Retry_connect:{bconnect}", Color.Green);
                    return;
                }
            }
        }



        private void button_do_Set_index(int index, bool bopen)
        {
            try
            {
                byte[] data = new byte[2];

                if (!bopen)
                {
                    //button.Image = global::IOdemo.Properties.Resources.软件图标01_211;
                    data[0] = 0x00;
                }
                else
                {
                    //button.Image = global::IOdemo.Properties.Resources.软件图标01_212;
                    data[0] = 0xff;
                }

                data[1] = 0x00;
                //组织协议
                byte[] res = SendTrainCyclostyle(dev_addr, data, DIGITAL_OUTPUT + index, 0, 0x05);


                for (int i = 0; i < 5; i++)
                {
                    if (Send(res))
                    {
                        SMLogWindow.OutLog($"SendIO:i:{i}:true", Color.Green);
                        break;
                    }
                    SMLogWindow.OutLog($"SendIO:i:{i}:fasle", Color.Green);
                }


            }
            catch (Exception)
            {
                // Printf("继电器控制\r\n");
            }
            finally
            {
                // isThreadAlive = false;
            }
        }

        private byte[] SendTrainCyclostyle(int node, byte[] data, int addr, int con, byte stat)
        {

            dev_addr = node;
            int crcVal = 0;
            byte[] headData = SendTrainHead(node, addr, con, stat);                   //写首部分数据
            byte[] headDataLen = SendTrainBytes(headData, ref con, stat);       //计算数据的长度，有字节则写入。
            byte[] res = new byte[headDataLen.Length + con + 2];

            headDataLen.CopyTo(res, 0);

            if ((stat == MB_WRITE_MULTIPLE_REGS) || (stat == MB_WRITE_MULTIPLE_COILS))
            {
                try
                {
                    Array.Copy(data, 0, res, headDataLen.Length, con);                   //把数据复制到数据中
                }
                catch (Exception)
                {
                    return res;
                }

            }
            if (stat == MB_WRITE_SINGLE_COIL)
                Array.Copy(data, 0, res, headDataLen.Length - 2, 2);                   //把数据复制到数据中

            crcVal = MyConvert.Crc16(res, res.Length - 2);
            res[res.Length - 2] = (byte)(crcVal & 0xFF);
            res[res.Length - 1] = (byte)(crcVal >> 8);

            return res;
        }

        /// <summary>
        /// 主控方式  发送指令模板
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="data">数据</param>
        /// <param name="addr">地址</param>
        /// <param name="con">变量个数</param>
        /// <param name="stat">功能码</param>
        /// <returns></returns>
        private static byte[] SendTrainHead(int node, int addr, int len, int stat)
        {
            byte[] head = new byte[6];

            head[0] = Convert.ToByte(node);
            head[1] = (byte)(stat & 0xFF);
            head[2] = (byte)(addr >> 8);
            head[3] = (byte)(addr & 0xFF);
            head[4] = (byte)(len >> 8);
            head[5] = (byte)(len & 0xFF);

            return head;
        }
        /// <summary>
        /// 计算数据长度 并在0x0f,0x10功能下 加载字节数
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="len"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        private static byte[] SendTrainBytes(byte[] arr, ref int len, byte stat)
        {
            byte[] res;
            switch (stat)
            {
                default: len = 0; break;

                case MB_READ_COILS:
                case MB_READ_DISCRETE:
                case MB_READ_HOLD_REG:
                case MB_READ_INPUT_REG:
                case MB_WRITE_SINGLE_COIL:
                case MB_WRITE_SINGLE_REG:
                    len = 0;
                    break;

                case MB_WRITE_MULTIPLE_COILS:
                    len = (len % 8 == 0) ? (len / 8) : (len / 8 + 1);
                    res = new byte[arr.Length + 1];
                    arr.CopyTo(res, 0);
                    res[arr.Length] = (byte)(len);
                    arr = res;
                    break;

                case MB_WRITE_MULTIPLE_REGS:
                    len *= 2;
                    res = new byte[arr.Length + 1];
                    arr.CopyTo(res, 0);
                    res[arr.Length] = (byte)len;      //把字节写入数据最后位置
                    arr = res;
                    break;

            }
            return arr;
        }


        #region 向服务器端发送数据
        private bool Send(byte[] send)
        {
            //Thread sendThread = new Thread(() =>
            // {
            try
            {
                Array.Clear(response, 0, 5024);
                //只有连接成功了才能发送数据，接收部分因为是连接成功才开启线程，所以不需要使用标志
                if (ClientSocket.Connected)
                {
                    string Res = HexValueConvertHexString(send, send.Length);
                    ClientSocket.Send(send);    //调用Send()函数发送数据
                    //Printf(DateTime.Now.ToString("hh:mm:ss  ") + "<-Tx：");   //打印发送数据的时间
                    //Printf(Res + "\r\n");   //打印发送的数据
                    SMLogWindow.OutLog($"Res:{Res}", Color.Green);
                    return true;
                }
                SMLogWindow.OutLog($"Connected:{ClientSocket.Connected}", Color.Red, loglevel: LogLevel.Error);
                bool bconnect = SocketConnect();
                SMLogWindow.OutLog($"Retry_connect:{bconnect}", Color.Green);
                return false;
            }
            catch (Exception ex)
            {

                SMLogWindow.OutLog($"{ex.ToString()}", Color.Red, loglevel: LogLevel.Error);

                bool bconnect=SocketConnect();
                SMLogWindow.OutLog($"Retry_connect:{bconnect}", Color.Green);
                return false;
            }
            // });
            // 启动线程
            // sendThread.Start();
        }

        public static string HexValueConvertHexString(byte[] Data, int DataLen)
        {
            string Str = "";
            for (int i = 0; i < DataLen; i++)
            {
                if (Data[i] < 0xF || Data[i] == 0xF)
                {
                    Str += " " + "0";
                    Str += Data[i].ToString("X");
                }
                else
                {
                    Str += " " + Data[i].ToString("X");
                }
            }
            return Str;
        }
        #endregion

    }
}
