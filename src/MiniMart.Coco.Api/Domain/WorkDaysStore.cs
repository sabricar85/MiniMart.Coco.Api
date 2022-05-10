using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMart.Coco.Api.Domain
{
    public class WorkDaysStore
    {
        public int ID { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public int DayNumber  { get; set; }

    }
}
