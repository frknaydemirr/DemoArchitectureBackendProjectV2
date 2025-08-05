using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        SqlConnection con = new SqlConnection("Server=DESKTOP-798VB1N\\MSSQLSERVERDEV;Database=DemoDb;Integrated Security=true;");
        SimpleContextDb context = new SimpleContextDb();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //SqlDataAdapter adp= new SqlDataAdapter("select * from Users", con);
            //DataTable da= new DataTable();
            //da.Clear();
            //adp.Fill(da);

            context.Users.ToList();



            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
