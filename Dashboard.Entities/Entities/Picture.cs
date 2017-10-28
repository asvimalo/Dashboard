using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities
{
    public class Picture 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }


    }
}