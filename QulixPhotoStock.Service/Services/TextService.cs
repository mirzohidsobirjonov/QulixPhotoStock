using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Data.IRepositories;
using QulixPhotoStock.Data.Repositories;
using QulixPhotoStock.Domain.Entities.Texts;
using QulixPhotoStock.Service.DTOs.TextDTOs;
using QulixPhotoStock.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QulixPhotoStock.Service.Services
{
    public class TextService : ITextService
    {
        private readonly ITextRepository textRepository;
        private readonly IAuthorRepository authorRepository;

        public TextService()
        {
            textRepository = new TextRepository();
            authorRepository = new AuthorRepository();
        }

        public async Task<TextForView> CreateAsync(TextForCreationDTO text)
        {
            var existAuthor = await authorRepository.GetAsync(a => a.Id == text.AuthorId);
            var existText = await textRepository.GetAsync(t => t.Name.Equals(text.Name));

            if (existText is not null)
                throw new Exception("Text already exist");

            if (existAuthor is null)
                throw new Exception("Author not found");

            var returnedText = await textRepository.CreateAsync(new Text
            {
                Name = text.Name,
                Content = text.Text,
                Cost = text.Cost,
                DateOfRegistration = DateTime.UtcNow,
                AuthorId = text.AuthorId
            });

            var textWithAuthor = await textRepository.GetAsync(t => t.Id == returnedText.Id);

            return Mapper(textWithAuthor);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Text, bool>> predicate)
        {
            var exist = await textRepository.GetAsync(predicate);

            if (exist is null)
                return false;

            await textRepository.DeleteAsync(predicate);
            return true;
        }

        public ICollection<TextForView> GetAll(Expression<Func<Text, bool>> predicate = null)
        {
            ICollection<TextForView> texts = new List<TextForView>();

            if (predicate is null)   
                foreach (var text in textRepository.GetAll().Include(t => t.Author).AsEnumerable())
                    texts.Add(Mapper(text));
            else
                foreach (var text in textRepository.GetAll(predicate).Include(t => t.Author).AsEnumerable())
                    texts.Add(Mapper(text));

            return texts;
        }

        public IEnumerable<TextForView> GetAllPagination(int pageIndex, int pageSize, Expression<Func<Text, bool>> predicate = null)
        {
            ICollection<TextForView> texts = new List<TextForView>();

            if (predicate is null)
                foreach (var text in textRepository.GetAll().Include(t => t.Author).AsEnumerable())
                    texts.Add(Mapper(text));
            else
                foreach (var text in textRepository.GetAll(predicate).Include(t => t.Author).AsEnumerable())
                    texts.Add(Mapper(text));

            return texts.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task<TextForView> GetAsync(Expression<Func<Text, bool>> predicate)
            => Mapper(await textRepository.GetAsync(predicate));

        public async Task<TextForView> UpdateAsync(long id, TextForCreationDTO text)
        {
            var existText = await textRepository.GetAsync(t => t.Id == id);
            var existAuthor = await authorRepository.GetAsync(a => a.Id == text.AuthorId);

            if (existText is null)
                throw new Exception("Text not found");

            if (existAuthor is null)
                throw new Exception("Author not found");

            existText.Name = text.Name;
            existText.Cost = text.Cost;
            existText.Content = text.Text;
            existText.AuthorId = text.AuthorId;

            return Mapper(await textRepository.UpdateAsync(existText));
        }

        private TextForView Mapper(Text text)
            => text is not null ? new TextForView()
            {
                Id = text.Id,
                Name = text.Name,
                Cost = text.Cost,
                Text = text.Content,
                NumberOfSales = text.NumberOfSales,
                DateOfCreation = text.DateOfRegistration,
                NameOfAuthor = text.Author.FirstName,
                NickNameOfAuthor = text.Author.NickName
            } : null;
    }
}
