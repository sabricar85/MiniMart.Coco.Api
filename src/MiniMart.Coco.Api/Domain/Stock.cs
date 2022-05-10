using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class Stock
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
       
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        

        [ForeignKey("Store")]
        public int StoreID  { get; set; }
        public int ProductCount { get; set; }
    }
}
