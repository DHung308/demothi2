using System.ComponentModel.DataAnnotations;

namespace AAAATHIDEMO.Models
{
    public class Faculty
    {
        [Key]
        public string FaID { get; set; }
        public string FaName { get; set; }
    }
}
