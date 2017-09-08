using Dashboard.Data.EF.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Picture : BaseEntity
    {
        
        public string Title { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
    }
}