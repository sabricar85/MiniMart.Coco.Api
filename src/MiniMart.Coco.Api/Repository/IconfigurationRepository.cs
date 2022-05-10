using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Repository
{
    public interface IConfigurationRepository
    {
        Task<bool> ConfigureDataBase();
    }
}
