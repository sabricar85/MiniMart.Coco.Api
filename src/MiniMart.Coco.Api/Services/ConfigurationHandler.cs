using System;
using System.Threading;
using System.Threading.Tasks;
using MiniMart.Coco.Api.Dtos.Responses;

using MediatR;
using MiniMart.Coco.Api.Profiles;
using MiniMart.Coco.Api.Repository;
using MiniMart.Coco.Api.Data;

namespace MiniMart.Coco.Api.Services
{
    public class ConfigurationHandler : IRequestHandler<ConfigurationQuery, ConfigurationResponse>
    {
        private readonly IConfigurationRepository configurationRepository;

        public ConfigurationHandler(IConfigurationRepository configurationRepository)
        {
 
            this.configurationRepository = configurationRepository ?? throw new ArgumentNullException(nameof(configurationRepository));

        }
        public async Task<ConfigurationResponse> Handle(ConfigurationQuery request, CancellationToken cancellationToken)
        {

            ConfigurationResponse ConfigurationResponse = new ConfigurationResponse();
            ConfigurationResponse.DataBaseReady = await configurationRepository.ConfigureDataBase();
            return ConfigurationResponse;



        }
    }
}
