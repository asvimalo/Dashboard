using System.ComponentModel.DataAnnotations;


namespace Dashboard.Data.ViewModelsAPI
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
