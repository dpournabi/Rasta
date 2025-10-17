using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ReadCulpritFilterModel
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int? JobTitleId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
