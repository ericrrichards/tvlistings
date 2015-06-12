namespace tvListings {
    using System.Collections.Generic;

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
}