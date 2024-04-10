using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SmoreVision.CommClass
{
    public struct COM_Config
    {
        public string PortName;
        public int BaudRate;
        public int Parity;
        public int DataBits;
        public int StopBits;
        public int HandShake;

        public int ReadBufferSize;
        public int WriteBufferSize;

        public string NewLine;
    }

    public class VisaCOM
    {
        public const string VERSION = "1.0.3.0003";

        public const int ERROR_OK = 0;
        public const int ERROR_PORT_OPEN = -1;
        public const int ERROR_PORT_CLOSE = -2;
        public const int ERROR_PORT_READ = -3;
        public const int ERROR_PORT_WRITE = -4;
        public const int ERROR_PORT_SET = -5;
        public const int ERROR_PORT_QUERY = -6;
        public const int ERROR_PORT_TIMEOUT = -7;

        // Define all type operate maximum timeout, unit is millisecond
        private const int READ_TIMEOUT = 100;
        private const int WRITE_TIMEOUT = 100;
        private const int SET_TIMEOUT = 100;
        private const int QUERY_TIMEOUT = 3000;

        private const int OPERATE_INTERVAL = 100;
        private const int THREAD_SCAN_INTERVAL = 100;

        public const string COM_DEFAULT_PORTNAME = "COM1";

        public const int DEFAULT_BAUDRATE = 115200;
        public const int DEFAULT_PARITY = (int)System.IO.Ports.Parity.None;
        public const int DEFAULT_DATABITS = 8;
        public const int DEFAULT_STOPBITS = (int)System.IO.Ports.StopBits.One;
        public const int DEFAULT_HANDSHAKE = (int)System.IO.Ports.Handshake.None;
        public const int DEFAULT_READ_BUFFERSIZE = 8192;
        public const int DEFAULT_WRITE_BUFFERSIZE = 8192;
        public const string DEFAULT_NEWLINE_FLAG = NEW_LINE_FLAG_CRLF;

        public const string NEW_LINE_FLAG_CR = "CR";
        public const string NEW_LINE_FLAG_LF = "LF";
        public const string NEW_LINE_FLAG_CRLF = "CRLF";

        public const string NEW_LINE_VALUE_CR = "\r";
        public const string NEW_LINE_VALUE_LF = "\n";
        public const string NEW_LINE_VALUE_CRLF = "\r\n";

        public string DeviceSelectString = "INST:NSEL {0}";
        public string DeviceConnectString = "*IDN?";
        public string DeviceIDPrefix = "";
        private string DeviceIDRead = "";

        public static Encoding FileEncoding = Encoding.Default;

        private bool Running = false;
        private SerialPort COMPort;
        public COM_Config Config = new COM_Config();

        public bool Opened
        {
            get { return Running; }
        }

        public string DeviceID
        {
            get { return DeviceIDRead; }
        }

        public VisaCOM()
        {
            SetConfigDefault();
        }

        // Initial COM device
        public void SetConfigDefault()
        {
            if (Config.Equals(null))
            {
                Config = new COM_Config();
            }

            Config.PortName = COM_DEFAULT_PORTNAME;
            Config.BaudRate = DEFAULT_BAUDRATE;
            Config.Parity = DEFAULT_PARITY;
            Config.DataBits = DEFAULT_DATABITS;
            Config.StopBits = DEFAULT_STOPBITS;
            Config.HandShake = DEFAULT_HANDSHAKE;
            Config.ReadBufferSize = DEFAULT_READ_BUFFERSIZE;
            Config.WriteBufferSize = DEFAULT_WRITE_BUFFERSIZE;
            Config.NewLine = NEW_LINE_FLAG_CRLF;
        }

        private int OpenPort()
        {
            if (Running)
            {
                return ERROR_OK;
            }

            // Create a new SerialPort object with default settings.
            COMPort = new SerialPort();
            COMPort.PortName = Config.PortName.Trim().ToUpper();
            COMPort.BaudRate = Config.BaudRate;

            // Allow the user to set the appropriate properties.
            COMPort.Parity = (Parity)Config.Parity;
            COMPort.DataBits = 8;
            COMPort.StopBits = (StopBits)Config.StopBits;
            COMPort.Handshake = (Handshake)Config.HandShake;

            // Set the read/write buffer size
            COMPort.ReadBufferSize = Config.ReadBufferSize;
            COMPort.WriteBufferSize = Config.WriteBufferSize;

            // Set the read/write timeouts
            COMPort.ReadTimeout = READ_TIMEOUT;
            COMPort.WriteTimeout = WRITE_TIMEOUT;

            if (Config.NewLine.Trim().ToUpper() == NEW_LINE_FLAG_CR)
            {
                COMPort.NewLine = NEW_LINE_VALUE_CR;
            }
            else if (Config.NewLine.Trim().ToUpper() == NEW_LINE_FLAG_LF)
            {
                COMPort.NewLine = NEW_LINE_VALUE_LF;
            }
            else
            {
                COMPort.NewLine = NEW_LINE_VALUE_CRLF;
            }

            try
            {
                COMPort.Open();
                COMPort.DiscardInBuffer();
            }
#if DEBUG
            catch (Exception e)
            {
				MessageBox.Show(e.Message);
				return ERROR_PORT_OPEN;
            }
#else
            catch
            {
                return ERROR_PORT_OPEN;
            }
#endif

            Running = true;
            return ERROR_OK;
        }

        public int Initial()
        {
            int returnValue = OpenPort();
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }
            return ERROR_OK;
        }

        public int Initial(COM_Config comConfig)
        {
            Config = comConfig;
            return Initial();
        }

        public int Initial(string comPort, int baudrate = DEFAULT_BAUDRATE)
        {
            Config.PortName = comPort;
            Config.BaudRate = baudrate;
            return Initial();
        }

        public int Initial(int address)
        {
            int returnValue = OpenPort();
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }
            return Connect(address);
        }

        public int Initial(int address, COM_Config comConfig)
        {
            Config = comConfig;
            return Initial(address);
        }

        public int Initial(int address, string comPort, int baudrate = DEFAULT_BAUDRATE)
        {
            Config.PortName = comPort;
            Config.BaudRate = baudrate;
            return Initial(address);
        }

        // Deinitial COM device, release resource
        public int Deinitial()
        {
            Running = false;
            if (COMPort == null)
            {
                return ERROR_OK;
            }

            if (COMPort.IsOpen)
            {
                COMPort.Close();
                COMPort = null;
            }
            return ERROR_OK;
        }

        // Send command string to programmer
        public int Read(ref string response, int readTimeout = READ_TIMEOUT)
        {
            try
            {
                if (!Running)
                {
                    return ERROR_PORT_OPEN;
                }

                COMPort.ReadTimeout = readTimeout;
                Thread.Sleep(readTimeout);
                response = COMPort.ReadExisting().Trim();
                return ERROR_OK;
            }
            catch (TimeoutException e)
            {
                return ERROR_PORT_TIMEOUT;
            }
            catch (Exception e)
            {
                return ERROR_PORT_READ;
            }
        }

        // Send command string to programmer
        public int Write(string command, int writeTimeout = WRITE_TIMEOUT)
        {
            try
            {
                if (!Running)
                {
                    return ERROR_PORT_OPEN;
                }
                command = command.Trim();
                // Clear read buffer
                COMPort.DiscardInBuffer();
                COMPort.WriteTimeout = writeTimeout;

                // Write command
                COMPort.Write(command);
                return ERROR_OK;
            }
#if DEBUG
            catch (TimeoutException e)
            {
                MessageBox.Show(e.Message);
                return ERROR_PORT_TIMEOUT;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return ERROR_PORT_WRITE;
            }
#else
            catch (TimeoutException e)
            {
                return ERROR_PORT_TIMEOUT;
            }
            catch
            {
                return ERROR_PORT_WRITE;
            }
#endif
        }

        // Send command string to programmer
        public int Set(string command)
        {
            return Write(command);
        }

        // Send command string to programmer
        public int Query(string command, ref string response, int queryTimeout = QUERY_TIMEOUT)
        {
            int returnValue = Write(command);
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }

            return Read(ref response, queryTimeout);
        }

        public int Connect(string connectString, string responsePrefix)
        {
            DeviceConnectString = connectString;
            DeviceIDPrefix = responsePrefix;
            return Connect();
        }

        public int Connect(string responsePrefix)
        {
            DeviceIDPrefix = responsePrefix;
            return Connect();
        }

        public int Connect()
        {
            DeviceIDRead = "";
            int returnValue = Query(DeviceConnectString, ref DeviceIDRead);
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }

            if (DeviceIDPrefix == "")
            {
                return ERROR_OK;
            }

            if (DeviceIDRead.StartsWith(DeviceIDPrefix))
            {
                return ERROR_OK;
            }
            return ERROR_PORT_QUERY;
        }

        public int Connect(int address, string connectString, string responsePrefix)
        {
            DeviceConnectString = connectString;
            DeviceIDPrefix = responsePrefix;
            return Connect(address);
        }

        public int Connect(int address, string responsePrefix)
        {
            DeviceIDPrefix = responsePrefix;
            return Connect(address);
        }

        public int Connect(int address)
        {
            DeviceIDRead = "";
            int returnValue = Set(string.Format(DeviceSelectString, address));
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }

            returnValue = Query(DeviceConnectString, ref DeviceIDRead);
            if (returnValue != ERROR_OK)
            {
                return returnValue;
            }

            if (DeviceIDPrefix == "")
            {
                return ERROR_OK;
            }

            if (DeviceIDRead.StartsWith(DeviceIDPrefix))
            {
                return ERROR_OK;
            }
            return ERROR_PORT_QUERY;
        }
    }
}
