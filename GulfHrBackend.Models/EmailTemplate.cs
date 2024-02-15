using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class EmailTemplate
    {
        [Key]
        public Guid Id { get; set; }
        public string Name{ get; set; }
    }
}
