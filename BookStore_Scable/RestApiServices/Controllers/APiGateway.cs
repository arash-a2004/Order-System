using Azure.Core;
using LibraryDemo.Commands.AddCartToQueue;
using LibraryDemo.Models;
using LibraryDemo.Queries.GetAllBooks;
using LibraryDemo.Queries.GetAllUser;
using LibraryDemo.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestApiServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APiGateway : ControllerBase
    {
        private readonly IMediator _mediator;

        public APiGateway(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public ActionResult<List<BookDto>> GetAllBooks([FromBody] ApiBook apiBook)
        {
            try
            {
                var res = _mediator.Send(new GetAllBooksQuery(apiBook)).Result;

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet]
        [Route("User/GetAllUsers")]
        public ActionResult<IEnumerable<object>> GetUsers([FromBody] LibraryDemo.Models.FromBody.GetAllUserApiBody body)
        {
            try
            {
                var res = _mediator.Send(new GetAllUserQuery(body));

                return Ok(res);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest("BAD Request");
            }
        }


        [HttpGet]
        [Route("User/GetUserById")]
        public async Task<ActionResult<User>> GetUserById([FromQuery] int id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));

                if (user != null)
                {
                    return Ok(user);
                }

                return NotFound("Not Found");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("User/AddBoodToCartByUserId")]
        public async Task<ActionResult> AddBoodToCartByUserId([FromHeader] int userId, [FromBody] BookDto book)
        {
            //TODO : check if book is repetitive or not ? 

            try
            {
                var res = await _mediator.Send(new AddCartToQueueCommand(userId, book));

                if (res == LibraryDemo.Models.enums.UserFoundState.NotFound)
                    return NotFound("NotFound");

                else if (res == LibraryDemo.Models.enums.UserFoundState.NotFound_WithThisSpecification)
                    return NotFound("User With This Specification Do Not recognized by the system");

                else if (res == LibraryDemo.Models.enums.UserFoundState.Ok)
                    return Ok();

                else return BadRequest("A Bad Request Catch By System");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

    }
}
