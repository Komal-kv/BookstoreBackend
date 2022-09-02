using CommonLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public WishListModel AddWishList(WishListModel wish, int UserId);
        public string DeleteWishList(int WishListId, int UserId);
        public List<WishListModel> GetWishList(int UserId);
    }
}
