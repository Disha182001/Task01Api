using ApplicationLayer.Commands;
using MediatR;
using Persistence.Context;
using Persistence.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationLayer.CommanHandlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly AppDBContext _context;
        public CreateStudentCommandHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Name = request.Name,
                Age = request.Age,
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);

            return student.StudentId;
        }
    }
}
