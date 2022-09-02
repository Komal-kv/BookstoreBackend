using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.WishList
{
    public class WishListModel
    {
        public int WishlistId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public BookUpdateModel book { get; set; }
    }
}
