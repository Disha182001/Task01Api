using Persistence.Context;
using Persistence.Entities.MediatRModels.Command;
using Persistence.Entities.Model;
using MediatR;


namespace MediatR.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Student>
    {
        private readonly AppDBContext _context;

        public CreateStudentCommandHandler(AppDBContext context)
        {
            _context = context;
        }

    }
}
