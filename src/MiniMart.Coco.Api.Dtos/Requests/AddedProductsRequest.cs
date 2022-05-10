using MediatR;
using MiniMart.Coco.Api.Dtos.Base;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
 

namespace MiniMart.Coco.Api.Dtos.Requests
{
    public class AddedProductsRequest : Query, IRequest<AddedProductsResponse>
    {
     
        public List<QueryProductDto> Products { get; set; }



 
    }
}
