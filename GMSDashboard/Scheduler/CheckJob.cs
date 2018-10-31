using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Quartz;

namespace GMSDashboard.Scheduler
{
    public class CheckJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            Debug.Print("\n ----------------------- DATA ----------------------- \n");
            Debug.Print(GetAll("https://localhost:44341/Websites"));
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