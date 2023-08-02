using Infrastructure.Entities;

namespace Services
{
    public class DTO
    {
        public class DisplayBook
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public List<Author> AuthorList { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }

            public DisplayBook(int id,string title, List<Author> authorList, string image, string description)
            {
                Id = id;
                Title = title;
                AuthorList = authorList;
                Image = image;
                Description = description;
            }
        }
    }
}