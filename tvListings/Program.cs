using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tvListings {
    class Program {
        private const string apikey = "bq4zwb4skur2gn9vzqedf6x7";
        static void Main(string[] args) {
            // services for exeter
            // http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/03833/info?locale=en-US&countrycode=US&apikey=bq4zwb4skur2gn9vzqedf6x7


            // channel listings for comcast exeter
            // http://api.rovicorp.com/TVlistings/v9/listings/servicedetails/serviceid/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7


            // espn listing (linear)
            // http://api.rovicorp.com/TVlistings/v9/listings/linearschedule/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7&duration=60&sourceid=423&inprogress=true
        
            
            // espn listing (grid)
            // http://api.rovicorp.com/TVlistings/v9/listings/gridschedule/64761/info?locale=en-US&apikey=bq4zwb4skur2gn9vzqedf6x7&duration=60&sourceid=423&includechannelimages=true
        }
    }
}
