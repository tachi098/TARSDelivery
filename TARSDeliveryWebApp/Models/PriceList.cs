using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("PriceList")]
    public class PriceList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must not be blank")]
        public string  Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price must not be blank")]
        public double PriceDistance { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price must not be blank")]
        public double PriceWeight { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime? Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
    }
}
