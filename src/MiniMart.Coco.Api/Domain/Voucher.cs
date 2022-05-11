using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class Voucher
    {
        public int ID { get; set; }
        public string Code { get; set; }
     

        [ForeignKey("Store")]
        public int StoreID { get; set; }
        public Store Store { get; set; }


        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int? DayNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        [ForeignKey("Discount")]
        public int DiscountID { get; set; }
        public Discount Discount { get; set; }

  
    }
}
