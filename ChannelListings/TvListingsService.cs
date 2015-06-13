namespace ChannelListings {
    using System;
    using System.Reflection;
    using System.ServiceProcess;

    using log4net;
    using log4net.Config;

    using Microsoft.Owin.Hosting;

    public partial class TvListingsService : ServiceBase {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IDisposable _webhost;
        public TvListingsService() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            Start();
        }

        protected override void OnStop() { Quit(); }

        public void Start() {
            XmlConfigurator.Configure();
            _webhost = WebApp.Start<Startup>("http://+/TvListings");
            Log.Debug("Running on http://+/TvListings");
        }

        public void Quit() {
            _webhost.Dispose();
        }
    }
}
