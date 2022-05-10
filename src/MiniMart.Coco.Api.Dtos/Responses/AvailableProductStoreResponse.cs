using MiniMart.Coco.Api.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMart.Coco.Api.Dtos.Responses
{
    public class AvailableProductStoreResponse : QueryAvailableProductDto
    {
        public List<AvailableProductDto> AvailableProducts { get; set; }
    }
}
