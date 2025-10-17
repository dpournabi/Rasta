using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetCurrentWeek
{
    public class CurrentWeekQuery: IRequest<CurrentWeekVm>
    {
        public long ProjectId { get; set; }
    }
    public class CurrentWeekQueryHandler : IRequestHandler<CurrentWeekQuery, CurrentWeekVm>
    {
        private readonly IApplicationDbContext _context;

        public CurrentWeekQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<CurrentWeekVm> Handle(CurrentWeekQuery request, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            var current = await this._context.ProjectWeekes.Where(item => item.ProjectId == request.ProjectId && !item.IsDelete 
                && now >= item.StartDate && now <= item.EndDate && !item.IsDelete).FirstOrDefaultAsync();

            return new CurrentWeekVm() { 
                ProjectId= request.ProjectId,
                WeekInfo = current
            };
        }
    }
}
