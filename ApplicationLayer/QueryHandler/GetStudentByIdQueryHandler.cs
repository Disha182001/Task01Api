using ApplicationLayer.Queries;
using MediatR;
using Persistence.Context;
using Persistence.Entities.Model;
using Persistence.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.QueryHandler
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
    {
        private readonly AppDBContext _context;

        public GetStudentByIdQueryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.FindAsync(request.StudentId);
        }
    }
}
