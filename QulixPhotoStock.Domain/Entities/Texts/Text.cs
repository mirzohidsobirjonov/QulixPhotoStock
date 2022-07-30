
using QulixPhotoStock.Domain.Commons;
using QulixPhotoStock.Domain.Entities.Authors;
using System.ComponentModel.DataAnnotations.Schema;

namespace QulixPhotoStock.Domain.Entities.Texts
{
    public class Text : Auditable
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public long NumberOfSales { get; set; } = 0;

        public long AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }


    }
}
