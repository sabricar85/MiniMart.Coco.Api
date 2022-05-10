using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class VoucherProductRestriction
    {
        public int ID { get; set; }
        
        [ForeignKey("VoucherID")]
        public int VoucherID { get; set; }
        public Voucher Voucher { get; set; }
        public int ProductID { get; set; }

    }
}
