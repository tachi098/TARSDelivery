using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("IdBill")]
        public int IdBill { get; set; }

        [ForeignKey("IdPackage")]
        public int IdPackage { get; set; }
    }
}
