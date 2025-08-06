using Business.Abstract;
using Entities.Concrete;
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

        [HttpPost("add")]
        //operation claim kaydı yapacağız ; karşı taraftan veri alıp onu işleyebileceğiz!
        //IActionResul-> httprequestleri dönüyor 200,400...
        public  IActionResult Add(OperationClaim operationClaim)
        {
             _operationClaimService.Add(operationClaim);
            return Ok("Registration successfully completed");

        }


    }
}
