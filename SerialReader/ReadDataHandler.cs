using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SerialReader
{
    //声明委托
    public delegate Task<string> SaveDataHandler(DeviceData deviceData);
}
