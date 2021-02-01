using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    public class BillPackageAccount
    {
        public virtual Bill GetBill { get; set; }
        public virtual Package GetPackage { get; set; }
        public virtual Account GetAccount { get; set; }
    }
}
