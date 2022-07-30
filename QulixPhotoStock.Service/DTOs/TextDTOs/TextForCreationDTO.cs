
using System.ComponentModel.DataAnnotations;

namespace QulixPhotoStock.Service.DTOs.TextDTOs
{
    public class TextForCreationDTO
    {
        [Required, MinLength(1), MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(600)]
        public string Text { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public long AuthorId { get; set; }
    }
}
