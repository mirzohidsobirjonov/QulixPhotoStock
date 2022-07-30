
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.Contexts;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Domain.Entities.Photos;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Data.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public override Task<Photo> GetAsync(Expression<Func<Photo, bool>> predicate)
            => (GetAll(predicate)).Include("Author").FirstOrDefaultAsync();
    }
}
