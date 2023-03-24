﻿using Data.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LMS.Utility;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _RgService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public RegisterController(IRegisterService RgService, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _RgService = RgService;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }





        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Account account) 
       

          {
            try
            {
                var VerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                account.VerificationToken = VerificationToken;
                var result = await _RgService.AddAccount(account);

                    var senderEmail = account.Email;
                    var subject = "Password Reset Request";
                    // reading The HTML file from the directory using the _hostingEnvironment
                    var path = Path.Combine(_hostingEnvironment.ContentRootPath, "EmailTemplate", "AccountConfirmation.html");
                    // to read that path and convert it into the Mesafe 
                    var message = await System.IO.File.ReadAllTextAsync(path);
                    var VerificationLink = _configuration.GetValue<string>("VerificationTokenLink");

                    
                    message = message.Replace("{UserFullName}", account.FirstName+account.LastName)
                                        .Replace("{ConfirmationLink}", "/verifyEmail/"+ VerificationToken)
                                        .Replace("{Username}",account.FirstName)
                                        .Replace("{Password}",account.PasswordHash);
                    bool isEmailSent = SendEmail.EmailSend(senderEmail, subject, message, null);

                if (isEmailSent)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to send password reset link to your email address.");
                }

                                       

              
            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
         }
               

     
    }
}
