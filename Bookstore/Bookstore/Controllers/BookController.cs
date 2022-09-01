using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookstore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [HttpPost("AddBook")]
        public ActionResult AddBook(BookPostModel bookPostModel)
        {
            try
            {
                var book = this.bookBL.AddBook(bookPostModel);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book added successfully", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to add book", data = book });
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook(BookUpdateModel updateModel)
        {
            try
            {
                var book = this.bookBL.UpdateBook(updateModel);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book updated successfully", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to update a book", data = book });
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook( int BookId)
        {
            try
            {
                var book = this.bookBL.DeleteBook(BookId);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book is deleted", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to delete a book", data = book });
            }
            catch
            {

                throw;
            }
        }

        [HttpGet("GetBookById")]
        public ActionResult GetBookById(int BookId)
        {
            try
            {
                var book = this.bookBL.GetBookById(BookId);

                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Getting your book", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to get your book", data = book });
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var book = this.bookBL.GetAllBooks();

                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Getting all of your books", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to get your all books", data = book });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
