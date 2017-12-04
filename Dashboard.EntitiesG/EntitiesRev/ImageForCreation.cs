using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.EntitiesG.EntitiesRev
{
 
    public class ImageForCreation 
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public byte[] Bytes { get; set; }


    }
}