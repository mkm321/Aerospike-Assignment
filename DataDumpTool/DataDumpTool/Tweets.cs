using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDumpTool
{
    class Tweets
    {
        public string author { get; set; }
        public string content { get; set; }
        public string region { get; set; }
        public string language { get; set; }
        public int following { get; set; }
        public int followers { get; set; }
        public string url { get; set; }
        public int retweet { get; set; }
        public string id { get; set; }
        public string authorid { get; set; }
    }
}
