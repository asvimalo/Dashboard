using System.ComponentModel.DataAnnotations;


namespace Dashboard.Models
{
    public class PictureForCreation
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
    }
}
