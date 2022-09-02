using CommonLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public WishListModel AddWishList(WishListModel wish, int UserId);
        public string DeleteWishList(int WishListId, int UserId);
        public List<WishListModel> GetWishList(int UserId);
    }
}
