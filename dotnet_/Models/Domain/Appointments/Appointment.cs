using Hasty.Models.Domain.Listings;
using System;
using System.Reflection;

namespace Hasty.Models.Domain.Appointments
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string Phone { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Listing Listing { get; set; }
        public BaseUser CreatedBy { get; set; }
        public BaseUser ModifiedBy { get; set; }
    }
}