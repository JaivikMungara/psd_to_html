using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mainproject.Models
{
    public partial class ContactUsAttachment
    {
        public int ContactUsAttachmentId { get; set; }
        public string Name { get; set; }
        public byte[] FileName { get; set; }
    }
}
