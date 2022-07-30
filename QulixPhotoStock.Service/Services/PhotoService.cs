
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Data.Repositories;
using QulixPhotoStock.Domain.Entities.Photos;
using QulixPhotoStock.Service.DTOs.PhotoAuthors;
using QulixPhotoStock.Service.DTOs.PhotoDTOs;
using QulixPhotoStock.Service.Helpers;
using QulixPhotoStock.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IAuthorRepository authorRepository;
        public PhotoService()
        {
            photoRepository = new PhotoRepository();
            authorRepository = new AuthorRepository();
        }

        public async Task<PhotoForView> CreateAsync(PhotoForCreationDTO photo)
        {
            var existAuthor = await authorRepository.GetAsync(a => a.Id == photo.AuthorId);
            var existPhoto = await photoRepository.GetAsync(p => p.Link.Equals(photo.Link));

            if (existPhoto is not null)
                throw new Exception("Photo already exist");

            if (existAuthor is null)
                throw new Exception("Author not found");
            
            (long sizeofPicture, string nameOfPicture) = (0, "");

            try
            {
                var informationOfPicture = PictureInfo.GetPictureMemory(photo.Link);
                sizeofPicture = informationOfPicture.sizeOfPicture;
                nameOfPicture = informationOfPicture.nameOfPictire;
            }
            catch (Exception)
            {

                throw new Exception("Link not found");
            }

            var returnedText = await photoRepository.CreateAsync(new Photo()
            {
                Name = nameOfPicture,
                Link = photo.Link,
                AuthorId = photo.AuthorId,
                Cost = photo.Cost,
                OriginalSize = sizeofPicture,
                DateOfRegistration = DateTime.UtcNow
            });

            var photoWithAuthor = await photoRepository.GetAsync(t => t.Id == returnedText.Id);

            return Mapper(photoWithAuthor);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Photo, bool>> predicate)
        {
            var exist = await photoRepository.GetAsync(predicate);

            if (exist is null)
                return false;

            await photoRepository.DeleteAsync(predicate);
            return true;
        }

        public async Task<PhotoForView> GetAsync(Expression<Func<Photo, bool>> predicate)
            => Mapper(await photoRepository.GetAsync(predicate));

        public ICollection<PhotoForView> GetAll(Expression<Func<Photo, bool>> predicate = null)
        {
            ICollection<PhotoForView> photos = new List<PhotoForView>();

            if (predicate is null)
                foreach (var text in photoRepository.GetAll().Include(p => p.Author).AsEnumerable())
                    photos.Add(Mapper(text));
            else
                foreach (var text in photoRepository.GetAll(predicate).Include(p => p.Author).AsEnumerable())
                    photos.Add(Mapper(text));

            return photos;
        }

        public IEnumerable<PhotoForView> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Photo, bool>> predicate = null)
        {
            ICollection<PhotoForView> photos = new List<PhotoForView>();

            if (predicate is null)
                foreach (var text in photoRepository.GetAll().Include(p => p.Author).AsEnumerable())
                    photos.Add(Mapper(text));
            else
                foreach (var text in photoRepository.GetAll(predicate).Include(p => p.Author).AsEnumerable())
                    photos.Add(Mapper(text));

            return photos.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task<PhotoForView> UpdateAsync(long id, PhotoForCreationDTO photo)
        {
            var existPhoto = await photoRepository.GetAsync(p => p.Id == id);
            var existAuthor = authorRepository.GetAsync(a => a.Id == photo.AuthorId);
            
            if (existAuthor is null)
                throw new Exception("Author not found");

            if (existPhoto is null)
                throw new Exception("Photo not found");

            (long sizeofPicture, string nameOfPicture) = (0, "");

            try
            {
                var informationOfPicture = PictureInfo.GetPictureMemory(photo.Link);
                sizeofPicture = informationOfPicture.sizeOfPicture;
                nameOfPicture = informationOfPicture.nameOfPictire;
            }
            catch (Exception)
            {

                throw new Exception("Link not found");
            }

            existPhoto.Name = nameOfPicture;
            existPhoto.OriginalSize = sizeofPicture;
            existPhoto.Link = photo.Link;
            existPhoto.Cost = photo.Cost;
            existPhoto.AuthorId = photo.AuthorId;

            return Mapper(await photoRepository.UpdateAsync(existPhoto));
        }
        private PhotoForView Mapper(Photo photo)
            => new PhotoForView()
            {
                Id = photo.Id,
                Name = photo.Name,
                Link = photo.Link,
                Cost = photo.Cost,
                OriginalSize = photo.OriginalSize,
                NameOfAuthor = photo.Author.FirstName,
                NickNameOfAuthor = photo.Author.NickName,
                DateOfCreation = photo.DateOfRegistration,
                NumberOfSales = photo.NumberOfSales
            };
    }
}
