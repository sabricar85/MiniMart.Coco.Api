using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
     
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public decimal Price { get; set; }

        
    

    }
}
