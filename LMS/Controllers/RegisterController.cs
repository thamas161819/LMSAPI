using Data.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Model;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _RegisterService;

        public RegisterController(IRegisterService registerService)
        {
            _RegisterService = registerService;
        }


        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            var result = await _RegisterService.AddAccount(account);
            return Ok(result);
        }
    }
}
