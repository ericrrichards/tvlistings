namespace tvListings {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;

    using Newtonsoft.Json;

    public class RoviApi {
        public const string ServicesUrl = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/{0}/info?locale=en-US&countrycode=US&apikey={1}";
        public const string ChannelListingsUrl = "http://api.rovicorp.com/TVlistings/v9/listings/servicedetails/serviceid/{0}/info?locale=en-US&apikey={1}";
        public const string GridSchedulesUrl = "http://api.rovicorp.com/TVlistings/v9/listings/gridschedule/{0}/info?locale=en-US&apikey={1}&duration=60&sourceid={2}";
        private readonly string _apikey;

        public RoviApi(string apiKey) {
            _apikey = apiKey;
        }

        public GridScheduleResult GetSchedule(IEnumerable<int> channelSourceIds) {
            RoviResult schedule;
            using (var wc = new WebClient()) {
                var url = String.Format(GridSchedulesUrl, 64761, _apikey, String.Join(",", channelSourceIds));
                Console.WriteLine(url);
                
                string schedJson = null;
                while (String.IsNullOrEmpty(schedJson)) {
                    try {
                        schedJson = wc.DownloadString(url);
                    } catch (Exception ex) {
                        Thread.Sleep(1000);
                    }
                }
                File.WriteAllText("sched.json", url + "\n\n" + schedJson);
                schedule = JsonConvert.DeserializeObject<RoviResult>(schedJson);

                foreach (var gridChannel in schedule.GridScheduleResult.GridChannels) {
                    Console.WriteLine("{0} {1}", gridChannel.Channel, gridChannel.DisplayName);
                    foreach (var airing in gridChannel.Airings) {
                        Console.WriteLine("\t{0}: {1} - {2}", airing.AiringTime.ToLocalTime().ToShortTimeString(), airing.Title, airing.EpisodeTitle);
                    }
                    break;
                }
            }
            return schedule.GridScheduleResult;
        }

        public ServiceDetailsResult GetChannels(int serviceId) {
            RoviResult channels;

            using (var wc = new WebClient()) {
                var writer = File.AppendText("channels.txt");
                string channelsJson = null;
                while (String.IsNullOrEmpty(channelsJson)) {
                    try {
                        channelsJson = wc.DownloadString(String.Format(ChannelListingsUrl, serviceId, _apikey));
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
            return channels.ServiceDetailsResult;
        }

        public ServicesResult GetServices(string postalCode) {
            RoviResult result;
            string servicesJson = null;
            using (var wc = new WebClient()) {
                while (String.IsNullOrEmpty(servicesJson)) {
                    try {
                        servicesJson = wc.DownloadString(String.Format(ServicesUrl, postalCode, _apikey));
                        
                    } catch (Exception ex) {
                        Thread.Sleep(1000);
                    }
                    
                }
                result = JsonConvert.DeserializeObject<RoviResult>(servicesJson);
            }
            return result.ServicesResult;
        }

        
    }
}