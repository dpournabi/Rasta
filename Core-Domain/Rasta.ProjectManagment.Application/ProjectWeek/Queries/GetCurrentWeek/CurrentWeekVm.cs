using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetCurrentWeek
{
    public class CurrentWeekVm
    {
        public long ProjectId { get; set; }
        public Domain.Entities.ProjectWeek? WeekInfo { get; set; }
    }
}
