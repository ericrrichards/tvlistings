namespace tvListings {
    using System.Collections.Generic;

    using Newtonsoft.Json;

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