
using QulixPhotoStock.Domain.Entities.Authors;
using QulixPhotoStock.Service.DTOs.AuthorDTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> CreateAsync(AuthorForCreationDTO author);
        Task<bool> DeleteAsync(Expression<Func<Author, bool>> predicate);
        Task<Author> GetAsync(Expression<Func<Author, bool>> predicate);
        IEnumerable<Author> GetAll(Expression<Func<Author, bool>> predicate = null);
        IEnumerable<Author> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Author, bool>> predicate = null);
        Task<Author> UpdateAsync(long id, AuthorForCreationDTO author);
    }
}
