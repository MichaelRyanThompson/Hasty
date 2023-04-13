using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hasty.Data.Providers;
using Hasty.Services.Interfaces;
using Hasty.Services;
using Hasty.Web.Controllers;
using Hasty.Models;
using Hasty.Web.Models.Responses;
using System;
using Hasty.Models.Domain.PatriotPoints;
using Hasty.Models.Requests.PatriotPoints;

namespace Hasty.Web.Api.Controllers
{
    [Route("api/patriotpoints")]
    [ApiController]
    public class PatriotPointsApiController : BaseApiController
    {
        private IPatriotPointsService _service = null;
        private IAuthenticationService<int> _authService = null;

        public PatriotPointsApiController(
            IPatriotPointsService service,
            ILogger<PatriotPointsApiController> logger,
            IAuthenticationService<int> authService) : base(logger)

        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("totals/user/{userId:int}")]
        public ActionResult<ItemResponse<PatriotPointsTotals>> GetByUserIdRunningTotals(int userId)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                PatriotPointsTotals totals = _service.GetByUserIdRunningTotals(userId);

                if (totals == null)
                {
                    code = 404;
                    response = new ErrorResponse("Listing Not Found");
                }
                else
                {
                    response = new ItemResponse<PatriotPointsTotals> { Item = totals };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<ItemResponse<Paged<PatriotPoints>>> GetPointsByUserPaginated(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                Paged<PatriotPoints> page = _service.GetPatriotPointsByUserIdPaginated(pageIndex, pageSize, userId);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<PatriotPoints>> { Item = page };
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
        public ActionResult<ItemResponse<int>> CreatePatriotPoints(PatriotPointsAddRequest model)
        {
            ObjectResult result = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.AddPatriotPoints(model, userId);

                ItemResponse<int> response = new ItemResponse<int> { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpPost("source")]
        public ActionResult<ItemResponse<int>> CreatePatriotPointsSource(PatriotPointsSourceAddRequest model)
        {
            ObjectResult result = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.AddPatriotPointsSource(model, userId);

                ItemResponse<int> response = new ItemResponse<int> { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpPut("source/{id:int}")]
        public ActionResult<SuccessResponse> UpdatePatriotPointsSource(PatriotPointsSourceUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.PatriotPointsSourceUpdate(model, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPut("delete/{id:int}")]
        public ActionResult<SuccessResponse> PatriotPointsSourceIsDeleted(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.PatriotPointsSourceUpdateIsDeleted(id, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpPut("expired/{id:int}")]
        public ActionResult<SuccessResponse> PatriotPointsSourceIsExpired(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.PatriotPointsSourceUpdateIsExpired(id, userId);
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
