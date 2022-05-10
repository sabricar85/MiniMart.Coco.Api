using MediatR;
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
    public class AvailableProductHandler : IRequestHandler<AvailableProductsRequest, AvailableProductsResponse>
    {
        private readonly IProductRepository productRepository;
        public AvailableProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<AvailableProductsResponse> Handle(AvailableProductsRequest request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetAvailableProducts(null);
            return result;
        }
    }
}
