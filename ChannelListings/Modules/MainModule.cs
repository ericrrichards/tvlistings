using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelListings.Modules {
    using System.Net;
    using System.Xml.Linq;

    using Nancy;
    using Nancy.Responses;

    using tvListings;

    public class MainModule :NancyModule {
        private const string _apiKey = "bq4zwb4skur2gn9vzqedf6x7";
        private static int[] _channelSourceIds = {
            2329, 1190,97,661,1211,	233,322,23206,1867,487,1879,203,2896,2330,315,	
            430,431,441,440,1275,398,410,427,408,432,426,399,450,429,4893,434,423,	
            435,12014,3486,404,2006,425,433,3460,26,1232,448,1269,1219,1254,1260,1216,1193,
            126,1280,4678,451,1216,11815,9426,4224,2193,8172,14506,1193,
            16218,14505,3389,4579,44423,44425,1184,3729,27129,30617,34509,
            14833,28942,43574,32939,
        };
        private static int[] _vlcSourceIds = {97, 1211, 233, 322, 
            661,398,410,427,432,423,435,12014,3486,425, 433,3460,26,1232,1254,9426, 3389,4579,44425,30617,43574,14833

        };
        private static List<VlcPlaylistItem> _playlist;

        public MainModule() {
            Get[""]=Get["/"]=Get["/Index"]=Get["index"] = Index;
            Get["/All"] = All;
            Get["/play/{id}"] = Play;
        }

        private dynamic Index(dynamic parameters) {

            _playlist = GetPlaylist();

            var schedule = new RoviApi(_apiKey).GetSchedule(_vlcSourceIds, 180);
            

            return View["Views/Index", schedule];
        }

        private dynamic Play(dynamic parameters) {
            int channelId = parameters.id;
            var plId = _playlist.FirstOrDefault(i => i.ChannelNum == channelId);
            if (plId != null) {
                using (var wc = new WebClient()) {
                    wc.Headers[HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(":megaman22"));
                    wc.DownloadString("http://localhost:8080/requests/status.xml?command=pl_play&id="+plId.ID);
                }
            }
            return new RedirectResponse("../", RedirectResponse.RedirectType.Temporary);
        }

        private List<VlcPlaylistItem> GetPlaylist() {
            string xml;
            using (var wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(":megaman22"));
                xml = wc.DownloadString("http://localhost:8080/requests/playlist_jstree.xml");
            }
            var doc = XDocument.Parse(xml);
            var playlist = new Dictionary<int, VlcPlaylistItem>();
            var items = doc.Descendants("item").Where(i => i.Attribute("uri") != null && i.Attribute("uri").Value.StartsWith("http://"));
            foreach (var item in items) {
                var plItem = new VlcPlaylistItem {
                    Uri = item.Attribute("uri").Value,
                    ID = Convert.ToInt32(item.Attribute("id").Value.Split('_').Last()),
                    Name = item.Attribute("name").Value,
                    ChannelNum = Convert.ToInt32(item.Attribute("uri").Value.Split(new[] {"/v"}, StringSplitOptions.None).Last())
                };
                if (!playlist.ContainsKey(plItem.ChannelNum)) {
                    playlist.Add(plItem.ChannelNum, plItem);
                }
            }

            return playlist.Values.ToList();
        }

        private dynamic All(dynamic parameters) {

            var schedule = new RoviApi(_apiKey).GetSchedule(_channelSourceIds, 180);
            

            return View["Views/Index", schedule];
        }
    }

    public class VlcPlaylistItem {
        public int ID { get; set; }
        public string Uri { get; set; }
        public int ChannelNum { get; set; }
        public string Name { get; set; }
    }
}
