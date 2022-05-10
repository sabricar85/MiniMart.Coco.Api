using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Repository
{
    public interface IProductRepository
    {
        Task<AvailableProductsResponse> GetAvailableProducts(int? storeID);
        Task<AvailableProductsResponse> GetAvailableProductByStoreAndProduct(int storeID, int productID);
        
    }
}
