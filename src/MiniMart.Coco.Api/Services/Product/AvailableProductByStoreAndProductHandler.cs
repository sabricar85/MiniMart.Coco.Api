using MediatR;
using MiniMart.Coco.Api.Dtos;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Services.Product
{
    public class AvailableProductByStoreAndProductHandler : IRequestHandler<AvailableProductByStoreAndProductRequest, AvailableProductsResponse>
    {
        private readonly IProductRepository productRepository;
        public AvailableProductByStoreAndProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<AvailableProductsResponse> Handle(AvailableProductByStoreAndProductRequest request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetAvailableProductByStoreAndProduct(request.StoreID, request.ProductID);
            return result;
        }
    }
}
