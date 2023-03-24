﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Data.Services;
using Data.Repositary;
using Model;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseService;

        public CourseController(ICourse courseService)
        {
            _courseService = courseService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var course = await _courseService.GetCourse();
            return Ok(course);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseByID(string CourseCode)
        {
            var Course = await _courseService.GetCourseByID(CourseCode);

            if (CourseCode != null)
            {
                Course.CourseCode = CourseCode;
            }
            if (Course == null)
            {
                return NotFound();
            }

            return Ok(Course);
        }



        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] AddCourse course)
        {
            var result = await _courseService.AddCourse(course);
            return Ok(result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(string Coursecode, [FromBody] Course course)
        {
            if (Coursecode!= course.CourseCode)
            {
                return BadRequest("The Course  ID in the URL doesn't match the one in the request body.");
            }

            var update = await _courseService.UpdateCourse(course);

            if (update == null)
            {
                return NotFound($"No category found with ID {Coursecode}");
            }

            return Ok(update);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string  CourseCode)
        {
            await _courseService.DeleteCourse (CourseCode);

            return NoContent();
        }
    }
}
