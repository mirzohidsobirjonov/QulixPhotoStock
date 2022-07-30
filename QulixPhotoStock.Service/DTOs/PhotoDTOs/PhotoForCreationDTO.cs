
using System.ComponentModel.DataAnnotations;

namespace QulixPhotoStock.Service.DTOs.PhotoAuthors
{
    public class PhotoForCreationDTO
    {
        [Required]
        public string Link { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public long AuthorId { get; set; }
    }
}
