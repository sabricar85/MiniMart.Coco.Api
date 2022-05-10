using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos
{
    public class PriceDto
    {
        public decimal OrginalPrice { get; set; }
        public decimal? PriceWithDiscount { get; set; }


    }
}
