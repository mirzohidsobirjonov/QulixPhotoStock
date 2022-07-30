
using System;

namespace QulixPhotoStock.Domain.Commons
{
    public abstract class Auditable
    {
        public long Id { get; set; }
        public DateTime DateOfRegistration { get; set; }

    }
}
