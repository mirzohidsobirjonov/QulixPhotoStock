
using QulixPhotoStock.Domain.Entities.Texts;
using QulixPhotoStock.Service.DTOs.TextDTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Interfaces
{
    public interface ITextService
    {
        Task<TextForView> CreateAsync(TextForCreationDTO text);
        Task<bool> DeleteAsync(Expression<Func<Text, bool>> predicate);
        Task<TextForView> GetAsync(Expression<Func<Text, bool>> predicate);
        ICollection<TextForView> GetAll(Expression<Func<Text, bool>> predicate = null);
        IEnumerable<TextForView> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Text, bool>> predicate = null);
        Task<TextForView> UpdateAsync(long id, TextForCreationDTO text);
    }
}
