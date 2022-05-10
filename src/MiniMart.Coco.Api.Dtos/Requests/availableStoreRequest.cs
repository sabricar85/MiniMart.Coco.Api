using MediatR;
using MiniMart.Coco.Api.Dtos.Base;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
namespace MiniMart.Coco.Api.Dtos.Requests
{
    public class availableStoreRequest :  IRequest<AvailableStoreResponse>
    {
        public DateTime DateTimeQuery { get; set; }

        public availableStoreRequest()
        {
            DateTimeQuery = DateTime.Now;
        }

    }
}
