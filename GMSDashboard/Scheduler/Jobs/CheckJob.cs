using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraryData;
using Quartz;

namespace GMSDashboard.Scheduler
{
    public class CheckJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(GetAll("https://localhost:44341/Websites"));
            Console.ReadLine();
        }

        public string GetAll(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Content-Type: application/json");

            var response = client.DownloadString(url);

            return response;
        }
    }
}