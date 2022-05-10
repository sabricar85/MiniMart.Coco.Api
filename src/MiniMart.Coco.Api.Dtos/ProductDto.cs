using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos
{
    public class ProductDto
    {
        public bool Added { get; set; }
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public PriceDto Price { get; set; }
        public decimal TotalPriceProducts { get; set; }

    }
}
