using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Picture 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}