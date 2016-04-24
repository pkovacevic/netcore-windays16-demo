
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static NetCoreWindaysDemo.Models;

namespace NetCoreWindaysDemo
{
    public class SentimentService
    {
        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";
        private const string AccountKey = "822b3dfb42ea44a884acadd5c98313b7";

        public static async Task<SentimentResult> Analyse(string sentence)
        {
            var result = await Analyse(new[] { sentence });
            return result.FirstOrDefault();
        }

        public static async Task<IList<SentimentResult>> Analyse(string[] sentences)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var input = new SentimentInput();
                for (int i = 0; i < sentences.Count(); i++)
                {
                    input.Documents.Add(new Document() { Id = i + 1, Text = sentences[i] });
                }


                // Request body. Insert your text data here in JSON format.
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input));

                // Detect sentiment:
                var uri = "text/analytics/v2.0/sentiment";
                var response = await CallEndpoint(client, uri, byteData);

                var output = JsonConvert.DeserializeObject<SentimentOutput>(response);



                return GenerateResult(input, output);
            }
        }

        private static IList<SentimentResult> GenerateResult(SentimentInput inputs, SentimentOutput outputs)
        {

            var ret = new List<SentimentResult>();

            foreach (var input in inputs.Documents)
            {
                foreach (var output in outputs.Documents)
                {
                    if (input.Id == output.Id)
                    {
                        ret.Add(new SentimentResult() { Sentence = input.Text, SentimentScore = output.Score });
                    }
                }
            }

            return ret;
        }

        private static async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }


  

    }
}