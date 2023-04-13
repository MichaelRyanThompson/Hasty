using Hasty.Models.Requests.Appointments;

namespace Models.Requests.Appointments

{
    public class AppointmentUpdateRequest : AppointmentAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
