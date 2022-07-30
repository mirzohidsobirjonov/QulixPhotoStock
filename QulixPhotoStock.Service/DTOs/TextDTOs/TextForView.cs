
using QulixPhotoStock.Domain.Entities.Authors;
using System;

namespace QulixPhotoStock.Service.DTOs.TextDTOs
{
    public class TextForView
    {
        public long Id { get; set; }
        public string Name  { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
        public decimal Cost { get; set; }
        public long NumberOfSales { get; set; }
        public string NameOfAuthor { get; set; }
        public string NickNameOfAuthor { get; set; }
    }
}
