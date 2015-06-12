namespace tvListings {
    using System;

    class Program {
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
            const string apiKey = "bq4zwb4skur2gn9vzqedf6x7";
            new RoviApi(apiKey).GetSchedule(_channelSourceIds);

            Console.ReadLine();
        }
    }

    
}
