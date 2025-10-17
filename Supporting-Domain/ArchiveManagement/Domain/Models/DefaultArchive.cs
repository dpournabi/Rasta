using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DefaultArchive
    {
        public int CategoryId { get; set; }
        public long ProjectId { get; set; }
        public long? ArchiveId { get; set; }
    }
}
