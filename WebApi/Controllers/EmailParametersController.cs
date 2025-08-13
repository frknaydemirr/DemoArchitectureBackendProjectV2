using Business.Repository.EmailParameterRepository;
using Business.Repository.OperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {

        private readonly IEmailParameterService  _emailParameterService;

        public EmailParametersController(IEmailParameterService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }


        [HttpPost("add")]

        public IActionResult Add(EmailParameters emailParameters)
        {
            var result = _emailParameterService.Add(emailParameters);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }


        [HttpPost("update")]

        public IActionResult Update(EmailParameters emailParameters)
        {
            var result = _emailParameterService.Update(emailParameters);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

        [HttpPost("delete")]

        public IActionResult Delete(EmailParameters emailParameters)
        {
            var result = _emailParameterService.Delete(emailParameters);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

        //Tokenımızı kontrol ediyor; istersek de rolümüzü kontrol ediyor:
        [HttpGet("getList")]
        //[Authorize(Roles ="GetList")]
        public IActionResult GetList()
        {
            var result = _emailParameterService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

        [HttpGet("getById")]

        public IActionResult GetById(int id)
        {
            var result = _emailParameterService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }





    }
}
