using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerialReader
{
    public static class UploadData
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> UploadDataAsync( DeviceData requestData)
        {
            //var URL = _configuration[TuLingApiConfigConst.URL];
            var URL = "http://localhost:21021/api/DeviceData/SaveDeviceData";

            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            //request.Headers.Add("User-Agent", "BCDPChat");
            HttpContent httpContent = new JsonContent(requestData);
            request.Content = httpContent;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return await new Task<string>(() => response.StatusCode.ToString());
            }
        }

        private static async Task<SaveDeviceDataOutput> ProcessData()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<SaveDeviceDataOutput>(await streamTask);
            return repositories;
        }
    }
}
