using Dashboard.Data.EF.Entities;


namespace Dashboard.Data.Entities
{
    public class Picture : BaseEntity
    {
        
        public string Title { get; set; }
        public string FileName { get; set; }
        public int? UserId { get; set; }
        
        
    }
}