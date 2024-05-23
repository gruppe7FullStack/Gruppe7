using System.ComponentModel.DataAnnotations;

namespace Gruppe7.Models
{
    public class Observation
    {
        [Key]
        public Guid Elementid { get; set; }

        public double Value { get; set; }
        public DateTime Date { get; set; }

        public string TimeOffset { get; set; }

        public string TimeResolution { get; set; }

        public int TimeSeriesId { get; set; }
    }
}
