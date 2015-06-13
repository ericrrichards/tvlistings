namespace tvListings {
    using System;
    using System.Collections.Generic;

    public class GridScheduleResult {
        public string Status { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<GridChannel> GridChannels { get; set; }
    }
    public class GridChannel {
        public int SourceId { get; set; }
        public int Channel { get; set; }
        public string DisplayName { get; set; }
        public List<Airing> Airings { get; set; }
    }

    public class Airing {
        public int ProgramId { get; set; }
        public int SeriesId { get; set; }
        public string Title { get; set; }
        public string EpisodeTitle { get; set; }
        public DateTime AiringTime { get; set; }
        public DateTime AiringTimeLocal { get  {return AiringTime.ToLocalTime().AddSeconds(-AiringTime.Second).AddMilliseconds(-AiringTime.Millisecond);}}
        public int Duration { get; set; }
        public string TVRating { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
    }
}