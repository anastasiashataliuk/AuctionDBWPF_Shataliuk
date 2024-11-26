using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALEF.Models
{
    [Table("tblAuctionProduct")]
    public partial class TblAuctionProduct
    {
        public int AuctionProduct_Id { get; set; } // Унікальний ідентифікатор для зв'язку аукціон-продукт

        public int Auction_Id { get; set; } // Ідентифікатор аукціону

        public int Product_Id { get; set; } // Ідентифікатор продукту

        public decimal Quantity { get; set; } // Кількість продукту в аукціоні (додано)

        // Віртуальні властивості для навігації до аукціону та продукту
        [ForeignKey("Auction_Id")]
        public virtual TblAuction Auction { get; set; }
        [ForeignKey("Product_Id")]
        public virtual TblProduct Product { get; set; }
    }
}
