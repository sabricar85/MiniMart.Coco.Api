using MediatR;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MiniMart.Coco.Api.Services.Store
{
    public class StoreHandler : IRequestHandler<availableStoreRequest, AvailableStoreResponse>
    {
        private readonly IStoreRepository storeRepository;
        public StoreHandler(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        public async Task<AvailableStoreResponse> Handle(availableStoreRequest request, CancellationToken cancellationToken)
        {
            var result = await storeRepository.GetAvailableStore(request.DateTimeQuery);
            return result;
        }
    }
}
