using Hasty.Models.Domain;
using Hasty.Models.Domain.Appointments;
using Hasty.Models.Domain.Users;
using Hasty.Models.Requests.Users;

namespace Hasty.Services.Interfaces
{
    public interface IEmailService
    {
        void TestEmail(string toEmail);
        
        void ContactAdmin(ContactUs model);
        
        void ConfirmationEmail(ContactUs model);
        
        void SendRegisterConfirmation(UserAddRequest model, string token);
        
        void SendResetPassword(UserAuth model, string token);
        
        void SendNewsletter(NewsletterSub model);
        
        void SendAppointmentConfirmationToUser(AppointmentConfirmation model);
        
        void SendAppointmentConfirmationToListingOwner(AppointmentConfirmation model);

    }
}