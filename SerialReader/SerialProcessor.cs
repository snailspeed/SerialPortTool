using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;

namespace SerialReader
{
    public static class SerialProcessor
    {
        private const string SPLIT_STRING_MARK = "#";
        private const string TRIM_STRING = "\0";

        private static readonly ConcurrentQueue<string> Datas = new ConcurrentQueue<string>();

        public static event SaveDataHandler OnSaveData;

        public static void PushData(string data)
        {
            Datas.Enqueue(data.Replace(TRIM_STRING, string.Empty).Trim());
        }

        public static string SaveData()
        {
            Thread t = new Thread(new ThreadStart(ProcessData));
            t.Start();

            return string.Empty;
        }

        private async static void ProcessData()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (Datas.Count > 0 && Datas.TryDequeue(out string result))
                {
                    sb.Append(result);

                    if (result.Contains(SPLIT_STRING_MARK))
                    {
                        string[] dataline = sb.ToString().Split(SPLIT_STRING_MARK);

                        if (dataline.Length > 0)
                        {
                            var requestData = new DeviceData
                            {
                                Data = dataline[0].Replace(TRIM_STRING, string.Empty).Trim(),
                                CreationTime = DateTime.Now
                            };

                            await OnSaveData?.Invoke(requestData);

                            sb = sb.Replace(dataline[0] + SPLIT_STRING_MARK, string.Empty);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(5);
                }
            }

        }

    }
}
