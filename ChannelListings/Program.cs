using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ChannelListings {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            if (args.Length == 0) {
                var servicesToRun = new ServiceBase[] {
                    new TvListingsService(), 
                };
                ServiceBase.Run(servicesToRun);
            } else {
                var svc = new TvListingsService();
                svc.Start();
                //Process.Start(ChimeService.Url.Replace("+", "localhost"));
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();
                svc.Quit();
                Console.WriteLine("Terminated, press enter to exit");
                Console.ReadLine();

            }
        }
    }
}
