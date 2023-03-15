using Data.Repositary;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Data;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateResetTokenController : ControllerBase
    {
        private readonly IValidateResetToken _validateResetToken;
        private readonly IConfiguration _configuration;

        public ValidateResetTokenController(IValidateResetToken validateResetToken, IConfiguration configuration)
        {
            _validateResetToken = validateResetToken;
            _configuration = configuration;
        }

        //[HttpGet("ResetPassword/{resetToken}")]
        //public async Task<IActionResult> ValidateToken(string resetToken)
        // {
            
        //    //string email = "";  // get the email associated with the reset token from the database
        //    var result = await _validateResetToken.ValidateResetToken(resetToken);
            

        //    return Ok();

        //}


        [HttpGet("ResetPassword/{resetToken}")]
        public async Task<IActionResult> ValidateToken(string resetToken)
        {
            try
            {
                var (isValid, isExpired) = await _validateResetToken.ValidateResetToken(resetToken);

                if (!isValid)
                {
                    return BadRequest("Reset token is invalid.");
                }

                if (isExpired)
                {
                    return BadRequest("Reset token has expired.");
                }

                return Ok("Reset token is valid and not expired.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
