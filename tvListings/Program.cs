using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tvListings {
    using System.IO;
    using System.Net;
    using System.Threading;

    using Newtonsoft.Json;
    class Program {
        private const string apikey = "bq4zwb4skur2gn9vzqedf6x7";
        public const string postalCode = "03833";
        public const string servicesUrl = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/{0}/info?locale=en-US&countrycode=US&apikey={1}";
        public const string channelListingsUrl = "http://api.rovicorp.com/TVlistings/v9/listings/servicedetails/serviceid/{0}/info?locale=en-US&apikey={1}";
        public const string channelSchedulesUrl = "http://api.rovicorp.com/TVlistings/v9/listings/gridschedule/{0}/info?locale=en-US&apikey={1}&duration=60&sourceid={2}&includechannelimages=true";

        public static int[] _channelSourceIds = {
            2329, 1190,97,661,1211,	233,1551,322,3853,23206,1867,20258,487,1879,203,2896,2330,3454,315,1881,20259,9397,	
            430,431,441,33,440,1275,398,410,424,427,408,432,438,426,452,399,1202,450,429,428,442,4893,28,434,5072,423,	
            435,12014,3486,404,2006,425,1168,436,433,3460,26,1232,448,1269,1219,1254,1260,439,1185,1216,1193,23113,24672,
            33927,126,20260,1280,4678,5072,37357,33,16137,451,1216,11815,9426,4224,2193,8172,14506,16397,28508,1261,1193,
            16218,14505,1168,3389,4579,4580,1160,5740,44423,1202,1161,44425,1184,1125,3818,3729,19810,27129,30617,34509,
            17891,14833,28942,9397,43574,19598,32939,19438,20981,
        };





        static void Main(string[] args) {


            // channel listings for comcast exeter
            // http://api.rovicorp.com/TVlistings/v9/listings/servicedetails/serviceid/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7


            // espn listing (linear)
            // http://api.rovicorp.com/TVlistings/v9/listings/linearschedule/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7&duration=60&sourceid=423&inprogress=true


            // espn listing (grid)
            // http://api.rovicorp.com/TVlistings/v9/listings/gridschedule/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7&duration=60&sourceid=423&includechannelimages=true

            //var result = GetServices();
            //var channels = GetChannels();
            using (var wc = new WebClient()) {
                var url = string.Format(channelSchedulesUrl, 64761, apikey, string.Join(",", _channelSourceIds));
                Console.WriteLine(url);
                var schedJson = wc.DownloadString(url);
                File.WriteAllText("sched.json", url + "\n\n" +schedJson);
            }


            Console.ReadLine();
        }

        private static RoviResult GetChannels() {
            RoviResult channels;

            using (var wc = new WebClient()) {
                var writer = File.AppendText("channels.txt");
                string channelsJson = null;
                while (string.IsNullOrEmpty(channelsJson)) {
                    try {
                        channelsJson = wc.DownloadString(string.Format(channelListingsUrl, 64761, apikey));
                    } catch (Exception ex) {
                        Thread.Sleep(1000);
                    }
                }
                channels = JsonConvert.DeserializeObject<RoviResult>(channelsJson);

                foreach (var channel in channels.ServiceDetailsResult.ChannelLineup.Channels) {
                    writer.WriteLine("{0}\t\t{2},\t\t{1}", channel.ChannelNum, channel.FullName, channel.SourceId);
                }
                writer.Dispose();
            }
            return channels;
        }

        private static RoviResult GetServices() {
            RoviResult result = null;
            string servicesJson = null;
            using (var wc = new WebClient()) {
                while (string.IsNullOrEmpty(servicesJson)) {
                    try {
                        servicesJson = wc.DownloadString(string.Format(servicesUrl, postalCode, apikey));
                        result = JsonConvert.DeserializeObject<RoviResult>(servicesJson);
                    } catch (Exception ex) {
                        Thread.Sleep(1000);
                    }
                }
            }
            return result;
        }
    }

    public class RoviResult {
        public ServicesResult ServicesResult { get; set; }
        public ServiceDetailsResult ServiceDetailsResult { get; set; }
    }



    public class ServicesResult {
        public string Status { get; set; }
        public Services Services { get; set; }
    }

    public class Services {
        public List<Service> Service { get; set; }
    }

    public class Service {
        public string ServiceClass { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }

    }

    public class ServiceDetailsResult {
        public string Status { get; set; }
        public ChannelLineup ChannelLineup { get; set; }
    }

    public class ChannelLineup {
        public string ServiceClass { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public List<Channel> Channels { get; set; }
    }

    public class Channel {
        public int SourceId { get; set; }
        [JsonProperty("Channel")]
        public int ChannelNum { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string SourceType { get; set; }
        public int SourceAttributes { get; set; }
    }
}
