using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration configuration;
        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("AddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName ", bookPostModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookPostModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", bookPostModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookPostModel.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookPostModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", bookPostModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", bookPostModel.Description);
                    cmd.Parameters.AddWithValue("@BookImage", bookPostModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookPostModel.BookQuantity);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return bookPostModel;
                    }
                    else
                    {
                        return null;
                    }
                }
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
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("DeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", BookId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Book deleted";
                    }
                    else
                    {
                        return "Failed to delete";
                    }
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public List<GetAllBookModel> GetAllBooks()
        {
            List<GetAllBookModel> books = new List<GetAllBookModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            books.Add(new GetAllBookModel
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                BookName = reader["BookName"].ToString(),
                                AuthorName = reader["AuthorName"].ToString(),
                                Rating = reader["Rating"].ToString(),
                                RatingCount = Convert.ToInt32(reader["RatingCount"]),
                                DiscountPrice = reader["DiscountPrice"].ToString(),
                                ActualPrice = reader["ActualPrice"].ToString(),
                                BookImage = reader["BookImage"].ToString(),
                                BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            });
                        }
                        con.Close();
                        return books;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BookPostModel GetBookById(int BookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("GetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", BookId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        BookPostModel model = new BookPostModel();
                        while (reader.Read())
                        {
                            BookId = Convert.ToInt32(reader["BookId"]);
                            model.BookName = reader["BookName"].ToString();
                            model.AuthorName = reader["AuthorName"].ToString();
                            model.Rating = reader["Rating"].ToString();
                            model.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                            model.DiscountPrice = reader["DiscountPrice"].ToString();
                            model.ActualPrice = reader["ActualPrice"].ToString();
                            model.BookImage = reader["BookImage"].ToString();
                            model.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);

                        }
                        con.Close();
                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }
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
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("UpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", bookUpdate.BookId);
                    cmd.Parameters.AddWithValue("@BookName ", bookUpdate.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookUpdate.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", bookUpdate.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookUpdate.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookUpdate.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", bookUpdate.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", bookUpdate.Description);
                    cmd.Parameters.AddWithValue("@BookImage", bookUpdate.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookUpdate.BookQuantity);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return bookUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
