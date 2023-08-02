using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthorServices
{
    public interface IAuthorService
    {
        public Task<List<Author>> GetAllAuthors(PagingParameters p);
        public Task<Author> GetAuthorDetailsById(int id);
        public Task<Author> GetAuthorDetailsByNameAndLastname(string name,string lastname);
        public Task<Author> EditAuthorDetails(Author author);
        //public Task<int> DeleteAuthor(int id);



    }
}
