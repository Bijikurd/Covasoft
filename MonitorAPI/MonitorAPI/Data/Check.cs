using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MonitorAPI.Data
{

    public class DataObject
    {
        public string Value { get; set; }
        public string Url { get; set; }
    } 

    public class Check
    { 


    }

    public class Service
    {
        public bool GetStatus(string URL)
        {
            string FullURL = URL;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(FullURL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }

    public class Website
    {
        public bool GetStatus(string URL, string word)
        {

            if (ReadTextFromUrl(URL).Contains(word))
            {
                return true;
            }

            return false;
        }

        string ReadTextFromUrl(string url)
        {
            // WebClient is still convenient
            // Assume UTF8, but detect BOM - could also honor response charset I suppose
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var textReader = new StreamReader(stream, Encoding.UTF8, true))
            {
                return textReader.ReadToEnd();
            }
        }

    }

}
