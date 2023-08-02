using System.ComponentModel.DataAnnotations;

namespace AAAATHIDEMO.Models
{
    public class Student
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
