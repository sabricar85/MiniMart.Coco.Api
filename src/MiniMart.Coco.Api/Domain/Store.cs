using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
