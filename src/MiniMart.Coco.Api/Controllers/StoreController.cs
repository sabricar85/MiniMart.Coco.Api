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
 
    public class StoreController :  ControllerBase
    {
        private readonly IMediator mediator;
        public StoreController(IMediator mediator)
        {

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        /// <summary>
        /// Available stores 
        /// </summary>
        /// <remarks>
        /// stores in a specific moment
        /// </remarks>
        /// <param name="query"></param>
        /// <returns>returns Available stores in a specific moment</returns>
        [HttpPost]
        [Route("AvailableStore")]
        public async Task<ActionResult<AvailableStoreResponse>> get([FromBody, Required] availableStoreRequest query)
        {
            var response = await this.mediator.Send(query);

            return response;
        }

    }
}