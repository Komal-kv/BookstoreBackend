using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            try
            {
                return this.bookRL.AddBook(bookPostModel);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteBook(int BookId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public List<GetAllBookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public BookPostModel GetBookById(int BookId)
        {
            try
            {
                return this.bookRL.GetBookById(BookId);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public BookUpdateModel UpdateBook(BookUpdateModel bookUpdate)
        {
            try
            {
                return this.bookRL.UpdateBook(bookUpdate);
            }
            catch(Exception e)
            {

                throw e;
            }
        }
    }
}
