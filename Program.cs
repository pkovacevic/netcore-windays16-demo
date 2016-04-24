using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWindaysDemo
{
    public class Program
    {

        public static string[] PositiveResponses = new[] { "Totally agree.", "I fully agree and endorse your views.", "Thank you for sharing your positive thoughts", "I admire your openness", "Thank you for your thought-provoking but positive message. You are doing a noble service" };
        public static string[] NegativeResponses = new[] { "Well I could also say a thing or two about you.", "Well you aren't helping either.", "It's because your talk stinks.", "Look who's talking.", "It’s your fault too you know?" };

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("How would you rate your Windays16 experience so far?");
            var ans = Console.ReadLine();
            
            try
            {
                var result = SentimentService.Analyse(ans).Result;

                if (result.SentimentScore > 0.5)
                {
                    Console.WriteLine("({0}) {1} ", result.SentimentScore, PositiveResponses[new Random().Next(0, PositiveResponses.Count())]);
                }
                else
                {
                    Console.WriteLine("({0}) {1}", result.SentimentScore, NegativeResponses[new Random().Next(0, NegativeResponses.Count())]);
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Demo gods are not on your side today Petar...");
            }
        }
    }
}
