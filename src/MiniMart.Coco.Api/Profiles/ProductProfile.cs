using MediatR;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Profiles
{
    public class ProductProfile : IRequest<AddedProductsResponse>
    {
        public ProductProfile()
        {

        }
    }
}
