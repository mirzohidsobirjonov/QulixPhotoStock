
using QulixPhotoStock.Domain.Commons;
using QulixPhotoStock.Domain.Entities.Authors;
using System.ComponentModel.DataAnnotations.Schema;

namespace QulixPhotoStock.Domain.Entities.Photos
{
    public class Photo : Auditable
    {
        public string Name { get; set; }
        public string Link { get; set; }
        /// <summary>
        /// Size of Photo in Kb
        /// </summary>
        public long OriginalSize { get; set; }
        public decimal Cost { get; set; }
        public long NumberOfSales { get; set; }

        public long AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
    }
}
