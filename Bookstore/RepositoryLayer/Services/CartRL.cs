using CommonLayer.CartModel;
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
    public class CartRL : ICartRL
    {
        private readonly IConfiguration configuration;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CartModel AddCart(CartModel cart, int UserId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpAddCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", cart.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity ", cart.BookQuantity);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return cart;
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

        public List<CartPostModel> GetAllCart()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpGetAllBookInCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<CartPostModel> carts = new List<CartPostModel>();
                        CartPostModel model = new CartPostModel();
                        while (reader.Read())
                        {
                            BookUpdateModel updateModel = new BookUpdateModel();
                            updateModel.BookId = Convert.ToInt32(reader["BookId"]);
                            updateModel.BookName = reader["BookName"].ToString();
                            updateModel.AuthorName = reader["AuthorName"].ToString();
                            updateModel.DiscountPrice = reader["DiscountPrice"].ToString();
                            updateModel.ActualPrice = reader["ActualPrice"].ToString();
                            updateModel.BookImage = reader["BookImage"].ToString();
                            model.CartId = Convert.ToInt32(reader["CartId"]);
                            model.UserId = Convert.ToInt32(reader["UserId"]);
                            model.BookId = Convert.ToInt32(reader["BookId"]);
                            model.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                            model.bookUpdateModel = updateModel;
                            carts.Add(model);
                        }
                        con.Close();
                        return carts;
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

        public string RemoveCart(int CartId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpRemoveCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId ", CartId);


                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Remove Cart";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public CartModel UpdateCart(int CartId, CartModel cart, int UserId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpUpdateCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId ", CartId);
                    cmd.Parameters.AddWithValue("@BookQuantity ", cart.BookQuantity);
                    cmd.Parameters.AddWithValue("@BookId ", cart.BookId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return cart;
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
    }
}
