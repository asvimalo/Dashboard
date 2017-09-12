

using Dashboard.Data.EF.Entities;

namespace Dashboard.Data.ViewModelsAPI
{
    public class PictureViewModel : BaseEntity
    {
        
        public string Title { get; set; }
        public string FileName { get; set; }
        
    }
}