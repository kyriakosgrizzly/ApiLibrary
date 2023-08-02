using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Services.DTO;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookrepo;
        private readonly IGenericRepository<Author> _authorrepo;

        public BookService(IGenericRepository<Book> bookrepo, IGenericRepository<Author> author)
        {
            _bookrepo = bookrepo;
            _authorrepo = author;
        }
        public async Task<List<DisplayBook>> GetAllTitles(PagingParameters p,FilterSettings? f, string filtertitle, string filtername)
        {
            var query = _bookrepo.Query().AsNoTracking().Include(x => x.AuthorList);
            switch (f)
            {
                case FilterSettings.Title:
                    query = query.Where(b => b.Title.ToLower().Contains(filtertitle.ToLower())).Include(x => x.AuthorList);
                    break;
                case FilterSettings.AuthorName:
                    query = query.Where(b =>
                    b.AuthorList.Any(author =>
                    author.FirstName.Contains(filtername, StringComparison.OrdinalIgnoreCase) ||
                    author.LastName.Contains(filtername, StringComparison.OrdinalIgnoreCase))).Include(x => x.AuthorList);
                    break;
                default:
                    break;
            }

            return await query.Select(x => new DisplayBook(x.Id, x.Title, x.AuthorList, x.Image, x.Description)).Skip((p.PageNumber - 1) * p.PageSize)
                        .Take(p.PageSize).ToListAsync();


        }
        public async Task<List<Book>> GetAllBookDetails(PagingParameters p)
        {

            var books = await _bookrepo.Query().AsNoTracking().Skip((p.PageNumber - 1) * p.PageSize)
                .Take(p.PageSize).ToListAsync();
            return books.ToList();
        }
        public async Task<Book> GetBookDetailsById(int id)
        {

            var book = await _bookrepo.Query().Where(x => x.Id == id).Include(x => x.AuthorList).SingleOrDefaultAsync();
            return book;
        }
        public async Task<List<DisplayBook>> GetBookDetailsByName(string title)
        {
            var titles = await _bookrepo.Query().Where(x => x.Title == title).Select(x => new DisplayBook(x.Id, x.Title, x.AuthorList, x.Image, x.Description)).ToListAsync();
            return titles;
        }

        public async Task<Book> EditBookDetails(Book book)
        {
            var _book = await _bookrepo.GetSingleOrDefaultAsync(book.Id);
            if (_book == null) throw new Exception("Book not found");
            _book.Title = book.Title;
            _book.Description = book.Description;
            _book.Image = book.Image;
            _book.Rating = book.Rating;
            _book.PublishmentDate = book.PublishmentDate;
            _book.IsTaken = book.IsTaken;
            _book.AuthorList = book.AuthorList;


            await _bookrepo.SaveChangesAsync();
            return _book;
        }
        public async Task<int> DeleteBook(int id)
        {
            await _bookrepo.DeleteAsync(id);
            await _bookrepo.SaveChangesAsync();
            return id;
        }
        public async Task<Book> AddBook(Book book)
        {
            await _bookrepo.AddAsync(book);
            await _bookrepo.SaveChangesAsync();
            return book;
        }
        public async Task<List<Author>> GetAllAuthors(PagingParameters p)
        {

            var authors = await _authorrepo.Query().AsNoTracking().Skip((p.PageNumber - 1) * p.PageSize)
                .Take(p.PageSize).ToListAsync();
            return authors;

        }
    }
}
