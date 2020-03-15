using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerialReader
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Read data feom serial port.
            SerialPortTool st = new SerialPortTool("COM6");
            // Save data to db.
            SerialProcessor.OnSaveData += St_SaveDataAsync;
            SerialProcessor.SaveData();

            Console.ReadLine();
        }

        /// <summary>
        /// Sts the save data asynchronous.
        /// </summary>
        /// <param name="deviceData">The device data.</param>
        /// <returns></returns>
        private static async Task<string> St_SaveDataAsync(DeviceData deviceData)
        {
            return await UploadData.UploadDataAsync(deviceData);
        }
    }
}
