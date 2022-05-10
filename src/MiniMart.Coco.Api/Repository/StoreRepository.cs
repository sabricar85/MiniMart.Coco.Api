using MiniMart.Coco.Api.Data;
using MiniMart.Coco.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos.Responses;
using MiniMart.Coco.Api.Dtos;

namespace MiniMart.Coco.Api.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext dbContext; 
        
        public StoreRepository(DataContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<AvailableStoreResponse> GetAvailableStore(DateTime dateTimeQuery)
        {
            AvailableStoreResponse AvailableStoreResponse = new AvailableStoreResponse();

            TimeSpan hour  = new TimeSpan(dateTimeQuery.Hour, dateTimeQuery.Minute, 0);
            DateTime today = dateTimeQuery.Date;
            int DateOfWeek = (int)DateTime.Today.DayOfWeek;

            var store = await (from s in dbContext.Stores
                               join w in dbContext.WorkDaysStores on s.ID equals w.StoreID
                               where w.DayNumber == DateOfWeek && w.StartHour < hour &&   w.EndHour > hour
                               select new 
                               {
                                   Name = s.Name,
                                   Address = s.Address,
                                   StartHour = w.StartHour,
                                   EndHour = w.EndHour,
                               }
                               ).ToListAsync();

             AvailableStoreResponse.Stores = new List<StoreDto>();
            foreach (var item in store)
            {
                StoreDto Store = new StoreDto();
                Store.Name = item.Name;
                Store.Address = item.Address;
                Store.StartHour = item.StartHour.ToString(); ;
                Store.EndHour = item.EndHour.ToString();
                AvailableStoreResponse.Stores.Add(Store);
            }
            return AvailableStoreResponse;

        }
    }
}
