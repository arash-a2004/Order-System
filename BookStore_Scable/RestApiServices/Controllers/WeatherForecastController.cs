using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using RestApiServices.Models;
using System.Net;

namespace RestApiServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpGet("[action]")]
        public ActionResult<List<Book>> GetAll([FromBody] string Title = "")
        {
            try
            {
                BookStoreDbContext dbContext = new BookStoreDbContext();
                if(Title != "")
                {
                    var books = dbContext.Books.Where(e => e.Title == Title).Skip(0).Take(10).ToList();
                    return Ok(books);
                }
                else
                {
                    var books = dbContext.Books.Skip(0).Take(10).ToList();
                    return Ok(books);
                }
            }
            catch
            {

                return BadRequest("BAD REquest");
            }
        }
    }
}
