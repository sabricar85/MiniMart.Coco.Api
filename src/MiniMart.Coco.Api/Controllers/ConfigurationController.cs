using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MiniMart.Coco.Api.Dtos.Responses;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Coco.Api.Profiles;

namespace MiniMart.Coco.Api.Controllers
{
   
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator mediator;
        public ConfigurationController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        /// <summary>
        /// Create database, tables and records
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param /> 
        /// <returns>confirm database created</returns>
        [HttpGet]
        [Route("Setup")]
        public async Task<ActionResult<ConfigurationResponse>> Setup()
        {
            ConfigurationQuery query = new ConfigurationQuery();

            ConfigurationResponse response = await this.mediator.Send(query);

            return response;
        }
    }
}
 