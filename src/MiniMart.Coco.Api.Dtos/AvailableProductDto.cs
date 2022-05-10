using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos
{
    public  class AvailableProductDto
    {
 
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }

    }
}
