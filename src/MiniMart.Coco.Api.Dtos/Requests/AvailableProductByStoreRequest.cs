using MediatR;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos.Requests
{
    public class AvailableProductByStoreRequest : IRequest<AvailableProductsResponse>
    {
        public int StoreID { get; set; }
    }
}
