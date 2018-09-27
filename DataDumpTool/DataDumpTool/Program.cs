using Aerospike.Client;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDumpTool
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"C:\tweets1.csv");
            CsvReader csvread = new CsvReader(sr);
            //csvread.Configuration.HasHeaderRecord = false;
            IEnumerable<Tweets> record = csvread.GetRecords<Tweets>();
            var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
            string nameSpace = "AirEngine";
            string setName = "Mohit";
            int flag = 0;
            int count=0;
            foreach (var rec in record) // Each record will be fetched and printed on the screen
            {
                if(count<=20000)
                {
                    if (flag != 0)
                    {
                        var url = rec.url;
                        string[] urlKey = url.Split('/');
                        var key = new Key(nameSpace, setName, urlKey[5]);
                        aerospikeClient.Put(new WritePolicy(), key, new Bin[] {
                            new Bin("author", rec.author),
                            new Bin("content",rec.content),
                            new Bin("region",rec.region),
                            new Bin("language",rec.language),
                            new Bin("following",rec.following),
                            new Bin("followers",rec.followers),
                            new Bin("id",rec.id),
                            new Bin("retweet",rec.retweet),
                            new Bin("authorid",rec.authorid)
                        });
                    }
                    else
                    {
                        flag = 1;
                    }
                }
            }
            sr.Close();
        }
    }
}
