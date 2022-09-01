using BusinessLayer.Interfaces;
using CommonLayer.CartModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bookstore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("AddToCart")]
        public ActionResult AddtoCart(CartModel cart, int UserId)
        {
            try
            {
                var res = this.cartBL.AddCart(cart, UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Book added to the cart successfully", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to add book in cart", data = res });
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("UpdateCart")]
        public ActionResult updateCart(int cartId, CartModel cart, int UserId)
        {
            try
            {
                var res = this.cartBL.UpdateCart(cartId, cart, UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Updated cart successfully", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed update cart", data = res });

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("GetAllBookInCart")]
        public ActionResult GetAllBookInCart(int UserId)
        {
            try
            {
                //var currentUser = HttpContext.User;
                //int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
              
                var res = this.cartBL.GetAllCart(UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Getting all books from cart", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to get cart Items", data = res });
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [HttpDelete("DeleteCartItem")]
        public ActionResult RemoveCart(int cartId)
        {
            try
            {
                var res = this.cartBL.RemoveCart(cartId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Deleting books from cart", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to delete cart Items", data = res });
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }
    }
}
