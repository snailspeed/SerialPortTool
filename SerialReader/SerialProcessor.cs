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
        private const char SPLIT_MARK = '#';
        private const char TRIM_CHAR = '\0';
        private const string TRIM_STRING = "\0";
        private static readonly ConcurrentQueue<char> Datas = new ConcurrentQueue<char>();

        private static readonly ConcurrentQueue<string> Datas2 = new ConcurrentQueue<string>();

        public static event SaveDataHandler OnSaveData;

        public static void PushData(string data)
        {
            Datas2.Enqueue(data);

            //Datas.Union(data.AsEnumerable());

            //data.Where(x => x != TRIM_CHAR).ToList().ForEach(x =>
            //{
            //    Datas.Enqueue(x);
            //});
        }

        public static string SaveData()
        {
            Thread t = new Thread(new ThreadStart(ProcessData));
            t.Start();

            return string.Empty;
        }

        private async static void ProcessData2()
        {
            Memory<char> memory = new Memory<char>();
            while (true)
            {
                if (Datas.Count > 0)
                {
                    memory = memory.ToArray().Union(Datas.ToArray()).ToArray();
                    Datas.Clear();

                    if (memory.Span.Contains(SPLIT_MARK))
                    {
                        int mi = memory.Span.IndexOf(SPLIT_MARK);
                        int ml = memory.Length;
                        string value = memory.Slice(0, mi).ToString().Replace(TRIM_STRING, string.Empty).Trim();
                        if (value.Length > 0)
                        {
                            var requestData = new DeviceData
                            {
                                Data = value,
                                CreationTime = DateTime.Now
                            };

                            await OnSaveData?.Invoke(requestData);

                            memory = memory.Slice(mi, ml - mi - 1);
                        }
                    }
                }
            }
        }

        private async static void ProcessData()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (Datas.Count > 0 && Datas.TryDequeue(out char result))
                {
                    if (result != SPLIT_MARK)
                    {
                        sb.Append(result);
                    }
                    else
                    {
                        string value = sb.ToString().Replace(TRIM_STRING, string.Empty).Trim();
                        if (value.Length > 0)
                        {
                            var requestData = new DeviceData
                            {
                                Data = value,
                                CreationTime = DateTime.Now
                            };

                            await OnSaveData?.Invoke(requestData);

                            sb.Clear();
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
