
using System.ComponentModel.DataAnnotations;

namespace Calendar.Api.Entities
{
    public class CalendarEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Members { get; set; }

        [Required]
        public string EventOrganizer { get; set; }


      

    }
}
