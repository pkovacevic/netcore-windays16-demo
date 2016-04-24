using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWindaysDemo
{
    public class Models
    {
        public class SentimentResult
        {
            public string Sentence { get; set; }
            public float SentimentScore { get; set; }
        }

        internal class SentimentInput
        {
            public List<Document> Documents { get; set; }

            public SentimentInput()
            {
                Documents = new List<Document>();
            }
        }

        internal class Document
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        internal class SentimentOutput
        {
            public List<Result> Documents { get; set; }
            public SentimentOutput()
            {
                Documents = new List<Result>();
            }

        }

        internal class Result
        {
            public int Id { get; set; }
            public float Score { get; set; }

        }
    }
}
