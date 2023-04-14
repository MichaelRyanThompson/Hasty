using Hasty.Models.Domain.Listings;
using Hasty.Models.Domain.Users;
using System.Collections.Generic;

namespace Hasty.Models.Domain.Appointments
{
    public class AppointmentConfirmation
    {
        public UserProfile User { get; set; }
        
        public Listing Property { get; set; }
        
        public UserProfile ListingOwner { get; set; }

    }
}
