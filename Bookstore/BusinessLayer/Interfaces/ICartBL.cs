using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public CartModel AddCart(CartModel cart, int UserId);
        public string RemoveCart(int CartId);
        public CartModel UpdateCart(int CartId, CartModel cart, int UserId);
        public List<CartPostModel> GetAllCart();
    }
}
