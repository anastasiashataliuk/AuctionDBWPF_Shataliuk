using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALEF.Models
{
    [Table("tblUser")] // Map to the database table 'tblUser'
    public partial class TblUser
    {
        public int User_Id { get; set; } // Primary Key
        public string Name { get; set; } // User Name
        public string Email { get; set; } // User Password (hashed)
        public string Role { get; set; } // Role (e.g., Admin, User, etc.)

        // Navigation property for relationships, if needed in the future
        public virtual ICollection<TblAuction> Auctions { get; set; } = new List<TblAuction>();
    }
}
