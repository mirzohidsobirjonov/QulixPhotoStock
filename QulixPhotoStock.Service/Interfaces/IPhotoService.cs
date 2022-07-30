
using QulixPhotoStock.Domain.Entities.Photos;
using QulixPhotoStock.Domain.Entities.Texts;
using QulixPhotoStock.Service.DTOs.PhotoAuthors;
using QulixPhotoStock.Service.DTOs.PhotoDTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoForView> CreateAsync(PhotoForCreationDTO photo);
        Task<bool> DeleteAsync(Expression<Func<Photo, bool>> predicate);
        Task<PhotoForView> GetAsync(Expression<Func<Photo, bool>> predicate);
        ICollection<PhotoForView> GetAll(Expression<Func<Photo, bool>> predicate = null);
        IEnumerable<PhotoForView> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Photo, bool>> predicate = null);
        Task<PhotoForView> UpdateAsync(long id, PhotoForCreationDTO photo);
    }
}
