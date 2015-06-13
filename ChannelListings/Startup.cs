namespace ChannelListings {
    using System.Collections.Generic;

    using Nancy.ViewEngines.Razor;

    using Owin;

    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.UseNancy();
        }
    }
    public class RazorConfig : IRazorConfiguration {
        public IEnumerable<string> GetAssemblyNames() {
            yield return "tvListings";
        }

        public IEnumerable<string> GetDefaultNamespaces() {
            yield return "tvListings";
        }

        public bool AutoIncludeModelNamespace { get { return true; } }
    }
}