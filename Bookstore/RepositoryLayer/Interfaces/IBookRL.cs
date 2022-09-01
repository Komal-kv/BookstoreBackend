using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);
        public BookUpdateModel UpdateBook(BookUpdateModel bookUpdate);
        public string DeleteBook(int BookId);
        public BookPostModel GetBookById(int BookId);
        public List<GetAllBookModel> GetAllBooks();
    }
}
