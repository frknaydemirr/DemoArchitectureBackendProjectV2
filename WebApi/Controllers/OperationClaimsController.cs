using Business.Repository.OperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
    //interfacelerin hangi programa bağlı olduğunu programa söyleme-> Dependency Injection:
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        //operation claim kaydı yapacağız ; karşı taraftan veri alıp onu işleyebileceğiz!
        //IActionResul-> httprequestleri dönüyor 200,400...
        [HttpPost("add")]

        public  IActionResult Add(OperationClaim operationClaim)
        {
          var result=   _operationClaimService.Add(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
           

        }


        [HttpPost("update")]

        public IActionResult Update(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

        [HttpPost("delete")]

        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
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
            var result = _operationClaimService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

        [HttpGet("getById")]

        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }


    }
}
