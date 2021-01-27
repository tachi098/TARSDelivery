using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    public class BillPackage
    {
        /* Bill */
        public int BillId { get; set; }
        public int? BillAccountId { get; set; }
        public int BillPackageId { get; set; }
        public DateTime BillCreate_at { get; set; }
        public DateTime? BillUpdate_at { get; set; }
        public DateTime? BillDelete_at { get; set; }

        /* Package */
        public int PackageId { get; set; }
        public string PackageTitle { get; set; }
        public string PackageNameFrom { get; set; }
        public string PackageEmail { get; set; }
        public string PackageAddressFrom { get; set; }
        public string PackageType { get; set; }
        public string PackageZipCode { get; set; }
        public string PackageNameTo { get; set; }
        public string PackageAddressTo { get; set; }
        public double PackageWeight { get; set; }
        public double PackageDistance { get; set; }
        public string PackageMessage { get; set; }
        public double PackageTotalPrice { get; set; }
        public int PackageStatus { get; set; }
        public int? PackageBranchId { get; set; }
        public int? PackageAccountId { get; set; }
        public DateTime PackageCreate_at { get; set; }
        public DateTime? PackageUpdate_at { get; set; }
        public DateTime? PackageDelete_at { get; set; }

    }
}
