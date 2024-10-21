using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<ActivityDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            public Handler(DataContext context, ILogger<List> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                this.mapper = mapper;
            }

            private readonly DataContext _context;
            private readonly ILogger  _logger;
            private readonly IMapper mapper;

            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activities.ProjectTo<ActivityDto>(mapper.ConfigurationProvider).ToListAsync();

                

                return Result<List<ActivityDto>>.Success(activities);  
            }
        }
    }
}
