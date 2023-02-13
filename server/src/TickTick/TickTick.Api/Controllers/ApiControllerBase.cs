using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TickTick.Api.ResponseWrappers;

namespace TickTick.Api.Controllers
{

    [Route("v{v:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> ExecuteRequest<T>(IRequest<T> request)
        {
            var response = new Response<T>();


            try
            {
                if (request == null) throw new ArgumentNullException("Request cannot be null");
                var result = await mediator.Send(request);

                response.Status = HttpStatusCode.OK;
                response.Data = result;

                return StatusCode((int)response.Status, response);
            }
            catch (Exception ex)
            {
                var errors = new string[] {
                    ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message: string.Empty,
                };

                response.Errors = errors;
                response.Status = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.Status, response);
            }
        }
    }
}
