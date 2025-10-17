using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public partial class ArchiveItem
    {
        [NotMapped]
        public string? Icon { get; set; }
        [NotMapped]
        public bool IsDefault { get; set; }
    }
}
