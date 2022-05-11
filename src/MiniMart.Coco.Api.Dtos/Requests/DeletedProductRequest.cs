using MediatR;
using MiniMart.Coco.Api.Dtos.Base;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos.Requests
{
    public class DeletedProductRequest : Query, IRequest<DeletedProductResponse>
    {
        public QueryProductDto ProductDelete { get; set; }
        public decimal TotalPriceProducts { get; set; }
        public decimal TotalPrice { get; set; }


    }
}
