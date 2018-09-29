using Aerospike.Client;
using AerospikleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AerospikleWebApi.Controllers
{
    public class TweetsController : ApiController
    {
        [HttpPost]
        public List<Records> GetAllRecords([FromBody] List<long> url)
        {
            var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
            string namespaces = "AirEngine";
            string sets = "Mohit";
            List<Records> records = new List<Records>();
            foreach (long uid in url)
            {
                Record record = aerospikeClient.Get(new BatchPolicy(), new Key(namespaces, sets, uid.ToString()));
                Records result = new Records();
                result.id = record.GetValue("id").ToString();
                result.author = record.GetValue("author").ToString();
                result.content = record.GetValue("content").ToString();
                result.region = record.GetValue("region").ToString();
                result.language = record.GetValue("language").ToString();
                result.followers = int.Parse(record.GetValue("followers").ToString());
                result.following = int.Parse(record.GetValue("following").ToString());
                result.retweet = int.Parse(record.GetValue("retweet").ToString());
                result.authorid = record.GetValue("authorid").ToString();
                records.Add(result);
            }
            return records;

        }
        [HttpPut]
        public IHttpActionResult UpdateRecord([FromBody] Update update)
        {
            var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
            string namespaces = "AirEngine";
            string sets = "Mohit";
            var key = new Key(namespaces, sets, update.url);
            aerospikeClient.Put(new WritePolicy(), key, new Bin[] {
                new Bin(update.bin, update.value)
            });
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteRecord([FromBody] string url)
        {
            var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
            string namespaces = "AirEngine";
            string sets = "Mohit";
            var key = new Key(namespaces, sets, url);
            bool result = aerospikeClient.Delete(new WritePolicy(), key);
            return Ok(result);
        }
    }
}
