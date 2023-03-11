using Data.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Model;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _RgService;

        public RegisterController(IRegisterService RgService)
        {
            _RgService = RgService;
        }



        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var categories = await _RgService.GetCategories();
        //    return Ok(categories);
        //}





        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Account account) 
        {

            var result = await _RgService.AddAccount(account);
            return Ok(result);
        }



      


     
    }
}
