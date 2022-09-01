using BusinessLayer.Interfaces;
using CommonLayer.CartModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public CartModel AddCart(CartModel cart, int UserId)
        {
            try
            {
                return this.cartRL.AddCart(cart, UserId);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public List<CartPostModel> GetAllCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllCart(UserId);
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
                return this.cartRL.RemoveCart(CartId);
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
                return this.cartRL.UpdateCart(CartId, cart, UserId);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }
    }
}
