using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dashboard.Models
{
    public class PictureToUpdate
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}
