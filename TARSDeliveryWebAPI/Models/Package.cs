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

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "NameTo must not be blank")]
        public string NameTo { get; set; }

        [Required(ErrorMessage = "NameFrom must not be blank")]
        public string NameFrom { get; set; }

        [Required(ErrorMessage = "Address must not be blank")]
        public string AddressTo { get; set; }

        [Required(ErrorMessage = "Address must not be blank")]
        public string AddressFrom { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{6,20}@[a-z]{2,5}(.[a-z]{2,5}){1,2}$", ErrorMessage = "Email is invalid...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Weight must not be blank")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Distance must not be blank")]
        public double Distance { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
        public virtual ICollection<BillDetail> billDetails { get; set; }
    }
}
