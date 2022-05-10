using MiniMart.Coco.Api.Data;
using MiniMart.Coco.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniMart.Coco.Api.Repository
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private readonly DataContext dbContext;

        public ConfigurationRepository(DataContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }


        public async Task<bool> ConfigureDataBase()
        {
          var  dbCreated =  await  dbContext.Database.EnsureCreatedAsync();
            try
            {
                 if (dbCreated)
                 {
                      InsertDaysOfWeeks();
                      InsertCategories();
                      InsertProducts();
                      InsertStores();
                      InsertWorkDaysStore();
                      InsertStock();
                      InsertInsertDiscountType();
                      InsertDiscount();
                      InsertVouchers();
                      InsertVoucherProductRestriction();
                  }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
   
           return dbCreated;  
        }

        private void InsertCategories()
        {

            dbContext.Categories.Add(new Category() { Name = "Soda" });
            dbContext.SaveChanges();
            dbContext.Categories.Add(new Category() { Name = "Food" });
            dbContext.SaveChanges();
            dbContext.Categories.Add(new Category() { Name = "Cleaning" });
            dbContext.SaveChanges();
            dbContext.Categories.Add(new Category() { Name = "Bathroom" });
            dbContext.SaveChanges();
        }

        private void InsertInsertDiscountType()
        {

            dbContext.DiscountTypes.Add(new DiscountType() { Description = "Unit same" }); dbContext.SaveChanges();
            dbContext.DiscountTypes.Add(new DiscountType() { Description = "On the second unit" }); dbContext.SaveChanges();
            dbContext.DiscountTypes.Add(new DiscountType() { Description = "Pay 2 take 3" }); dbContext.SaveChanges();
        }

        private void InsertDaysOfWeeks()
        {

            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 1,Description = "Monday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 2,Description = "Tuesday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 3,Description = "Wednesday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 4,Description = "Thursday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 5,Description = "Friday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 6,Description = "Saturday" }); dbContext.SaveChanges();
            dbContext.DaysOfWeeks.Add(new DaysOfWeek() { DayNumber = 0,Description = "Sunday" }); dbContext.SaveChanges();
        }

        private void InsertProducts()
        {
            //sodas
            dbContext.Products.Add(new Product() { Name = "Cold Ice Tea",           CategoryID= 1, Price = 10.20m});   dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Coffee flavoured milk",  CategoryID = 1, Price = 12.20m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Nuke - Cola",            CategoryID = 1, Price = 14.10m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Sprute",                 CategoryID = 1, Price = 15 });     dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Slurm",                  CategoryID = 1, Price = 8.50m });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Diet Slurm",             CategoryID = 1, Price = 10.50m }); dbContext.SaveChanges();

            //food
            dbContext.Products.Add(new Product() { Name = "Salsa Cookies",        CategoryID = 2, Price = 30.20m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Windmill Cookies",     CategoryID = 2, Price = 42.20m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Garlic-o-bread 2000",  CategoryID = 2, Price = 24.10m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "LACTEL bread",         CategoryID = 2, Price = 25.00m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Ravioloches x12",      CategoryID = 2, Price = 10.50m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Ravioloches x48",      CategoryID = 2, Price = 15.50m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Milanga ganga",        CategoryID = 2, Price = 15.50m }); dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Milanga ganga napo",   CategoryID = 2, Price = 15.50m }); dbContext.SaveChanges();

            //Cleaning
            dbContext.Products.Add(new Product() { Name = "Atlantis detergent", CategoryID = 3, Price = 10.30m });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Virulanita",         CategoryID = 3, Price = 7.20m  });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Sponge, Bob",        CategoryID = 3, Price = 5.10m  });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Generic mop",        CategoryID = 3, Price = 12.00m });  dbContext.SaveChanges();

            //Bathroom
            dbContext.Products.Add(new Product() { Name = "Pure steel toilet paper", CategoryID = 4, Price = 10.20m });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Generic soap",            CategoryID = 4, Price = 12.20m });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "PANTONE shampoo",         CategoryID = 4, Price = 14.10m });  dbContext.SaveChanges();
            dbContext.Products.Add(new Product() { Name = "Cabbagegate toothpaste",  CategoryID = 4, Price = 15 });      dbContext.SaveChanges();


        }

        private void InsertStock()
        {
         //store Downtown 1
            //sodas                                        
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 1, ProductCount=  10});  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 2, ProductCount = 5 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 3, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 4, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 5, ProductCount = 50 }); dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 6, ProductCount = 23 }); dbContext.SaveChanges();

            //food 
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 7,  ProductCount = 12 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 8,  ProductCount = 23 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 9,  ProductCount = 8 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 10, ProductCount = 10 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 11, ProductCount = 51 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 12, ProductCount = 6 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 13, ProductCount = 10 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 14, ProductCount = 20 }); dbContext.SaveChanges();

            ////Cleaning                                    
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 15, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 16, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1, ProductID = 17, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1,  ProductID = 18, ProductCount = 0 }); dbContext.SaveChanges();

            ////Bathroom 
            dbContext.Stocks.Add(new Stock() { StoreID = 1,  ProductID = 19, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1,  ProductID = 20, ProductCount = 5 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1,  ProductID = 21, ProductCount = 2 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 1,  ProductID = 22, ProductCount = 15 }); dbContext.SaveChanges();

            //store COCO Bay

            //sodas 
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 1, ProductCount = 10 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 2, ProductCount = 5 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 3, ProductCount = 2 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 4, ProductCount = 5 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 5, ProductCount = 10 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 6, ProductCount = 0 }); dbContext.SaveChanges();

            //food 
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 7, ProductCount = 10 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 8, ProductCount = 5 });     dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 9, ProductCount = 13 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 10, ProductCount = 7 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 11, ProductCount = 20 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 12, ProductCount = 6 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 13, ProductCount = 5 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 14, ProductCount = 12 }); dbContext.SaveChanges();

            //Cleaning 
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 15, ProductCount = 10 }); dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 16, ProductCount = 3 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 17, ProductCount = 5 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 18, ProductCount = 12}); dbContext.SaveChanges();

            //Bathroom 
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 19, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 20, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 21, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 2, ProductID = 22, ProductCount = 0 }); dbContext.SaveChanges();

            //store COCO Mall
            //sodas 
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 1, ProductCount = 10 }); dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 2, ProductCount = 5 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 3, ProductCount = 2 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 4, ProductCount = 5 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 5, ProductCount = 10 }); dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 6, ProductCount = 2 });  dbContext.SaveChanges();

            //food 
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 7, ProductCount = 12 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 8, ProductCount = 23 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 9, ProductCount = 8 });    dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 10, ProductCount = 10 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 11, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 12, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 13, ProductCount = 0 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 14, ProductCount = 0 }); dbContext.SaveChanges();

            //Cleaning 
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 15, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 16, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 17, ProductCount = 0 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 18, ProductCount = 0 }); dbContext.SaveChanges();

            //Bathroom 
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 19, ProductCount = 10 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 20, ProductCount = 7 });   dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 21, ProductCount = 25 });  dbContext.SaveChanges();
            dbContext.Stocks.Add(new Stock() { StoreID = 3, ProductID = 22, ProductCount = 3 }); dbContext.SaveChanges();

        }
        private void InsertStores()
        {
            dbContext.Stores.Add(new Store() { Name = "COCO Downtown", Address = "Mrs Smith 71" });  dbContext.SaveChanges();
            dbContext.Stores.Add(new Store() { Name = "COCO Bay", Address = "London W6 7HB UK" });   dbContext.SaveChanges();
            dbContext.Stores.Add(new Store() { Name = "COCO Mall", Address = "Boston, MA 02130" }); dbContext.SaveChanges();
        
        }

        private void InsertWorkDaysStore()
        {
 
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(12, 0, 0),  DayNumber = 1 });   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 1 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(12, 0, 0),  DayNumber = 2 });   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 2 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(12, 0, 0),  DayNumber = 3 });   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 3 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(12, 0, 0),  DayNumber =4 });   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber =4 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(8, 0, 0), EndHour = new TimeSpan(12, 0, 0),  DayNumber = 5 });   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 1, StartHour = new TimeSpan(16, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 5 });  dbContext.SaveChanges();

 
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(13, 0, 0),  DayNumber = 1 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(17, 0, 0), EndHour = new TimeSpan(21, 0, 0),  DayNumber = 1});   dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(13, 0, 0),  DayNumber = 2 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(17, 0, 0), EndHour = new TimeSpan(21, 0, 0),  DayNumber = 2 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(13, 0, 0),  DayNumber = 3 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(17, 0, 0), EndHour = new TimeSpan(21, 0, 0),  DayNumber = 3 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(13, 0, 0),  DayNumber = 4 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(17, 0, 0), EndHour = new TimeSpan(21, 0, 0),  DayNumber = 4 });  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(10, 0, 0), EndHour = new TimeSpan(13, 0, 0),  DayNumber = 5});  dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 2, StartHour = new TimeSpan(17, 0, 0), EndHour = new TimeSpan(21, 0, 0),  DayNumber = 5});  dbContext.SaveChanges();

            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 0 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 1 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 2 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 4 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 5 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(9, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 6});     dbContext.SaveChanges();

            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(7, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 6 });    dbContext.SaveChanges();
            dbContext.WorkDaysStores.Add(new WorkDaysStore() { StoreID = 3, StartHour = new TimeSpan(7, 0, 0), EndHour = new TimeSpan(20, 0, 0), DayNumber = 0});     dbContext.SaveChanges();

        }

        private void InsertDiscount()
        {
                                                                                               
            dbContext.Discount.Add(new Discount() { PercentOff = 20, DiscountTypeID = 1, });   dbContext.SaveChanges();
            dbContext.Discount.Add(new Discount() { LimitUnitOff = 6, DiscountTypeID = 3, });  dbContext.SaveChanges();
            dbContext.Discount.Add(new Discount() { PercentOff = 10, DiscountTypeID = 1, });   dbContext.SaveChanges();
            dbContext.Discount.Add(new Discount() { PercentOff = 30, DiscountTypeID = 2, });   dbContext.SaveChanges();
            dbContext.Discount.Add(new Discount() { PercentOff = 50, DiscountTypeID = 2, });   dbContext.SaveChanges();;
        }

        private void InsertVouchers()
        {
            //COCO Bay has
            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCO1V1F8XOG1MZZ",
                StoreID = 2,
                CategoryID = 3,
                DayNumber = 3,
                StartDate = new DateTime(2022, 1, 27, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 13, 0, 0, 0),
                DiscountID =  1
            }); dbContext.SaveChanges();
            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCO1V1F8XOG1MZZ",
                StoreID = 2,
                CategoryID = 3,
                DayNumber = 4,
                StartDate = new DateTime(2022, 1, 27, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 13, 0, 0, 0),
                DiscountID = 1
            }); dbContext.SaveChanges();

            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCOKCUD0Z9LUKBN",
                StoreID = 2,
                CategoryID = 2,
                StartDate = new DateTime(2022, 1, 24, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 6, 0, 0, 0),
                DiscountID =  2                
            }); dbContext.SaveChanges();

            //COCO Mall
            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCOG730CNSG8ZVX",
                StoreID = 3,
                CategoryID = 4,
                StartDate = new DateTime(2022, 1, 31, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 9, 0, 0, 0),
                DiscountID = 3
            }); dbContext.SaveChanges();


            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCOG730CNSG8ZVX",
                StoreID = 3,
                CategoryID = 1,
                StartDate = new DateTime(2022, 1, 31, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 9, 0, 0, 0),
                DiscountID = 3
            }); dbContext.SaveChanges();

            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCO2O1USLC6QR22",
                StoreID = 1,
                CategoryID = 1,
                StartDate = new DateTime(2022, 1, 2, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 28, 0, 0,0),
                DiscountID = 4,
            }); dbContext.SaveChanges();

            dbContext.Vouchers.Add(new Voucher()
            {
                Code = "COCO0FLEQ287CC05",
                StoreID = 1,
                CategoryID = 4,
                StartDate = new DateTime(2022, 1, 2, 0, 0, 0),
                ExpirationDate = new DateTime(2023, 2, 28, 0, 0, 0),
                DiscountID = 5,
                DayNumber = 1,
            }); dbContext.SaveChanges();
        }

        private void InsertVoucherProductRestriction()
        {
        //    dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 5, ProductID = 1 }); dbContext.SaveChanges();
        //    dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 5, ProductID = 2 }); dbContext.SaveChanges();
        //    dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 5, ProductID = 3 }); dbContext.SaveChanges();

            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 6, ProductID = 1 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 6, ProductID = 2 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction(){ VoucherID = 6, ProductID = 4 }); dbContext.SaveChanges();

            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 7, ProductID = 19 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 7, ProductID = 20 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 7, ProductID = 21 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 7, ProductID = 22 }); dbContext.SaveChanges();

            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 7 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 9 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 10 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 11 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 12 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 13 }); dbContext.SaveChanges();
            dbContext.VoucherProductRestrictions.Add(new VoucherProductRestriction() { VoucherID = 3, ProductID = 14 }); dbContext.SaveChanges();
        }

    }
}
