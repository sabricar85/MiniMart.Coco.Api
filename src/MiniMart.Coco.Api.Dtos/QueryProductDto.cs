using MediatR;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos
{
    public class QueryProductDto  
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
    }
}
