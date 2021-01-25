using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Fullname must not be blank")]
        public string FullName { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{6,20}@[a-z]{2,5}(.[a-z]{2,5}){1,2}$", ErrorMessage = "Email is invalid...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must not be blank")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address must not be blank")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone must not be blank")]
        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Avartar { get; set; }

        [ForeignKey("BranchId")]
        public int? BranchId { get; set; }

        public string Code { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime? Update_at { get; set; }

        public DateTime? Delete_at { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }
        public virtual Role Role { get; set; }
    }
}
