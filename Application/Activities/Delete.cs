using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public Handler(DataContext context)
            {
                _context = context;
                
            }

            private readonly DataContext _context;
            

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity is not null)
                {
                    _context.Activities.Remove(activity);
                    await _context.SaveChangesAsync();

                }

            }
        }
    }
}
