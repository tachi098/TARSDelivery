using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "TotalPrice must not be blank")]
        public double TotalPrice { get; set; }
        
        [ForeignKey("BranchId")]
        public int BranchId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public int Status { get; set; }
        public virtual ICollection<BillDetail> billDetails { get; set; }
    }
}
