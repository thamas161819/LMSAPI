﻿using Data.Repositary;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _StudentService;

        public StudentController(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var student = await _StudentService.GetStudent();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            var result = await _StudentService.AddStudent(student);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest("The category ID in the URL doesn't match the one in the request body.");
            }

            var updatedCategory = await _StudentService.UpdateStudent(student);

            if (updatedCategory == null)
            {
                return NotFound($"No category found with ID {id}");
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _StudentService.DeleteStudent(id);

            return NoContent();
        }
    }
}
