using BusinessLayer.Interfaces;
using CommonLayer.WishList;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishRL;
        public WishListBL(IWishListRL wishRL)
        {
            this.wishRL = wishRL;
        }

        public WishListModel AddWishList(WishListModel wish, int UserId)
        {
            try
            {
                return wishRL.AddWishList(wish, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                return wishRL.DeleteWishList(WishListId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<WishListModel> GetWishList(int UserId)
        {
            try
            {
                return wishRL.GetWishList(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
