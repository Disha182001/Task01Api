using ApplicationLayer.Commands;
using ApplicationLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities.Model;

namespace Task02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatStudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger <MediatStudentController>   _logger;    

        public MediatStudentController(IMediator mediator, ILogger<MediatStudentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateStudent(CreateStudentCommand command)
        {
            var studentId = await _mediator.Send(command);
            return Ok(studentId);
            
        }

        // Dispatch Query
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var query = new GetStudentByIdQuery { StudentId = id };
            var student = await _mediator.Send(query);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
            _logger.LogInformation("student readed ");
        }
    }
}
