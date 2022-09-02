using CommonLayer.Model;
using CommonLayer.WishList;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        private readonly IConfiguration configuration;
        public WishListRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public WishListModel AddWishList(WishListModel wish, int UserId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpAddWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", wish.bookId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return wish;
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

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpDeleteWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WishListId ", WishListId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Delete WishList";
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

        public List<ViewWishList> GetWishList(int UserId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpGetWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<ViewWishList> cart = new List<ViewWishList>();
                        WishListModel model = new WishListModel();


                        while (reader.Read())
                        {
                            BookUpdateModel bookModel = new BookUpdateModel();
                            bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.DiscountPrice = reader["DiscountPrice"].ToString();
                            bookModel.ActualPrice = reader["ActualPrice"].ToString();
                            bookModel.BookImage = reader["BookImage"].ToString();
                            model.WishlistId = Convert.ToInt32(reader["WishListId"]);
                            model.bookId = Convert.ToInt32(reader["BookId"]);
                            model.userId = Convert.ToInt32(reader["UserId"]);

                            //cart.Add(model);
                        }
                        con.Close();
                        return cart;
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
