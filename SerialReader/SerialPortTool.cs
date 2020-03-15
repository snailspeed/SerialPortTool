using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialReader
{
    /// <summary>
    /// SerialPortTool class
    /// </summary>
    public class SerialPortTool
    {
        private SerialPort CurrentPort { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortTool"/> class.
        /// </summary>
        /// <param name="portName">Serial port name. etc. COM6</param>
        /// <param name="readBufferSize">Size of the read buffer.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="parity">The parity.</param>
        /// <param name="stopBits">The stop bits.</param>
        /// <param name="dataBits">The data bits.</param>
        /// <param name="readTimeout">The read timeout.</param>
        public SerialPortTool(string portName, int readBufferSize = 128, int baudRate = 9600,
            Parity parity = Parity.None, StopBits stopBits = StopBits.One,
            int dataBits = 8, int readTimeout = 1000)
        {
            CurrentPort = new SerialPort();

            CurrentPort.ReadBufferSize = readBufferSize;
            CurrentPort.PortName = portName;        //端口号 
            CurrentPort.BaudRate = baudRate;        //比特率 
            CurrentPort.Parity = parity;            //奇偶校验 
            CurrentPort.StopBits = stopBits;        //停止位 
            CurrentPort.DataBits = dataBits;        //数据位
            CurrentPort.ReadTimeout = readTimeout;  //读超时，即在1000内未读到数据就引起超时异常 

            //绑定数据接收事件，因为发送是被动的，所以你无法主动去获取别人发送的代码，只能通过这个事件来处理
            CurrentPort.DataReceived += CurrentPort_DataReceived;
            CurrentPort.Open();
        }

        private void CurrentPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //byte[] receiveStr;

            SerialPort sp = sender as SerialPort;
            if (sp == null)
                return;
            byte[] readBuffer = new byte[sp.ReadBufferSize];
            sp.Read(readBuffer, 0, readBuffer.Length);

            //赋值
            //receiveStr = readBuffer;
            //将byte[]转换为字符串。
            string str = Encoding.Default.GetString(readBuffer);

            // Print string to console. 
            Console.Write(str.Replace("\0", string.Empty).Trim());

            // Add string to queue.
            SerialProcessor.PushData(str);

            //await UploadData.UploadDataAsync(requestData);

            // Lower cpu using percent
            Thread.Sleep(100);
        }
    }
}
