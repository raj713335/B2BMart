using B2BMart.API.Errors;
using B2BMart.DataLayer.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BMart.API.Controllers
{
    public class BuggyController : BaseController
    {
        private readonly IUnitOfWork _uow;
        public BuggyController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSectetText()
        {
            return "Secret Stuff";
        }



        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _uow.ProductRepository.Find(42);

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _uow.ProductRepository.Find(42);
            var thingsToReturn = thing.ToString();

            if (thing == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetGetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
