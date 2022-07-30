
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Domain.Entities.Texts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Data.Repositories
{
    public class TextRepository : GenericRepository<Text>, ITextRepository
    {
        public override Task<Text> GetAsync(Expression<Func<Text, bool>> predicate)
            => (GetAll(predicate)).Include("Author").FirstOrDefaultAsync();
    }
}
