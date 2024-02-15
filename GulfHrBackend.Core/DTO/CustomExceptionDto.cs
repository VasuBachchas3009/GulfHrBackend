using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.Core.DTO
{
    public class CustomExceptionDto<T>
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public T data { get; set; }
    }
}
