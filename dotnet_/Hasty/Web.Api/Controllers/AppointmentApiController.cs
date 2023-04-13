using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hasty.Models;
using Hasty.Models.Domain.Appointments;
using Hasty.Models.Requests.Appointments;
using Hasty.Services;
using Hasty.Services.Interfaces;
using Hasty.Web.Controllers;
using Hasty.Web.Models.Responses;
using System;
using Hasty.Data.Providers;


namespace Hasty.Web.Api.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentApiController : BaseApiController
    {
        private IDataProvider _data = null;
        private IListingService _listingService = null;
        private IUserService _userService = null;
        private IAppointmentService _service = null;
        private IAuthenticationService<int> _authService = null;
        private IEmailService _emailService = null;

        public AppointmentApiController(
            IDataProvider data,
            IListingService listingService,
            IUserService userService,
            IAppointmentService service,
            IEmailService emailService,
            ILogger<AppointmentApiController> logger,
            IAuthenticationService<int> authService) : base(logger)

        {
            _data = data;
            _listingService = listingService;
            _userService = userService;
            _service = service;
            _authService = authService;
            _emailService = emailService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Appointment>> Get(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Appointment appointment = _service.Get(id);

                if (appointment == null)
                {
                    code = 404;
                    response = new ErrorResponse("Listing Not Found");
                }
                else
                {
                    response = new ItemResponse<Appointment> { Item = appointment };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("createdby/{userId:int}")]
        public ActionResult<ItemResponse<Paged<Appointment>>> GetByCreatedByPaginated(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                Paged<Appointment> page = _service.GetByUserIdPaginated(pageIndex, pageSize, userId);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Appointment>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(AppointmentAddRequest model)
        {
            ObjectResult result = null;
            try
            {

                int userId = _authService.GetCurrentUserId();
                int id = _service.Add(model, userId);

                int listingId = model.ListingId;

                AppointmentConfirmation appointment = _service.GetAppointmentConfirmation(userId, listingId);

                ItemResponse<int> response = new ItemResponse<int> { Item = id };
                result = Created201(response);

                _emailService.SendAppointmentConfirmationToUser(appointment);

                _emailService.SendAppointmentConfirmationToListingOwner(appointment);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(AppointmentUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPut("confirmed/{id:int}")]
        public ActionResult<SuccessResponse> AppointmentIsConfirmed(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.UpdateIsConfirmed(id, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpPut("canceled/{id:int}")]
        public ActionResult<SuccessResponse> AppointmentIsCanceled(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.UpdateIsCanceled(id, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }
    }
}
