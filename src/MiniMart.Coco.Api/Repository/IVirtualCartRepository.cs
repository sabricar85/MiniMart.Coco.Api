using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Repository
{
    public interface IVirtualCartRepository
    {
        Task<AddedProductsResponse> AddProducts(AddedProductsRequest AddedProductsRequest);
        Task<DeletedProductResponse> DeleteProduct(DeletedProductRequest DeletedProductRequest);
    }
}
