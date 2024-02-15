using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class Module
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
