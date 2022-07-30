
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Data.Repositories;
using QulixPhotoStock.Domain.Entities.Authors;
using QulixPhotoStock.Service.DTOs.AuthorDTOs;
using QulixPhotoStock.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorService()
        {
            authorRepository = new AuthorRepository();
        }

        public async Task<Author> CreateAsync(AuthorForCreationDTO author)
        {
            var exist = await authorRepository.GetAsync(a => a.NickName.ToLower().Equals(author.NickName.ToLower()));

            if (exist is not null)
                throw new Exception("Author already exist");

            return await authorRepository.CreateAsync(new Author()
            {
                FirstName = author.FirsName,
                LastName = author.LastName,
                NickName = author.NickName.ToLower(),
                DateOfBirth = author.DateOfBirth,
                DateOfRegistration = DateTime.UtcNow
            });
        }

        public async Task<bool> DeleteAsync(Expression<Func<Author, bool>> predicate)
        {
            var exist = await authorRepository.GetAsync(predicate);

            if (exist is null)
                return false;

            await authorRepository.DeleteAsync(predicate);
            return true;
        }

        public IEnumerable<Author> GetAll(Expression<Func<Author, bool>> predicate = null)
           => predicate is null ? authorRepository.GetAll().Include(a => a.Photos).Include(a => a.Texts).AsEnumerable()
              : authorRepository.GetAll(predicate).Include(a => a.Photos).Include(a => a.Texts).AsEnumerable();

        public IEnumerable<Author> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Author, bool>> predicate = null)
            => authorRepository.GetAll(predicate).Include(a => a.Photos).Include(a => a.Texts).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        public async Task<Author> GetAsync(Expression<Func<Author, bool>> predicate)
            => await authorRepository.GetAsync(predicate);

        public async Task<Author> UpdateAsync(long id, AuthorForCreationDTO author)
        {
            var exist = await authorRepository.GetAsync(a => a.Id == id);

            if (exist is null)
                throw new Exception("Author not found");

            exist.FirstName = author.FirsName;
            exist.LastName = author.LastName;
            exist.NickName = author.NickName.ToLower();
            exist.DateOfBirth = author.DateOfBirth;

            return await authorRepository.UpdateAsync(exist);
        }
    }
}
