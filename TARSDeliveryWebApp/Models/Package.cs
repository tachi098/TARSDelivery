using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title must not be blank")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Title must 5 to 500 char")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Name from must not be blank")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name from must 5 to 20 char")]
        public string NameFrom { get; set; }

        [Required(ErrorMessage = "Email must not be blank")]
        [RegularExpression(@"^\w{1,}@\w{2,}(\.\w{2,}){1,2}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address origin must not be blank")]
        public string AddressFrom { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "ZipCode must not be blank")]
        [RegularExpression(@"^[\d]+$", ErrorMessage = "ZipCode is invalid")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Name must not be blank")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name must 5 to 20 char")]
        public string NameTo { get; set; }

        [Required(ErrorMessage = "Address destination must not be blank")]
        public string AddressTo { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }

        [Required(ErrorMessage = "Name to must not be blank")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Name to must 5 to 1000 char")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
        public int Status { get; set; }

        [ForeignKey("BranchId")]
        public int? BranchId { get; set; }

        [ForeignKey("AccountId")]
        public int? AccountId { get; set; }
        public string PriceListName { get; set; }

        public DateTime Create_at { get; set; }
        public DateTime? Update_at { get; set; }
        public DateTime? Delete_at { get; set; }
        public virtual Bill GetBill { get; set; }
        public virtual Account GetAccount { get; set; }
        public virtual Branch GetBranch { get; set; }
    }
}
