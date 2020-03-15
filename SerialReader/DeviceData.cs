using System;
using System.Collections.Generic;
using System.Text;

namespace SerialReader
{
    /// <summary>
    /// Device data
    /// </summary>
    public class DeviceData
    {
        public string Data { get; set; }
        public virtual DateTime CreationTime { get; set; }

        public DeviceData()
        {
            CreationTime = DateTime.Now;
        }
    }
}
