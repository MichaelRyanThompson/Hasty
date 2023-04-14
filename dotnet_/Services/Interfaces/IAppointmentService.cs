using Hasty.Models;
using Hasty.Models.Domain.Appointments;
using Hasty.Models.Requests.Appointments;

namespace Hasty.Services.Interfaces
{
	public interface IAppointmentService
	{
		Appointment Get(int id);

		Paged<Appointment> GetByUserIdPaginated(int pageIndex, int pageSize, int userId);
		
		AppointmentConfirmation GetAppointmentConfirmation(int userId, int listingId);
		
		int Add(AppointmentAddRequest model, int userId);
		
		void Update(AppointmentUpdateRequest model, int userId);
		
		void UpdateIsConfirmed(int id, int modifiedBy);
		
		void UpdateIsCanceled(int id, int modifiedBy);
	}
}
