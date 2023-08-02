using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.DTO;

namespace Services.BookServices
{
    public interface IBookService
    {
        Task<List<DisplayBook>> GetAllTitles(PagingParameters p, FilterSettings? f, string filtertitle, string filtername);

        Task<Book> GetBookDetailsById(int id);

        Task<List<DisplayBook>> GetBookDetailsByName(string title);

        Task<Book> EditBookDetails(Book book);

        Task<int> DeleteBook(int id);

        Task<List<Book>> GetAllBookDetails(PagingParameters p);

        Task<Book> AddBook(Book book);

        Task<List<Author>> GetAllAuthors(PagingParameters p);

    }
}
