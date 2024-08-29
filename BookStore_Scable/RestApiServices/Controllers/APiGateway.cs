using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApiServices.Models;
using RestApiServices.Services;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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


        [HttpGet]
        [Route("User/GetAllUsers")]
        public ActionResult<IEnumerable<object>> GetUsers([FromBody] Models.FromBody.GetAllUserApiBody body)
        {
            try
            {
                Services.GetAllUser users = new();

                var userList = users.GetAllUsers();

                return Ok(userList);
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
                Services.UserById user = new();

                var SUser = await user.GetUserById(id);

                if (SUser != null)
                {
                    return Ok(SUser);
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
                BookStoreDbContext db = new();

                var user = db.Users.Include(e => e.Cart).Where(e => e.Id == userId).FirstOrDefault();

                if (user == null)
                {
                    return NotFound("User Not Found");
                }

                var bookSelected = db.Books
                   .Include(e => e.Authors)
                   .Include(e => e.Carts)
                   .Where(e => e.Title == book.Title)
                   .Distinct()
                   .FirstOrDefault();

                if (bookSelected == null)
                {
                    return NotFound("Book Not Found");
                }

                bool flag = false;
                foreach (var item in book.Authors)
                {
                    foreach (var item2 in bookSelected.Authors)
                    {
                        if (item2.Name == item)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }

                if (!flag)
                {
                    return NotFound("There are no books with this specification");
                }

                Cart cart = new Cart()
                {
                    User = user,
                    UserId = userId,
                };
                if(cart.Books == null)
                {
                    cart.Books = new List<Book>();
                }
                cart.Books.Add(bookSelected);

                string jsonString = JsonSerializer.Serialize(cart);

                AddCartToRabbitQueue.AddCartToQueue(jsonString);

                return Ok(cart);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
