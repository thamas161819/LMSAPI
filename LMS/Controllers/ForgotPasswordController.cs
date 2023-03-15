using Data.Repositary;
using Data.Services;
using LMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Security.Cryptography;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService, IConfiguration configuration)
        {
            _forgotPasswordService = forgotPasswordService;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword( ForgotPassword email )
        {
            try
            {
                var ResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                var emailExists = await _forgotPasswordService.IsEmailExists(email.Email, ResetToken);

                if (emailExists!=null)
                {
                    var senderEmail = "thamas9824@gmail.com";
                    var subject = "Password Reset Request";
                    
                    var message = System.IO.File.ReadAllText(@"D:\C# projects\LMSAPI\LMS\EmailTemplate\PasswordReset.html");
                    var resetPasswordLink = _configuration.GetValue<string>("ResetPasswordLink");
               
                    message = message.Replace("{ResetPasswordLink}", resetPasswordLink+"/ForgortPassword/ResetPassword/RestToken=?"+ ResetToken);
                    bool isEmailSent = SendEmail.EmailSend(senderEmail, subject, message, null);
             

                    if (isEmailSent)
                    {
                        return Ok("Password reset link sent to your email address.");
                    }
                    else
                    {
                        return BadRequest("Failed to send password reset link to your email address.");
                    }
                    return Ok(emailExists);

                }

                return BadRequest("Email doesn't exist");
            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
         }






    }
}
