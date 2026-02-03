using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_WebAPICRUD.Data;
using Practice_WebAPICRUD.Interfaces;
using Practice_WebAPICRUD.Models;
using Practice_WebAPICRUD.PaginationResultRequest;

namespace Practice_WebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _context;
        public StudentController(IStudentService context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(int id)
        {
            try
            {
                var res = await _context.GetStudentById(id);
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(res);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("Create_Student")]
        public async Task<ActionResult> CreateStudent(Student std)
        {
            try
            {
                await _context.CreateStudent(std);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetByPage")]
        public async Task<ActionResult> GetStudentByPage(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _context.GetStudentByPage(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

      
        }

        [HttpPost("Search")]
        public async Task<ActionResult> SearchStudent(RequestModel req)
        {
            try
            {
                var data = await _context.SearchStudent(req);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(204, ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateStudent(StudentUpdate req)
        {
            try
            {
                var data = await _context.UpdateStudent(req);
                if (data == null)
                {
                    return NotFound("Student not found");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete_Student")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var data =await _context.DeleteStudent(id);
            if(!data)
            {
                return NotFound("Student not matched please enter the right informaton:");
            }
            return Ok("Student deleted: now !");
        }
      
    }
}
