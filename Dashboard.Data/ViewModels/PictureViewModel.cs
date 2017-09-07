using Dashboard.Data.Entities;

namespace Dashboard.Data.ViewModel
{
    public class PictureViewModel
    {
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
    }
}