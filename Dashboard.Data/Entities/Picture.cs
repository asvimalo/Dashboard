using Dashboard.Data.EF.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Picture : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int? UserId { get; set; }
        
        
    }
}