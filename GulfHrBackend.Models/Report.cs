using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ModuleId { get; set; }
    }
}
