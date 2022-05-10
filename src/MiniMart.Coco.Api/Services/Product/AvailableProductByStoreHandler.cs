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
    public class AvailableProductByStoreHandler: IRequestHandler<AvailableProductByStoreRequest, AvailableProductsResponse>
    {
        private readonly IProductRepository productRepository;
        public AvailableProductByStoreHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<AvailableProductsResponse> Handle(AvailableProductByStoreRequest request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetAvailableProducts(request.StoreID);
            return result;
        }
    }
}
