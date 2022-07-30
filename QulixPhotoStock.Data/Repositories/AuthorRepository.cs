
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Domain.Entities.Authors;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public override Task<Author> GetAsync(Expression<Func<Author, bool>> predicate)
            => (GetAll(predicate)).Include("Photos").Include("Texts").FirstOrDefaultAsync();
    }
}
