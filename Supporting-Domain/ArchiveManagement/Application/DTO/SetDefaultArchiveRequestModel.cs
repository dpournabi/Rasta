using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class SetDefaultArchiveRequestModel
    {
        public long ProjectId { get; set; }
        public int CategoryId { get; set; }
        public long? ArchiveId { get; set; }
    }
}
