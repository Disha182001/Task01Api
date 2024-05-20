using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities.Model;

namespace Task02
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudController : ControllerBase
    {
        //WithoutRepository
       

            private readonly AppDBContext _appDbContext;
        private readonly ILogger<StudController> _logger;

            public StudController(AppDBContext appDbContext,ILogger <StudController>logger)
            {
                _appDbContext = appDbContext;
            _logger = logger;   
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var students = await _appDbContext.Students.ToListAsync();
               _logger.LogInformation("Readed successfully");
                return Ok(students);
            }

            [HttpPost]
            public async Task<IActionResult> Post(Student student)
            {
                _appDbContext.Students.Add(student);
                await _appDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = student.StudentId }, student);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, Student student)
            {
                if (id != student.StudentId)
                {
                    return BadRequest();
                }

                _appDbContext.Entry(student).State = EntityState.Modified;

                try
                {
                    await _appDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            [HttpDelete("{postId}")]
            public async Task<IActionResult> Delete(int postId)
            {
                var student = await _appDbContext.Students.FindAsync(postId);
                if (student == null)
                {
                    return NotFound();
                }

                _appDbContext.Students.Remove(student);
                await _appDbContext.SaveChangesAsync();

                return NoContent();
            }

            private bool StudentExists(int id)
            {
                return _appDbContext.Students.Any(e => e.StudentId == id);
            }

        }
    }

