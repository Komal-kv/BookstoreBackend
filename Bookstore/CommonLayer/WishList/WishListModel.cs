using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.WishList
{
    public class WishListModel
    {
        //public int WishlistId { get; set; }
        //public int userId { get; set; }
        //public int bookId { get; set; }
        //public BookUpdateModel book { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        public int WishListId { get; set; }

        public BookUpdateModel book { get; set; }
    }
}
