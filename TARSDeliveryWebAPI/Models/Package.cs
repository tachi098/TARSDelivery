using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string NameFrom { get; set; }
        public string Email { get; set; }
        public string AddressFrom { get; set; }
        public string Type { get; set; }
        public string ZipCode { get; set; }
        public string NameTo { get; set; }
        public string AddressTo { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }
        public string Message { get; set; }
        public double TotalPrice { get; set; }
        public int Status { get; set; }
        public int? BranchId { get; set; }
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
