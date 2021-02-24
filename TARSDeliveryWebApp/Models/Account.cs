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

        [Required(ErrorMessage = "Email must not be blank")]
        [RegularExpression(@"^\w{1,}@\w{2,}(\.\w{2,}){1,2}$", ErrorMessage = "Email is invalid...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must not be blank")]
        [MinLength(6, ErrorMessage = "Password greater than 5 character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address must not be blank")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone must not be blank")]
        [RegularExpression(@"^\d{9,11}$", ErrorMessage = "Phone is invalid")]
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
        public virtual ICollection<Bill> GetBills { get; set; }
        public virtual ICollection<Package> GetPackages { get; set; }
        public virtual Branch GetBranch { get; set; }
        public virtual Role GetRole { get; set; }
    }
}
