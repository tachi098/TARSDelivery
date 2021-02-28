using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must not be blank")]
        public string  Name { get; set; }
        [Required(ErrorMessage = "Address must not be blank")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone must not be blank")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Phone is invalid")]
        public string Phone { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime? Update_at { get; set; }
        public DateTime? Delete_at { get; set; }
        public virtual ICollection<Account> GetAccounts { get; set; }
        public virtual ICollection<Package> GetPackages { get; set; }
    }
}
