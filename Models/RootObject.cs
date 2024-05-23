using System.ComponentModel.DataAnnotations;

namespace Gruppe7.Models
{
    public class RootObject
    {
        [Key]
        public Guid SourceId { get; set; }
        public DateTime ReferenceTime { get; set; }

        public ICollection<Observation> Observations { get; set; }
    }
}
