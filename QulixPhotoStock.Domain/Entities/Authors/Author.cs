
using QulixPhotoStock.Domain.Commons;
using QulixPhotoStock.Domain.Entities.Photos;
using QulixPhotoStock.Domain.Entities.Texts;
using System;
using System.Collections.Generic;

namespace QulixPhotoStock.Domain.Entities.Authors
{
    public class Author : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Text> Texts { get; set; }
    }
}
