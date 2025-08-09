using Business.Repository.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("add")]
        //public IActionResult Add(AuthDto authDto)
        //{
        //    _userService.Add(authDto);
        //    return Ok("User has been recorded succesfully");
        //}

        [HttpGet("getList")]
        public IActionResult GetList()
        {

            var result = _userService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult getById(int id)
        {

          var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("update")]
        public IActionResult Update(User user)
        {

            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {

            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("changePassword")]
        public IActionResult changePassword(UserChangePasswordDto userChangePasswordDto)
        {

            var result = _userService.ChangePassword(userChangePasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
