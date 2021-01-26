using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime? Update_at { get; set; }
        public DateTime? Delete_at { get; set; }
        public virtual ICollection<Account> GetAccounts { get; set; }
        public virtual ICollection<Package> GetPackages { get; set; }
    }
}
