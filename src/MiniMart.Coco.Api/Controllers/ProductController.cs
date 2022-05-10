using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Coco.Api.Dtos;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;

namespace MiniMart.Coco.Api.Controllers
{
 
    public class ProductController :  ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        /// <summary>Available Product,across stores</summary>
        /// <remarks>
        /// </remarks>
        /// <param></param>
        /// <returns>/ Available Product</returns>
        [HttpGet]
        [Route("api/AvailableProduct")]
        public async Task<ActionResult<AvailableProductsResponse>> getAvailableProduct()
        {
            AvailableProductsRequest query = new AvailableProductsRequest();
            var response = await this.mediator.Send(query);
            return response;
        }
        /// <summary>
        /// Available ProductS By Store
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="Store"></param>
        /// <returns> Available Product</returns>
        [HttpGet]
        [Route("api/AvailableProductSByStore")]
        public async Task<ActionResult<AvailableProductsResponse>> getAvailableProducts([FromQuery] AvailableProductByStoreRequest Store)
        {
            var response = await this.mediator.Send(Store);
            return response;
        }
        [HttpGet]
        [Route("api/AvailableProductByStoreAndProduct")]
        public async Task<ActionResult<AvailableProductsResponse>> getAvailableProductByStoreAndProduct([FromQuery] AvailableProductByStoreAndProductRequest Store)
        {
            var response = await this.mediator.Send(Store);
            return response;
        }

    }
}