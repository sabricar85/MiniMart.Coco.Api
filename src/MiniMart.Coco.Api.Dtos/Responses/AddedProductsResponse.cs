using MiniMart.Coco.Api.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos.Responses
{
    public class AddedProductsResponse : Query
    {      
        public List<ProductDto> Products { get; set; }
        public decimal TotalPrice { get; set; }

        public ApplyVoucherDto Voucher { get; set; }


    }
}
