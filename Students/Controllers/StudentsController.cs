using Microsoft.AspNetCore.Mvc;
using Serilog;
using Students.ExceptionHandling;
using Students.Models;
using Students.Services;


namespace Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Field for _service a interface declaration.
        private readonly IStudentInterface _service;
        //Contstructor with a object implementing the interface
        public StudentsController(IStudentInterface service)
        {
            // field gets an instance of a class that implements the interface as a value.
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _service.GetAll();

                return Ok(students);
            }
            catch(DataNotFoundException ex)
            {
                Log.Error(ex, "Data not found error ocurred");
                return NotFound("Students not found");
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred.");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid student id. The id must be greater than zero.");
                }
                var student = await _service.GetById(id);

                return Ok(student);
            }
            catch(DataNotFoundException ex)
            {
                Log.Error(ex, "Data not found error ocurred");
                return NotFound("Student not found");
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred.");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student std)
        {
            try
            {
                var createdStudent = await _service.Create(std);
                return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (DataCreationException ex)
            {
                Log.Error(ex, "Error creating student");
                return BadRequest("Failed to create student: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student std)
        {
            try
            {
                var updatedStudent = await _service.Create(std);
                return CreatedAtAction(nameof(GetById), new { id = updatedStudent.Id }, updatedStudent);
            }
            catch (DataUpdateException ex)
            {
                Log.Error(ex, "Error creating student");
                return BadRequest("Failed to create student: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch(DataDeleteException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred.");
            }
           
        }
    }
}
