using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    public class BillPackageAccount
    {
        public virtual Bill GetBill { get; set; }
        public virtual Package GetPackage { get; set; }
        public virtual Account GetAccount { get; set; }
    }
}
