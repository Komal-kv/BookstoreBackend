using BusinessLayer.Interfaces;
using CommonLayer.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bookstore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishBL;
        public WishListController(IWishListBL wishBL)
        {
            this.wishBL = wishBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddWishList")]
        public ActionResult AddWishList(WishListModel wishList)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var list = this.wishBL.GetWishList(UserId);

                if (list != null)
                {
                    return this.Ok(new { success = true, message = "Added to your WishList", data = list });
                }
                return this.BadRequest(new { success = false, message = "Failed to add in WishList", data = list });

            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteWishList")]
        public ActionResult RemoveWishList(int WishListId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var list = this.wishBL.DeleteWishList(WishListId, UserId);

                if (list != null)
                {
                    return this.Ok(new { success = true, message = "Deleting your WishList", data = list });
                }
                return this.BadRequest(new { success = false, message = "Failed to delete WishList", data = list });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishList")]
        public ActionResult GetWishList()
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var list = this.wishBL.GetWishList(UserId);

                if (list != null)
                {
                    return this.Ok(new { success = true, message = "Getting your WishList", data = list });
                }
                return this.BadRequest(new { success = false, message = "Failed to get WishList", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
