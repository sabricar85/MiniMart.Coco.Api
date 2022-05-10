using MiniMart.Coco.Api.Data;
using MiniMart.Coco.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Dtos;

namespace MiniMart.Coco.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext dbContext; 
        
        public ProductRepository(DataContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<AvailableProductsResponse> GetAvailableProducts(int? StoreID)
        {
            AvailableProductsResponse AvailableProductsResponse = new AvailableProductsResponse();
 
            var products = await (from prod in dbContext.Products
                                   join stock in dbContext.Stocks on prod.ID equals stock.ProductID
                                   where (StoreID == stock.StoreID || (StoreID== null) )  
                                   && stock.ProductCount>0
                                  group stock by new{ stock.ProductID, prod.Name} into  groupProduct
                                  select new 
                                            {
                                             ProductID = groupProduct.Key.ProductID,
                                             ProductName = groupProduct.Key.Name,
                                             Quantity = groupProduct.Sum(p=>p.ProductCount)
                                  }).ToListAsync();

            AvailableProductsResponse.AvailableProducts = new List<AvailableProductDto>();
            foreach (var item in products)
            {
                AvailableProductDto AvailableProduct =  new AvailableProductDto();

                AvailableProduct.ProductID = item.ProductID;
                AvailableProduct.ProductName = item.ProductName.ToString();
                AvailableProduct.TotalQuantity = item.Quantity;
                AvailableProductsResponse.AvailableProducts.Add(AvailableProduct);
            }

            return AvailableProductsResponse;
        }


        
       public async Task<AvailableProductsResponse> GetAvailableProductByStoreAndProduct(int StoreID, int ProductID)
        {
            AvailableProductsResponse AvailableProductsResponse = new AvailableProductsResponse();

            var products = await (from prod in dbContext.Products
                                  join stock in dbContext.Stocks on prod.ID equals stock.ProductID
                                  where (StoreID == stock.StoreID && (prod.ID == ProductID)) && stock.ProductCount > 0
                                  group stock by new { stock.ProductID, prod.Name, prod.Price} into groupProduct
                                  select new
                                  {
                                      ProductID = groupProduct.Key.ProductID,
                                      ProductName = groupProduct.Key.Name,
                                      Quantity = groupProduct.Sum(p => p.ProductCount),
                                  }).ToListAsync();

            AvailableProductsResponse.AvailableProducts = new List<AvailableProductDto>();
            foreach (var item in products)
            {
                AvailableProductDto AvailableProduct = new AvailableProductDto();

                AvailableProduct.ProductID = item.ProductID;
                AvailableProduct.ProductName = item.ProductName.ToString();
                AvailableProduct.TotalQuantity = item.Quantity;
                AvailableProductsResponse.AvailableProducts.Add(AvailableProduct);
            }

            return AvailableProductsResponse;
        }
    }
}
