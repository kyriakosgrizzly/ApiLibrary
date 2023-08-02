using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {

        private readonly IGenericRepository<Author> _authorrepo;

        public AuthorService( IGenericRepository<Author> author)
        {
            _authorrepo = author;
        }
        public async Task<List<Author>> GetAllAuthors(PagingParameters p)
        {
            var authors = await _authorrepo.Query().AsNoTracking().Skip((p.PageNumber - 1) * p.PageSize)
                 .Take(p.PageSize).ToListAsync();
            return authors.ToList();
        }
        public async Task<Author> GetAuthorDetailsById(int id)
        {

            var author = await _authorrepo.GetSingleOrDefaultAsync(id);
            return author;
        }
        public async Task<Author> GetAuthorDetailsByNameAndLastname(string name,string lastname)
        {
            var author = await _authorrepo.Query().Where(x => x.FirstName == name && x.LastName == lastname).SingleOrDefaultAsync();
            return author;
        }
        public async Task<Author> EditAuthorDetails(Author author)
        {
            var _author = await _authorrepo.GetSingleOrDefaultAsync(author.Id);
            if (_author == null) throw new Exception("Author not found");
            _author.FirstName = author.FirstName;
            _author.LastName = author.LastName;
            _author.Birthdate = author.Birthdate;


            await _authorrepo.SaveChangesAsync();
            return _author;
        }

    }
}
