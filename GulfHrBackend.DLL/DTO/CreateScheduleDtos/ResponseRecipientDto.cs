using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO.CreateScheduleDtos
{
    public class ResponseRecipientDto
    {
        public Guid Id {  get; set; }
        public Guid RecipientId {  get; set; }
        public Guid ScheduleId { get; set; }
        public string Type {  get; set; }

    }
}
