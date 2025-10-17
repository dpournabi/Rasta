using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetArchiveItemResponseModel
    {
        public byte[] FileBuffer { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}
