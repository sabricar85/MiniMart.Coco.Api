using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;

namespace MiniMart.Coco.Api.Controllers
{
    [ApiController]
    public class VirtualCartController : ControllerBase
    {
        private readonly IMediator mediator;

        public VirtualCartController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// returns the products that were added to the cart
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>returns the products that were added to the cart</returns>
        [HttpPost]
        [Route("api/AddProducts")]
        public async Task<ActionResult<AddedProductsResponse>> AddProducts([FromBody] AddedProductsRequest request)
        {
            
            AddedProductsResponse response = await this.mediator.Send(request);
            return response;
        }
        /// <summary>
        /// delete a product
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param /> 
        /// <returns> removes a product and returns products of the same type that are in the cart</returns>
        [HttpPost]
        [Route("api/DeletedProduct")]
        public async Task<ActionResult<DeletedProductResponse>> DeletedProduct([FromBody, Required] DeletedProductRequest query)
        {

            DeletedProductResponse response = await this.mediator.Send(query);
            return response;
        }
    }
}