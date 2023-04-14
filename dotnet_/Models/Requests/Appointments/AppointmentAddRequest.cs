using System;
using System.ComponentModel.DataAnnotations;

namespace Hasty.Models.Requests.Appointments
{
    public class AppointmentAddRequest
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int ListingId { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public TimeSpan Time { get; set; }
    }
}
