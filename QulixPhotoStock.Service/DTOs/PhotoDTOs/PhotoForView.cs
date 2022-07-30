
using QulixPhotoStock.Domain.Entities.Authors;
using System;

namespace QulixPhotoStock.Service.DTOs.PhotoDTOs
{
    public class PhotoForView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public long OriginalSize { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string NameOfAuthor { get; set; }
        public string NickNameOfAuthor { get; set; }
        public decimal Cost { get; set; }
        public long NumberOfSales { get; set; }

    }
}
