using MediatR;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MiniMart.Coco.Api.Services.VirtualCart
{
    public class VirtualCartDeleteProductHandler : IRequestHandler<DeletedProductRequest, DeletedProductResponse>
    {
        private readonly IVirtualCartRepository virtualCartRepository;
        public VirtualCartDeleteProductHandler(IVirtualCartRepository virtualCartRepository)
        {
            this.virtualCartRepository = virtualCartRepository ?? throw new ArgumentNullException(nameof(virtualCartRepository));
        }
        public async Task<DeletedProductResponse> Handle(DeletedProductRequest request, CancellationToken cancellationToken)
        {
            var result = await virtualCartRepository.DeleteProduct(request);
            return result;
        }

    }
}
