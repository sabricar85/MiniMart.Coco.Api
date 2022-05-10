using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class Discount
    {
        public int ID { get; set; }
        public int PercentOff { get; set; }
        public int? LimitUnitOff { get; set; }
        
        [ForeignKey("DiscountTypeID")] 
        public int DiscountTypeID {get; set;}
        public DiscountType DiscountType { get; set; }

    }
}
