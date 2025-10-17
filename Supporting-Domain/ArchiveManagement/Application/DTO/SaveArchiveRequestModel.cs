using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class SaveArchiveRequestModel
    {
        public int? CategoryId { get; set; }
        public long? ParentId { get; set; }
        public string? Title { get; set; }
        public long ProjectId { get; set; }
    }
}
