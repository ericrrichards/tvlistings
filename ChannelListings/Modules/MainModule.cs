using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelListings.Modules {
    using Nancy;

    using tvListings;

    public class MainModule :NancyModule {
        private const string _apiKey = "bq4zwb4skur2gn9vzqedf6x7";
        private static int[] _channelSourceIds = {
            2329, 1190,97,661,1211,	233,322,23206,1867,487,1879,203,2896,2330,315,	
            430,431,441,440,1275,398,410,427,408,432,426,399,450,429,4893,434,423,	
            435,12014,3486,404,2006,425,433,3460,26,1232,448,1269,1219,1254,1260,1216,1193,23113,24672,
            126,1280,4678,451,1216,11815,9426,4224,2193,8172,14506,1193,
            16218,14505,3389,4579,44423,44425,1184,3729,27129,30617,34509,
            14833,28942,43574,32939,
        };
        //private static int[] _channelSourceIds = {
        //    2329, 1190
        //};
        public MainModule() {
            Get[""]=Get["/"]=Get["/Index"]=Get["index"] = Index;
        }

        private dynamic Index(dynamic parameters) {

            var schedule = new RoviApi(_apiKey).GetSchedule(_channelSourceIds, 180);


            return View["Views/Index", schedule];
        }
    }
}
