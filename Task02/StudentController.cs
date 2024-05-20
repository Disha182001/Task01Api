using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities.Model;
using Persistence.Entities.ViewModel;
using Persistence.Repository.Implementation;
using Persistence.Repository.Interface;

namespace Task02
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IRepo<Student> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<StudController> _logger;

        public StudentController(IRepo<Student> repo, IMapper mapper,ILogger<StudController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;

        }
        [Route("addstudent")]
        [HttpPost]
        public async Task<ActionResult> addStudent(StudentVM student)
        {
            var st = _mapper.Map<Student>(student);
            var studen = await _repo.AddAsync(st);
            _logger.LogInformation("Student Created Successfully");
            return Ok(studen);
        }

        [Route("readstudent")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentVM>>> ReadStudent()
        {
            
            var students = await _repo.ListAllAsync();
            var ST = _mapper.Map<IEnumerable<StudentVM>>(students);
            return Ok(ST);
        }

        [Route("updatestudent")]
        [HttpPut]

        public async Task<ActionResult> updateStudent(StudentVM model)
        {
            var st = _mapper.Map<Student>(model);
            _logger.LogInformation("Student Updated Successfully message by logger");
            await _repo.UpdateAsync(st);

            
            return Ok("Student Updated SuccessFully");
        }

        [Route("deletestudent")]
        [HttpDelete]
        public async Task<ActionResult> deletestudent(int id)

        {
            var stud = await _repo.GetByIdAsync(id);
            _logger.LogWarning("Are You Sure ?");
            await _repo.DeleteAsync(stud);
            return Ok("Student Deleted SuccessFully");
        }

    }
}
