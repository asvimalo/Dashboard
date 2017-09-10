using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dashboard.Data.ViewModelsAPI
{
    public class PictureToUpdate
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}
