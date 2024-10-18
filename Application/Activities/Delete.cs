using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            public Handler(DataContext context)
            {
                _context = context;
                
            }

            private readonly DataContext _context;
            

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                

                _context.Activities.Remove(activity);
                var res = await _context.SaveChangesAsync() > 0;
                if (!res)
                {
                    return Result<Unit>.Failure("Failed to delete the activity!");
                }
                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}
