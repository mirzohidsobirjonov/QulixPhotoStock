
using System;
using System.ComponentModel.DataAnnotations;

namespace QulixPhotoStock.Service.DTOs.AuthorDTOs
{
    public class AuthorForCreationDTO
    {
        [Required]
        public string FirsName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
