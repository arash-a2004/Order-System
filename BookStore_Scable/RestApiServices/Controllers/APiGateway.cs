using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApiServices.Models;
using System.Net;

namespace RestApiServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APiGateway : ControllerBase
    {
        [HttpGet("[action]")]
        public ActionResult<List<BookDto>> GetAll([FromBody] ApiBook? apiBook)
        {
            try
            {
                //bool flag = true;
                BookStoreDbContext dbContext = new BookStoreDbContext();
                var query = dbContext.Books.AsQueryable();

                if (!string.IsNullOrEmpty(apiBook.Title))
                    query = query.Where(e => e.Title == apiBook.Title);

                //if (apiBook.BookAuthor != null)
                //{
                //    foreach(var author in apiBook.BookAuthor)
                //    {
                //        if (!string.IsNullOrEmpty(author))
                //        {
                //            flag = false;
                //            query = query.Include(u => u.Authors).Where(u=>u.Authors.Any(o=>o.Name == author));
                //        }
                //    }
                //}

                int Skip = 0;
                int Max = 10;

                if (apiBook.SkipCount != null)
                    Skip = apiBook.SkipCount.Value;

                if (apiBook.MaxResult != null)
                    Max = apiBook.MaxResult.Value;

                var result = query.Include(u => u.Authors).Skip(Skip).Take(Max).ToList();

                var res = new List<BookDto>();
                var list2 = new List<string>(); 

                foreach (var item in result) 
                {
                    list2.Clear();
                    list2.AddRange(item.Authors.Select(e => e.Name));
                    res.Add(
                            new BookDto()
                            {
                                Title = item.Title,
                                Authors = list2
                            }
                            
                        );
                }

                return Ok(res);
            }
            catch
            {

                return BadRequest("BAD REquest");
            }
        }


        [HttpGet("[action]")]
        public ActionResult<IEnumerable<object>> GetUsers([FromBody] Models.FromBody.GetAllUserApiBody body)
        {
            try
            {
                Services.GetAllUser users = new();

                var userList = users.GetAllUsers();

                return Ok(userList);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest("BAD Request");
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<User>> GetUserById([FromQuery]int id)
        {
            try
            {
                Services.UserById user = new();

                var SUser = await user.GetUserById(id);

                if(SUser != null)
                {
                    return Ok(SUser);
                }

                return NotFound("Not Found");
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
