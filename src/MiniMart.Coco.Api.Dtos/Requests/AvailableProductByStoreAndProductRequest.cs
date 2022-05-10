using MediatR;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;
namespace MiniMart.Coco.Api.Dtos
{
    public class AvailableProductByStoreAndProductRequest: IRequest<AvailableProductsResponse>
    {
        public int StoreID { get; set; }
        public int ProductID { get; set; }
    }
}
