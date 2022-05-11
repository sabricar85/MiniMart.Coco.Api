using MiniMart.Coco.Api.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos.Responses
{
    public class DeletedProductResponse : Query
    {
        public ProductDto  product  { get; set; }
        public ApplyVoucherDto Voucher { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
