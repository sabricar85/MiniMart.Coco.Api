using MediatR;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Services

{
    public class VirtualCartAddProductHandler : IRequestHandler<AddedProductsRequest, AddedProductsResponse>
    {
        private readonly IVirtualCartRepository virtualCartRepository;
        public VirtualCartAddProductHandler(IVirtualCartRepository virtualCartRepository)
        {
            this.virtualCartRepository = virtualCartRepository ?? throw new ArgumentNullException(nameof(virtualCartRepository));
        }
        public async Task<AddedProductsResponse> Handle(AddedProductsRequest request, CancellationToken cancellationToken)
        {
            var result = await virtualCartRepository.AddProducts(request); 
            return  result;
        }
    }
}
