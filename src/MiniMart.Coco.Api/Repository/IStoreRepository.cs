using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Repository
{
    public interface IStoreRepository
    {
        Task<AvailableStoreResponse> GetAvailableStore(DateTime dateTimeQuery);
    }
}
