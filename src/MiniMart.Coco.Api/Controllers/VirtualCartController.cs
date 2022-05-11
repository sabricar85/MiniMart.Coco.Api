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
        /// Añade productos al carrito y descuenta las unidades que fueron añadidas al carrito
        /// Add products to the cart and discount the units that were added to the cart
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>returns the products that were added to the cart with prices </returns>
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
        /// Set the info of the product you want to delete. The product will be returned with your information if it was eliminated and the total price of the updated cart
        /// The number of products that need to be deleted should not be entered as a parameter, it is understood that only one will be deleted
        /// setear la info del producto que se desea eliminar.   Se retornará el producto con su informacion si fue eliminado y el precio total del carrito actualizado
        /// </remarks>
        /// <param name="query"></param>
        /// <returns>delete product</returns>
        [HttpPost]
        [Route("api/DeletedProduct")]
        public async Task<ActionResult<DeletedProductResponse>> DeletedProduct([FromBody, Required] DeletedProductRequest query)
        {

            DeletedProductResponse response = await this.mediator.Send(query);
            return response;
        }
    }
}